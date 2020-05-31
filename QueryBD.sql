create database escuela;
use escuela;
create table usuarios(
id int primary key not null,
nombre_usuario varchar(50) not null,
email varchar(50) not null,
contrasena varchar(50) not null,
identificacion varchar(50) not null,
nombre varchar(50) not null,
apellido varchar(50) not null,
fecha_nacimiento date not null,
telefono varchar(50) not null,
padre_id int,
constraint fk_padre  foreign key(padre_id) references usuarios(id) 
);

create table roles(
id int primary key not null,
descripcion varchar(50) not null
);

create table rol_usuario(
usuario_id int not null,
rol_id int not null,
constraint fk_usuario  foreign key(usuario_id) references usuarios(id),
constraint fk_rol  foreign key(rol_id) references roles(id)
);

create table deporte_tipo(
id int primary key not null,
descripcion varchar(50) not null,
);


create table deportes(
id int primary key not null,
descripcion varchar(50)
);


create table grupos(
id int primary key not null,
descripcion_grupo varchar(100),
hora_inicio time,
hora_fin time,
fecha_inicio date,
fecha_finalizacion date,
deporte_id int not null,
profesor_id int not null
constraint fk_matricula_profesor foreign key(profesor_id) references usuarios(id),
constraint fk_deporte_grupo  foreign key(deporte_id) references deportes(id)
);

create table escenarios(
id int primary key not null,
descripcion_escenarios varchar(100),
deporte_id int not null,
deporte_tipo_id int not null,
constraint fk_deporte_tipo  foreign key(deporte_tipo_id) references deporte_tipo(id),
constraint fk_deporte_escenario  foreign key(deporte_id) references deportes(id)
);

create table matriculas(
id int primary key not null,
fecha date,
aprobada bit,
grupo_id int not null,
estudiante_id int not null,
constraint fk_matricula_usuario  foreign key(estudiante_id) references usuarios(id),
constraint fk_matricula_grupo  foreign key(grupo_id) references grupos(id)
);

create table permisos(
id int primary key not null,
descripcion varchar(50) not null
);

create table permiso_rol(
permiso_id int not null,
rol_id int not null,
constraint fk_permisos  foreign key(permiso_id) references permisos(id),
constraint fk_rol_permiso  foreign key(rol_id) references roles(id)
);

create table calificaciones(
id int primary key not null,
matricula_id int not null,
nota decimal(20,0),
constraint fk_calificacion_matricula  foreign key(matricula_id) references matriculas(id)
);

/*Datos Semilla*/

/*Usuarios*/
INSERT INTO usuarios values(1,'alex','alex@gmail.com','123','1010548720','Alexander','Carrillo','1984-10-10','2127214',null);/*Director*/
INSERT INTO usuarios values(2,'alejo','alejo@gmail.com','456','8128238','Alejandro','Solano','1984-06-21','2337479',null);/*Padre*/
INSERT INTO usuarios values(3,'jesus','jesus@gmail.com','789','40796262','Jesus','Munera','1984-06-21','2747895',null);/*Profesor*/
INSERT INTO usuarios values(4,'estiven','estiven@gmail.com','91011','324587','Estiven','Usme','1984-06-21','3245871',2);/*Hijo*/

/*Roles*/
INSERT INTO roles values(1,'Director');
INSERT INTO roles values(2,'Padre');
INSERT INTO roles values(3,'Profesor');
INSERT INTO roles values(4,'Estudiante');

/*Roles Usuario*/
INSERT INTO rol_usuario values(1,1);
INSERT INTO rol_usuario values(2,2);
INSERT INTO rol_usuario values(3,3);
INSERT INTO rol_usuario values(4,4);

/*Consulta Join*/
select usuarios.id,usuarios.nombre,roles.id as rolId,roles.descripcion from usuarios 
join rol_usuario on rol_usuario.usuario_id = usuarios.id
join roles on rol_usuario.rol_id = roles.id
where usuario_id = 1

CREATE PROCEDURE [dbo].[SP_ConsultarRoles] AS
	BEGIN
		SELECT id,descripcion FROM roles	
	END
GO

/*JOHAN*/
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

CREATE PROCEDURE [dbo].[SP_Consultar] AS
	BEGIN
		SELECT * FROM conexion
	ENDConexion
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