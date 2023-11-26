CREATE DATABASE fastcardDB;

USE fastcardDB;

CREATE TABLE users(
	ID INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
	username VARCHAR(50) NOT NULL,
	Password VARCHAR(50) NOT NULL,
	email VARCHAR(100) NOT null,
	CardImage LONGBLOB
);

SELECT * FROM users;
INSERT INTO users (username, Password, email) VALUES ('igor.afonso', '123456', 'igor.teste@yahoo.com');

CREATE TABLE operations(
	IDUser INT NOT NULL,
	CompleteName VARCHAR(100),
	OperationDate DATETIME,
	BornDate DATE,
	CPF VARCHAR(15),
	RG VARCHAR(20),
	TeachingInstitution VARCHAR(100),
	HaveBF CHAR(1),
	HaveCadUniq CHAR(1),
	CityTeachingInstitutin VARCHAR(100),
	Period VARCHAR(50),
	TermsOfUse CHAR(1),
	MonthStudy VARCHAR(50)
);

SELECT * FROM operations;

CREATE TABLE adress(
	IDUser INT NOT NULL,
	Adress VARCHAR(100),
	Number VARCHAR(10),
	Neightborhood VARCHAR(100),
	MonthStudy VARCHAR(50)
);

create table colors(
	FirstDay varchar(10) primary key,
	ColorOfMonth VARCHAR(50)
);

SELECT * FROM adress;

select * from colors;
insert into colors (FirstDay, ColorOfMonth) values ('01', 'Azul');
insert into colors (FirstDay, ColorOfMonth) values ('02', 'Verde');
insert into colors (FirstDay, ColorOfMonth) values ('03', 'Vermelho');
insert into colors (FirstDay, ColorOfMonth) values ('04', 'Amarelo');
insert into colors (FirstDay, ColorOfMonth) values ('05', 'Rosa');
insert into colors (FirstDay, ColorOfMonth) values ('06', 'Violeta');
insert into colors (FirstDay, ColorOfMonth) values ('07', 'Preto');
insert into colors (FirstDay, ColorOfMonth) values ('08', 'Branco');
insert into colors (FirstDay, ColorOfMonth) values ('09', 'Cinza');
insert into colors (FirstDay, ColorOfMonth) values ('10', 'Roxo');
insert into colors (FirstDay, ColorOfMonth) values ('11', 'Laranja');
insert into colors (FirstDay, ColorOfMonth) values ('12', 'Dourado');