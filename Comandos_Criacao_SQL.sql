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