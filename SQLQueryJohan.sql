/*MODIFICACION DE LA TABLA DEPORTES_MATRICULA*/
create table grupo_matricula(
grupo_id int not null,
matricula_id int not null,
constraint fk_matricula_grupos  foreign key(grupo_id) references grupos(id),
constraint fk_matricula_matriculas  foreign key(matricula_id) references matriculas(id)
);
/*ELIMINAR Y VOLVER A MONTAR*/
create table conexion(
identificacion varchar(20),
nombre varchar(50) not null,
contrasena varchar(50) not null,
rol varchar(50) not null
);
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
						  WHERE identificacion = @identificacion)
		BEGIN
			DECLARE @padre_id int
			SET @padre_id = (select [usuario_id]
										from usuarios as us 
										join rol_usuario as rs on rs.usuario_id = us.id 
										join roles as ro on ro.id = rs.rol_id
										WHERE identificacion = @identPadre AND ro.descripcion='Padre')
			
			IF EXISTS(select nombre_usuario from usuarios WHERE nombre_usuario = @nombre_usuario)
			BEGIN
				SELECT '1' AS CodRpta,
			   'El nombre de USUARIO ya existe' AS Mensaje
			RETURN
			END
			
			INSERT INTO usuarios(nombre_usuario, email, contrasena, identificacion, nombre, apellido, fecha_nacimiento, telefono, padre_id) 
				VALUES (@nombre_usuario,@email,@contrasena,@identificacion,@nombre,@apellido, @fecha_nacimiento, @telefono, @padre_id)
			
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
/*Modificar Usuario (Solo esta configurado Estudiante)*/
CREATE PROCEDURE[dbo].[SP_ModificarUsuario]
@formulario VARCHAR(20),
@identificacion VARCHAR(50),
@nombre_usuario varchar(50),				
@email varchar(50),
@contrasena varchar(50),
@nombre varchar(100),
@apellido varchar(20),
@fecha_nacimiento varchar(50),
@telefono varchar(50),
@identPadre varchar(50)
AS
	IF @formulario='frmGestHijo'
	BEGIN
		DECLARE @IdEstudiante VARCHAR(3)
		SET @IdEstudiante = (select [id] from usuarios WHERE identificacion=@identificacion)

		IF NOT EXISTS(SELECT id as id_usuario FROM usuarios WHERE nombre_usuario=@nombre_usuario AND id !=@IdEstudiante)
		BEGIN
			DECLARE @encargado VARCHAR(50)
			SET @encargado = (select [rol] from conexion)

			IF @encargado='Director'
			BEGIN
				IF NOT EXISTS (SELECT usuario_id as id_usuario
												from usuarios as us 
												join rol_usuario as rs on rs.usuario_id = us.id 
												join roles as ro on ro.id = rs.rol_id
												WHERE identificacion = @identPadre AND ro.descripcion='Padre')
				BEGIN
					SELECT '1' AS CodRpta,
					'No se puede modificar el estudiante ya que no existe ningun usuario con rol de padre asosiado a la identificación de padre ingresada' AS Mensaje
				RETURN
				END
			END

			DECLARE @padre_id int
			SET @padre_id = (select [usuario_id]
										from usuarios as us 
										join rol_usuario as rs on rs.usuario_id = us.id 
										join roles as ro on ro.id = rs.rol_id
										WHERE identificacion = @identPadre AND ro.descripcion='Padre')
				
			UPDATE usuarios
			SET nombre_usuario=@nombre_usuario, email=@email, contrasena=@contrasena, nombre=@nombre, apellido=@apellido, fecha_nacimiento=@fecha_nacimiento, telefono=@telefono, padre_id=@padre_id
			WHERE identificacion=@identificacion

			SELECT '0' AS CodRpta,
					'Estudiante modificado exitosamente' AS Mensaje

			EXEC SP_CargarEstudiantes
			RETURN
		END
		SELECT '1' AS CodRpta,
		'El nombre de usuario ingresado ya existe' AS Mensaje
	END

	/*Modificar Usuario - Construir*/
	IF @formulario='frmGestUsuario'
	BEGIN
		DECLARE @X INT
	END

