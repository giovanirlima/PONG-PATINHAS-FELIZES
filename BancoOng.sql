CREATE DATABASE Ong;

USE Ong;

CREATE TABLE Pessoa(
	CPF VARCHAR(14) CONSTRAINT PK_Pessoa PRIMARY KEY,
	Nome VARCHAR(50) NOT NULL,
	Sexo VARCHAR(10),
	Rua VARCHAR(20),
	Numero int,
	Bairro VARCHAR(20),
	Cidade VARCHAR(20),
	Estado VARCHAR(20),
	Telefone VARCHAR(11));

CREATE TABLE Animal(
	CHIP INT IDENTITY CONSTRAINT PK_Animal PRIMARY KEY,
	Raca VARCHAR(20),
	Sexo VARCHAR(10),
	Nome VARCHAR(50));

CREATE TABLE Pessoa_Adota_Animal(
	CPF VARCHAR(14) CONSTRAINT FK_Pessoa FOREIGN KEY (CPF) REFERENCES PESSOA(CPF),
	CHIP INT CONSTRAINT FK_Animal FOREIGN KEY (CHIP) REFERENCES Animal(CHIP),
	Quantidade int identity,
	CONSTRAINT FK_Pessoa_Adota_Animal PRIMARY KEY (CPF, CHIP, Quantidade));

CREATE TABLE Animal_Familia(
	CHIP INT CONSTRAINT FK_Animal_Familia
	FOREIGN KEY (CHIP) REFERENCES Animal(CHIP),
	Familia varchar(20),
	PRIMARY KEY(CHIP)); 
	

INSERT INTO Animal VALUES('Doberman', 'Macho', 'Negao');

INSERT INTO Animal_Familia VALUES(1, 'Cachorro');