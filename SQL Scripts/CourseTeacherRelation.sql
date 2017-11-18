Create table CourseTeacherRelation
(
	Id int identity(1,1) ,
	SessionId int,
	SemesterId int,
	SectionId int,
	CourseId int,
	TeacherId int
	primary key (Id),
	foreign key (SessionId) references Session(Id),
	foreign key (SemesterId) references Semester(Id),
	foreign key (SectionId) references Section(Id),
	foreign key (CourseId) references Course(Id),
	foreign key (TeacherId) references Teacher(Id)
)

create proc spGetCourse
(

	@SessionId int,
	@SemesterId int

)
As
Begin
Select * from Course where SessionId =@SessionId and SemesterId=@SemesterId
End

create proc spGetTeacher
As
Begin
Select * from Teacher
End

create proc spPkViolationCTR
As
Begin
Select * from CourseTeacherRelation
End

create proc spInsertCTR
(

	@SessionId int,
	@SemesterId int,
	@SectionId int,
	@CourseId int,
	@TeacherId int
)
As
Begin
insert into CourseTeacherRelation  values(@SessionId,@SemesterId,@SectionId,@CourseId,@TeacherId)
End

create proc spListCTR
(
	@SessionId int,
	@SemesterId int,
	@SectionId int

)
As
Begin
select CourseTeacherRelation.Id,Teacher.EmployeeId,Teacher.FirstName,Course.CourseName  from CourseTeacherRelation
inner join Course on Course.Id =  CourseTeacherRelation.CourseId 
inner join Teacher on Teacher.Id =  CourseTeacherRelation.TeacherId
where CourseTeacherRelation.SessionId =@SessionId and CourseTeacherRelation.SemesterId=@SemesterId and CourseTeacherRelation.SectionId=@SectionId
End


create proc spUpdateCTR
(
	@Id int  ,
	@SessionId int,
	@SemesterId int,
	@SectionId int,
	@CourseId int,
	@TeacherId int

)
As
Begin
Update CourseTeacherRelation
Set SessionId = @SessionId, SemesterId=@SemesterId,SectionId=@SectionId,
CourseId=@CourseId,TeacherId=@TeacherId
where Id = @Id
End


create proc spDeleteCtr
(

	@Id int
)
As
Begin
delete from CourseTeacherRelation where @Id = id
End