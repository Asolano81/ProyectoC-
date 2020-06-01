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
GO;


/*Consultar Combo Grupo*/
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




