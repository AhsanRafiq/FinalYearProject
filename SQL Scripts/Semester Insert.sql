USE [FYPWebApp]
GO

INSERT INTO [dbo].[Semester]
           ([SessionId]
           ,[SemesterNo]
           ,[SemesterStartDay]
           ,[SemesterStartMonth]
           ,[SemesterStartYear]
           ,[SemesterEndDay]
           ,[SemesterEndMonth]
           ,[SemesterEndYear]
           ,[Active])
     VALUES
           (3
           ,'1'
         ,'04'
           ,'November'
           ,'2016'
           ,'04'
           ,'November'
           ,'2020'
          ,'No')
GO
Select * from Semester


