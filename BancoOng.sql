CREATE DATABASE ONG;

USE ONG;

CREATE TABLE Pessoa(
	CPF VARCHAR(14) CONSTRAINT PK_Pessoa PRIMARY KEY,
	Nome VARCHAR(50) NOT NULL,	
	Nascimento date,
	Sexo VARCHAR(10),
	Rua VARCHAR(20),
	Numero VARCHAR(10),
	Bairro VARCHAR(20),
	Cidade VARCHAR(20),
	Estado VARCHAR(20),
	Telefone VARCHAR(11));

CREATE TABLE Animal(
	CHIP INT IDENTITY CONSTRAINT PK_Animal PRIMARY KEY,
	Familia VARCHAR(20) NOT NULL,
	Raca VARCHAR(20) NOT NULL,
	Sexo VARCHAR(10) NOT NULL,
	Nome VARCHAR(50));

CREATE TABLE Pessoa_Adota_Animal(
	CPF VARCHAR(14) CONSTRAINT FK_Pessoa FOREIGN KEY (CPF) REFERENCES PESSOA(CPF),
	CHIP INT CONSTRAINT FK_Animal FOREIGN KEY (CHIP) REFERENCES Animal(CHIP),
	Quantidade int,
	CONSTRAINT FK_Pessoa_Adota_Animal PRIMARY KEY (CHIP));

CREATE TABLE Animais_Disponiveis(
	CHIP INT CONSTRAINT PK_Animais_Disp FOREIGN KEY (CHIP) REFERENCES Animal(CHIP),
	CONSTRAINT PK_Animais_Disponiveis PRIMARY KEY(CHIP));

