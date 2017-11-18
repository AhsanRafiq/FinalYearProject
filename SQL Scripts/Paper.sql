
	create proc spInsertPaperDetails
	(

		@SessionId int ,
		@SemesterId int ,
		@Section nvarchar(20) ,
		@CourseId int ,
		@PaperName nvarchar(100) ,
		@TeacherName nvarchar(100) ,
		@NoOfQuestions nchar(3) ,
		@TimeAllowed nchar(3) ,
		@Active nchar(3) ,
		@Password nchar(10) ,
		@TotalMarks int
	)
	As
	Begin
	insert into PaperDetails values(@SessionId,@SemesterId,@Section,@CourseId,@PaperName,@TeacherName,
	@NoOfQuestions,@TimeAllowed,@Active,@Password,@TotalMarks)
	End




	--Paper Question Part
	CREATE TABLE dbo.PaperQuestion
	(
		Id int IDENTITY(1,1) NOT NULL,
		PaperId int NOT NULL,
		Text nvarchar(max) NOT NULL,
		QuestionPicture varbinary(max) NULL,
		Resources nvarchar(max) NULL,
		Marks int NULL,
		primary key([Id]),
		foreign key(PaperId) references PaperDetails(Id)
	)

	create proc spInsertPaperQuestion
	(
	
		@PaperId int,
		@Text nvarchar(max),
		@QuestionPicture varbinary(max),
		@Resources nvarchar(max),
		@Marks int 
	
	)
	As
	Begin
	insert into PaperQuestion values(@PaperId,@Text,@QuestionPicture,@Resources,@Marks)
	End


	create table QuestionMCQ
	(
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[QuestionId] [int] NOT NULL,
		[Text] [nvarchar](200) NULL,
		[Correct] [nchar](1) NOT NULL
		primary key([Id] ),
		foreign key([QuestionId]) references PaperQuestion(Id)
	)

	create proc spInsertQuestionMCQ
	(

		@QuestionId int,
		@Text nvarchar(200) ,
		@Correct nchar(1) 
	)
	As
	begin
	insert into QuestionMCQ values (@QuestionId,@Text,@Correct)
	End


	create proc spGetTeacherBySessionSemesterCourse
	(
		@SessionId int,
		@SemesterId int,
		@CourseId int
	)
	As
	Begin
	Select * from Teacher where Id IN(select TeacherId from CourseTeacherRelation where SessionId =@SessionId and SemesterId=@SemesterId 
	and CourseId = @CourseId )
	End

create proc spGetCourseName
(
	@Id int

)
As
Begin
Select * from Course where Id = @Id
End

create proc spGetSessionName
(
	@Id int

)
As
Begin
Select * from Session where Id = @Id
End


create proc spGetSemesterName
(
	@Id int
)
As
Begin
Select * from Semester where Id = @Id
End