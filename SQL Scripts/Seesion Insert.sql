USE [FYPWebApp]
GO

INSERT INTO [dbo].[Session]
           ([SessionId]
           ,[Program]
           ,[SessionStartDay]
           ,[SessionStartMonth]
           ,[SessionStartYear]
           ,[SessionEndDay]
           ,[SessionEndMonth]
           ,[SessionEndYear])
     VALUES
           ('2014-2018'
           ,'BSCS'
           ,'04'
           ,'November'
           ,'2016'
           ,'04'
           ,'November'
           ,'2020')
GO

select * from Session


