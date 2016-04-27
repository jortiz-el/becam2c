
--Creacion de la tabla
CREATE TABLE clientes (
id number(3),
nombre varchar2(255) NOT NULL,
apellidos varchar2(255) NOT NULL,
nif varchar2(255) NOT NULL ,
sexo varchar2(1),
fecha_nacimiento DATE,
fecha_alta DATE,
fecha_baja DATE,
email varchar2(255),
telefono varchar2(15) NOT NULL,
direccion varchar2(255),
domicilio varchar2(255),
poblacion varchar2(255),
provincia varchar2(255),
codigo_postal number(6),
PRIMARY KEY (id)
);

--creacion de los comentarios
COMMENT ON TABLE clientes IS 'Tabla que contiene los clientes';
COMMENT ON COLUMN clientes.id IS 'Identificador unico del cliente';
COMMENT ON COLUMN clientes.nombre IS 'Nombre del cliente';
COMMENT ON COLUMN clientes.apellidos IS 'Apellidos del cliente';
COMMENT ON COLUMN clientes.nif IS 'nif o cif del cliente';
COMMENT ON COLUMN clientes.sexo IS 'sexo del cliente';
COMMENT ON COLUMN clientes.fecha_nacimiento IS 'fecha de nacimiento del cliente';
COMMENT ON COLUMN clientes.fecha_alta IS 'fecha de alta del cliente';
COMMENT ON COLUMN clientes.fecha_baja IS 'fecha de baja del cliente';
COMMENT ON COLUMN clientes.email IS 'email del cliente';
COMMENT ON COLUMN clientes.telefono IS 'telefono del cliente';
COMMENT ON COLUMN clientes.direccion IS 'direccion del cliente';
COMMENT ON COLUMN clientes.domicilio IS 'domicilio del cliente';
COMMENT ON COLUMN clientes.poblacion IS 'poblacion del cliente';
COMMENT ON COLUMN clientes.provincia IS 'provincia del cliente';
COMMENT ON COLUMN clientes.codigo_postal IS 'codigo_postal del cliente';

--creacion del sinonimo
CREATE OR REPLACE SYNONYM TRDSALDABA.clientes FOR DSALDABA.clientes;
GRANT DELETE, INSERT, SELECT, UPDATE ON DSALDABA.clientes TO TRDSALDABA;




CREATE TABLE polizas (
id number(3),
n_poliza number(10),
nombre varchar2(255) NOT NULL,
producto varchar2(255) NOT NULL,
ramo varchar2(255) NOT NULL,
tarjeta number(9) UNIQUE,
matricula varchar2(10),
fecha_alta DATE,
fecha_baja DATE,
PRIMARY KEY (id)
);

COMMENT ON TABLE polizas IS 'Tabla que contiene las polizas ';
COMMENT ON COLUMN polizas.id IS 'Identificador unico de la poliza';
COMMENT ON COLUMN polizas.n_poliza IS 'numero  de la poliza';
COMMENT ON COLUMN polizas.nombre IS 'nombre  de la poliza';
COMMENT ON COLUMN polizas.producto IS 'producto  de la poliza';
COMMENT ON COLUMN polizas.ramo IS 'ramo  de la poliza';
COMMENT ON COLUMN polizas.tarjeta IS 'numero de tarjeta  de la poliza';
COMMENT ON COLUMN polizas.matricula IS 'matricula asiganda a la poliza';
COMMENT ON COLUMN polizas.fecha_alta IS 'fecha alta de la poliza';
COMMENT ON COLUMN polizas.fecha_baja IS 'fecha baja de la poliza';

CREATE OR REPLACE SYNONYM TRDSALDABA.polizas FOR DSALDABA.polizas;
GRANT DELETE, INSERT, SELECT, UPDATE ON DSALDABA.polizas TO TRDSALDABA;



CREATE TABLE clientes_polizas (
id number(3),
id_cliente int number(3),
id_poliza int number(3),
fecha_baja date,
PRIMARY KEY (id)
);

ALTER TABLE clientes_polizas 
ADD CONSTRAINT fk_idCliente FOREIGN KEY (id_cliente) REFERENCES clientes(id)
ADD CONSTRAINT fk_idPoliza FOREIGN KEY (id_poliza) REFERENCES polizas(id);

COMMENT ON TABLE clientes_polizas IS 'Tabla que contiene las relaciones entre clientes y polizas ';
COMMENT ON COLUMN clientes_polizas.id IS 'Identificador unico para la tabla relacional';
COMMENT ON COLUMN clientes_polizas.id_cliente IS 'Identificador asociado a la tabla cliente';
COMMENT ON COLUMN clientes_polizas.id_poliza IS 'Identificador asociado a la tabla polizas';
COMMENT ON COLUMN clientes_polizas.fecha_baja IS 'fecha de baja de la poliza asociada al cliente';

CREATE OR REPLACE SYNONYM TRDSALDABA.clientes_polizas FOR DSALDABA.clientes_polizas;
GRANT DELETE, INSERT, SELECT, UPDATE ON DSALDABA.clientes_polizas TO TRDSALDABA;


--Creacion de secuencias

CREATE SEQUENCE DSALDABA.clientes_seq
  START WITH 1
  MAXVALUE 9999999999999999999999999999
  MINVALUE 0
  NOCYCLE
  NOCACHE
  NOORDER;

CREATE OR REPLACE SYNONYM TRDSALDABA.clientes_seq FOR DSALDABA.clientes_seq;
GRANT SELECT ON DSALDABA.clientes_seq TO TRDSALDABA;


CREATE SEQUENCE DSALDABA.polizas_seq
  START WITH 1
  MAXVALUE 9999999999999999999999999999
  MINVALUE 0
  NOCYCLE
  NOCACHE
  NOORDER;

CREATE OR REPLACE SYNONYM TRDSALDABA.polizas_seq FOR DSALDABA.polizas_seq;
GRANT SELECT ON DSALDABA.polizas_seq TO TRDSALDABA;


CREATE SEQUENCE DSALDABA.clientes_polizas_seq
  START WITH 1
  MAXVALUE 9999999999999999999999999999
  MINVALUE 0
  NOCYCLE
  NOCACHE
  NOORDER;

CREATE OR REPLACE SYNONYM TRDSALDABA.clientes_polizas_seq FOR DSALDABA.clientes_polizas_seq;
GRANT SELECT ON DSALDABA.clientes_polizas_seq TO TRDSALDABA;

