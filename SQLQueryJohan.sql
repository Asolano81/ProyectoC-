/*ELIMINAR Y VOLVER A MONTAR*/
reate table conexion(
identificacion varchar(20),
nombre varchar(50) not null,
contrasena varchar(50) not null,
rol varchar(50) not null
);

CREATE PROCEDURE [dbo].[SP_RealizarConexion]
@Nombre VARCHAR(50),
@contrasena VARCHAR(20),
@Rol VARCHAR(20)
AS
	BEGIN		
		IF EXISTS(select us.nombre_usuario,us.contrasena,ro.id as idRol,ro.descripcion 
					  from usuarios as us 
					  join rol_usuario as rs on rs.usuario_id = us.id 
					  join roles as ro on ro.id = rs.rol_id
					  WHERE nombre_usuario = @Nombre AND contrasena = @contrasena AND ro.descripcion = @Rol)
		BEGIN
			DECLARE @Identificacion VARCHAR(20)
			SET @Identificacion = (select [identificacion]
									  from usuarios as us 
									  join rol_usuario as rs on rs.usuario_id = us.id 
									  join roles as ro on ro.id = rs.rol_id
									  WHERE nombre_usuario = @Nombre AND contrasena = @contrasena AND ro.descripcion = @Rol)
			INSERT INTO conexion(identificacion, nombre, contrasena, rol)values(@Identificacion,@Nombre,@contrasena,@Rol)

			SELECT '0' AS respuesta,
				'Usuario conectado' AS Mensaje

			RETURN
		END
		
		SELECT '1' AS Respuesta,
		   'Datos incorrectos por favor validar registro' AS Mensaje		
	END
GO
/*ELIMINAR Y VOLVER A MONTAR*/
CREATE PROCEDURE [dbo].[SP_RealizarConexion]
@Nombre VARCHAR(50),
@contrasena VARCHAR(20),
@Rol VARCHAR(20)
AS
	BEGIN		
		IF EXISTS(select us.nombre_usuario,us.contrasena,ro.id as idRol,ro.descripcion 
					  from usuarios as us 
					  join rol_usuario as rs on rs.usuario_id = us.id 
					  join roles as ro on ro.id = rs.rol_id
					  WHERE nombre_usuario = @Nombre AND contrasena = @contrasena AND ro.descripcion = @Rol)
		BEGIN
			DECLARE @Identificacion VARCHAR(20)
			SET @Identificacion = (select [identificacion]
									  from usuarios as us 
									  join rol_usuario as rs on rs.usuario_id = us.id 
									  join roles as ro on ro.id = rs.rol_id
									  WHERE nombre_usuario = @Nombre AND contrasena = @contrasena AND ro.descripcion = @Rol)
			INSERT INTO conexion(identificacion, nombre, contrasena, rol)values(@Identificacion,@Nombre,@contrasena,@Rol)

			SELECT '0' AS respuesta,
				'Usuario conectado' AS Mensaje

			RETURN
		END
		
		SELECT '1' AS Respuesta,
		   'Datos incorrectos por favor validar registro' AS Mensaje		
	END
GO
/*Cargar Estudiante*/
CREATE PROCEDURE [dbo].[SP_CargarEstudiantes] 
AS
	DECLARE @Rol VARCHAR(50)
	SET @Rol = (select [rol] from conexion)
		
	IF @Rol='Director'
	BEGIN
		SELECT us.nombre_usuario, us.email, us.contrasena, us.identificacion, us.nombre, us.apellido, us.fecha_nacimiento, us.telefono, us2.nombre as padre
		from usuarios as us 
		join rol_usuario as rs on rs.usuario_id = us.id 
		join roles as ro on ro.id = rs.rol_id
		join usuarios as us2 on us2.id = us.padre_id
		WHERE ro.descripcion='Estudiante' 
		RETURN
	END
	
	DECLARE @DocEncargado VARCHAR(50)
	SET @DocEncargado = (select [identificacion] from conexion)
	DECLARE @IdEncargado VARCHAR(50)
	SET @IdEncargado = (select [id] from usuarios where identificacion=@DocEncargado)
	PRINT @IdEncargado
		SELECT us.nombre_usuario, us.email, us.contrasena, us.identificacion, us.nombre, us.apellido, us.fecha_nacimiento, us.telefono, us2.nombre as padre
		from usuarios as us 
		join usuarios as us2 on us2.id = @IdEncargado
		WHERE us.padre_id = @IdEncargado
