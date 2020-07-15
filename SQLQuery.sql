use ControlInventario
GO
drop table CuadresDeCajas 
GO
Create table CuadresDeCajas(
	CuadreDeCajaId int NOT NULL,
	Fecha datetime,
	Dosmil int,
	Mil int,
	Quinientos int,
	Doscientos int,
	Cien int,
	Cincuenta int,
	Veinticinco int,
	Veinte int,
	Diez int,
	Cinco int,
	Uno int,
	TotalVendido real,
	Diferencia real,
	TotalEnCaja real,
)
GO
ALTER TABLE CuadresDeCajas ADD PRIMARY KEY (CuadreDeCajaId);