create database escuela;
use escuela;
create table usuarios(
id int primary key not null identity,
nombre_usuario varchar(50) not null,
email varchar(50) not null,
contrasena varchar(50) not null,
identificacion varchar(50) not null,
nombre varchar(50) not null,
apellido varchar(50) not null,
fecha_nacimiento varchar(50) not null,
telefono varchar(50) not null,
padre_id int,
constraint fk_padre  foreign key(padre_id) references usuarios(id) 
);

create table roles(
id int primary key identity not null,
descripcion varchar(50) not null
);

create table rol_usuario(
usuario_id int not null,
rol_id int not null,
constraint fk_usuario  foreign key(usuario_id) references usuarios(id),
constraint fk_rol  foreign key(rol_id) references roles(id)
);

create table deporte_tipo(
id int primary key identity not null,
descripcion varchar(50) not null,
);


create table deportes(
id int primary key identity not null,
descripcion varchar(50)
);


create table grupos(
id int primary key identity not null,
descripcion_grupo varchar(100) not null,
hora_inicio varchar(50) not null,
hora_fin varchar(50) not null,
dia varchar(50) not null,
deporte_id int not null,
profesor_id int not null
constraint fk_matricula_profesor foreign key(profesor_id) references usuarios(id),
constraint fk_deporte_grupo  foreign key(deporte_id) references deportes(id)
);

create table escenarios(
id int primary key identity not null,
descripcion_escenarios varchar(100) not null,
deporte_id int not null,
deporte_tipo_id int not null,
constraint fk_deporte_tipo  foreign key(deporte_tipo_id) references deporte_tipo(id),
constraint fk_deporte_escenario  foreign key(deporte_id) references deportes(id)
);

create table matriculas(
id int primary key identity not null,
fecha varchar(50),
aprobada varchar(20),
estudiante_id int not null,
constraint fk_matricula_usuario  foreign key(estudiante_id) references usuarios(id)
);

create table grupo_matricula(
grupo_id int not null,
matricula_id int not null,
constraint fk_matricula_grupos  foreign key(grupo_id) references grupos(id),
constraint fk_matricula_matriculas  foreign key(matricula_id) references matriculas(id)
);

create table permisos(
id int primary key identity not null,
descripcion varchar(50) not null
);

create table permiso_rol(
permiso_id int not null,
rol_id int not null,
constraint fk_permisos  foreign key(permiso_id) references permisos(id),
constraint fk_rol_permiso  foreign key(rol_id) references roles(id)
);

create table calificaciones(
id int primary key identity not null,
matricula_id int not null,
nota decimal(20,0),
constraint fk_calificacion_matricula  foreign key(matricula_id) references matriculas(id)
);

create table conexion(
identificacion varchar(20),
nombre varchar(50) not null,
contrasena varchar(50) not null,
rol varchar(50) not null
);
GO



/*Datos Semilla*/

/*Insert Usuarios*/
INSERT INTO usuarios values('alex','alex@gmail.com','123','1010548720','Alexander','Carrillo','1984-10-10','2127214',null);/*Director*/
INSERT INTO usuarios values('alejo','alejo@gmail.com','456','8128238','Alejandro','Solano','1984-06-21','2337479',null);/*Padre*/
INSERT INTO usuarios values('jesus','jesus@gmail.com','789','40796262','Jesus','Munera','1984-06-21','2747895',null);/*Profesor*/
INSERT INTO usuarios values('estiven','estiven@gmail.com','91011','324587','Estiven','Usme','1984-06-21','3245871',2);/*Hijo*/

/* Insert Roles*/
INSERT INTO roles values('Director');
INSERT INTO roles values('Padre');
INSERT INTO roles values('Profesor');
INSERT INTO roles values('Estudiante');

/*Insert Roles Usuario*/
INSERT INTO rol_usuario values(1,1);
INSERT INTO rol_usuario values(2,2);
INSERT INTO rol_usuario values(3,3);
INSERT INTO rol_usuario values(4,4);


/* Insert Deportes*/
INSERT INTO deportes values('Futbol');
INSERT INTO deportes values('Baloncesto');
INSERT INTO deportes values('Voleibol');
INSERT INTO deportes values('Ajedrez');


/*Stores Procedure*/

/*Consultar Roles*/
CREATE PROCEDURE SP_ConsultarRoles
AS
	SELECT id,descripcion FROM roles		
GO


/*Consultar Combo Grupo*/
CREATE PROCEDURE [dbo].[SP_ConsultarComboGrupos]
@TipoConsulta VARCHAR(100)
AS
BEGIN
	IF @TipoConsulta = 'DEPORTES'
	BEGIN
		--Deportes
		SELECT id,descripcion FROM deportes
		RETURN
	END
	
	IF @TipoConsulta = 'PROFESORES' 
		BEGIN
			--Profesores
			SELECT DISTINCT usuarios.id AS id, nombre AS nom_profesor, rol_id as ROL
			FROM usuarios INNER JOIN rol_usuario ON usuarios.id = rol_usuario.usuario_id
			WHERE rol_usuario.rol_id = 3
		END
