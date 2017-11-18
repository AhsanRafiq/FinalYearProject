CREATE TABLE [dbo].[Session](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SessionId] [nchar](9) NOT NULL,
	[Program] [nchar](4) NOT NULL,
	[SessionStartDay] [nchar](2) NOT NULL,
	[SessionStartMonth] [nchar](15) NOT NULL,
	[SessionStartYear] [nchar](4) NOT NULL,
	[SessionEndDay] [nchar](2) NOT NULL,
	[SessionEndMonth] [nvarchar](15) NOT NULL,
	[SessionEndYear] [nchar](4) NOT NULL
	primary key(Id)
	)


create procedure [dbo].[spInsertSession]
(
@SessionId nchar(9),
@Program nchar(4),
@SessionStartDay nchar(2),
@SessionStartMonth nchar(15),
@SessionStartYear nchar(4),
@SessionEndDay nchar(2),
@SessionEndMonth nvarchar(15),
@SessionEndYear nchar(4)

)
As
Begin

Insert into Session(SessionID,Program,SessionStartDay,SessionStartMonth,SessionStartYear,SessionEndDay,SessionEndMonth,SessionEndYear)
values (@SessionID ,@Program,@SessionStartDay,@SessionStartMonth,@SessionStartYear,@SessionEndDay,@SessionEndMonth,@SessionEndYear)
End


Create Procedure [dbo].[spListSession]
as     
Begin    
Select * from [Session];    
End

create proc [dbo].[spUpdateSession]
(
	@Id int,
	@SessionId nchar(9),
	@Program nchar(4),
	@SessionStartDay nchar(2),
	@SessionStartMonth nchar(15),
	@SessionStartYear nchar(4),
	@SessionEndDay nchar(2),
	@SessionEndMonth nvarchar(15),
	@SessionEndYear nchar(4)
)
As
Begin
	
	update [Session]
	set SessionId =@SessionId , Program =@Program , SessionStartDay = @SessionStartDay, SessionStartMonth =@SessionStartMonth,
	SessionStartYear = @SessionStartYear, SessionEndDay = @SessionEndDay, SessionEndMonth =@SessionEndMonth , SessionEndYear =@SessionEndYear
	where [Session].Id = @Id
End

create proc spPkViolationSession
As
Begin
select * from Session
End