GO
/*Eliminar Usuario (Cualquier tipo de usuario)*/
CREATE PROCEDURE [dbo].[SP_EliminarUsuario]
@formulario VARCHAR(20),
@identificacion VARCHAR(50)
AS
BEGIN

	DECLARE @IdUsuario VARCHAR(50)
	SET @IdUsuario = (select [id] from usuarios where identificacion=@identificacion)

	DELETE FROM rol_usuario WHERE usuario_id=@IdUsuario

	DELETE FROM usuarios
	WHERE id=@IdUsuario

	IF @formulario='frmGestHijo'
	BEGIN
		SELECT '0' AS CodRpta,
			'Estudiante eliminado exitosamente' AS Mensaje
		EXEC SP_CargarEstudiantes
		RETURN
	END

	IF @formulario='frmGestUsuario'
	BEGIN
		SELECT '0' AS CodRpta,
			'Grupo eliminado exitosamente' AS Mensaje
		 /*EXEC **CREAR PROCEDIMIENTO SP_CargarEstudiantes*/
		RETURN
	END		
END
GO
/*Cambios de ConsultarGrupo*/
CREATE PROCEDURE [dbo].[SP_ConsultarGrupo] 
@Descripcion VARCHAR(20)
AS
BEGIN
		SELECT gr.descripcion_grupo, gr.hora_inicio, gr.hora_fin, gr.dia, gr.deporte_id, gr.profesor_id, us.nombre FROM grupos as gr
		INNER JOIN usuarios as us ON gr.profesor_id = us.id
		WHERE descripcion_grupo = @Descripcion
END
GO
/*Consultar Combos Matricula*/
CREATE PROCEDURE [dbo].[SP_ConsultarComboMatriculas]
@TipoConsulta VARCHAR(100),
@IdDeporte INT
AS
BEGIN
	IF @TipoConsulta = 'DEPORTES'
	BEGIN
		--Deportes
		SELECT id, descripcion FROM deportes
		RETURN
	END
	
	IF @TipoConsulta = 'GRUPOS' 
		BEGIN
			--Grupos
			SELECT DISTINCT id, descripcion_grupo
			FROM grupos
			WHERE deporte_id = @IdDeporte
		END
END
GO
/*Registrar Matricula*/
CREATE PROCEDURE [dbo].[SP_RegistrarMatricula]
@DocEstudiante varchar(50),				
@Fecha varchar(50),
@IdGrupo int,
@IdDeporte int
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
										  WHERE identificacion = @DocEstudiante AND ro.descripcion='Estudiante')
			BEGIN
				SELECT '1' AS CodRpta,
				'El documento del Estudiante ingresado no existe' AS Mensaje
			RETURN
			END
		END

		DECLARE @IdEstudiante int
		SET @IdEstudiante = (select [id] from usuarios where identificacion=@DocEstudiante)

		DECLARE @IdMatricula int

		IF NOT EXISTS(select * from matriculas WHERE estudiante_id = @IdEstudiante)
		BEGIN
			
			INSERT INTO matriculas(fecha, aprobada, estudiante_id) 
			VALUES (@Fecha, 'Pendiente', @IdEstudiante)

			SET @IdMatricula = (select [id] from matriculas where estudiante_id=@IdEstudiante)

			INSERT INTO grupo_matricula(grupo_id, matricula_id) values (@IdGrupo, @IdMatricula)

			SELECT '0' AS CodRpta,
					'Deporte matriculado exitosamente' AS Mensaje
			EXEC SP_CargarMatriculas
			RETURN
		END

		SET @IdMatricula = (select [id] from matriculas where estudiante_id=@IdEstudiante)

		IF NOT EXISTS(select * from grupo_matricula WHERE grupo_id = @IdGrupo AND matricula_id=@IdMatricula)
		BEGIN
			INSERT INTO grupo_matricula(grupo_id, matricula_id) values (@IdGrupo, @IdMatricula)
			SELECT '0' AS CodRpta,
					'Deporte matriculado exitosamente' AS Mensaje
			EXEC SP_CargarMatriculas
			RETURN
		END
		SELECT '1' AS CodRpta,
			   'El estudiante ya tiene matriculado este deporte' AS Mensaje
		RETURN
	END
