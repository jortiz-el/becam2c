/*
* consultas ORACLE
 */


SELECT polizas.id,
        n_poliza,
        nombre,
        producto,
        ramo,
        tarjeta,
        matricula,
        clientes_polizas.fecha_alta,
        clientes_polizas.fecha_baja
    FROM    polizas
        INNER JOIN
            clientes_polizas
        ON (POLIZAS.ID = clientes_polizas.id_poliza)
    WHERE {0};


INSERT INTO POLIZAS (ID, n_poliza, nombre, producto, ramo, tarjeta, matricula, fecha_alta)
			VALUES (clientes_seq.NEXTVAL, :num_poliza, :name_poliza, :producto, :ramo, :tarjeta, :matricula , SYSDATE)
			RETURNING ID INTO :id_Poliza;


UPDATE POLIZAS                                                                   
        SET 
            n_poliza = :n_poliza,
            nombre = :nombre,
            producto = :producto,
            ramo = :ramo,
            tarjeta = :tarjeta,
            matricula = :matricula
        WHERE id = :id ;

SELECT count(*)
	    FROM clientes
	    WHERE {0} ;