END
GO

/*Consulta Conexion*/
CREATE PROCEDURE [dbo].[SP_ConsultarConexion] AS	
		SELECT * FROM conexion
GO

/*Realizar Conexion con validacion de usuarios*/

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
	
/*Cargar Usuarios en Director*/
CREATE PROCEDURE [dbo].[SP_CargarGruposRegistradosEnDirector] AS
SELECT us.nombre_usuario, us.email, us.contrasena, us.identificacion, us.nombre, us.apellido, us.fecha_nacimiento, us.telefono
		from usuarios as us 
		join rol_usuario as rs on rs.usuario_id = us.id 
		join roles as ro on ro.id = rs.rol_id
		WHERE ro.descripcion !='Estudiante'
GO

/*Crear Usuaurio PADRE-PROFESOR-DIRECTOR*/
CREATE PROCEDURE [dbo].[SP_RegistrarUsuarioDirector] 
@nombre_usuario VARCHAR(20),
@email VARCHAR(20),
@contrasena VARCHAR(20),
@identificacion VARCHAR(20),
@nombre VARCHAR(20),
@apellido VARCHAR(20),
@Fecha_nacimiento VARCHAR(20),
@telefono VARCHAR(20),
@idrol INT
AS
IF exists (select nombre_usuario,identificacion,nombre,apellido from usuarios where identificacion = @identificacion AND nombre_usuario = @nombre_usuario) 
	BEGIN
		SELECT '1' AS CodRpta,
				'El usuario ya se encuentra agregado con la misma identificación' AS Mensaje
		RETURN
	END
IF exists (select identificacion,nombre_usuario from usuarios where identificacion = @identificacion) 
	BEGIN
		SELECT '1' AS CodRpta,
				'La identificación ya cuenta con un usuario distinto, contactese con el administrador para proporcionar dicha información' AS Mensaje
		RETURN
	END
IF exists (select identificacion,nombre_usuario from usuarios where nombre_usuario = @nombre_usuario) 
	BEGIN
		SELECT '1' AS CodRpta,
				'El nombre de usuario ya cuenta con una identificación distinta, contactese con el administrador para proporcionar dicha informació' AS Mensaje
		RETURN
	END

INSERT INTO usuarios(nombre_usuario, email, contrasena, identificacion, nombre, apellido, fecha_nacimiento, telefono) 
				VALUES (@nombre_usuario,@email,@contrasena,@identificacion,@nombre,@apellido, @fecha_nacimiento, @telefono)

				DECLARE @IdUsuario int
				SET @IdUsuario = (select [id] from usuarios where identificacion=@identificacion)

INSERT INTO rol_usuario(usuario_id, rol_id) values (@IdUsuario, @idrol)
				SELECT '0' AS CodRpta,
				'El usuario se ha registrado correctamente' AS Mensaje
	EXEC SP_CargarGruposRegistradosEnDirector
GO

/*Cargar Grupo*/
CREATE PROCEDURE [dbo].[SP_CargarGrupos] AS	
		SELECT grupos.descripcion_grupo AS Descripcion, grupos.hora_inicio AS Hora_Inicio, grupos.hora_fin AS Hora_Fin, grupos.dia AS Dia, deportes.descripcion as Deporte, usuarios.nombre AS Profesor
		FROM grupos INNER JOIN deportes ON grupos.deporte_id = deportes.id
		INNER JOIN usuarios ON usuarios.id = profesor_id
GO

/*Crear Grupo*/
CREATE PROCEDURE [dbo].[SP_CrearGrupo]
@Descripcion VARCHAR(20),
@HoraIn VARCHAR(10),
@HoraFin VARCHAR(10),
@Dia VARCHAR(50),
@Id_Deporte INT,
@Id_Profesor INT
AS
BEGIN
	IF NOT EXISTS(SELECT id as id_grupo FROM grupos WHERE descripcion_grupo = @Descripcion)
	BEGIN
		IF NOT EXISTS(SELECT id as id_grupo FROM grupos WHERE profesor_id=@Id_Profesor AND dia = @Dia AND hora_inicio = @HoraIn)
		BEGIN
				INSERT INTO grupos(descripcion_grupo, hora_inicio, hora_fin, dia, deporte_id, profesor_id) 
					VALUES (@Descripcion,@HoraIn,@HoraFin,@Dia,@Id_Deporte,@Id_Profesor)

				SELECT '0' AS CodRpta,
						'Grupo creado exitosamente' AS Mensaje

				EXEC SP_CargarGrupos
				RETURN
		END
		SELECT '1' AS CodRpta,
		   'El docente seleccionado ya esta asingado a otro grupo para el mismo dia y hora de inicio' AS Mensaje
		   RETURN
		   
	END
	SELECT '1' AS CodRpta,
		   'Ya existe un grupo con esa descripcion' AS Mensaje
