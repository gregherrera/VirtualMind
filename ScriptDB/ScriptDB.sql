USE [Cotizacion]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

USE [Cotizacion]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Compras')
BEGIN
	Drop Table [Dbo].[Compras]
END
GO

CREATE TABLE [dbo].[Compras](
	[Id] [bigint] NOT NULL Identity(1,1),
	[Id_Usuario] [bigint] NOT NULL,
	[Id_Moneda] [nvarchar](25) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Tasa] [decimal](12, 2) NOT NULL,
	[Monto] [decimal](12, 2) NOT NULL,
	[Valor] [decimal](12, 2) NOT NULL
) ON [PRIMARY]
GO

Alter table [dbo].[Compras] Add constraint PK_Compras Primary Key(Id) 
GO

Create Index IDX_Compras_Usuario ON [dbo].[Compras](Id_usuario)
GO

Create Index IDX_Compras_Moneda ON [dbo].[Compras](Id_moneda)
GO

Create Index IDX_Compras_Fecha ON [dbo].[Compras](Fecha)
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Limites')
BEGIN
	Drop Table [Dbo].[Limites]
END
GO

CREATE TABLE [dbo].[Limites](
	[Id] [bigint] NOT NULL Identity(1,1),
	[Id_Moneda] [nvarchar](25) NOT NULL,
	[Id_Usuario] [bigint] NOT NULL,
	[anio] [int] NOT NULL,
	[Mes] [int] NOT NULL,
	[Monto] [decimal](16, 0) NOT NULL
) ON [PRIMARY]
GO

Alter table [dbo].[Limites] Add constraint PK_Limites Primary Key(Id) 
GO

Alter table [dbo].[Limites] Add constraint UQ_Limites Unique(Id_Moneda, Id_Usuario, anio, mes) 
GO

Create Index IDX_Limites_Usuario ON [dbo].[Limites](Id_usuario)
GO

Create Index IDX_Limites_Moneda ON [dbo].[Limites](Id_moneda)
GO

Create Index IDX_Limites_Anio ON [dbo].[Limites](Anio)
GO

Create Index IDX_Limites_Mes ON [dbo].[Limites](Mes)
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Monedas')
BEGIN
	Drop Table [Dbo].[Monedas]
END
GO

CREATE TABLE [dbo].[Monedas](
	[Id] [nvarchar](25) NOT NULL,
	[Url] [nvarchar](150) NOT NULL,
	[Factor] [decimal](5, 2) NOT NULL
) ON [PRIMARY]
GO

Alter table [dbo].[Monedas] Add constraint PK_Monedas Primary Key(Id) 
GO

Alter table [dbo].[Compras] Add constraint FK_Compras_Monedas Foreign Key(Id_moneda) References [dbo].[Monedas](Id) 
GO

Alter table [dbo].[Limites] Add constraint FK_Limites_Monedas Foreign Key(Id_moneda) References [dbo].[Monedas](Id) 
GO