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
aprobada bit,
estudiante_id int not null,
constraint fk_matricula_usuario  foreign key(estudiante_id) references usuarios(id)
);

create table deporte_matricula(
deporte_id int not null,
matricula_id int not null,
constraint fk_matricula_deporte  foreign key(deporte_id) references deportes(id),
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
nombre varchar(50) not null,
contrasena varchar(50) not null,
rol varchar(50) not null
);


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
INSERT INTO rol_usuario values(1,3);
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
GO;


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
			INSERT INTO conexion(nombre, contrasena, rol)values(@Nombre,@contrasena,@Rol)

			SELECT '0' AS respuesta,
				'Usuario conectado' AS Mensaje

			RETURN
		END
		
		SELECT '1' AS Respuesta,
		   'Datos incorrectos por favor validar registro' AS Mensaje		
	END
GO


CREATE PROCEDURE [dbo].[SP_CrearUsuarios]
@nombre_usuario varchar(50),				
@email varchar(50),
@contrasena varchar(50),
@identificacion varchar(50),
@nombre varchar(100),
@apellido varchar(20),
@fecha_nacimiento varchar(50),
@telefono varchar(50),
@padre_id int,
@rol_id int
AS
	BEGIN		
		-- Validar que no exista un usuario repetido.
		IF EXISTS(select nombre_usuario,identificacion from usuarios WHERE nombre_usuario = @nombre_usuario AND identificacion = @identificacion)

		BEGIN
			SELECT '0' AS respuesta,
				'El usuario ya esta registrado' AS Mensaje
			RETURN
		END

		-- Validar el rol para el registro de estudiante 
		IF EXISTS(select nombre_usuario,identificacion from usuarios WHERE nombre_usuario = @nombre_usuario AND identificacion = @identificacion)


		BEGIN
			INSERT INTO conexion(nombre, contrasena, rol)values(@Nombre,@contrasena,@Rol)

			SELECT '0' AS respuesta,
				'Usuario conectado' AS Mensaje

			RETURN
		END
		
		SELECT '1' AS Respuesta,
		   'Datos incorrectos por favor validar registro' AS Mensaje		
	END
GO





/*Consultar Grupo*/
CREATE PROCEDURE [dbo].[SP_ConsultarGrupo] 
@Descripcion VARCHAR(20)
AS
BEGIN
		SELECT * FROM grupos
		WHERE descripcion_grupo = @Descripcion
END
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

