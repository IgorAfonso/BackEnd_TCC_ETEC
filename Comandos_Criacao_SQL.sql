CREATE DATABASE fastcardDB;

USE fastcardDB;

CREATE TABLE users(
	ID INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
	username VARCHAR(50) NOT NULL,
	Password VARCHAR(50) NOT NULL,
	email VARCHAR(100) NOT NULL
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
	Period VARCHAR(50) PRIMARY KEY
);

SELECT * FROM operations;

CREATE TABLE adress(
	IDUser INT NOT NULL,
	Adress VARCHAR(100),
	Number VARCHAR(10),
	Neightborhood VARCHAR(100),
	Period VARCHAR(50) PRIMARY KEY
);

SELECT * FROM adress;