GO
/*Cargar Matriculas*/
CREATE PROCEDURE [dbo].[SP_CargarMatriculas] 
AS
	DECLARE @Rol VARCHAR(50)
	SET @Rol = (select [rol] from conexion)
		
	IF @Rol='Director'
	BEGIN
		SELECT us.identificacion, us.nombre, us.apellido, dp.descripcion as deporte, gr.descripcion_grupo as grupo, us2.nombre as nom_profesor, us2.apellido as ape_profesor ,gr.dia, gr.hora_inicio, gr.hora_fin, mt.fecha, mt.aprobada as estado
		from matriculas as mt 
		join usuarios as us on us.id = mt.estudiante_id
		join grupo_matricula as gm on gm.matricula_id = mt.id
		join grupos as gr on gr.id = gm.grupo_id
		join usuarios as us2 on us2.id = gr.profesor_id
		join deportes as dp on dp.id = gr.deporte_id
		RETURN
	END
	
	DECLARE @DocEncargado VARCHAR(50)
	SET @DocEncargado = (select [identificacion] from conexion)
	DECLARE @IdEncargado VARCHAR(50)
	SET @IdEncargado = (select [id] from usuarios where identificacion=@DocEncargado)
		SELECT mt.fecha, mt.aprobada as estado, gr.descripcion_grupo as grupo, gr.dia, gr.hora_inicio, gr.hora_fin, dp.descripcion as deporte
		from matriculas as mt 
		join usuarios as us on us.id = mt.estudiante_id
		join grupo_matricula as gm on gm.matricula_id = mt.id
		join grupos as gr on gr.id = gm.grupo_id 
		join deportes as dp on dp.id = gr.deporte_id
		WHERE us.id = @IdEncargado
GO
/*Modificar Matricula(REALIZAR PROCEDIMIENTO ALAMACENADO DE CONSULTA MATRICULA E IMPLEMENTARLO EN VISUAL)*/ 
CREATE PROCEDURE[dbo].[SP_ModificarMatricula]
@DocEstudiante VARCHAR(20),
@IdGrupoViejo INT,
@IdGrupoNuevo INT
AS
	
	DECLARE @IdEstudiante VARCHAR(5)
	SET @IdEstudiante = (select [id] from usuarios WHERE identificacion=@DocEstudiante)

	DECLARE @IdMatricula VARCHAR(3)
	SET @IdMatricula = (select [id] from matriculas WHERE estudiante_id=@IdEstudiante)

	UPDATE grupo_matricula 
	SET grupo_id = @IdGrupoNuevo
	WHERE grupo_id = @IdGrupoViejo AND matricula_id = @IdMatricula

	SELECT '0' AS CodRpta,
			'Matricula modificada correctamente' AS Mensaje
	EXEC SP_CargarMatriculas
GO
/*Cambios de ConsultarMatricula*/
CREATE PROCEDURE [dbo].[SP_ConsultarMatricula] 
@DocEstudiante VARCHAR(20),
@IdDeporte int
AS
	DECLARE @encargado VARCHAR(50)
	SET @encargado = (select [rol] from conexion)

	IF @encargado='Director'
	BEGIN
		IF NOT EXISTS (SELECT usuario_id as id_usuario
										from usuarios as us 
										join rol_usuario as rs on rs.usuario_id = us.id 
										join roles as ro on ro.id = rs.rol_id
										WHERE identificacion = @DocEstudiante AND ro.descripcion='Estudiante')
		BEGIN
			SELECT '1' AS CodRpta,
			'El documento del Estudiante ingresado no existe' AS Mensaje
		RETURN
		END
	END

	DECLARE @IdEstudiante VARCHAR(3)
	SET @IdEstudiante = (select [id] from usuarios WHERE identificacion=@DocEstudiante)

	IF EXISTS (SELECT gr.id as id_grupo
				from matriculas as mt 
				join usuarios as us on us.id = mt.estudiante_id
				join grupo_matricula as gm on gm.matricula_id = mt.id
				join grupos as gr on gr.id = gm.grupo_id 
				join deportes as dp on dp.id = gr.deporte_id
				WHERE us.id = @IdEstudiante AND  dp.id = @IdDeporte)
	BEGIN
		SELECT '0' AS CodRpta,
				'Consulta Exitosa' AS Mensaje
		SELECT gr.id as id_grupo, mt.fecha as fecha_matricula
				from matriculas as mt 
				join usuarios as us on us.id = mt.estudiante_id
				join grupo_matricula as gm on gm.matricula_id = mt.id
				join grupos as gr on gr.id = gm.grupo_id 
				join deportes as dp on dp.id = gr.deporte_id
				WHERE us.id = @IdEstudiante AND  dp.id = @IdDeporte
		RETURN
	END
	SELECT '0' AS CodRpta,
				'El estudiante no tiene matriculado ese deporte' AS Mensaje
