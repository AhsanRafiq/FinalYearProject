USE [master]
GO
/****** Object:  Database [FYPWebApp]    Script Date: 10/13/2017 6:44:56 PM ******/
CREATE DATABASE [FYPWebApp] ON  PRIMARY 
( NAME = N'FYPWebApp', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\FYPWebApp.mdf' , SIZE = 2304KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FYPWebApp_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\FYPWebApp_log.LDF' , SIZE = 576KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [FYPWebApp] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FYPWebApp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FYPWebApp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FYPWebApp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FYPWebApp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FYPWebApp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FYPWebApp] SET ARITHABORT OFF 
GO
ALTER DATABASE [FYPWebApp] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [FYPWebApp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FYPWebApp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FYPWebApp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FYPWebApp] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FYPWebApp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FYPWebApp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FYPWebApp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FYPWebApp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FYPWebApp] SET  ENABLE_BROKER 
GO
ALTER DATABASE [FYPWebApp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FYPWebApp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FYPWebApp] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FYPWebApp] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FYPWebApp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FYPWebApp] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FYPWebApp] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FYPWebApp] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FYPWebApp] SET  MULTI_USER 
GO
ALTER DATABASE [FYPWebApp] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FYPWebApp] SET DB_CHAINING OFF 
GO
USE [FYPWebApp]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Admin](
	[UserName] [varchar](9) NOT NULL,
	[Password] [nchar](8) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Department] [varchar](100) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[ContactNumber] [nchar](12) NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Course]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SessionId] [int] NOT NULL,
	[SemesterId] [int] NOT NULL,
	[CourseCode] [nvarchar](20) NOT NULL,
	[CourseName] [nvarchar](100) NOT NULL,
	[CreditHour] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CourseTeacherRelation]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseTeacherRelation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SessionId] [int] NULL,
	[SemesterId] [int] NULL,
	[SectionId] [int] NULL,
	[CourseId] [int] NULL,
	[TeacherId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Material]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Material](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SessionId] [int] NOT NULL,
	[SemesterId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[MaterialName] [varchar](100) NOT NULL,
	[Description] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Material] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MaterialSectionRelation]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialSectionRelation](
	[Id] [int] NOT NULL,
	[MaterialId] [int] NOT NULL,
	[SectionId] [int] NOT NULL,
 CONSTRAINT [PK_MaterialSectionRelation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MidTerm]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MidTerm](
	[Id] [int] NOT NULL,
	[SessionId] [int] NOT NULL,
	[SemesterId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[StartDay] [nchar](2) NOT NULL,
	[StartMonth] [nchar](15) NOT NULL,
	[StartYear] [nchar](4) NOT NULL,
	[Time] [time](7) NOT NULL,
 CONSTRAINT [PK_MidTerm] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MidtermSectionRelation]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MidtermSectionRelation](
	[Id] [int] NOT NULL,
	[MidtermId] [int] NOT NULL,
	[SectionId] [int] NOT NULL,
 CONSTRAINT [PK_MidtermSection] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Notification]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Notification](
	[Id] [int] NOT NULL,
	[SessionId] [int] NOT NULL,
	[SemesterId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[NotificationName] [varchar](50) NOT NULL,
	[Descrition] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NotificationSectionRelation]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationSectionRelation](
	[Id] [int] NOT NULL,
	[NotificationId] [int] NOT NULL,
	[SectionId] [int] NOT NULL,
 CONSTRAINT [PK_NotificationSectionRelation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PaperDetails]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaperDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SessionId] [int] NOT NULL,
	[SemesterId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[PaperName] [nvarchar](100) NOT NULL,
	[TeacherId] [int] NOT NULL,
	[NoOfQuestions] [nchar](3) NOT NULL,
	[TimeAllowed] [nchar](3) NOT NULL,
	[Active] [nchar](3) NOT NULL,
	[Password] [nchar](10) NOT NULL,
	[TotalMarks] [int] NOT NULL,
 CONSTRAINT [PK__PaperDet__3214EC077861F40F] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PaperQuestion]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PaperQuestion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PaperId] [int] NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[QuestionPicture] [varbinary](max) NULL,
	[Resources] [nvarchar](max) NULL,
	[Marks] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PaperResult]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PaperResult](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SessionId] [int] NOT NULL,
	[SemesterId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[SectionId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[MarksObtained] [int] NOT NULL,
	[TotalMarks] [int] NOT NULL,
	[ResultSheet] [varchar](max) NOT NULL,
 CONSTRAINT [PK_PaperResult] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PaperSectionRelation]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaperSectionRelation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PaperId] [int] NOT NULL,
	[SectionId] [int] NOT NULL,
 CONSTRAINT [PK_PaperSectionRelation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QuestionMCQ]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionMCQ](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[Correct] [nchar](1) NOT NULL,
 CONSTRAINT [PK__Question__3214EC07D9E9969E] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Section]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Section](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SessionId] [int] NOT NULL,
	[SemesterId] [int] NOT NULL,
	[SectionName] [nchar](2) NOT NULL,
	[Shift] [nchar](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Semester]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
 CONSTRAINT [PK_Semester] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Session]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SessionId] [nchar](9) NOT NULL,
	[Program] [nchar](4) NOT NULL,
	[SessionStartDay] [nchar](2) NOT NULL,
	[SessionStartMonth] [nchar](15) NOT NULL,
	[SessionStartYear] [nchar](4) NOT NULL,
	[SessionEndDay] [nchar](2) NOT NULL,
	[SessionEndMonth] [nvarchar](15) NOT NULL,
	[SessionEndYear] [nchar](4) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Student]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SessionId] [int] NOT NULL,
	[SemesterId] [int] NOT NULL,
	[SectionId] [int] NOT NULL,
	[RollNumber] [nchar](13) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[FatherName] [nvarchar](50) NOT NULL,
	[Password] [nchar](8) NOT NULL,
	[Photo] [varbinary](max) NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
	[Password] [nvarchar](8) NOT NULL,
	[Photo] [varbinary](max) NULL,
 CONSTRAINT [PK__Teacher__3214EC07337012D7] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Course] ON 

