CREATE TABLE [dbo].[Course](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SessionId] int not null,
	[SemesterId] int  NOT NULL,
	[CourseCode] [nvarchar](20) NOT NULL,
	[CourseName] [nvarchar](100) NOT NULL,
	[CreditHour] [int] NOT NULL,
	primary key([Id]),
	Foreign key([SessionId]) references [Session](Id),
	Foreign key([SemesterId]) references [Semester](Id),
	)

create proc [dbo].[spInsertCourse]
(
	@SessionId int,
	@SemesterId int,
	@CourseCode nvarchar(20), 
	@CourseName nvarchar(100), 
	@CreditHour int
)
As
Begin
insert into Course values(@SessionId,@SemesterId,@CourseCode,@CourseName ,@CreditHour)
End


create proc spPkViolationCourse
As
Begin
Select * from [Course]
End


delete from [Course]

create  proc spListCourse
(
	@SessionId int,
	@SemesterId int
)
As
Begin
Select * from Course where SessionId = @SessionId and SemesterId = @SemesterId
End

create proc spDeleteCourse
(
	@Id int
)
As
Begin
delete from Course where Id = @Id
End

create proc spUpdateCourse
(
	@Id int,
	@SessionId int,
	@SemesterId int,
	@CourseCode nvarchar(20), 
	@CourseName nvarchar(100), 
	@CreditHour int
)
As
Begin
update Course 
set  SessionId=@SessionId ,SemesterId=@SemesterId, CourseCode = @CourseCode 
, CourseName = @CourseName, CreditHour =@CreditHour
where Id = @Id
End