GO
/*Eliminar Deporte de Matricula*/
CREATE PROCEDURE [dbo].[SP_EliminarDeporteMatri]
@DocEstudiante VARCHAR(50),
@IdGrupo INT

AS
BEGIN

	DECLARE @IdEstudiante VARCHAR(50)
	SET @IdEstudiante = (select [id] from usuarios where identificacion=@DocEstudiante)

	DECLARE @IdMatricula int
	SET @IdMatricula = (select [id] from matriculas where estudiante_id=@IdEstudiante)

	DELETE FROM grupo_matricula WHERE matricula_id=@IdMatricula AND grupo_id=@IdGrupo

	SELECT '0' AS CodRpta,
		'Deporte eliminado exitosamente de la matricula' AS Mensaje
	EXEC SP_CargarMatriculas
END
GO
/*Cerrar Sesion*/
CREATE PROCEDURE [dbo].[SP_CerrarSesion] AS	
		TRUNCATE TABLE conexion
GO
/*Cambios de Consulta los roles de 1 sola persona*/
CREATE PROCEDURE [dbo].[SP_ConsultarRolDeUs] 
@Documento VARCHAR(20)
AS
	IF NOT EXISTS(SELECT * FROM usuarios WHERE identificacion=@Documento)
	BEGIN
		SELECT '1' AS CodRpta,
			'No existe ningun usuario registrado con el documento ingresado' AS Mensaje
		RETURN
	END
	
	IF EXISTS (SELECT usuario_id as id_usuario
										from usuarios as us 
										join rol_usuario as rs on rs.usuario_id = us.id 
										join roles as ro on ro.id = rs.rol_id
										WHERE identificacion = @Documento AND ro.descripcion='Estudiante')
	BEGIN
		SELECT '1' AS CodRpta,
			'No puedes añadirle roles a un Usuario que es estudiante' AS Mensaje
		RETURN
	END

	DECLARE @IdUsuario VARCHAR(3)
	SET @IdUsuario = (select [id] from usuarios WHERE identificacion=@Documento)

	SELECT '0' AS CodRpta,
				'Consulta Exitosa' AS Mensaje

	SELECT us.identificacion as cedula, us.nombre, us.apellido, ro.descripcion as Roles
	from usuarios as us 
	join rol_usuario as rs on rs.usuario_id = us.id 
	join roles as ro on ro.id = rs.rol_id
	WHERE identificacion = @Documento
GO
/*Añadir roles a una persona*/
CREATE PROCEDURE[dbo].[SP_AñadirRolUsuario]
@Documento VARCHAR(20),
@IdRol INT
AS
	
	IF EXISTS(SELECT ro.descripcion
				from usuarios as us 
				join rol_usuario as rs on rs.usuario_id = us.id 
				join roles as ro on ro.id = rs.rol_id
				WHERE us.identificacion = @Documento AND ro.id = @IdRol)
	BEGIN 
		SELECT '1' AS CodRpta,
			'El usuario ya posee el Rol solicitado. No es posible registrarlo.' AS Mensaje
		RETURN
	END

	DECLARE @IdUsuario VARCHAR(3)
	SET @IdUsuario = (select [id] from usuarios WHERE identificacion=@Documento)

	INSERT INTO rol_usuario (rol_id, usuario_id) values(@IdRol, @IdUsuario)

	SELECT '0' AS CodRpta,
			'Rol añadido exitosamente al usuario' AS Mensaje
	EXEC SP_ConsultarRolDeUs @Documento
GO





