USE [Cotizacion]
GO

DELETE FROM [dbo].[Compras]
GO

DELETE FROM [dbo].[Limites]
GO

DELETE FROM [dbo].[Monedas]
GO

INSERT INTO [dbo].[Monedas]
		   ([Descripcion]
           ,[Url]
           ,[Factor])
     VALUES
           ('dolar'
           ,'https://www.bancoprovincia.com.ar/Principal/Dolar'
           ,1)
GO

INSERT INTO [dbo].[Monedas]
           ([Descripcion]
           ,[Url]
           ,[Factor])
     VALUES
           ('real'
           ,'https://www.bancoprovincia.com.ar/Principal/Dolar'
           ,0.25)
GO

INSERT INTO [dbo].[Limites]
           ([Id_Moneda]
           ,[Id_Usuario]
           ,[anio]
           ,[Mes]
           ,[Monto])
     VALUES
           (1
           ,1
           ,2021
           ,8
           ,200.00)
GO

INSERT INTO [dbo].[Limites]
           ([Id_Moneda]
           ,[Id_Usuario]
           ,[anio]
           ,[Mes]
           ,[Monto])
     VALUES
           (2
           ,1
           ,2021
           ,8
           ,300.00)
GO

INSERT INTO [dbo].[Limites]
           ([Id_Moneda]
           ,[Id_Usuario]
           ,[anio]
           ,[Mes]
           ,[Monto])
     VALUES
           (1
           ,2
           ,2021
           ,8
           ,200.00)
GO

INSERT INTO [dbo].[Limites]
           ([Id_Moneda]
           ,[Id_Usuario]
           ,[anio]
           ,[Mes]
           ,[Monto])
     VALUES
           (2
           ,2
           ,2021
           ,8
           ,300.00)
GO

INSERT INTO [dbo].[Limites]
           ([Id_Moneda]
           ,[Id_Usuario]
           ,[anio]
           ,[Mes]
           ,[Monto])
     VALUES
           (1
           ,1
           ,2021
           ,9
           ,200.00)
GO

INSERT INTO [dbo].[Limites]
           ([Id_Moneda]
           ,[Id_Usuario]
           ,[anio]
           ,[Mes]
           ,[Monto])
     VALUES
           (2
           ,1
           ,2021
           ,9
           ,300.00)
GO

INSERT INTO [dbo].[Limites]
           ([Id_Moneda]
           ,[Id_Usuario]
           ,[anio]
           ,[Mes]
           ,[Monto])
     VALUES
           (1
           ,2
           ,2021
           ,9
           ,200.00)
GO

INSERT INTO [dbo].[Limites]
           ([Id_Moneda]
           ,[Id_Usuario]
           ,[anio]
           ,[Mes]
           ,[Monto])
     VALUES
           (2
           ,2
           ,2021
           ,9
           ,300.00)
GO