GO
/*Registrar Estudiante*/
CREATE PROCEDURE [dbo].[SP_RegistrarEstudiante]
@nombre_usuario varchar(50),				
@email varchar(50),
@contrasena varchar(50),
@identificacion varchar(50),
@nombre varchar(100),
@apellido varchar(20),
@fecha_nacimiento varchar(50),
@telefono varchar(50),
@identPadre varchar(50)
AS
	DECLARE @encargado VARCHAR(50)
	SET @encargado = (select [rol] from conexion)

	BEGIN
		IF @encargado='Director'
		BEGIN
			IF NOT EXISTS (SELECT usuario_id as id_usuario
										  from usuarios as us 
										  join rol_usuario as rs on rs.usuario_id = us.id 
										  join roles as ro on ro.id = rs.rol_id
										  WHERE identificacion = @identPadre AND ro.descripcion='Padre')
			BEGIN
				SELECT '1' AS CodRpta,
				'No se puede registrar el estudiante ya que no existe ningun usuario con rol de padre asosiado a la identificación de padre ingresada' AS Mensaje
			RETURN
			END
		END
		IF NOT EXISTS(select us.identificacion, ro.descripcion 
						  from usuarios as us 
						  join rol_usuario as rs on rs.usuario_id = us.id 
						  join roles as ro on ro.id = rs.rol_id
						  WHERE identificacion = @identificacion AND ro.descripcion='Estudiante')
		BEGIN
			DECLARE @padre_id int
			SET @padre_id = (select [usuario_id]
										from usuarios as us 
										join rol_usuario as rs on rs.usuario_id = us.id 
										join roles as ro on ro.id = rs.rol_id
										WHERE identificacion = @identPadre AND ro.descripcion='Padre')
			INSERT INTO usuarios(nombre_usuario, email, contrasena, identificacion, nombre, apellido, fecha_nacimiento, telefono, padre_id) 
				VALUES (@nombre,@email,@contrasena,@identificacion,@nombre,@apellido, @fecha_nacimiento, @telefono, @padre_id)
			
			DECLARE @estudianteId int
			SET @estudianteId = (select [id] from usuarios where identificacion=@identificacion)

			INSERT INTO rol_usuario(usuario_id, rol_id) values (@estudianteId, 4)

			SELECT '0' AS CodRpta,
					'Estudiante creado exitosamente' AS Mensaje
			EXEC SP_CargarEstudiantes
			RETURN
		END
		SELECT '1' AS CodRpta,
			   'El estudiante ya existe' AS Mensaje
		RETURN
	END
GO
/*Consultar Usuario*/
CREATE PROCEDURE[dbo].[SP_ConsultarUsuario]
@formulario VARCHAR(20),
@identificacion VARCHAR(50)
AS

	IF @formulario='frmGestHijo'
	BEGIN
		IF (SELECT count(*) from usuarios as us 
			join rol_usuario as rs on rs.usuario_id = us.id 
			join roles as ro on ro.id = rs.rol_id
			join usuarios as us2 on us2.id = us.padre_id
			WHERE us.identificacion=@identificacion) !=0 
		BEGIN
			SELECT '0' AS CodRpta,
					'Consulta Exitosa' AS Mensaje
			SELECT us.nombre_usuario, us.email, us.contrasena, us.identificacion, us.nombre, us.apellido, us.fecha_nacimiento, us.telefono, us2.identificacion as documento_padre
			from usuarios as us 
			join rol_usuario as rs on rs.usuario_id = us.id 
			join roles as ro on ro.id = rs.rol_id
			join usuarios as us2 on us2.id = us.padre_id
			WHERE us.identificacion=@identificacion
			RETURN
		END
		SELECT '1' AS CodRpta,
			'El estudiante no existe' AS Mensaje
		RETURN
	END

	IF @formulario='frmGestUsuario'
	BEGIN
		DECLARE @Rol VARCHAR(50)
		SET @Rol = (SELECT [descripcion]
					from usuarios as us 
					join rol_usuario as rs on rs.usuario_id = us.id 
					join roles as ro on ro.id = rs.rol_id
					WHERE us.identificacion=@identificacion)

		IF @Rol='Estudiante'
		BEGIN
			SELECT '1' AS CodRpta,
				'El usuario que esta consultado es un estudiante, estos solo pueden ser administrados desde la rama de los padres' AS Mensaje
			RETURN
		END

		IF (SELECT count(*) from usuarios as us 
					join rol_usuario as rs on rs.usuario_id = us.id 
					join roles as ro on ro.id = rs.rol_id
					WHERE us.identificacion=@identificacion) !=0 
		BEGIN
			SELECT '0' AS CodRpta,
					'Consulta Exitosa' AS Mensaje
			SELECT us.nombre_usuario, us.email, us.contrasena, us.identificacion, us.nombre, us.apellido, us.fecha_nacimiento, us.telefono, rs.rol_id as id_rol
			from usuarios as us 
			join rol_usuario as rs on rs.usuario_id = us.id 
			join roles as ro on ro.id = rs.rol_id
			WHERE us.identificacion=@identificacion
			RETURN
		END
		SELECT '1' AS CodRpta,
			'El usuario no existe' AS Mensaje
		RETURN
	END
GO