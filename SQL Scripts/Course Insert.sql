USE [FYPWebApp]
GO

INSERT INTO [dbo].[Course]
           ([SessionId]
           ,[SemesterId]
           ,[CourseCode]
           ,[CourseName]
           ,[CreditHour])
     VALUES
           (3
           ,1
           ,'CS-110'
           ,'ITU'
           ,3)
GO

Select * from Course