INSERT [dbo].[Course] ([Id], [SessionId], [SemesterId], [CourseCode], [CourseName], [CreditHour]) VALUES (10, 9, 36, N'CS-101', N'ITC', 3)
INSERT [dbo].[Course] ([Id], [SessionId], [SemesterId], [CourseCode], [CourseName], [CreditHour]) VALUES (11, 9, 37, N'CS-102', N'DLD', 3)
INSERT [dbo].[Course] ([Id], [SessionId], [SemesterId], [CourseCode], [CourseName], [CreditHour]) VALUES (12, 10, 50, N'CS-103', N'Programming 1', 3)
INSERT [dbo].[Course] ([Id], [SessionId], [SemesterId], [CourseCode], [CourseName], [CreditHour]) VALUES (13, 10, 51, N'CS-104', N'Programming 2', 3)
INSERT [dbo].[Course] ([Id], [SessionId], [SemesterId], [CourseCode], [CourseName], [CreditHour]) VALUES (14, 11, 52, N'CS-105', N'DB 1', 3)
INSERT [dbo].[Course] ([Id], [SessionId], [SemesterId], [CourseCode], [CourseName], [CreditHour]) VALUES (15, 11, 53, N'CS-106', N'DB 2', 3)
INSERT [dbo].[Course] ([Id], [SessionId], [SemesterId], [CourseCode], [CourseName], [CreditHour]) VALUES (16, 12, 66, N'CS-107', N'LPC', 3)
INSERT [dbo].[Course] ([Id], [SessionId], [SemesterId], [CourseCode], [CourseName], [CreditHour]) VALUES (17, 12, 67, N'CS-108', N'Compiler', 3)
SET IDENTITY_INSERT [dbo].[Course] OFF
SET IDENTITY_INSERT [dbo].[CourseTeacherRelation] ON 

INSERT [dbo].[CourseTeacherRelation] ([Id], [SessionId], [SemesterId], [SectionId], [CourseId], [TeacherId]) VALUES (2, 9, 36, 14, 10, 2)
INSERT [dbo].[CourseTeacherRelation] ([Id], [SessionId], [SemesterId], [SectionId], [CourseId], [TeacherId]) VALUES (3, 9, 37, 15, 11, 4)
INSERT [dbo].[CourseTeacherRelation] ([Id], [SessionId], [SemesterId], [SectionId], [CourseId], [TeacherId]) VALUES (4, 10, 50, 18, 12, 6)
INSERT [dbo].[CourseTeacherRelation] ([Id], [SessionId], [SemesterId], [SectionId], [CourseId], [TeacherId]) VALUES (5, 10, 51, 20, 13, 2)
SET IDENTITY_INSERT [dbo].[CourseTeacherRelation] OFF
SET IDENTITY_INSERT [dbo].[PaperDetails] ON 

INSERT [dbo].[PaperDetails] ([Id], [SessionId], [SemesterId], [CourseId], [PaperName], [TeacherId], [NoOfQuestions], [TimeAllowed], [Active], [Password], [TotalMarks]) VALUES (8, 9, 36, 10, N'Comp', 2, N'5  ', N'10 ', N'Yes', N'789654123 ', 10)
INSERT [dbo].[PaperDetails] ([Id], [SessionId], [SemesterId], [CourseId], [PaperName], [TeacherId], [NoOfQuestions], [TimeAllowed], [Active], [Password], [TotalMarks]) VALUES (13, 9, 37, 11, N'DLD1', 4, N'2  ', N'10 ', N'Yes', N'1234567   ', 10)
SET IDENTITY_INSERT [dbo].[PaperDetails] OFF
SET IDENTITY_INSERT [dbo].[PaperQuestion] ON 