END
GO

/*Modificar Grupo*/

CREATE PROCEDURE [dbo].[SP_ModificarGrupo]
@Descripcion VARCHAR(20),
@HoraIn VARCHAR(10),
@HoraFin VARCHAR(10),
@Dia VARCHAR(50),
@Id_Deporte INT,
@Id_Profesor INT
AS
BEGIN
	IF NOT EXISTS(SELECT id as id_grupo FROM grupos WHERE descripcion_grupo!=@Descripcion AND profesor_id=@Id_Profesor AND dia = @Dia AND hora_inicio = @HoraIn)
	BEGIN
			UPDATE grupos
			SET hora_inicio=@HoraIn, hora_fin=@HoraFin, dia=@Dia, deporte_id=@Id_Deporte, profesor_id=@Id_Profesor
			WHERE descripcion_grupo=@Descripcion

			SELECT '0' AS CodRpta,
					'Grupo modificado exitosamente' AS Mensaje

			EXEC SP_CargarGrupos
			RETURN
	END
	SELECT '1' AS CodRpta,
		'El docente seleccionado ya esta asingado a otro grupo para el mismo dia y hora de inicio' AS Mensaje
END
GO

/*Eliminar Grupo*/
CREATE PROCEDURE [dbo].[SP_EliminarGrupo]
@Descripcion VARCHAR(20)
AS
BEGIN
	DELETE FROM grupos
	WHERE descripcion_grupo=@Descripcion

	SELECT '0' AS CodRpta,
			'Grupo eliminado exitosamente' AS Mensaje

	EXEC SP_CargarGrupos
	RETURN
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
------
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
		UPDATE usuarios
			SET nombre_usuario=@nombre_usuario, email=@email, nombre=@nombre, contrasena=@contrasena, apellido=@apellido, fecha_nacimiento=@fecha_nacimiento, telefono=@telefono
			WHERE identificacion=@identificacion

			SELECT '0' AS CodRpta,
					'Usuario modificado exitosamente' AS Mensaje

			EXEC SP_CargarGruposRegistradosEnDirector
			RETURN
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
		
	IF EXISTS(SELECT us.nombre_usuario, us.email, us.contrasena, us.identificacion, us.nombre, us.apellido, us.fecha_nacimiento, us.telefono, us2.nombre as padre
				from usuarios as us 
				join usuarios as us2 on us2.id = @IdUsuario
				WHERE us.padre_id = @IdUsuario)
	BEGIN
		SELECT '1' AS CodRpta,
			'Esta intentando Eliminar un usuario que tiene rol de padre, por favor elimine los estudiantes que tiene matriculados como sus hijos para poder elimianarlo, recuerde que puede hacerlo desde la rama Padres' AS Mensaje
		RETURN
	END

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
			'usuario eliminado exitosamente' AS Mensaje
		 EXEC SP_CargarGruposRegistradosEnDirector
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


/*Consultar Matriculas*/
CREATE PROCEDURE [dbo].[SP_CargarMatriculas] 
AS
	DECLARE @Rol VARCHAR(50)
	SET @Rol = (select [rol] from conexion)
		
	IF @Rol='Director'
	BEGIN
		SELECT mt.fecha, mt.aprobada as estado, us.nombre as estudiante, gr.descripcion_grupo as grupo, gr.dia, gr.hora_inicio, gr.hora_fin, dp.descripcion as deporte
		from matriculas as mt 
		join usuarios as us on us.id = mt.estudiante_id
		join grupo_matricula as gm on gm.matricula_id = mt.id
		join grupos as gr on gr.id = gm.grupo_id 
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

		IF NOT EXISTS(select * from matriculas WHERE estudiante_id = @IdEstudiante)
		BEGIN
			
			INSERT INTO matriculas(fecha, aprobada, estudiante_id) 
				VALUES (@Fecha, 'Pendiente', @IdEstudiante)

			DECLARE @IdMatricula int
			SET @IdMatricula = (select [id] from matriculas where estudiante_id=@IdEstudiante)

			INSERT INTO grupo_matricula(grupo_id, matricula_id) values (@IdGrupo, @IdMatricula)

			SELECT '0' AS CodRpta,
					'Deporte matriculado exitosamente' AS Mensaje
			EXEC SP_CargarMatriculas
			RETURN
		END

		IF NOT EXISTS(select * from grupo_matricula WHERE grupo_id = @IdGrupo)
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



