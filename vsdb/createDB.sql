CREATE DATABASE [VaccinationSystemDB] COLLATE Polish_CS_AS;
GO

USE [VaccinationSystemDB];
GO

CREATE TABLE [dbo].[Admins] (
	id uniqueidentifier NOT NULL PRIMARY KEY,
	pesel varchar(11) NOT NULL,
	firstName varchar(50) NOT NULL,
	lastName varchar(50) NOT NULL,
	dateOfBirth datetime2(7) NOT NULL,
	mail varchar(100) NOT NULL,
	password nvarchar(MAX) NOT NULL,
	phoneNumber varchar(15) NOT NULL
);
GO

CREATE TABLE [dbo].[VaccinationCenters] (
	id uniqueidentifier NOT NULL PRIMARY KEY,
	name varchar(100) NOT NULL,
	city varchar(40) NOT NULL,
	address varchar(100) NOT NULL,
	active bit NOT NULL
);
GO

CREATE TABLE [dbo].[Vaccines] (
	id uniqueidentifier NOT NULL PRIMARY KEY,
	company varchar(50) NOT NULL,
	name varchar(50) NOT NULL,
	numberOfDoses int NOT NULL,
	minDaysBetweenDoses int NOT NULL,
	maxDaysBetweenDoses int NOT NULL,
	virus int NOT NULL,
	minPatientAge int NOT NULL,
	maxPatientAge int NOT NULL,
	active bit NOT NULL,
	VaccinationCenterid uniqueidentifier FOREIGN KEY REFERENCES [dbo].[VaccinationCenters](id)
);
GO

CREATE TABLE [dbo].[Patients] (
	id uniqueidentifier NOT NULL PRIMARY KEY,
	active bit NOT NULL,
	pesel varchar(11) NOT NULL,
	firstName varchar(50) NOT NULL,
	lastName varchar(50) NOT NULL,
	dateOfBirth datetime2(7) NOT NULL,
	mail varchar(100) NOT NULL,
	password nvarchar(MAX) NOT NULL,
	phoneNumber varchar(15) NOT NULL
);
GO

CREATE TABLE [dbo].[VaccinesInCenters] (
	id uniqueidentifier NOT NULL PRIMARY KEY,
	vaccineId uniqueidentifier FOREIGN KEY REFERENCES [dbo].[Vaccines](id),
	vaccinationCenterId uniqueidentifier FOREIGN KEY REFERENCES [dbo].[VaccinationCenters](id)
);
GO

CREATE TABLE [dbo].[Doctors] (
	doctorId uniqueidentifier NOT NULL PRIMARY KEY,
	vaccinationCenterId uniqueidentifier FOREIGN KEY REFERENCES [dbo].[VaccinationCenters](id),
	patientAccountId uniqueidentifier FOREIGN KEY REFERENCES [dbo].[Patients](id),
	active bit
);
GO

CREATE TABLE [dbo].[OpeningHours] (
	id uniqueidentifier NOT NULL PRIMARY KEY,
	[from] time NOT NULL,
	[to] time NOT NULL,
	vaccinationCenterId uniqueidentifier FOREIGN KEY REFERENCES [dbo].[VaccinationCenters](id),
	day int NOT NULL
);
GO

CREATE TABLE [dbo].[TimeSlots] (
	id uniqueidentifier NOT NULL PRIMARY KEY,
	[from] datetime2 NOT NULL,
	[to] datetime2 NOT NULL,
	doctorId uniqueidentifier FOREIGN KEY REFERENCES [dbo].[Doctors](doctorId),
	isFree bit NOT NULL,
	active bit NOT NULL
);
GO

CREATE TABLE [dbo].[Appointments] (
	id uniqueidentifier NOT NULL PRIMARY KEY,
	whichDose int NOT NULL,
	timeSlotId uniqueidentifier FOREIGN KEY REFERENCES [dbo].[TimeSlots](id),
	patientId uniqueidentifier FOREIGN KEY REFERENCES [dbo].[Patients](id),
	vaccineId uniqueidentifier FOREIGN KEY REFERENCES [dbo].[Vaccines](id),
	[state] int NOT NULL,
	vaccineBatchNumber varchar(100),
	certifyState int NOT NULL,
	Patientid1 uniqueidentifier FOREIGN KEY REFERENCES [dbo].[Patients](id),
	doctorId uniqueidentifier FOREIGN KEY REFERENCES [dbo].[Doctors](doctorId),
	doctorId1 uniqueidentifier FOREIGN KEY REFERENCES [dbo].[Doctors](doctorId)
);
GO

