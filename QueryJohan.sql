CREATE PROCEDURE [dbo].[SP_ConsultarComboGrupos]
@TipoConsulta VARCHAR(100)
AS
BEGIN
	IF @TipoConsulta = 'DEPORTES'
	BEGIN
		--Cursos
		SELECT id,descripcion FROM deportes
	END
END

create table conexion(
identificacion varchar(50) primary key not null,
nombre varchar(50) not null,
rol varchar(50) not null,
);

CREATE PROCEDURE [dbo].[SP_ConsultarConexion] AS
	BEGIN
		SELECT * FROM conexion
	END
GO

CREATE PROCEDURE [dbo].[SP_RealizarConexion]
@Identificacion VARCHAR(20),
@Nombre VARCHAR(50),
@Rol VARCHAR(20)
AS
BEGIN
	INSERT INTO conexion(identificacion, nombre, rol)
			VALUES(@Identificacion,	@Nombre, @Rol)
END
GO


