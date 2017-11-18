CREATE TABLE [dbo].[Section](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SessionId] [int] NOT NULL,
	[SemesterId] [int] NOT NULL,
	[SectionName] [nchar](2) NOT NULL,
	[Shift] [nchar](7) NOT NULL,
	primary key(Id),
	Foreign key (SessionId) references [Session](Id),
	Foreign key (SemesterId) references [Semester](Id)
	)

	
create proc [dbo].[spGetSemesters]
(
	@SessionId int
)
As
Begin
select Semester.SemesterNo from Semester
where Semester.SessionId = @SessionId

End

create proc spPkViolationSection
As
Begin
select * from [Section]
End

create proc [dbo].[spInsertSection]
(
	
	@SessionId int,
	@SemesterId int,
	@SectionName nchar(2) ,
	@Shift nchar(7) 
)
As 
Begin
insert into [Section] (SessionId ,SemesterId,SectionName,Shift) 
values (@SessionId ,@SemesterId,@SectionName,@Shift)
End
create proc	[dbo].[spListSection]
As
Begin
select  Session.SessionId ,Semester.SemesterNo,Section.Id, Section.SectionName,  Section.Shift
from Section
inner join [Session]
on Section.SessionId = [Session].Id
inner join Semester
on Section.SemesterId = Semester.Id
End	

create proc [dbo].spDeleteSection
(
	@Id int
)
As
Begin
Delete from [Section] where Id = @Id
End

create proc [dbo].[spGetSemesterId]
(
	@SemesterNo nchar(1)
)
As
Begin

Select Semester.Id from Semester
where Semester.SemesterNo = @SemesterNo

End

create proc [dbo].[spUpdateSection]
(
	@Id int,
	@SessionId int,
	@SemesterId int,
	@SectionName nchar(2),
	@Shift nchar(7)



)
As
Begin
update Section
set SessionId = @SessionId , SemesterId = @SemesterId, 
SectionName =@SectionName , Shift = @Shift
where Id = @Id
End