/*
Author: Jose Angulo
Date: 2/19/2023
Assignment:Assignment 5B: Stored Procedures, Functions, Triggers
*/

USE master
GO

/****** Object:  Database VeterinaryClinic  ******/
IF DB_ID('VeterinaryClinic') IS NOT NULL
	DROP DATABASE VeterinaryClinic
GO

CREATE DATABASE VeterinaryClinic
GO

USE VeterinaryClinic
Go

CREATE TABLE Animal
(AnimalID int NOT NULL IDENTITY,
 BreedID int NOT NULL,
 OwnerID int NOT NULL,
 PhysicianID int NULL,
 [Name] varchar(128) NOT NULL,
 PRIMARY KEY (AnimalID));

CREATE TABLE [Owner]
(OwnerID int NOT NULL IDENTITY,
 AnimalID int NOT NULL,
 ZipCodeID int NULL,
 LastName varchar(128) NOT NULL,
 FirstName varchar(128) NOT NULL,
 Street varchar(256) NULL,
 Phone varchar(10) NOT NULL,
 PRIMARY KEY (OwnerID));

CREATE TABLE Breed
(BreedID int NOT NULL IDENTITY,
 [Name] varchar(128) NOT NULL,
 [Description] text NULL,
 PRIMARY KEY (BreedID));

CREATE TABLE Physician
(PhysicianID int NOT NULL IDENTITY,
 ZipCodeID int NULL,
 LastName varchar(128) NOT NULL,
 FirstName varchar(128) NOT NULL,
 Street varchar(256) NULL,
 Phone varchar(10) NOT NULL,
 PRIMARY KEY (PhysicianID));

CREATE TABLE MedicalCondition
(MedicalConditionID int NOT NULL IDENTITY,
 CommonName varchar(128) NOT NULL,
 ScientificName varchar(128) NOT NULL,
 PRIMARY KEY (MedicalConditionID));

CREATE TABLE ZipCode
(ZipCodeID int NOT NULL IDENTITY,
 ZipCode varchar(10),
 City varchar(128),
 [State] varchar(128),
 PRIMARY KEY (ZipCodeID));

CREATE TABLE Appointment
(AppointmentID int NOT NULL IDENTITY,
 AnimalID int NOT NULL,
 OwnerID int NOT NULL,
 PhysicianID int NOT NULL,
 StartDate date NOT NULL,
 StartTime time NOT NULL,
 PRIMARY KEY (AppointmentID));

CREATE TABLE MedicalHistory
(MedicalHistoryID int NOT NULL IDENTITY,
 MedicalConditionID int NOT NULL,
 AnimalID int NOT NULL,
 DateAdded date NOT NULL,
 PRIMARY KEY (MedicalHistoryID));

--Constraints for Animal Table
ALTER TABLE Animal
   ADD CONSTRAINT FK_BreedAnimal FOREIGN KEY (BreedID) REFERENCES Breed(BreedID);
ALTER Table Animal
   ADD CONSTRAINT FK_OwnerAnimal FOREIGN KEY (OwnerID) REFERENCES [Owner](OwnerID);
ALTER TABLE Animal
   ADD CONSTRAINT FK_PhysicianAnimal FOREIGN KEY (PhysicianID) REFERENCES Physician(PhysicianID);

--Constraints for Owner Table
ALTER TABLE [Owner]
    ADD CONSTRAINT FK_AnimalOwner FOREIGN KEY (AnimalID) REFERENCES Animal(AnimalID);
ALTER TABLE [Owner]
   ADD CONSTRAINT FK_ZipCodeOwner FOREIGN KEY (ZipCodeID) REFERENCES ZipCode(ZipCodeID);

--Constraints for Physician
ALTER TABLE Physician
   ADD CONSTRAINT FK_ZipCodePhysician FOREIGN KEY (ZipCodeID) REFERENCES ZipCode(ZipCodeID);

--Constraints for Appointment Table
ALTER TABLE Appointment
   ADD CONSTRAINT FK_AnimalAppointment FOREIGN KEY (AnimalID) REFERENCES Animal(AnimalID);
ALTER TABLE Appointment
   ADD CONSTRAINT FK_OwnerAppointment FOREIGN KEY (OwnerID) REFERENCES [Owner](OwnerID);
ALTER TABLE Appointment
   ADD CONSTRAINT FK_PhysicianAppointment FOREIGN KEY (PhysicianID) REFERENCES Physician(PhysicianID);

--Constraints for MedicalHistory Table
ALTER TABLE MedicalHistory
   ADD CONSTRAINT FK_AnimalMedicalHistory FOREIGN KEY (AnimalID) REFERENCES Animal(AnimalID);
ALTER TABLE MedicalHistory
   ADD CONSTRAINT FK_MedicalConditionMedicalHistory FOREIGN KEY (MedicalConditionID) REFERENCES MedicalCondition(MedicalConditionID);



