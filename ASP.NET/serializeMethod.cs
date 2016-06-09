#region Serialize

public static string Serialize<T>(this T obj, bool omitXMLDeclaration = true, bool omitXMLNamespace = true)
{
    try
    {
        XmlSerializer serializer = new XmlSerializer(obj.GetType());
        using (MemoryStream memStream = new MemoryStream())
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Encoding = new UTF8Encoding(false),
                Indent = true,
                OmitXmlDeclaration = omitXMLDeclaration
            };

            using (XmlWriter writer = XmlWriter.Create(memStream, settings))
            {
                XmlSerializerNamespaces xns = new XmlSerializerNamespaces();
                if ((omitXMLNamespace))
                    xns.Add("", "");
                serializer.Serialize(writer, obj, xns);
            }

            return Encoding.UTF8.GetString(memStream.ToArray());
        }
    }
    catch
    {
        return null;
    }
}

#endregion