INSERT [dbo].[PaperQuestion] ([Id], [PaperId], [Text], [QuestionPicture], [Resources], [Marks]) VALUES (24, 8, N'Best way to specify whitespace in a String.Split operation', 0x, N'https://stackoverflow.com/questions/6111298/best-way-to-specify-whitespace-in-a-string-split-operation', 2)
INSERT [dbo].[PaperQuestion] ([Id], [PaperId], [Text], [QuestionPicture], [Resources], [Marks]) VALUES (25, 8, N'C# Regex.Split: Removing empty results', 0x, N'https://stackoverflow.com/questions/4912365/c-sharp-regex-split-removing-empty-results', 2)
INSERT [dbo].[PaperQuestion] ([Id], [PaperId], [Text], [QuestionPicture], [Resources], [Marks]) VALUES (26, 8, N'How to: Parse Strings Using String.Split (C# Programming Guide)', 0x, N'https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/strings/how-to-parse-strings-using-string-split', 2)
INSERT [dbo].[PaperQuestion] ([Id], [PaperId], [Text], [QuestionPicture], [Resources], [Marks]) VALUES (27, 8, N'I think my question was a little misunderstood. It was never about how I can do it. It was only about how can I do it by changing the Regex in the above code.', 0x, N'https://stackoverflow.com/questions/4912365/c-sharp-regex-split-removing-empty-results', 2)
INSERT [dbo].[PaperQuestion] ([Id], [PaperId], [Text], [QuestionPicture], [Resources], [Marks]) VALUES (28, 8, N'Best way to specify whitespace in a String.Split operation', 0x, N'https://stackoverflow.com/questions/6111298/best-way-to-specify-whitespace-in-a-string-split-operation', 2)
INSERT [dbo].[PaperQuestion] ([Id], [PaperId], [Text], [QuestionPicture], [Resources], [Marks]) VALUES (32, 13, N'Best way to specify whitespace in a String.Split operation', 0x, N'https://stackoverflow.com/questions/6111298/best-way-to-specify-whitespace-in-a-string-split-operation', 2)
INSERT [dbo].[PaperQuestion] ([Id], [PaperId], [Text], [QuestionPicture], [Resources], [Marks]) VALUES (33, 13, N'C# Regex.Split: Removing empty results', 0x, N'https://stackoverflow.com/questions/4912365/c-sharp-regex-split-removing-empty-results', 2)
INSERT [dbo].[PaperQuestion] ([Id], [PaperId], [Text], [QuestionPicture], [Resources], [Marks]) VALUES (34, 13, N'How to: Parse Strings Using String.Split (C# Programming Guide)', 0x, N'https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/strings/how-to-parse-strings-using-string-split', 2)
SET IDENTITY_INSERT [dbo].[PaperQuestion] OFF
SET IDENTITY_INSERT [dbo].[PaperSectionRelation] ON 

INSERT [dbo].[PaperSectionRelation] ([Id], [PaperId], [SectionId]) VALUES (9, 8, 14)
INSERT [dbo].[PaperSectionRelation] ([Id], [PaperId], [SectionId]) VALUES (10, 8, 15)
INSERT [dbo].[PaperSectionRelation] ([Id], [PaperId], [SectionId]) VALUES (15, 13, 16)
INSERT [dbo].[PaperSectionRelation] ([Id], [PaperId], [SectionId]) VALUES (16, 13, 17)
SET IDENTITY_INSERT [dbo].[PaperSectionRelation] OFF
SET IDENTITY_INSERT [dbo].[QuestionMCQ] ON 

INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (97, 24, N'1', N'1')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (98, 24, N'2', N'0')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (99, 24, N'3', N'0')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (100, 24, N'4', N'0')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (101, 25, N'1', N'0')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (102, 25, N'2', N'1')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (103, 25, N'3', N'0')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (104, 25, N'4', N'0')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (105, 26, N'1', N'0')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (106, 26, N'2', N'0')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (107, 26, N'3', N'1')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (108, 26, N'4', N'0')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (109, 27, N'1', N'0')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (110, 27, N'2', N'0')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (111, 27, N'3', N'1')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (112, 27, N'4', N'0')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (113, 28, N'1', N'0')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (114, 28, N'2', N'0')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (115, 28, N'3', N'0')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (116, 28, N'4', N'1')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (117, 32, N'1', N'1')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (118, 32, N'2', N'0')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (119, 32, N'3', N'0')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (120, 32, N'4', N'0')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (121, 33, N'1', N'0')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (122, 33, N'2', N'1')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (123, 33, N'3', N'0')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (124, 33, N'4', N'0')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (125, 34, N'1', N'0')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (126, 34, N'2', N'0')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (127, 34, N'3', N'1')
INSERT [dbo].[QuestionMCQ] ([Id], [QuestionId], [Text], [Correct]) VALUES (128, 34, N'4', N'0')
SET IDENTITY_INSERT [dbo].[QuestionMCQ] OFF
SET IDENTITY_INSERT [dbo].[Section] ON 

INSERT [dbo].[Section] ([Id], [SessionId], [SemesterId], [SectionName], [Shift]) VALUES (14, 9, 36, N'A ', N'Morning')
INSERT [dbo].[Section] ([Id], [SessionId], [SemesterId], [SectionName], [Shift]) VALUES (15, 9, 36, N'B ', N'Morning')
INSERT [dbo].[Section] ([Id], [SessionId], [SemesterId], [SectionName], [Shift]) VALUES (16, 9, 37, N'C ', N'Morning')
INSERT [dbo].[Section] ([Id], [SessionId], [SemesterId], [SectionName], [Shift]) VALUES (17, 9, 37, N'D ', N'Morning')
INSERT [dbo].[Section] ([Id], [SessionId], [SemesterId], [SectionName], [Shift]) VALUES (18, 10, 50, N'A ', N'Morning')
INSERT [dbo].[Section] ([Id], [SessionId], [SemesterId], [SectionName], [Shift]) VALUES (19, 10, 50, N'B ', N'Morning')
INSERT [dbo].[Section] ([Id], [SessionId], [SemesterId], [SectionName], [Shift]) VALUES (20, 10, 51, N'C ', N'Morning')
INSERT [dbo].[Section] ([Id], [SessionId], [SemesterId], [SectionName], [Shift]) VALUES (21, 10, 51, N'D ', N'Morning')
INSERT [dbo].[Section] ([Id], [SessionId], [SemesterId], [SectionName], [Shift]) VALUES (22, 11, 52, N'A ', N'Morning')
INSERT [dbo].[Section] ([Id], [SessionId], [SemesterId], [SectionName], [Shift]) VALUES (23, 11, 52, N'B ', N'Morning')
INSERT [dbo].[Section] ([Id], [SessionId], [SemesterId], [SectionName], [Shift]) VALUES (24, 11, 53, N'C ', N'Morning')
INSERT [dbo].[Section] ([Id], [SessionId], [SemesterId], [SectionName], [Shift]) VALUES (25, 11, 53, N'D ', N'Morning')
INSERT [dbo].[Section] ([Id], [SessionId], [SemesterId], [SectionName], [Shift]) VALUES (26, 12, 67, N'D ', N'Morning')
INSERT [dbo].[Section] ([Id], [SessionId], [SemesterId], [SectionName], [Shift]) VALUES (27, 12, 67, N'C ', N'Morning')
INSERT [dbo].[Section] ([Id], [SessionId], [SemesterId], [SectionName], [Shift]) VALUES (28, 12, 66, N'C ', N'Morning')
INSERT [dbo].[Section] ([Id], [SessionId], [SemesterId], [SectionName], [Shift]) VALUES (29, 12, 66, N'A ', N'Morning')
SET IDENTITY_INSERT [dbo].[Section] OFF
SET IDENTITY_INSERT [dbo].[Semester] ON 

INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (36, 9, N'1', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (37, 9, N'2', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (38, 9, N'3', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (39, 9, N'4', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (40, 9, N'5', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (41, 9, N'6', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (42, 9, N'7', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (43, 9, N'8', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (44, 10, N'8', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (45, 10, N'7', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (46, 10, N'6', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (47, 10, N'5', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (48, 10, N'4', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (49, 10, N'3', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (50, 10, N'2', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (51, 10, N'1', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (52, 11, N'1', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (53, 11, N'2', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (54, 11, N'3', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (55, 11, N'4', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (56, 11, N'5', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (57, 11, N'6', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (58, 11, N'7', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (59, 11, N'8', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (60, 12, N'8', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (61, 12, N'7', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (62, 12, N'6', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (63, 12, N'5', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (64, 12, N'4', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (65, 12, N'3', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (66, 12, N'2', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
INSERT [dbo].[Semester] ([Id], [SessionId], [SemesterNo], [SemesterStartDay], [SemesterStartMonth], [SemesterStartYear], [SemesterEndDay], [SemesterEndMonth], [SemesterEndYear], [Active]) VALUES (67, 12, N'1', N'04', N'November       ', N'2016', N'04', N'November', N'2020', N'No ')
SET IDENTITY_INSERT [dbo].[Semester] OFF
SET IDENTITY_INSERT [dbo].[Session] ON 

INSERT [dbo].[Session] ([Id], [SessionId], [Program], [SessionStartDay], [SessionStartMonth], [SessionStartYear], [SessionEndDay], [SessionEndMonth], [SessionEndYear]) VALUES (9, N'2013-2017', N'BSCS', N'04', N'November       ', N'2013', N'04', N'November', N'2017')
INSERT [dbo].[Session] ([Id], [SessionId], [Program], [SessionStartDay], [SessionStartMonth], [SessionStartYear], [SessionEndDay], [SessionEndMonth], [SessionEndYear]) VALUES (10, N'2014-2018', N'BSCS', N'04', N'November       ', N'2014', N'04', N'November', N'2018')
INSERT [dbo].[Session] ([Id], [SessionId], [Program], [SessionStartDay], [SessionStartMonth], [SessionStartYear], [SessionEndDay], [SessionEndMonth], [SessionEndYear]) VALUES (11, N'2015-2019', N'BSCS', N'04', N'November       ', N'2015', N'04', N'November', N'2019')
INSERT [dbo].[Session] ([Id], [SessionId], [Program], [SessionStartDay], [SessionStartMonth], [SessionStartYear], [SessionEndDay], [SessionEndMonth], [SessionEndYear]) VALUES (12, N'2016-2020', N'BSCS', N'04', N'November       ', N'2016', N'04', N'November', N'2020')
SET IDENTITY_INSERT [dbo].[Session] OFF
SET IDENTITY_INSERT [dbo].[Teacher] ON 

INSERT [dbo].[Teacher] ([Id], [EmployeeId], [FirstName], [LastName], [Designation], [Education], [ContactNumber], [PostalAddress], [Email], [Password], [Photo]) VALUES (2, N'EMP101  ', N'Ahsan', N'Rafiq', N'Professor', N'BSCS(HONS) CS', N'03360067336', N'H45', N'ahsanawan@hotmail.com', N'12345678', NULL)
INSERT [dbo].[Teacher] ([Id], [EmployeeId], [FirstName], [LastName], [Designation], [Education], [ContactNumber], [PostalAddress], [Email], [Password], [Photo]) VALUES (4, N'EMP102  ', N'Atif', N'Ishaq', N'Assistent Professor', N'MSCS', N'090078601', N'H 78', N'atifishaq@gmail.com', N'123456', NULL)
INSERT [dbo].[Teacher] ([Id], [EmployeeId], [FirstName], [LastName], [Designation], [Education], [ContactNumber], [PostalAddress], [Email], [Password], [Photo]) VALUES (6, N'EMP103  ', N'Moez', N'Rehman', N'Professor', N'MS EE', N'090078601', N'H 98', N'moezrehnman@gmail.com', N'987456', NULL)
SET IDENTITY_INSERT [dbo].[Teacher] OFF
ALTER TABLE [dbo].[Course]  WITH CHECK ADD FOREIGN KEY([SemesterId])
REFERENCES [dbo].[Semester] ([Id])
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([Id])
GO
ALTER TABLE [dbo].[CourseTeacherRelation]  WITH CHECK ADD FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
GO
ALTER TABLE [dbo].[CourseTeacherRelation]  WITH CHECK ADD FOREIGN KEY([SectionId])
REFERENCES [dbo].[Section] ([Id])
GO
ALTER TABLE [dbo].[CourseTeacherRelation]  WITH CHECK ADD FOREIGN KEY([SemesterId])
REFERENCES [dbo].[Semester] ([Id])
GO
ALTER TABLE [dbo].[CourseTeacherRelation]  WITH CHECK ADD FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([Id])
GO
ALTER TABLE [dbo].[CourseTeacherRelation]  WITH CHECK ADD  CONSTRAINT [FK__CourseTea__Teach__619B8048] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Teacher] ([Id])
GO
ALTER TABLE [dbo].[CourseTeacherRelation] CHECK CONSTRAINT [FK__CourseTea__Teach__619B8048]
GO
ALTER TABLE [dbo].[Material]  WITH CHECK ADD  CONSTRAINT [FK_Material_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
GO
ALTER TABLE [dbo].[Material] CHECK CONSTRAINT [FK_Material_Course]
GO
ALTER TABLE [dbo].[Material]  WITH CHECK ADD  CONSTRAINT [FK_Material_Semester] FOREIGN KEY([SemesterId])
REFERENCES [dbo].[Course] ([Id])
GO
ALTER TABLE [dbo].[Material] CHECK CONSTRAINT [FK_Material_Semester]
GO
ALTER TABLE [dbo].[Material]  WITH CHECK ADD  CONSTRAINT [FK_Material_Session] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([Id])
GO
ALTER TABLE [dbo].[Material] CHECK CONSTRAINT [FK_Material_Session]
GO
ALTER TABLE [dbo].[MaterialSectionRelation]  WITH CHECK ADD  CONSTRAINT [FK_MaterialSectionRelation_Material] FOREIGN KEY([MaterialId])
REFERENCES [dbo].[Material] ([Id])
GO
ALTER TABLE [dbo].[MaterialSectionRelation] CHECK CONSTRAINT [FK_MaterialSectionRelation_Material]
GO
ALTER TABLE [dbo].[MaterialSectionRelation]  WITH CHECK ADD  CONSTRAINT [FK_MaterialSectionRelation_Section] FOREIGN KEY([SectionId])
REFERENCES [dbo].[Section] ([Id])
GO
ALTER TABLE [dbo].[MaterialSectionRelation] CHECK CONSTRAINT [FK_MaterialSectionRelation_Section]
GO
ALTER TABLE [dbo].[MidTerm]  WITH CHECK ADD  CONSTRAINT [FK_MidTerm_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
GO
ALTER TABLE [dbo].[MidTerm] CHECK CONSTRAINT [FK_MidTerm_Course]
GO
ALTER TABLE [dbo].[MidTerm]  WITH CHECK ADD  CONSTRAINT [FK_MidTerm_Semester] FOREIGN KEY([SemesterId])
REFERENCES [dbo].[Semester] ([Id])
GO
ALTER TABLE [dbo].[MidTerm] CHECK CONSTRAINT [FK_MidTerm_Semester]
GO
ALTER TABLE [dbo].[MidTerm]  WITH CHECK ADD  CONSTRAINT [FK_MidTerm_Session] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([Id])
GO
ALTER TABLE [dbo].[MidTerm] CHECK CONSTRAINT [FK_MidTerm_Session]
GO
ALTER TABLE [dbo].[MidtermSectionRelation]  WITH CHECK ADD  CONSTRAINT [FK_MidtermSection_MidTerm] FOREIGN KEY([MidtermId])
REFERENCES [dbo].[MidTerm] ([Id])
GO
ALTER TABLE [dbo].[MidtermSectionRelation] CHECK CONSTRAINT [FK_MidtermSection_MidTerm]
GO
ALTER TABLE [dbo].[MidtermSectionRelation]  WITH CHECK ADD  CONSTRAINT [FK_MidtermSection_Section] FOREIGN KEY([SectionId])
REFERENCES [dbo].[Section] ([Id])
GO
ALTER TABLE [dbo].[MidtermSectionRelation] CHECK CONSTRAINT [FK_MidtermSection_Section]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Course]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Semester] FOREIGN KEY([SemesterId])
REFERENCES [dbo].[Semester] ([Id])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Semester]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Session] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([Id])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Session]
GO
ALTER TABLE [dbo].[NotificationSectionRelation]  WITH CHECK ADD  CONSTRAINT [FK_NotificationSectionRelation_Notification] FOREIGN KEY([NotificationId])
REFERENCES [dbo].[Notification] ([Id])
GO
ALTER TABLE [dbo].[NotificationSectionRelation] CHECK CONSTRAINT [FK_NotificationSectionRelation_Notification]
GO
ALTER TABLE [dbo].[NotificationSectionRelation]  WITH CHECK ADD  CONSTRAINT [FK_NotificationSectionRelation_Section] FOREIGN KEY([SectionId])
REFERENCES [dbo].[Section] ([Id])
GO
ALTER TABLE [dbo].[NotificationSectionRelation] CHECK CONSTRAINT [FK_NotificationSectionRelation_Section]
GO
ALTER TABLE [dbo].[PaperDetails]  WITH CHECK ADD  CONSTRAINT [FK__PaperDeta__Cours__6FE99F9F] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
GO
ALTER TABLE [dbo].[PaperDetails] CHECK CONSTRAINT [FK__PaperDeta__Cours__6FE99F9F]
GO
ALTER TABLE [dbo].[PaperDetails]  WITH CHECK ADD  CONSTRAINT [FK__PaperDeta__Semes__6EF57B66] FOREIGN KEY([SemesterId])
REFERENCES [dbo].[Semester] ([Id])
GO
ALTER TABLE [dbo].[PaperDetails] CHECK CONSTRAINT [FK__PaperDeta__Semes__6EF57B66]
GO
ALTER TABLE [dbo].[PaperDetails]  WITH CHECK ADD  CONSTRAINT [FK__PaperDeta__Sessi__6E01572D] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([Id])
GO
ALTER TABLE [dbo].[PaperDetails] CHECK CONSTRAINT [FK__PaperDeta__Sessi__6E01572D]
GO
ALTER TABLE [dbo].[PaperQuestion]  WITH CHECK ADD  CONSTRAINT [FK__PaperQues__Paper__00200768] FOREIGN KEY([PaperId])
REFERENCES [dbo].[PaperDetails] ([Id])
GO
ALTER TABLE [dbo].[PaperQuestion] CHECK CONSTRAINT [FK__PaperQues__Paper__00200768]
GO
ALTER TABLE [dbo].[PaperResult]  WITH CHECK ADD  CONSTRAINT [FK_PaperResult_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
GO
ALTER TABLE [dbo].[PaperResult] CHECK CONSTRAINT [FK_PaperResult_Course]
GO
ALTER TABLE [dbo].[PaperResult]  WITH CHECK ADD  CONSTRAINT [FK_PaperResult_Section] FOREIGN KEY([SectionId])
REFERENCES [dbo].[Section] ([Id])
GO
ALTER TABLE [dbo].[PaperResult] CHECK CONSTRAINT [FK_PaperResult_Section]
GO
ALTER TABLE [dbo].[PaperResult]  WITH CHECK ADD  CONSTRAINT [FK_PaperResult_Semester] FOREIGN KEY([SemesterId])
REFERENCES [dbo].[Semester] ([Id])
GO
ALTER TABLE [dbo].[PaperResult] CHECK CONSTRAINT [FK_PaperResult_Semester]
GO
ALTER TABLE [dbo].[PaperResult]  WITH CHECK ADD  CONSTRAINT [FK_PaperResult_Session] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([Id])
GO
ALTER TABLE [dbo].[PaperResult] CHECK CONSTRAINT [FK_PaperResult_Session]
GO
ALTER TABLE [dbo].[PaperResult]  WITH CHECK ADD  CONSTRAINT [FK_PaperResult_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[PaperResult] CHECK CONSTRAINT [FK_PaperResult_Student]
GO
ALTER TABLE [dbo].[PaperSectionRelation]  WITH CHECK ADD  CONSTRAINT [FK_PaperSectionRelation_PaperDetails] FOREIGN KEY([PaperId])
REFERENCES [dbo].[PaperDetails] ([Id])
GO
ALTER TABLE [dbo].[PaperSectionRelation] CHECK CONSTRAINT [FK_PaperSectionRelation_PaperDetails]
GO
ALTER TABLE [dbo].[PaperSectionRelation]  WITH CHECK ADD  CONSTRAINT [FK_PaperSectionRelation_Section] FOREIGN KEY([SectionId])
REFERENCES [dbo].[Section] ([Id])
GO
ALTER TABLE [dbo].[PaperSectionRelation] CHECK CONSTRAINT [FK_PaperSectionRelation_Section]
GO
ALTER TABLE [dbo].[QuestionMCQ]  WITH CHECK ADD  CONSTRAINT [FK__QuestionM__Quest__02FC7413] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[PaperQuestion] ([Id])
GO
ALTER TABLE [dbo].[QuestionMCQ] CHECK CONSTRAINT [FK__QuestionM__Quest__02FC7413]
GO
ALTER TABLE [dbo].[Section]  WITH CHECK ADD FOREIGN KEY([SemesterId])
REFERENCES [dbo].[Semester] ([Id])
GO
ALTER TABLE [dbo].[Section]  WITH CHECK ADD FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([Id])
GO
ALTER TABLE [dbo].[Semester]  WITH CHECK ADD FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([Id])
GO
ALTER TABLE [dbo].[Semester]  WITH CHECK ADD  CONSTRAINT [FK_Semester_Session] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([Id])
GO
ALTER TABLE [dbo].[Semester] CHECK CONSTRAINT [FK_Semester_Session]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Section] FOREIGN KEY([SectionId])
REFERENCES [dbo].[Section] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Section]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Semester] FOREIGN KEY([SemesterId])
REFERENCES [dbo].[Semester] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Semester]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Session] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Session] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Session]
GO
/****** Object:  StoredProcedure [dbo].[GetPaperQuestions]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetPaperQuestions]
(
	@PaperId int

)
As
Begin
Select * from [dbo].[PaperQuestion] where PaperId = @PaperId 

End


GO
/****** Object:  StoredProcedure [dbo].[GetQuestionOptions]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetQuestionOptions]
(

	@QuestionId int
)
As
Begin
Select * from [dbo].[QuestionMCQ] where QuestionId = @QuestionId
End


GO
/****** Object:  StoredProcedure [dbo].[spCheckPkViolation]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spCheckPkViolation]
As
Begin
select * from Semester
End



GO
/****** Object:  StoredProcedure [dbo].[spDeleteCourse]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[spDeleteCourse]
(
	@Id int
)
As
Begin
delete from Course where Id = @Id
End



GO
/****** Object:  StoredProcedure [dbo].[spDeleteCtr]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spDeleteCtr]
(

	@Id int
)
As
Begin
delete from CourseTeacherRelation where @Id = id
End



GO
/****** Object:  StoredProcedure [dbo].[spDeleteSection]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spDeleteSection]
(
	@Id int
)
As
Begin
Delete from [Section] where Id = @Id
End



GO
/****** Object:  StoredProcedure [dbo].[spDeleteSemester]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spDeleteSemester]
(
	@Id int
)
As
Begin
delete from Semester where Id =@Id 
End



GO
/****** Object:  StoredProcedure [dbo].[spDeleteSession]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spDeleteSession]
(
	@Id int
)
As
Begin
Delete from [Session]  where Id = @Id
End




GO
/****** Object:  StoredProcedure [dbo].[spDeleteStudent]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spDeleteStudent]
(
	@Id int
)
As
Begin
delete from Student where Id= @Id
End



GO
/****** Object:  StoredProcedure [dbo].[spDeleteTeacher]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spDeleteTeacher]
(

	@Id int
)
As
Begin
delete from Teacher where Id = @Id
End



GO
/****** Object:  StoredProcedure [dbo].[spDisplayPaper]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spDisplayPaper]
(
	@PaperId int
)
As
Begin
Select * from [dbo].[PaperDetails] where Id = @PaperId
End


GO
/****** Object:  StoredProcedure [dbo].[spDisplayPaperForExam]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spDisplayPaperForExam]
(
	@SessionId int,
	@SemesterId int

)
As
Begin
select * from [dbo].[PaperDetails] where SessionId = @SessionId and SemesterId= @SemesterId and  Active ='Yes'
End
GO
/****** Object:  StoredProcedure [dbo].[spGetCourse]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spGetCourse]
(

	@SessionId int,
	@SemesterId int

)
As
Begin
Select * from Course where SessionId =@SessionId and SemesterId=@SemesterId
End



GO
/****** Object:  StoredProcedure [dbo].[spGetCourseName]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[spGetCourseName]
(
	@Id int

)
As
Begin
Select * from Course where Id = @Id
End



GO
/****** Object:  StoredProcedure [dbo].[spGetPaperIdForQuestions]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spGetPaperIdForQuestions]
(
	@SessionId int,
	@SemesterId int,
	@PaperName nvarchar(100)

)
As
Begin
Select Id from PaperDetails where SessionId=@SessionId and SemesterId = @SemesterId and PaperName =@PaperName
End



GO
/****** Object:  StoredProcedure [dbo].[spGetPaperNames]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spGetPaperNames]
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


GO
/****** Object:  StoredProcedure [dbo].[spGetQuestionId]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[spGetQuestionId]
(
	@PaperId int,
	@Text nvarchar(max)
	
)
As
Begin
Select Id from PaperQuestion where PaperId=@PaperId and Text=@Text
End



GO
/****** Object:  StoredProcedure [dbo].[spGetQuestionsById]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spGetQuestionsById]
(
	@QuestionId int

)
As
Begin
Select Text,QuestionPicture,Resources,Marks from [dbo].[PaperQuestion]
where Id = @QuestionId
End

GO
/****** Object:  StoredProcedure [dbo].[spGetSection]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spGetSection]
	(
		@SessionId int,
		@SemesterId int
	)
	As
	Begin
	Select * from Section where SessionId = @SessionId and SemesterId = @SemesterId
	End



GO
/****** Object:  StoredProcedure [dbo].[spGetSectionsByPaperId]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spGetSectionsByPaperId]
(
	@Id int

)
As
Begin

Select SectionName from Section where Id in (select SectionId from [dbo].[PaperSectionRelation] where PaperId = @Id)

End


GO
/****** Object:  StoredProcedure [dbo].[spGetSemesterId]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spGetSemesterId]
(
	@SemesterNo nchar(1)
)
As
Begin

Select Semester.Id from Semester
where Semester.SemesterNo = @SemesterNo

End



GO
/****** Object:  StoredProcedure [dbo].[spGetSemesterName]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spGetSemesterName]
(
	@Id int
)
As
Begin
Select * from Semester where Id = @Id
End



GO
/****** Object:  StoredProcedure [dbo].[spGetSemesters]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spGetSemesters]
(
	@SessionId int
)
As
Begin
select * from Semester
where Semester.SessionId = @SessionId

End




GO
/****** Object:  StoredProcedure [dbo].[spGetSemestersForActive]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spGetSemestersForActive]
(
	@SessionId int
)
As
Begin
select * from Semester where SessionId = @SessionId
End



GO
/****** Object:  StoredProcedure [dbo].[spGetSessionName]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spGetSessionName]
(
	@Id int

)
As
Begin
Select * from Session where Id = @Id
End



GO
/****** Object:  StoredProcedure [dbo].[spGetTeacher]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spGetTeacher]
As
Begin
Select * from Teacher
End



GO
/****** Object:  StoredProcedure [dbo].[spGetTeacherBySessionSemesterCourse]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	create proc [dbo].[spGetTeacherBySessionSemesterCourse]
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



GO
/****** Object:  StoredProcedure [dbo].[spInsertCourse]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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



GO
/****** Object:  StoredProcedure [dbo].[spInsertCTR]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spInsertCTR]
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



GO
/****** Object:  StoredProcedure [dbo].[spInsertOption]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spInsertOption]
(
	@QuestionId int,
	@Text nvarchar(max),
	@Correct nchar(1)
)
as 
Begin
Insert into QuestionMCQ values(@QuestionId,@Text,@Correct) 
End



GO
/****** Object:  StoredProcedure [dbo].[spInsertPaperDetails]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spInsertPaperDetails]
	(

		@SessionId int ,
		@SemesterId int ,
		@CourseId int ,
		@PaperName nvarchar(100) ,
		@TeacherId int ,
		@NoOfQuestions nchar(3) ,
		@TimeAllowed nchar(3) ,
		@Active nchar(3) ,
		@Password nchar(10) ,
		@TotalMarks int
	)
	As
	Begin
	insert into PaperDetails values(@SessionId,@SemesterId,@CourseId,@PaperName,@TeacherId,
	@NoOfQuestions,@TimeAllowed,@Active,@Password,@TotalMarks)
	End


GO
/****** Object:  StoredProcedure [dbo].[spInsertPaperQuestion]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	create proc [dbo].[spInsertPaperQuestion]
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



GO
/****** Object:  StoredProcedure [dbo].[spInsertQuestion]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spInsertQuestion]
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



GO
/****** Object:  StoredProcedure [dbo].[spInsertQuestionMCQ]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	create proc [dbo].[spInsertQuestionMCQ]
	(

		@QuestionId int,
		@Text nvarchar(200) ,
		@Correct nchar(1) 
	)
	As
	begin
	insert into QuestionMCQ values (@QuestionId,@Text,@Correct)
	End



GO
/****** Object:  StoredProcedure [dbo].[spInsertSection]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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



GO
/****** Object:  StoredProcedure [dbo].[spInsertSections]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spInsertSections]
(
	@PaperId int,
	@SectionId int

)
As
Begin
insert into PaperSectionRelation values(@PaperId,@SectionId)
End



GO
/****** Object:  StoredProcedure [dbo].[spInsertSemester]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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



GO
/****** Object:  StoredProcedure [dbo].[spInsertSession]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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



GO
/****** Object:  StoredProcedure [dbo].[spListCourse]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  proc [dbo].[spListCourse]
(
	@SessionId int,
	@SemesterId int
)
As
Begin
Select * from Course where SessionId = @SessionId and SemesterId = @SemesterId
End



GO
/****** Object:  StoredProcedure [dbo].[spListCTR]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spListCTR]
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



GO
/****** Object:  StoredProcedure [dbo].[spListSection]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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



GO
/****** Object:  StoredProcedure [dbo].[spListSemester]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spListSemester]
As 
Begin
select [Session].SessionId,Semester.id,SemesterNo,SemesterStartDay,SemesterStartMonth,SemesterStartYear,SemesterEndDay,SemesterEndMonth,SemesterEndYear,Active from Semester
inner join [Session] 
on Semester.SessionId = Session.id
End



GO
/****** Object:  StoredProcedure [dbo].[spListSession]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[spListSession]
as     
Begin    
Select * from [Session];    
End



GO
/****** Object:  StoredProcedure [dbo].[spListStudent]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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



GO
/****** Object:  StoredProcedure [dbo].[spListTeacher]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	create proc [dbo].[spListTeacher]
	As
	Begin
	Select * from Teacher
	End



GO
/****** Object:  StoredProcedure [dbo].[spMtachPassword]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spMtachPassword]
(
	@PaperId int,
	@Password nchar(10)
)
As
Begin
select CAST(COUNT(*) AS BIT) FROM [dbo].[PaperDetails] WHERE (Id = @PaperId and Password =@Password)
End
GO
/****** Object:  StoredProcedure [dbo].[spPkViolationCourse]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spPkViolationCourse]
As
Begin
Select * from [Course]
End



GO
/****** Object:  StoredProcedure [dbo].[spPkViolationCTR]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spPkViolationCTR]
As
Begin
Select * from CourseTeacherRelation
End



GO
/****** Object:  StoredProcedure [dbo].[spPkViolationSection]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spPkViolationSection]
As
Begin
select * from [Section]
End



GO
/****** Object:  StoredProcedure [dbo].[spPkViolationSession]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spPkViolationSession]
As
Begin
select * from Session
End



GO
/****** Object:  StoredProcedure [dbo].[spPkViolationStudent]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spPkViolationStudent]
As
Begin
Select * from Student
End



GO
/****** Object:  StoredProcedure [dbo].[spPkViolationTeacher]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spPkViolationTeacher]
	As
	Begin
	Select * from Teacher
	End



GO
/****** Object:  StoredProcedure [dbo].[spUpdateCourse]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spUpdateCourse]
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



GO
/****** Object:  StoredProcedure [dbo].[spUpdateCTR]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spUpdateCTR]
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



GO
/****** Object:  StoredProcedure [dbo].[spUpdatePaper]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spUpdatePaper]
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


GO
/****** Object:  StoredProcedure [dbo].[spUpdateSection]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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



GO
/****** Object:  StoredProcedure [dbo].[spUpdateSemester]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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



GO
/****** Object:  StoredProcedure [dbo].[spUpdateSession]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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



GO
/****** Object:  StoredProcedure [dbo].[spUpdateStudent]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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



GO
/****** Object:  StoredProcedure [dbo].[spUpdateTeacher]    Script Date: 10/13/2017 6:44:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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



GO
USE [master]
GO
ALTER DATABASE [FYPWebApp] SET  READ_WRITE 
GO
