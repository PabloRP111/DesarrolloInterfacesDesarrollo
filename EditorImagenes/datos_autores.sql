create table autores (
id integer primary key auto_increment,
nombre varchar(30) unique not null,
apellido varchar(30) not null,
grupo varchar(40) not null
);

insert into autores (nombre, apellido, grupo) values('Jero');