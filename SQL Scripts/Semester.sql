CREATE TABLE [dbo].[Semester](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SessionId] [int] NOT NULL,
	[SemesterNo] [nchar](1) NOT NULL,
	[SemesterStartDay] [nchar](2) NOT NULL,
	[SemesterStartMonth] [nchar](15) NOT NULL,
	[SemesterStartYear] [nchar](4) NOT NULL,
	[SemesterEndDay] [nchar](2) NOT NULL,
	[SemesterEndMonth] [nvarchar](15) NOT NULL,
	[SemesterEndYear] [nchar](4) NOT NULL,
	[Active] [nchar](3) NOT NULL,

	 FOREIGN KEY (SessionId) REFERENCES [Session](Id)
	)


	create proc [dbo].[spInsertSemester]
(
	
	@SessionId int,
	@SemesterNo nchar(1),
	@SemesterStartDay nchar(2),
	@SemesterStartMonth nchar(15),
	@SemesterStartYear nchar(4),
	@SemesterEndDay nchar(2),
	@SemesterEndMonth nvarchar(15),
	@SemesterEndYear nchar(4),
	@Active nvarchar(3)
)
As 
Begin
insert into [Semester] (SessionId ,SemesterNo,SemesterStartDay,SemesterStartMonth,SemesterStartYear,
SemesterEndDay,SemesterEndMonth,SemesterEndYear,Active) 
values (@SessionId ,@SemesterNo,@SemesterStartDay,@SemesterStartMonth,@SemesterStartYear,
@SemesterEndDay,@SemesterEndMonth,@SemesterEndYear,@Active)
End

create proc [dbo].[spCheckPkViolation]
As
Begin
select * from Semester
End

create proc spGetSemestersForActive
(
	@SessionId int
)
As
Begin
select * from Semester where SessionId = @SessionId
End


create proc [dbo].[spListSemester]
As 
Begin
select [Session].SessionId,Semester.id,SemesterNo,SemesterStartDay,SemesterStartMonth,SemesterStartYear,SemesterEndDay,SemesterEndMonth,SemesterEndYear,Active from Semester
inner join [Session] 
on Semester.SessionId = Session.id
End

create proc [dbo].[spUpdateSemester](
	
	@Id int,
	@SessionId int,
	@SemesterNo nchar(1),
	@SemesterStartDay nchar(2),
	@SemesterStartMonth nchar(15),
	@SemesterStartYear nchar(4),
	@SemesterEndDay nchar(2),
	@SemesterEndMonth nvarchar(15),
	@SemesterEndYear nchar(4),
	@Active nchar(3)
)
As
Begin
update Semester
set  SessionId = @SessionId, SemesterNo = @SemesterNo , SemesterStartDay = @SemesterStartDay , SemesterStartMonth = @SemesterStartMonth ,
	 SemesterStartYear = @SemesterStartYear ,  SemesterEndDay = @SemesterEndDay , SemesterEndMonth = @SemesterEndMonth,
	 SemesterEndYear = @SemesterEndYear , Active = @Active

	 where Id = @Id
End

create proc [dbo].[spDeleteSemester]
(
	@Id int
)
As
Begin
delete from Semester where Id =@Id 
End