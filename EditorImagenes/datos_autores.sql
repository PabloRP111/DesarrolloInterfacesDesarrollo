create table autores (
id integer primary key auto_increment,
nombre varchar(30) unique not null,
apellido varchar(30) not null,
grupo integer not null,
constraint fk_grupos_autores (grupo) references grupos(id)
);

create table grupos(
id integer primary key,
nombre_grupo varchar(30)
);


-grupos-
insert into grupos values(1,'diseño');
insert into grupos values(2,'testing');
insert into grupos values(3,'documentacion');
insert into grupos values(4,'desarrollo');



--diseño--
insert into autores (nombre, apellido, grupo) values('Abel','Riquelme',1);
insert into autores (nombre, apellido, grupo) values('Alex','Lopez',1);
insert into autores (nombre, apellido, grupo) values('Alejandro','Asencio',1);
insert into autores (nombre, apellido, grupo) values('David','Toledo',1);
insert into autores (nombre, apellido, grupo) values('Christian','Christian',1);
insert into autores (nombre, apellido, grupo) values('Alejandro','Lopez',1);

--testing--
insert into autores (nombre, apellido, grupo) values('Leticia','Garcia',2);

--documentacion--
insert into autores (nombre, apellido, grupo) values('Marcos','García',3);
insert into autores (nombre, apellido, grupo) values('Carlos','Martín',3);
insert into autores (nombre, apellido, grupo) values('Juan Jose','Medina',3);

--desarrollo--
insert into autores (nombre, apellido, grupo) values('Jero','Casares',4);
insert into autores (nombre, apellido, grupo) values('David','López',4);
insert into autores (nombre, apellido, grupo) values('Javier','Vaquero',4);
insert into autores (nombre, apellido, grupo) values('Alexis','García',4);
insert into autores (nombre, apellido, grupo) values('Juan','Carmona',4);
insert into autores (nombre, apellido, grupo) values('Joaquín','Moreno',4);
insert into autores (nombre, apellido, grupo) values('Antonio Jesus','Rodriguez',4);
insert into autores (nombre, apellido, grupo) values('Pablo','Rosas',4);
insert into autores (nombre, apellido, grupo) values('Francisco José','Jiménez',4);
insert into autores (nombre, apellido, grupo) values('Pablo','Rosas',4);
insert into autores (nombre, apellido, grupo) values('Juan','Tortosa',4);
insert into autores (nombre, apellido, grupo) values('Jenaro','Leal',4);