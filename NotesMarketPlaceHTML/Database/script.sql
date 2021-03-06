USE [master]
GO
/****** Object:  Database [NotesMarketPlace]    Script Date: 10-04-2021 12:58:10 ******/
CREATE DATABASE [NotesMarketPlace]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NotesMarketPlace', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\NotesMarketPlace.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NotesMarketPlace_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\NotesMarketPlace_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [NotesMarketPlace] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NotesMarketPlace].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NotesMarketPlace] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET ARITHABORT OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NotesMarketPlace] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NotesMarketPlace] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET  ENABLE_BROKER 
GO
ALTER DATABASE [NotesMarketPlace] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NotesMarketPlace] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET RECOVERY FULL 
GO
ALTER DATABASE [NotesMarketPlace] SET  MULTI_USER 
GO
ALTER DATABASE [NotesMarketPlace] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NotesMarketPlace] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NotesMarketPlace] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NotesMarketPlace] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NotesMarketPlace] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'NotesMarketPlace', N'ON'
GO
ALTER DATABASE [NotesMarketPlace] SET QUERY_STORE = OFF
GO
USE [NotesMarketPlace]
GO
/****** Object:  Table [dbo].[AddCategory]    Script Date: 10-04-2021 12:58:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](100) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK__AddCateg__3214EC2702E39C57] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AddCountry]    Script Date: 10-04-2021 12:58:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddCountry](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [varchar](100) NOT NULL,
	[CountryCode] [varchar](10) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK__AddCount__3214EC275983B733] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AddType]    Script Date: 10-04-2021 12:58:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](100) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK__AddType__3214EC27D233073C] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Downloads]    Script Date: 10-04-2021 12:58:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Downloads](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[Seller] [int] NOT NULL,
	[Downloader] [int] NOT NULL,
	[IsSellerHasAllowedDownload] [bit] NOT NULL,
	[AttachmentPath] [varchar](max) NULL,
	[IsAttachmentDownloaded] [bit] NOT NULL,
	[AttachmentDownloadedDate] [datetime] NULL,
	[IsPaid] [bit] NOT NULL,
	[PurchasedPrice] [decimal](18, 0) NULL,
	[NoteTitle] [varchar](100) NOT NULL,
	[Category] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK__Download__3214EC279644EA20] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ManageSystemConfiguration]    Script Date: 10-04-2021 12:58:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ManageSystemConfiguration](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SupportEmail] [varchar](100) NOT NULL,
	[SupportContactNumber] [varchar](15) NOT NULL,
	[EmailAddress_es] [varchar](100) NOT NULL,
	[FacebookURL] [varchar](100) NULL,
	[TwitterURL] [varchar](100) NULL,
	[LinkedInURL] [varchar](100) NULL,
	[DefaultNoteDisplayImage] [varchar](max) NOT NULL,
	[DefaultProfilePicture] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK__ManageSy__3214EC27824F852E] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteDetails]    Script Date: 10-04-2021 12:58:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UID] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CategoryID] [int] NOT NULL,
	[TypeID] [int] NULL,
	[CountryID] [int] NULL,
	[NoteTitle] [varchar](100) NOT NULL,
	[DisplayPicture] [varchar](max) NULL,
	[NumberOfPages] [varchar](20) NULL,
	[Description] [varchar](max) NOT NULL,
	[University] [varchar](200) NULL,
	[InstitutionName] [varchar](200) NULL,
	[Course] [varchar](100) NULL,
	[CourseCode] [varchar](100) NULL,
	[Professor] [varchar](100) NULL,
	[IsPaid] [bit] NOT NULL,
	[SellPrice] [decimal](18, 0) NOT NULL,
	[NotesPreview] [varchar](max) NULL,
	[ActionedBy] [int] NULL,
	[Remark] [varchar](200) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[PublishedDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK__NoteDeta__3214EC27CB9DD414] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteReview]    Script Date: 10-04-2021 12:58:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteReview](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[AgainstDownloadID] [int] NOT NULL,
	[ReviewedByID] [int] NOT NULL,
	[Rate] [decimal](18, 0) NOT NULL,
	[Comment] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK__NoteRevi__3214EC27CBBC38FA] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReferenceData]    Script Date: 10-04-2021 12:58:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReferenceData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](100) NOT NULL,
	[Datavalue] [varchar](100) NOT NULL,
	[RefCategory] [varchar](100) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_ReferenceData] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SellerNotesAttachments]    Script Date: 10-04-2021 12:58:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SellerNotesAttachments](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[FileName] [varchar](100) NOT NULL,
	[FilePath] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK__SellerNo__3214EC277112E60F] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpamReports]    Script Date: 10-04-2021 12:58:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpamReports](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ReportedBy] [int] NOT NULL,
	[NoteID] [int] NOT NULL,
	[AgainstDownloadID] [int] NOT NULL,
	[Remark] [varchar](200) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK__SpamRepo__3214EC27FB01DCA7] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 10-04-2021 12:58:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UID] [int] NOT NULL,
	[SecondaryEmail] [varchar](100) NULL,
	[DateOfBirth] [datetime] NULL,
	[Gender] [varchar](50) NULL,
	[PhoneNumber_CountryCode] [varchar](4) NULL,
	[PhoneNumber] [varchar](20) NULL,
	[ProfilePicture] [varchar](max) NULL,
	[AddressLine_1] [varchar](100) NULL,
	[AddressLine_2] [varchar](100) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[ZipCode] [varchar](50) NULL,
	[Country] [varchar](50) NULL,
	[University] [varchar](100) NULL,
	[College] [varchar](100) NULL,
	[SubmittedDate] [datetime] NULL,
	[SubmittedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK__UserProf__3214EC27479034A8] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 10-04-2021 12:58:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK__UserRole__3214EC279FD164B9] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10-04-2021 12:58:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[EmailID] [varchar](100) NOT NULL,
	[PhoneNumber] [varchar](20) NULL,
	[PhoneNumber_CountryCode] [varchar](4) NULL,
	[Password] [varchar](50) NOT NULL,
	[IsEmailVerified] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK__Users__3214EC27BEF880B5] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AddCategory] ON 

INSERT [dbo].[AddCategory] ([ID], [CategoryName], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'Information Technology', N'Information Technology', CAST(N'2021-03-29T19:39:01.420' AS DateTime), 2, NULL, NULL, 1)
INSERT [dbo].[AddCategory] ([ID], [CategoryName], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'Computer Science', N'Computer Science', CAST(N'2021-03-29T19:40:11.300' AS DateTime), 2, NULL, NULL, 1)
INSERT [dbo].[AddCategory] ([ID], [CategoryName], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'History', N'History', CAST(N'2021-03-29T19:41:05.583' AS DateTime), 2, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[AddCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[AddCountry] ON 

INSERT [dbo].[AddCountry] ([ID], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'India', N'+91', CAST(N'2021-03-29T19:28:56.617' AS DateTime), 2, CAST(N'2021-03-29T19:32:54.977' AS DateTime), 2, 1)
INSERT [dbo].[AddCountry] ([ID], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'United Kingdom', N'+55', CAST(N'2021-03-29T19:29:39.287' AS DateTime), 2, NULL, NULL, 1)
INSERT [dbo].[AddCountry] ([ID], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'United States Of America', N'+1', CAST(N'2021-03-29T19:30:28.110' AS DateTime), 2, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[AddCountry] OFF
GO
SET IDENTITY_INSERT [dbo].[AddType] ON 

INSERT [dbo].[AddType] ([ID], [Type], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'University Notes', N'University Notes', CAST(N'2021-03-29T17:25:23.967' AS DateTime), 2, NULL, NULL, 1)
INSERT [dbo].[AddType] ([ID], [Type], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'Story Books', N'Story Book', CAST(N'2021-03-29T20:21:43.847' AS DateTime), 2, CAST(N'2021-03-29T20:27:15.997' AS DateTime), 2, 1)
INSERT [dbo].[AddType] ([ID], [Type], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'Handwritten Notes', N'Handwritten Notes', CAST(N'2021-03-29T20:22:37.183' AS DateTime), 2, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[AddType] OFF
GO
SET IDENTITY_INSERT [dbo].[Downloads] ON 

INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [Category], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (36, 45, 6, 8, 1, N'/Members/6/45/Attachment/Attachment1_31-03-21.pdf', 1, CAST(N'2021-04-01T15:20:31.753' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), N'Machine Learning', N'Information Technology', CAST(N'2021-04-01T15:20:31.757' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [Category], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (37, 46, 6, 8, 1, N'/Members/6/46/Attachment/Attachment1_31-03-21.pdf', 1, CAST(N'2021-04-01T15:20:31.753' AS DateTime), 1, CAST(20 AS Decimal(18, 0)), N'Artificial Intelligence', N'Computer Science', CAST(N'2021-04-01T15:47:03.997' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [Category], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (38, 46, 6, 8, 1, N'/Members/6/46/Attachment/Attachment1_31-03-21.pdf', 1, CAST(N'2021-04-02T09:42:05.323' AS DateTime), 1, CAST(20 AS Decimal(18, 0)), N'Artificial Intelligence', N'Computer Science', CAST(N'2021-04-02T09:38:30.600' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [Category], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (39, 47, 7, 8, 1, N'/Members/7/47/Attachment/Attachment1_01-04-21.pdf', 1, CAST(N'2021-04-02T09:42:24.640' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), N'Data Science', N'Information Technology', CAST(N'2021-04-02T09:42:24.647' AS DateTime), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Downloads] OFF
GO
SET IDENTITY_INSERT [dbo].[ManageSystemConfiguration] ON 

INSERT [dbo].[ManageSystemConfiguration] ([ID], [SupportEmail], [SupportContactNumber], [EmailAddress_es], [FacebookURL], [TwitterURL], [LinkedInURL], [DefaultNoteDisplayImage], [DefaultProfilePicture], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'bhumitsheth02@gmail.com', N'demo', N'bopupaxax-0114@yopmail.com', N'demo', N'demo', N'demo1', N'/DefaultImg//NP_31-03-21.jpg', N'/DefaultImg//DP_31-03-21.png', CAST(N'2021-03-30T17:27:41.077' AS DateTime), 3, CAST(N'2021-03-31T10:16:36.830' AS DateTime), 3, 1)
SET IDENTITY_INSERT [dbo].[ManageSystemConfiguration] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteDetails] ON 

INSERT [dbo].[NoteDetails] ([ID], [UID], [Status], [CategoryID], [TypeID], [CountryID], [NoteTitle], [DisplayPicture], [NumberOfPages], [Description], [University], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotesPreview], [ActionedBy], [Remark], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [PublishedDate], [IsActive]) VALUES (45, 6, 4, 1, 1, 1, N'Machine Learning', NULL, N'123', N'The book is related to Machine Learning', NULL, N'Government Engineering College Bhavnagar', N'Information Technology', N'12', N'K.Swami', 0, CAST(0 AS Decimal(18, 0)), NULL, 2, N'Inappropriate', CAST(N'2021-03-31T12:50:21.493' AS DateTime), 6, CAST(N'2021-04-03T11:52:42.927' AS DateTime), 6, CAST(N'2021-04-03T12:32:16.217' AS DateTime), 1)
INSERT [dbo].[NoteDetails] ([ID], [UID], [Status], [CategoryID], [TypeID], [CountryID], [NoteTitle], [DisplayPicture], [NumberOfPages], [Description], [University], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotesPreview], [ActionedBy], [Remark], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [PublishedDate], [IsActive]) VALUES (46, 6, 4, 2, 1, 2, N'Artificial Intelligence', NULL, N'152', N'The book is related to Machine Learning', NULL, N'University of London', N'Computer', N'85', N'Kerry Louis', 1, CAST(20 AS Decimal(18, 0)), N'/Members/6/46/Preview_31-03-21.pdf', 3, N'Inappropriate', CAST(N'2021-03-31T12:53:59.607' AS DateTime), 6, CAST(N'2021-04-02T19:18:52.437' AS DateTime), NULL, CAST(N'2021-04-03T12:33:38.073' AS DateTime), 1)
INSERT [dbo].[NoteDetails] ([ID], [UID], [Status], [CategoryID], [TypeID], [CountryID], [NoteTitle], [DisplayPicture], [NumberOfPages], [Description], [University], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotesPreview], [ActionedBy], [Remark], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [PublishedDate], [IsActive]) VALUES (47, 7, 2, 1, 2, 3, N'Data Science', NULL, N'152', N'The book is related to Data Science.', NULL, N'Texas University', N'Computer', N'1236', N'James Martin', 0, CAST(0 AS Decimal(18, 0)), NULL, NULL, N'Inappropriate', CAST(N'2021-04-01T10:06:11.963' AS DateTime), 7, CAST(N'2021-04-02T19:17:32.420' AS DateTime), 7, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UID], [Status], [CategoryID], [TypeID], [CountryID], [NoteTitle], [DisplayPicture], [NumberOfPages], [Description], [University], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotesPreview], [ActionedBy], [Remark], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [PublishedDate], [IsActive]) VALUES (48, 6, 4, 1, 3, 3, N'Computer Networks & System', NULL, N'450', N'The book is related to Computer Networks', NULL, N'Texas University', N'Computer', N'7', N'Bratt Anderson', 0, CAST(0 AS Decimal(18, 0)), NULL, 2, NULL, CAST(N'2021-04-07T12:11:09.163' AS DateTime), 6, CAST(N'2021-04-07T12:18:16.193' AS DateTime), 6, CAST(N'2021-04-03T12:33:38.073' AS DateTime), 1)
INSERT [dbo].[NoteDetails] ([ID], [UID], [Status], [CategoryID], [TypeID], [CountryID], [NoteTitle], [DisplayPicture], [NumberOfPages], [Description], [University], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotesPreview], [ActionedBy], [Remark], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [PublishedDate], [IsActive]) VALUES (49, 6, 1, 1, 1, 1, N'Operating System', NULL, N'123', N'The book is related to Machine Learning', NULL, N'Government Engineering College Bhavnagar', N'Information Technology', N'12', N'K.Swami', 0, CAST(0 AS Decimal(18, 0)), NULL, NULL, NULL, CAST(N'2021-04-08T16:21:34.813' AS DateTime), 6, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[NoteDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteReview] ON 

INSERT [dbo].[NoteReview] ([ID], [NoteID], [AgainstDownloadID], [ReviewedByID], [Rate], [Comment], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (9, 46, 37, 8, CAST(4 AS Decimal(18, 0)), N'Excellent
', CAST(N'2021-04-03T19:34:03.273' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[NoteReview] ([ID], [NoteID], [AgainstDownloadID], [ReviewedByID], [Rate], [Comment], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (10, 46, 37, 8, CAST(5 AS Decimal(18, 0)), N'Excellent', CAST(N'2021-04-03T19:34:22.203' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[NoteReview] ([ID], [NoteID], [AgainstDownloadID], [ReviewedByID], [Rate], [Comment], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (11, 47, 39, 8, CAST(5 AS Decimal(18, 0)), N'Excellent', CAST(N'2021-04-03T19:34:38.507' AS DateTime), NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[NoteReview] OFF
GO
SET IDENTITY_INSERT [dbo].[ReferenceData] ON 

INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'Draft', N'Draft', N'NotesStatus', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'Submitted For Review', N'Submitted For Review', N'NotesStatus', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'In Review', N'In Review', N'NotesStatus', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, N'Published', N'Published', N'NotesStatus', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, N'Rejected', N'Rejected', N'NotesStatus', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, N'Removed', N'Removed', N'NotesStatus', NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[ReferenceData] OFF
GO
SET IDENTITY_INSERT [dbo].[SellerNotesAttachments] ON 

INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (59, 45, N'Attachment1_31-03-21.pdf;', N'/Members/6/45/Attachment/Attachment1_31-03-21.pdf', CAST(N'2021-03-31T12:50:21.547' AS DateTime), 6, CAST(N'2021-03-31T12:51:06.430' AS DateTime), NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (60, 46, N'Attachment1_31-03-21.pdf;', N'/Members/6/46/Attachment/Attachment1_31-03-21.pdf', CAST(N'2021-03-31T12:53:59.633' AS DateTime), 6, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (61, 47, N'Attachment1_01-04-21.pdf;', N'/Members/7/47/Attachment/Attachment1_01-04-21.pdf', CAST(N'2021-04-01T10:06:12.050' AS DateTime), 7, CAST(N'2021-04-01T10:09:37.430' AS DateTime), NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (62, 48, N'Attachment1_07-04-21.pdf;', N'/Members/6/48/Attachment/Attachment1_07-04-21.pdf', CAST(N'2021-04-07T12:11:14.993' AS DateTime), 6, CAST(N'2021-04-07T12:18:22.013' AS DateTime), NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (63, 49, N'Attachment1_08-04-21.pdf;', N'/Members/6/49/Attachment/Attachment1_08-04-21.pdf', CAST(N'2021-04-08T16:21:34.963' AS DateTime), 6, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[SellerNotesAttachments] OFF
GO
SET IDENTITY_INSERT [dbo].[SpamReports] ON 

INSERT [dbo].[SpamReports] ([ID], [ReportedBy], [NoteID], [AgainstDownloadID], [Remark], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (6, 8, 46, 37, N'Not Good', CAST(N'2021-04-03T19:36:58.673' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[SpamReports] ([ID], [ReportedBy], [NoteID], [AgainstDownloadID], [Remark], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (7, 8, 47, 39, N'Hello', CAST(N'2021-04-03T19:38:38.317' AS DateTime), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[SpamReports] OFF
GO
SET IDENTITY_INSERT [dbo].[UserProfile] ON 

INSERT [dbo].[UserProfile] ([ID], [UID], [SecondaryEmail], [DateOfBirth], [Gender], [PhoneNumber_CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine_1], [AddressLine_2], [City], [State], [ZipCode], [Country], [University], [College], [SubmittedDate], [SubmittedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, 2, N'xyz@gmail.com', NULL, NULL, N'+91', N'9429092499', N'/Members/2/DP_31-03-21.png', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2021-03-31T10:20:18.417' AS DateTime), 2, CAST(N'2021-03-31T11:24:55.507' AS DateTime), 2)
INSERT [dbo].[UserProfile] ([ID], [UID], [SecondaryEmail], [DateOfBirth], [Gender], [PhoneNumber_CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine_1], [AddressLine_2], [City], [State], [ZipCode], [Country], [University], [College], [SubmittedDate], [SubmittedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, 8, NULL, CAST(N'2021-04-14T00:00:00.000' AS DateTime), N'unknown', N'+91', N'9429092499', NULL, N'demo', N'demo', N'demo', N'Gujarat', N'12345', N'United Kingdom', N'demo', N'demo', CAST(N'2021-04-04T10:26:12.057' AS DateTime), 8, NULL, NULL)
INSERT [dbo].[UserProfile] ([ID], [UID], [SecondaryEmail], [DateOfBirth], [Gender], [PhoneNumber_CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine_1], [AddressLine_2], [City], [State], [ZipCode], [Country], [University], [College], [SubmittedDate], [SubmittedBy], [ModifiedDate], [ModifiedBy]) VALUES (3, 6, NULL, CAST(N'2021-03-28T00:00:00.000' AS DateTime), N'male', N'+91', N'9456985236', NULL, N'Shivaji Park', N'Gogha Lane', N'Ahmedabad ', N'Gujarat', N'14569302', N'India', N'GTU', N'Government Engineering College', CAST(N'2021-04-04T11:39:12.507' AS DateTime), 6, NULL, NULL)
INSERT [dbo].[UserProfile] ([ID], [UID], [SecondaryEmail], [DateOfBirth], [Gender], [PhoneNumber_CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine_1], [AddressLine_2], [City], [State], [ZipCode], [Country], [University], [College], [SubmittedDate], [SubmittedBy], [ModifiedDate], [ModifiedBy]) VALUES (4, 7, NULL, CAST(N'2021-01-31T00:00:00.000' AS DateTime), N'male', N'+55', N'5896347896', NULL, N'Square Times', N'Metropolitan Park', N'London', N'London', N'47896', N'United Kingdom', N'University of London', N'London College of Arts', CAST(N'2021-04-04T11:41:14.487' AS DateTime), 7, NULL, NULL)
INSERT [dbo].[UserProfile] ([ID], [UID], [SecondaryEmail], [DateOfBirth], [Gender], [PhoneNumber_CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine_1], [AddressLine_2], [City], [State], [ZipCode], [Country], [University], [College], [SubmittedDate], [SubmittedBy], [ModifiedDate], [ModifiedBy]) VALUES (5, 3, N'xyz@gmail.com', NULL, NULL, N'+91', N'7894561230', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2021-04-07T12:55:48.237' AS DateTime), 3, NULL, NULL)
SET IDENTITY_INSERT [dbo].[UserProfile] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRoles] ON 

INSERT [dbo].[UserRoles] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'superadmin', N'superadmin', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[UserRoles] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'admin', N'admin', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[UserRoles] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'member', N'member', NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[UserRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [EmailID], [PhoneNumber], [PhoneNumber_CountryCode], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, 2, N'bhumitbhai', N'sheth', N'shethbhumit02@gmail.com', N'9427494841', N'+91', N'7ece99e593ff5dd200e2b9233d9ba654', 1, CAST(N'2021-03-28T17:23:29.200' AS DateTime), NULL, CAST(N'2021-03-29T23:15:48.067' AS DateTime), NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [EmailID], [PhoneNumber], [PhoneNumber_CountryCode], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, 1, N'Adam', N'Smith', N'bhumithsheth@gmail.com', NULL, NULL, N'e6e061838856bf47e1de730719fb2609', 1, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [EmailID], [PhoneNumber], [PhoneNumber_CountryCode], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, 3, N'arpit', N'sheth', N'orrappegoz-2005@yopmail.com', NULL, NULL, N'36237ee4e2207807adde67236d2dc7bd', 1, CAST(N'2021-03-31T12:48:07.360' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [EmailID], [PhoneNumber], [PhoneNumber_CountryCode], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (7, 3, N'Samay', N'Sharma', N'symmysaloga-8515@yopmail.com', NULL, NULL, N'36237ee4e2207807adde67236d2dc7bd', 1, CAST(N'2021-04-01T10:04:18.353' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [EmailID], [PhoneNumber], [PhoneNumber_CountryCode], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (8, 3, N'aayush', N'dhankar', N'yfigiti-1540@yopmail.com', NULL, NULL, N'36237ee4e2207807adde67236d2dc7bd', 1, CAST(N'2021-04-01T15:18:07.373' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [EmailID], [PhoneNumber], [PhoneNumber_CountryCode], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (10, 3, N'Ankush', N'Patel', N'udafere-0744@yopmail.com', NULL, NULL, N'36237ee4e2207807adde67236d2dc7bd', 1, CAST(N'2021-04-03T21:58:38.043' AS DateTime), NULL, NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__7ED91AEE4DFF0773]    Script Date: 10-04-2021 12:58:10 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [UQ__Users__7ED91AEE4DFF0773] UNIQUE NONCLUSTERED 
(
	[EmailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AddCategory] ADD  CONSTRAINT [DF_AddCategory_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[AddCountry] ADD  CONSTRAINT [DF_AddCountry_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[AddType] ADD  CONSTRAINT [DF_AddType_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ManageSystemConfiguration] ADD  CONSTRAINT [DF_ManageSystemConfiguration_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[NoteDetails] ADD  CONSTRAINT [DF__NoteDetai__IsAct__07C12930]  DEFAULT ('TRUE') FOR [IsActive]
GO
ALTER TABLE [dbo].[NoteReview] ADD  CONSTRAINT [DF_NoteReview_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ReferenceData] ADD  CONSTRAINT [DF_ReferenceData_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[SellerNotesAttachments] ADD  CONSTRAINT [DF__SellerNot__IsAct__0B91BA14]  DEFAULT ('TRUE') FOR [IsActive]
GO
ALTER TABLE [dbo].[UserRoles] ADD  CONSTRAINT [DF__UserRoles__IsAct__72C60C4A]  DEFAULT ('TRUE') FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__IsEmailVe__778AC167]  DEFAULT ('FALSE') FOR [IsEmailVerified]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__IsActive__787EE5A0]  DEFAULT ('TRUE') FOR [IsActive]
GO
ALTER TABLE [dbo].[AddCategory]  WITH CHECK ADD  CONSTRAINT [FK_AddCategory_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[AddCategory] CHECK CONSTRAINT [FK_AddCategory_Users]
GO
ALTER TABLE [dbo].[AddCountry]  WITH CHECK ADD  CONSTRAINT [FK_AddCountry_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[AddCountry] CHECK CONSTRAINT [FK_AddCountry_Users]
GO
ALTER TABLE [dbo].[AddType]  WITH CHECK ADD  CONSTRAINT [FK_AddType_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[AddType] CHECK CONSTRAINT [FK_AddType_Users]
GO
ALTER TABLE [dbo].[Downloads]  WITH CHECK ADD  CONSTRAINT [FK__Downloads__NoteI__0E6E26BF] FOREIGN KEY([NoteID])
REFERENCES [dbo].[NoteDetails] ([ID])
GO
ALTER TABLE [dbo].[Downloads] CHECK CONSTRAINT [FK__Downloads__NoteI__0E6E26BF]
GO
ALTER TABLE [dbo].[Downloads]  WITH CHECK ADD  CONSTRAINT [FK_Downloads_Users] FOREIGN KEY([Seller])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Downloads] CHECK CONSTRAINT [FK_Downloads_Users]
GO
ALTER TABLE [dbo].[Downloads]  WITH CHECK ADD  CONSTRAINT [FK_Downloads_Users1] FOREIGN KEY([Downloader])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Downloads] CHECK CONSTRAINT [FK_Downloads_Users1]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK__NoteDetai__Categ__04E4BC85] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[AddCategory] ([ID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK__NoteDetai__Categ__04E4BC85]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK__NoteDetai__Count__06CD04F7] FOREIGN KEY([CountryID])
REFERENCES [dbo].[AddCountry] ([ID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK__NoteDetai__Count__06CD04F7]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK__NoteDetai__TypeI__05D8E0BE] FOREIGN KEY([TypeID])
REFERENCES [dbo].[AddType] ([ID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK__NoteDetai__TypeI__05D8E0BE]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK__NoteDetails__UID__03F0984C] FOREIGN KEY([UID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK__NoteDetails__UID__03F0984C]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_ReferenceData] FOREIGN KEY([Status])
REFERENCES [dbo].[ReferenceData] ([ID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_ReferenceData]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_Users] FOREIGN KEY([ActionedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_Users]
GO
ALTER TABLE [dbo].[NoteReview]  WITH CHECK ADD  CONSTRAINT [FK__NoteRevie__Again__1AD3FDA4] FOREIGN KEY([AgainstDownloadID])
REFERENCES [dbo].[Downloads] ([ID])
GO
ALTER TABLE [dbo].[NoteReview] CHECK CONSTRAINT [FK__NoteRevie__Again__1AD3FDA4]
GO
ALTER TABLE [dbo].[NoteReview]  WITH CHECK ADD  CONSTRAINT [FK__NoteRevie__NoteI__19DFD96B] FOREIGN KEY([NoteID])
REFERENCES [dbo].[NoteDetails] ([ID])
GO
ALTER TABLE [dbo].[NoteReview] CHECK CONSTRAINT [FK__NoteRevie__NoteI__19DFD96B]
GO
ALTER TABLE [dbo].[NoteReview]  WITH CHECK ADD  CONSTRAINT [FK__NoteRevie__Revie__1BC821DD] FOREIGN KEY([ReviewedByID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[NoteReview] CHECK CONSTRAINT [FK__NoteRevie__Revie__1BC821DD]
GO
ALTER TABLE [dbo].[SellerNotesAttachments]  WITH CHECK ADD  CONSTRAINT [FK__SellerNot__NoteI__0A9D95DB] FOREIGN KEY([NoteID])
REFERENCES [dbo].[NoteDetails] ([ID])
GO
ALTER TABLE [dbo].[SellerNotesAttachments] CHECK CONSTRAINT [FK__SellerNot__NoteI__0A9D95DB]
GO
ALTER TABLE [dbo].[SpamReports]  WITH CHECK ADD  CONSTRAINT [FK__SpamRepor__Again__151B244E] FOREIGN KEY([AgainstDownloadID])
REFERENCES [dbo].[Downloads] ([ID])
GO
ALTER TABLE [dbo].[SpamReports] CHECK CONSTRAINT [FK__SpamRepor__Again__151B244E]
GO
ALTER TABLE [dbo].[SpamReports]  WITH CHECK ADD  CONSTRAINT [FK__SpamRepor__NoteI__14270015] FOREIGN KEY([NoteID])
REFERENCES [dbo].[NoteDetails] ([ID])
GO
ALTER TABLE [dbo].[SpamReports] CHECK CONSTRAINT [FK__SpamRepor__NoteI__14270015]
GO
ALTER TABLE [dbo].[SpamReports]  WITH CHECK ADD  CONSTRAINT [FK__SpamRepor__Repor__1332DBDC] FOREIGN KEY([ReportedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SpamReports] CHECK CONSTRAINT [FK__SpamRepor__Repor__1332DBDC]
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK__UserProfile__UID__7B5B524B] FOREIGN KEY([UID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK__UserProfile__UID__7B5B524B]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK__Users__RoleID__76969D2E] FOREIGN KEY([RoleID])
REFERENCES [dbo].[UserRoles] ([ID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK__Users__RoleID__76969D2E]
GO
USE [master]
GO
ALTER DATABASE [NotesMarketPlace] SET  READ_WRITE 
GO