CREATE TABLE [dbo].[Certificates] (
	id uniqueidentifier NOT NULL PRIMARY KEY,
	url varchar(250) NOT NULL,
	vaccineId uniqueidentifier FOREIGN KEY REFERENCES [dbo].[Vaccines](id),
	patientId uniqueidentifier FOREIGN KEY REFERENCES [dbo].[Patients](id)
);
GO

CREATE TABLE [dbo].[VaccinationCounts] (
	id uniqueidentifier NOT NULL PRIMARY KEY,
	virus int NOT NULL,
	[count] int NOT NULL,
	patientId uniqueidentifier FOREIGN KEY REFERENCES [dbo].[Patients](id)
);
GO

BEGIN TRANSACTION
	  INSERT INTO [dbo].[Admins] ([id]
      ,[pesel]
      ,[firstName]
      ,[lastName]
      ,[dateOfBirth]
      ,[mail]
      ,[password]
      ,[phoneNumber])
	VALUES ('a73a1d1a-b5fg-56c9-ba56-1924f93d6634', '81111111111', 'Jan', 'Kowalski', '1981-11-11 00:00:00.0000000', 'superadmin@mail.com', 'test123', '+48111222333');
	  INSERT INTO [dbo].[VaccinationCenters] ([id]
      ,[name]
      ,[city]
      ,[address]
      ,[active])
	VALUES ('837c1d09-8664-4480-beff-45fbd914c87e', 'Punkt Szczepień Populacyjnych', 'Warszawa', 'Żwirki i Wigury 95/97', 1),
    ('426955a9-4cbc-48a6-99a4-bda4a2137ab1', 'Apteczny Punkt Szczepień', 'Warszawa', 'Mokotowska 27/Lok.1 i 4', 1);
	  INSERT INTO [dbo].[Vaccines] ([id]
      ,[company]
      ,[name]
      ,[numberOfDoses]
      ,[minDaysBetweenDoses]
      ,[maxDaysBetweenDoses]
      ,[virus]
      ,[minPatientAge]
      ,[maxPatientAge]
      ,[active]
	  ,[VaccinationCenterid])
	VALUES ('aa82a7a5-a040-4dab-9ec4-600e44dbaf8e', 'Pfeizer', 'Pfeizer vaccine', 2, 30, 90, 0, 12, 120, 1, null),
    ('837c1d09-d92c-45d2-b47f-f2aeac90b3f7', 'Johnson and Johnson', 'J&J', 1, -1, -1, 0, 18, 120, 1, null),
    ('31d9b4bf-5c1c-4f2d-b997-f6096758eac9', 'Moderna', 'Moderna vaccine', 2, 30, 90, 0, 18, 99, 1, null);
	  INSERT INTO [dbo].[Patients] ([id]
      ,[pesel]
      ,[firstName]
      ,[lastName]
      ,[dateOfBirth]
      ,[mail]
      ,[password]
      ,[phoneNumber]
      ,[active])
	VALUES ('812c47eb-22b8-422a-a963-a976a26efdc8', '82121211111', 'Jan', 'Nowak', '1982-12-12 00:00:00.0000000', 'j.nowak@mail.com', 'test123', '+48555221331', 1),
    ('f969ffd0-6dbc-4900-8eb8-b4fe25906a74', '92120211122', 'Janina', 'Nowakowa', '1992-12-02 00:00:00.0000000', 'j.nowakowa@mail.com', 'test123', '+48576221390', 1),
    ('1448be96-c2de-4fdb-93c5-3caf1de2f8a0', '59062011333', 'Robert', 'Weide', '1959-06-20 00:00:00.0000000', 'robert.b.weide@mail.com', 'test123', '+48125200331', 1),
    ('acd9fa16-404e-4305-b57f-93659054be7e', '74011011111', 'Monika', 'Kowalska', '1974-01-10 00:00:00.0000000', 'm.kowalska@mail.com', 'test123', '+48349824991' , 1),
    ('31f90897-4455-4d26-aa61-d3c3adcd8f70', '82121211133', 'Leon', 'Izabelski', '1982-12-12 00:00:00.0000000', 'leonizabel@mail.com', 'test123', '+48903251026', 1);
	  INSERT INTO [dbo].[VaccinesInCenters] ([id]
      ,[vaccinationCenterId],
	  [vaccineId])
	VALUES ('d124c85f-9f24-48ef-a625-1ae972c84b62', '837c1d09-8664-4480-beff-45fbd914c87e', 'aa82a7a5-a040-4dab-9ec4-600e44dbaf8e'),
	('774d3c33-e3d3-4aac-806e-d47a738fghef', '837c1d09-8664-4480-beff-45fbd914c87e', '837c1d09-d92c-45d2-b47f-f2aeac90b3f7'),
	('76c3d0ee-02ee-46f8-8832-a458d80d76bb', '837c1d09-8664-4480-beff-45fbd914c87e', '31d9b4bf-5c1c-4f2d-b997-f6096758eac9'),
	('e9c7802b-7a52-4355-aedf-ba55c45a8920', '426955a9-4cbc-48a6-99a4-bda4a2137ab1', 'aa82a7a5-a040-4dab-9ec4-600e44dbaf8e'),
	('4852dde8-5p2c-41f0-8836-823d7ea59ddf', '426955a9-4cbc-48a6-99a4-bda4a2137ab1', '837c1d09-d92c-45d2-b47f-f2aeac90b3f7');
	  INSERT INTO [dbo].[Doctors] ([doctorId]
      ,[vaccinationCenterId]
      ,[patientAccountId]
      ,[active])
	VALUES ('9d77b5e9-2823-4274-b326-d371e5582274', '837c1d09-8664-4480-beff-45fbd914c87e', '1448be96-c2de-4fdb-93c5-3caf1de2f8a0', 1),
    ('003a7da9-f6e3-4342-85ea-0d3296f99d41', '426955a9-4cbc-48a6-99a4-bda4a2137ab1', 'acd9fa16-404e-4305-b57f-93659054be7e', 1);
	  INSERT INTO [dbo].[OpeningHours] ([id]
      ,[from]
      ,[to]
      ,[day]
      ,[vaccinationCenterId])
	VALUES ('4e994d2a-81ad-4f5f-8d26-69ca40524129', '08:00:00.0000000', '20:00:00.0000000', 0,'837c1d09-8664-4480-beff-45fbd914c87e'),
    ('9a0f1d66-6b29-4aaf-912a-56b4efcbf4ea', '08:00:00.0000000', '20:00:00.0000000', 1,'837c1d09-8664-4480-beff-45fbd914c87e'),
    ('b256c309-4828-45ac-966f-958a4bdbf167', '08:00:00.0000000', '20:00:00.0000000', 2,'837c1d09-8664-4480-beff-45fbd914c87e'),
    ('09936f73-c102-466d-a92e-890520860670', '08:00:00.0000000', '20:00:00.0000000', 3,'837c1d09-8664-4480-beff-45fbd914c87e'),
    ('d1ff9a4b-1681-4817-afe7-9c425450262b', '08:00:00.0000000', '20:00:00.0000000', 4,'837c1d09-8664-4480-beff-45fbd914c87e'),
    ('3cc64799-132f-463c-81bd-8040fa22bb67', '08:00:00.0000000', '20:00:00.0000000', 5,'837c1d09-8664-4480-beff-45fbd914c87e'),
    ('83e9eeab-66ae-4885-bc02-d26fe0a86163', '00:00:00.0000000', '00:00:00.0000000', 6,'837c1d09-8664-4480-beff-45fbd914c87e'),

    ('60049231-b166-444b-80b9-dd08a312805b', '08:00:00.0000000', '20:00:00.0000000', 0,'426955a9-4cbc-48a6-99a4-bda4a2137ab1'),
    ('d8dbe2a2-a6df-4dcc-8a77-e9381cdf2206', '08:00:00.0000000', '20:00:00.0000000', 1,'426955a9-4cbc-48a6-99a4-bda4a2137ab1'),
    ('ef15aafc-28ed-454f-bcf9-c1a82d46a986', '08:30:00.0000000', '20:00:00.0000000', 2,'426955a9-4cbc-48a6-99a4-bda4a2137ab1'),
    ('2a7bf9ed-158c-4358-9e41-bf14d6c83d63', '08:00:00.0000000', '20:00:00.0000000', 3,'426955a9-4cbc-48a6-99a4-bda4a2137ab1'),
    ('ba58651a-f8c1-4d1c-bbdc-ff68be15eac9', '08:00:00.0000000', '20:00:00.0000000', 4,'426955a9-4cbc-48a6-99a4-bda4a2137ab1'),
    ('c0a94c2c-35e1-4e0c-b9ca-73809a1ce1ff', '08:00:00.0000000', '20:00:00.0000000', 5,'426955a9-4cbc-48a6-99a4-bda4a2137ab1'),
    ('f1d56ff5-252a-4077-8cae-3b99c93072db', '00:00:00.0000000', '00:00:00.0000000', 6,'426955a9-4cbc-48a6-99a4-bda4a2137ab1');
	  INSERT INTO [dbo].[TimeSlots] ([id]
      ,[from]
      ,[to]
      ,[doctorId]
      ,[isFree]
      ,[active])
	VALUES ('50c12fe6-a321-4775-b6e3-d901dfda2616', '2022-03-25 12:30:00.0000000', '2022-03-25 12:45:00.0000000', '9d77b5e9-2823-4274-b326-d371e5582274', 0, 1),
    ('2e9009fd-d824-41b1-9a02-992cccb04d43', '2022-04-23 12:30:00.0000000', '2022-04-23 12:45:00.0000000', '9d77b5e9-2823-4274-b326-d371e5582274', 0, 1),
    ('f492deb3-02ca-41aa-a54c-98f83f915234', '2022-04-23 13:30:00.0000000', '2022-04-23 13:45:00.0000000', '9d77b5e9-2823-4274-b326-d371e5582274', 0, 1),
    ('43c89e51-b74f-48c3-80d9-f1304331d03d', '2022-05-25 12:45:00.0000000', '2022-05-25 13:00:00.0000000', '9d77b5e9-2823-4274-b326-d371e5582274', 0, 1),
    ('74c0ac81-a643-4846-ba42-9a310faf70f0', '2022-05-25 12:45:00.0000000', '2022-05-25 13:00:00.0000000', '9d77b5e9-2823-4274-b326-d371e5582274', 0, 1),
    ('3a6a6b44-5443-4760-9ce5-da1cc4644cc8', '2022-04-25 09:15:00.0000000', '2022-04-25 09:30:00.0000000', '9d77b5e9-2823-4274-b326-d371e5582274', 0, 1),
    ('5ab7b870-5bad-4b7f-8ee9-95773434842a', '2022-05-20 09:15:00.0000000', '2022-05-20 09:30:00.0000000', '9d77b5e9-2823-4274-b326-d371e5582274', 1, 1),
	('a6b79i71-5god-4c7f-5ff9-95773434865l', '2022-05-20 09:30:00.0000000', '2022-05-20 09:45:00.0000000', '9d77b5e9-2823-4274-b326-d371e5582274', 1, 1),
	('po27b869-o9ad-4b0f-4ek9-9kk734p5842q', '2022-05-20 10:15:00.0000000', '2022-05-20 10:30:00.0000000', '9d77b5e9-2823-4274-b326-d371e5582274', 1, 1),
	('tab7b8u9-5iad-4b7f-8iei-956634io842a', '2022-05-20 10:30:00.0000000', '2022-05-20 10:45:00.0000000', '9d77b5e9-2823-4274-b326-d371e5582274', 1, 1),
	('916678fa-6c7a-4806-a55e-948c6c4fd73f', '2022-05-30 09:15:00.0000000', '2022-05-30 09:30:00.0000000', '9d77b5e9-2823-4274-b326-d371e5582274', 1, 1),
	('6ff8ad9a-a3bc-4e95-9152-ff0afdbed43e', '2022-05-30 09:30:00.0000000', '2022-05-30 09:45:00.0000000', '9d77b5e9-2823-4274-b326-d371e5582274', 1, 1),
	('4dd70ef1-4942-487a-bec3-bd770de5984d', '2022-05-30 10:15:00.0000000', '2022-05-30 10:30:00.0000000', '9d77b5e9-2823-4274-b326-d371e5582274', 1, 1),
	('bc9f17bf-a4b9-47fc-be6d-da5c6b4bb2e7', '2022-05-30 10:30:00.0000000', '2022-05-30 10:45:00.0000000', '9d77b5e9-2823-4274-b326-d371e5582274', 1, 1),
	('51ba1e88-e7a9-4a6c-9560-2879174908cc', '2022-06-20 09:15:00.0000000', '2022-06-20 09:30:00.0000000', '9d77b5e9-2823-4274-b326-d371e5582274', 1, 1),
	('91d92d36-e9a2-4042-9ae7-a346b29144ba', '2022-06-20 09:30:00.0000000', '2022-06-20 09:45:00.0000000', '9d77b5e9-2823-4274-b326-d371e5582274', 1, 1),
	('4657e1ed-9d23-4b19-b460-77989cf4865f', '2022-06-20 10:15:00.0000000', '2022-06-20 10:30:00.0000000', '9d77b5e9-2823-4274-b326-d371e5582274', 1, 1),
	('f30c0a30-214a-4fed-86b8-efbd977b3ac7', '2022-06-20 10:30:00.0000000', '2022-06-20 10:45:00.0000000', '9d77b5e9-2823-4274-b326-d371e5582274', 1, 1),

    ('731e953c-6d7a-4aef-a40d-0ded3ec3cfd3', '2022-04-25 12:30:00.0000000', '2022-04-25 12:45:00.0000000', '003a7da9-f6e3-4342-85ea-0d3296f99d41', 0, 1),
    ('386791e1-966c-430b-8492-61badceb0c09', '2022-04-24 12:30:00.0000000', '2022-04-24 12:45:00.0000000', '003a7da9-f6e3-4342-85ea-0d3296f99d41', 0, 1),
    ('e2fd8ec7-eeb0-47c8-95a8-bd51d513db49', '2022-03-20 09:15:00.0000000', '2022-03-20 09:30:00.0000000', '003a7da9-f6e3-4342-85ea-0d3296f99d41', 0, 0),
    ('9e358a18-2b97-47ac-81f1-b82c204a80b6', '2022-05-20 09:15:00.0000000', '2022-05-20 09:30:00.0000000', '003a7da9-f6e3-4342-85ea-0d3296f99d41', 1, 1),
    ('456cdf7d-315d-4e69-96c7-2479d6cc7400', '2022-05-20 09:30:00.0000000', '2022-05-20 09:45:00.0000000', '003a7da9-f6e3-4342-85ea-0d3296f99d41', 1, 1),
    ('6526d366-b00d-4bb2-9da1-8c2c907be31c', '2022-05-20 10:15:00.0000000', '2022-05-20 10:30:00.0000000', '003a7da9-f6e3-4342-85ea-0d3296f99d41', 1, 1),
    ('39d798ca-7543-4117-a32e-3767f5d44964', '2022-05-20 10:30:00.0000000', '2022-05-20 10:45:00.0000000', '003a7da9-f6e3-4342-85ea-0d3296f99d41', 1, 1),
	('43di98ac-6533-4471-a23p-3768f5o41964', '2022-05-30 09:15:00.0000000', '2022-05-30 09:30:00.0000000', '003a7da9-f6e3-4342-85ea-0d3296f99d41', 1, 1),
	('95li98ac-6773-4491-a13l-3068t5o41263', '2022-05-30 09:30:00.0000000', '2022-05-30 09:45:00.0000000', '003a7da9-f6e3-4342-85ea-0d3296f99d41', 1, 1),
	('e6061392-f017-4f66-ae36-5eddede32f07', '2022-05-30 10:15:00.0000000', '2022-05-30 10:30:00.0000000', '003a7da9-f6e3-4342-85ea-0d3296f99d41', 1, 1),
	('3e1fae9d-48e1-49b6-8d8b-b935daea278d', '2022-05-30 10:30:00.0000000', '2022-05-30 10:45:00.0000000', '003a7da9-f6e3-4342-85ea-0d3296f99d41', 1, 1),
	('238190ad-1e58-44ab-b4b9-d1bbbef34b9d', '2022-06-20 09:15:00.0000000', '2022-06-20 09:30:00.0000000', '003a7da9-f6e3-4342-85ea-0d3296f99d41', 1, 1),
	('ec2e1016-33a7-45b1-ae95-7ad146a35314', '2022-06-20 09:30:00.0000000', '2022-06-20 09:45:00.0000000', '003a7da9-f6e3-4342-85ea-0d3296f99d41', 1, 1),
	('18c86c1f-4dc4-40c3-ae81-8aa33124dfc5', '2022-06-20 10:15:00.0000000', '2022-06-20 10:30:00.0000000', '003a7da9-f6e3-4342-85ea-0d3296f99d41', 1, 1),
	('bf0b9cd1-0f6d-4998-b3b0-96bb902eab04', '2022-06-20 10:30:00.0000000', '2022-06-20 10:45:00.0000000', '003a7da9-f6e3-4342-85ea-0d3296f99d41', 1, 1);
	  INSERT INTO [dbo].[Appointments] ([id]
      ,[whichDose]
      ,[timeSlotId]
      ,[patientId]
      ,[vaccineId]
      ,[state]
      ,[vaccineBatchNumber]
	  ,[certifyState]
	  ,[Patientid1]
	  ,[doctorId]
	  ,[doctorId1])
	VALUES ('04f3e06e-d098-4366-871d-a2a9c4020cbc', 1, '50c12fe6-a321-4775-b6e3-d901dfda2616', '812c47eb-22b8-422a-a963-a976a26efdc8', 'aa82a7a5-a040-4dab-9ec4-600e44dbaf8e', 2, 'abcd', 0, null, null, null),
    ('d7e25c56-27d4-4c79-b615-38bb5dc224f3', 2, '2e9009fd-d824-41b1-9a02-992cccb04d43', '812c47eb-22b8-422a-a963-a976a26efdc8', 'aa82a7a5-a040-4dab-9ec4-600e44dbaf8e', 2, 'abcd', 2, null, null, null),

    ('ced082ea-974c-4e62-bb20-49461e515bb1', 1, 'f492deb3-02ca-41aa-a54c-98f83f915234', 'f969ffd0-6dbc-4900-8eb8-b4fe25906a74', 'aa82a7a5-a040-4dab-9ec4-600e44dbaf8e', 2, 'abcd', 0, null, null, null),
    ('0d88ec97-a35a-497b-a561-6363280cb4c8', 2, '43c89e51-b74f-48c3-80d9-f1304331d03d', 'f969ffd0-6dbc-4900-8eb8-b4fe25906a74', 'aa82a7a5-a040-4dab-9ec4-600e44dbaf8e', 1, '', 1, null, null, null),

    ('2282885a-7323-4fcc-b1d8-f3429b1dfd33', 1, '731e953c-6d7a-4aef-a40d-0ded3ec3cfd3', '1448be96-c2de-4fdb-93c5-3caf1de2f8a0', '837c1d09-d92c-45d2-b47f-f2aeac90b3f7', 1, '', 0, null, null, null),
    
	('340eff40-cee8-4408-946a-23b6be94f3f8', 2, '386791e1-966c-430b-8492-61badceb0c09', 'acd9fa16-404e-4305-b57f-93659054be7e', 'aa82a7a5-a040-4dab-9ec4-600e44dbaf8e', 2, 'abcd', 2, null, null, null);
	  INSERT INTO [dbo].[Certificates] ([id]
      ,[url]
      ,[patientId]
      ,[vaccineId])
	VALUES ('4f341846-1436-4c2e-870f-bc9a9966e090', 'placeholder', '1448be96-c2de-4fdb-93c5-3caf1de2f8a0', 'aa82a7a5-a040-4dab-9ec4-600e44dbaf8e'),
    ('4682ca4c-be71-43e8-a7b1-9053d1a25533', 'placeholder', 'acd9fa16-404e-4305-b57f-93659054be7e', 'aa82a7a5-d92c-45d2-b47f-f2a3a5e0b3f7'),
    ('ce030cef-04c0-484e-ac2a-1bcc90a28b56', 'placeholder', '812c47eb-22b8-422a-a963-a976a26efdc8', 'aa82a7a5-d92c-45d2-b47f-f2a3a5e0b3f7');
	  INSERT INTO [dbo].[VaccinationCounts] ([id]
      ,[patientId]
      ,[virus]
      ,[count])
	VALUES ('713c46ec-22c8-412a-k9k3-a966a26rfdh7', '812c47eb-22b8-422a-a963-a976a26efdc8', 0, 2),
    ('o559ffo0-8dic-4801-8ib5-b4fh24907a84', 'f969ffd0-6dbc-4900-8eb8-b4fe25906a74', 0, 1),
    ('1558be69-cd2e-4jdb-90c5-3calfde2f8p0', '1448be96-c2de-4fdb-93c5-3caf1de2f8a0', 0, 2),
    ('acl3fy16-404e-4i06-b46f-93695051bp7e', 'acd9fa16-404e-4305-b57f-93659054be7e', 0, 2);
COMMIT