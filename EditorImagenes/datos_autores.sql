create table autores (
id integer primary key auto_increment,
nombre varchar(30) unique not null,
grupo varchar(40) not null
);