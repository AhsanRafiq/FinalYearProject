CREATE TABLE [dbo].[Student](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SessionId] [int] NOT NULL,
	[SemesterId] [int] NOT NULL,
	[SectionId] [int] NOT NULL,
	[RollNumber] [nchar](13) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[FatherName] [nvarchar](50) NOT NULL,
	[Password] [nchar](8) NOT NULL,
	primary key (Id),
	foreign key([SessionId]) References [Session](Id),
	foreign key(SemesterId) References [Semester](Id),
	foreign key([SectionId]) References [Section](Id),
	)


	create proc spGetSection
	(
		@SessionId int,
		@SemesterId int
	)
	As
	Begin
	Select * from Section where SessionId = @SessionId and SemesterId = @SemesterId
	End

create proc [dbo].[spInsertStudent]
(
	@SessionId int ,
	@SemesterId int,
	@SectionId int ,
	@RollNumber nchar(13),
	@Name nvarchar(50),
	@FatherName nvarchar(50),
	@Password nchar(8) 
)
As
Begin

insert into Student values(@SessionId,@SemesterId,@SectionId,@RollNumber,@Name ,@FatherName,@Password)

End


create proc spPkViolationStudent
As
Begin
Select * from Student
End


create  proc [dbo].[spListStudent]
(
	@SessionId int,
	@SemesterId int,
	@SectionId int

)
As
Begin
Select * from Student where SessionId = @SessionId and SemesterId = @SemesterId and SectionId = @SectionId
End

create proc [dbo].[spUpdateStudent]
(
	@Id int,
	@SessionId int ,
	@SemesterId int,
	@SectionId int ,
	@RollNumber nchar(13),
	@Name nvarchar(50),
	@FatherName nvarchar(50),
	@Password nchar(8) 
)
As
Begin
update Student
set  SessionId=@SessionId ,SemesterId=@SemesterId, RollNumber = @RollNumber 
, Name = @Name, FatherName =@FatherName,[Password]=@Password
where Id = @Id
End

create proc spDeleteStudent
(
	@Id int
)
As
Begin
delete from Student where Id= @Id
End

