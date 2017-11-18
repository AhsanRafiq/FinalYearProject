CREATE TABLE [dbo].[Teacher](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [nchar](8) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Designation] [nvarchar](100) NOT NULL,
	[Education] [nvarchar](50) NOT NULL,
	[ContactNumber] [nvarchar](12) NOT NULL,
	[PostalAddress] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](15) NOT NULL,
	primary key (Id)
	)

create proc [dbo].[spInsertTeacher]
(
	@EmployeeId nchar(8) ,
	@FirstName nvarchar(50) ,
	@LastName nvarchar(50) ,
	@Designation nvarchar(100) ,
	@Education nvarchar(50) ,
	@ContactNumber nvarchar(12) ,
	@PostalAddress nvarchar(100) ,
	@Email nvarchar(50) ,
	@Password nvarchar(15) 
)
As
Begin
insert into Teacher values(@EmployeeId,@FirstName,@LastName,@Designation ,@Education,
@ContactNumber,@PostalAddress,@Email,@Password)
End
GO







	create proc spPkViolationTeacher
	As
	Begin
	Select * from Teacher
	End

	create proc spListTeacher
	As
	Begin
	Select * from Teacher
	End

	create proc [dbo].[spUpdateTeacher]
(
	@Id int,
	@EmployeeId nchar(8) ,
	@FirstName nvarchar(50) ,
	@LastName nvarchar(50) ,
	@Designation nvarchar(100) ,
	@Education nvarchar(50) ,
	@ContactNumber nvarchar(12) ,
	@PostalAddress nvarchar(100) ,
	@Email nvarchar(50) ,
	@Password nvarchar(15) 
)
As
Begin
update Teacher
set  EmployeeId=@EmployeeId ,FirstName=@FirstName, LastName = @LastName 
, Designation = @Designation, Education =@Education, ContactNumber = @ContactNumber, PostalAddress =@PostalAddress, Email = @Email, Password =@Password
where Id = @Id
End

create proc spDeleteTeacher
(

	@Id int
)
As
Begin
delete from Teacher where Id = @Id
End