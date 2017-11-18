create proc spGetPaperIdForQuestions
(
	@SessionId int,
	@SemesterId int,
	@PaperName nvarchar(100)

)
As
Begin
Select Id from PaperDetails where SessionId=@SessionId and SemesterId = @SemesterId and PaperName =@PaperName
End

create proc spInsertQuestion
(
	@PaperId int,
	@Text nvarchar(max),
	@QuestionPicture varbinary(max),
	@Resources nvarchar(max),
	@Marks int

)
As
Begin
INSERT INTO [dbo].[PaperQuestion]
           ([PaperId]
           ,[Text]
           ,[QuestionPicture]
           ,[Resources]
           ,[Marks])
     VALUES
           (@PaperId
           ,@Text
           ,@QuestionPicture 
           ,@Resources
           ,@Marks)
End

create proc spGetQuestionId
(
	@PaperId int,
	@Text nvarchar(max)
	
)
As
Begin
Select Id from PaperQuestion where PaperId=@PaperId and Text=@Text
End


create proc spInsertOption
(
	@QuestionId int,
	@Text nvarchar(max),
	@Correct nchar(1)
)
as 
Begin
Insert into QuestionMCQ values(@QuestionId,@Text,@Correct) 
End

create proc spInsertSections
(
	@PaperId int,
	@SectionId int

)
As
Begin
insert into PaperSectionRelation values(@PaperId,@SectionId)
End










create proc spGetSectionsByPaperId
(
	@Id int

)
As
Begin

Select SectionName from Section where Id in (select SectionId from [dbo].[PaperSectionRelation] where PaperId = @Id)

End

spGetSectionsByPaperId 8

create proc spGetPaperNames
(
	@SessionId int,
	@SemesterId int,
	@CourseId int,
	@TeacherId int
)
As
Begin
Select * from [dbo].[PaperDetails] where SessionId = @SessionId and SemesterId = @SemesterId and
CourseId = @CourseId and TeacherId = @TeacherId
End

create proc spDisplayPaper
(
	@PaperId int
)
As
Begin
Select * from [dbo].[PaperDetails] where Id = @PaperId
End

create proc spUpdatePaper
(
	@PaperId int,
	@Active nchar(3),
	@Password nchar(10)
)
As
Begin
Update [dbo].[PaperDetails]
set Password = @Password , Active = @Active where Id = @PaperId
End

GetPaperQuestions 8


create proc GetPaperQuestions
(
	@PaperId int

)
As
Begin
Select * from [dbo].[PaperQuestion] where PaperId = @PaperId 

End

create proc GetQuestionOptions
(

	@QuestionId int
)
As
Begin
Select * from [dbo].[QuestionMCQ] where QuestionId = @QuestionId
End
GetQuestionOptions 28

GetPaperQuestions 8