USE [master]
GO
/****** Object:  Database [NotesMarketPlace]    Script Date: 09-03-2021 21:34:30 ******/
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
/****** Object:  Table [dbo].[AddCategory]    Script Date: 09-03-2021 21:34:30 ******/
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
/****** Object:  Table [dbo].[AddCountry]    Script Date: 09-03-2021 21:34:30 ******/
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
/****** Object:  Table [dbo].[AddType]    Script Date: 09-03-2021 21:34:30 ******/
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
/****** Object:  Table [dbo].[ContactUs]    Script Date: 09-03-2021 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactUs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [varchar](50) NOT NULL,
	[EmailID] [varchar](50) NOT NULL,
	[Subject] [varchar](100) NOT NULL,
	[Comments] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK__ContactU__3214EC273885667E] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Downloads]    Script Date: 09-03-2021 21:34:30 ******/
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
/****** Object:  Table [dbo].[ManageSystemConfiguration]    Script Date: 09-03-2021 21:34:30 ******/
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
	[DefaultNoteDisplayImage] [varbinary](1) NOT NULL,
	[DefaultProfilePicture] [varbinary](1) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK__ManageSy__3214EC27824F852E] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteDetails]    Script Date: 09-03-2021 21:34:30 ******/
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
	[Action] [varchar](50) NULL,
	[Remark] [varchar](200) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK__NoteDeta__3214EC27CB9DD414] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteReview]    Script Date: 09-03-2021 21:34:30 ******/
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
/****** Object:  Table [dbo].[ReferenceData]    Script Date: 09-03-2021 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReferenceData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](100) NOT NULL,
	[DataValue] [varchar](100) NOT NULL,
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
/****** Object:  Table [dbo].[SellerNotesAttachments]    Script Date: 09-03-2021 21:34:30 ******/
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
/****** Object:  Table [dbo].[SpamReports]    Script Date: 09-03-2021 21:34:30 ******/
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
/****** Object:  Table [dbo].[UserProfile]    Script Date: 09-03-2021 21:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UID] [int] NOT NULL,
	[SecondaryEmail] [varchar](100) NULL,
	[DateOfBirth] [datetime] NULL,
	[Gender] [int] NULL,
	[PhoneNumber_CountryCode] [varchar](4) NOT NULL,
	[PhoneNumber] [varchar](20) NOT NULL,
	[ProfilePicture] [varchar](max) NULL,
	[AddressLine_1] [varchar](100) NOT NULL,
	[AddressLine_2] [varchar](100) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[ZipCode] [varchar](50) NOT NULL,
	[Country] [varchar](50) NOT NULL,
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
/****** Object:  Table [dbo].[UserRoles]    Script Date: 09-03-2021 21:34:30 ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 09-03-2021 21:34:30 ******/
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

INSERT [dbo].[AddCategory] ([ID], [CategoryName], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'IT', N'Informaton Technology', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[AddCategory] ([ID], [CategoryName], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'CS', N'Computer Science', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[AddCategory] ([ID], [CategoryName], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'MBA', N'Masters Of Business Administration', NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[AddCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[AddCountry] ON 

INSERT [dbo].[AddCountry] ([ID], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'India', N'+91', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[AddCountry] ([ID], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'UK', N'+44', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[AddCountry] ([ID], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'USA', N'+1', NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[AddCountry] OFF
GO
SET IDENTITY_INSERT [dbo].[AddType] ON 

INSERT [dbo].[AddType] ([ID], [Type], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'Handwritten', N'Handwritten Books', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[AddType] ([ID], [Type], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'StoryBooks', N'Story Books', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[AddType] ([ID], [Type], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'Universitiesnotes', N'University Notes', NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[AddType] OFF
GO
SET IDENTITY_INSERT [dbo].[ContactUs] ON 

INSERT [dbo].[ContactUs] ([ID], [FullName], [EmailID], [Subject], [Comments], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, N'bhumit', N'abc@gmail.com', N'demo', N'this is demo', CAST(N'2021-03-01T23:08:14.453' AS DateTime), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ContactUs] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteDetails] ON 

INSERT [dbo].[NoteDetails] ([ID], [UID], [Status], [CategoryID], [TypeID], [CountryID], [NoteTitle], [DisplayPicture], [NumberOfPages], [Description], [University], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotesPreview], [Action], [Remark], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, 45, 4, 3, 2, 1, N'demo', N'~/Members/45/1/05-03-2021_15-02-12.png', N'123', N'demo', NULL, NULL, N'demo', N'12', N'demo', 1, CAST(12 AS Decimal(18, 0)), N'~/Members/45/1/PRACTICAL4.pdf', NULL, NULL, CAST(N'2021-03-05T15:02:12.343' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UID], [Status], [CategoryID], [TypeID], [CountryID], [NoteTitle], [DisplayPicture], [NumberOfPages], [Description], [University], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotesPreview], [Action], [Remark], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, 45, 4, 2, 1, 1, N'demo', N'~/Members/45/2/05-03-2021_16-25-22.jpg', N'123', N'demo', NULL, NULL, N'demo', N'12', N'demo', 1, CAST(1236 AS Decimal(18, 0)), N'~/Members/45/2/CD(PRACTICAL-10)_170210107056.pdf', NULL, NULL, CAST(N'2021-03-05T16:25:21.347' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UID], [Status], [CategoryID], [TypeID], [CountryID], [NoteTitle], [DisplayPicture], [NumberOfPages], [Description], [University], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotesPreview], [Action], [Remark], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, 45, 4, 2, 1, 1, N'Data Science', N'~/Members/45/3/05-03-2021_17-31-13.png', N'123', N'This is CS', NULL, NULL, N'demo', N'12', N'demo', 1, CAST(120 AS Decimal(18, 0)), N'~/Members/45/3/PRACTICAL1(.pdf', NULL, NULL, CAST(N'2021-03-05T17:31:13.370' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UID], [Status], [CategoryID], [TypeID], [CountryID], [NoteTitle], [DisplayPicture], [NumberOfPages], [Description], [University], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotesPreview], [Action], [Remark], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, 45, 4, 1, 1, 1, N'AI', N'~/Members/45/4/06-03-2021_17-31-35.png', N'123', N'demo', NULL, NULL, N'demo', N'12', N'demo', 1, CAST(145 AS Decimal(18, 0)), N'~/Members/45/4/INS(PRACTICAL-7)_170210107056.pdf', NULL, NULL, CAST(N'2021-03-06T17:31:33.930' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UID], [Status], [CategoryID], [TypeID], [CountryID], [NoteTitle], [DisplayPicture], [NumberOfPages], [Description], [University], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotesPreview], [Action], [Remark], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, 45, 4, 2, 1, 2, N'Machine Learning', N'~/Members/45/5/06-03-2021_17-34-42.jpg', N'123', N'demo', NULL, NULL, N'demo', N'12', N'demo', 0, CAST(0 AS Decimal(18, 0)), NULL, NULL, NULL, CAST(N'2021-03-06T17:34:42.167' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UID], [Status], [CategoryID], [TypeID], [CountryID], [NoteTitle], [DisplayPicture], [NumberOfPages], [Description], [University], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotesPreview], [Action], [Remark], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, 45, 4, 3, 3, 3, N'Management', N'~/Members/45/6/06-03-2021_17-38-06.jpg', N'123', N'demo', NULL, NULL, N'demo', N'12', N'demo', 0, CAST(0 AS Decimal(18, 0)), NULL, NULL, NULL, CAST(N'2021-03-06T17:38:06.393' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UID], [Status], [CategoryID], [TypeID], [CountryID], [NoteTitle], [DisplayPicture], [NumberOfPages], [Description], [University], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotesPreview], [Action], [Remark], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (7, 45, 4, 1, 3, 1, N'Operating System', N'~/Members/45/7/07-03-2021_11-59-03.jpg', N'123', N'demo', NULL, NULL, N'demo', N'12', N'demo', 1, CAST(125 AS Decimal(18, 0)), N'~/Members/45/7/PRACTICAL1(.pdf', NULL, NULL, CAST(N'2021-03-07T11:59:02.190' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UID], [Status], [CategoryID], [TypeID], [CountryID], [NoteTitle], [DisplayPicture], [NumberOfPages], [Description], [University], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotesPreview], [Action], [Remark], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (8, 45, 4, 2, 3, 1, N'Operating System', N'C:/Users/abc/source/repos/NotesMarketPlace/NotesMarketPlace/DefaultImg/customer-3.png', N'123', N'demo', NULL, NULL, N'demo', N'12', N'demo', 1, CAST(125 AS Decimal(18, 0)), N'~/Members/45/8/PRACTICAL1(.pdf', NULL, NULL, CAST(N'2021-03-07T12:07:39.183' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UID], [Status], [CategoryID], [TypeID], [CountryID], [NoteTitle], [DisplayPicture], [NumberOfPages], [Description], [University], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotesPreview], [Action], [Remark], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (9, 45, 4, 1, 3, 3, N'Computer Networks', N'~/Members/45/9/07-03-2021_14-47-04.jpg', N'123', N'demo', NULL, NULL, N'demo', N'12', N'demo', 1, CAST(126 AS Decimal(18, 0)), N'~/Members/45/9/PRACTICAL4.pdf', NULL, NULL, CAST(N'2021-03-07T14:47:03.463' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UID], [Status], [CategoryID], [TypeID], [CountryID], [NoteTitle], [DisplayPicture], [NumberOfPages], [Description], [University], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotesPreview], [Action], [Remark], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (10, 45, 4, 1, 1, 1, N'Data Science', N'~/Members/45/10/07-03-2021_15-16-55.png', N'123', N'demo', NULL, N'demo', N'demo', N'12', N'demo', 1, CAST(159 AS Decimal(18, 0)), N'~/Members/45/10/PRACTICAL4.pdf', NULL, NULL, CAST(N'2021-03-07T15:16:54.840' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UID], [Status], [CategoryID], [TypeID], [CountryID], [NoteTitle], [DisplayPicture], [NumberOfPages], [Description], [University], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotesPreview], [Action], [Remark], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (11, 45, 4, 1, 1, 1, N'Data Science1', N'C:/Users/abc/source/repos/NotesMarketPlace/NotesMarketPlace/DefaultImg/customer-3.png', N'123', N'demo', NULL, N'demo', N'demo', N'12', N'demo', 1, CAST(159 AS Decimal(18, 0)), N'~/Members/45/11/PRACTICAL1.pdf', NULL, NULL, CAST(N'2021-03-07T15:19:47.407' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UID], [Status], [CategoryID], [TypeID], [CountryID], [NoteTitle], [DisplayPicture], [NumberOfPages], [Description], [University], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotesPreview], [Action], [Remark], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (12, 45, 4, 2, 1, 1, N'Data Science1', N'C:/Users/abc/source/repos/NotesMarketPlace/NotesMarketPlace/DefaultImg/customer-3.png', N'123', N'demo', NULL, N'demo', N'demo', N'12', N'demo', 1, CAST(159 AS Decimal(18, 0)), N'~/Members/45/12/PRACTICAL2(ab).pdf', NULL, NULL, CAST(N'2021-03-07T15:20:31.020' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UID], [Status], [CategoryID], [TypeID], [CountryID], [NoteTitle], [DisplayPicture], [NumberOfPages], [Description], [University], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotesPreview], [Action], [Remark], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (13, 45, 4, 2, 1, 1, N'Data Science2', N'C:/Users/abc/source/repos/NotesMarketPlace/NotesMarketPlace/DefaultImg/customer-3.png', N'123', N'demo', NULL, N'demo', N'demo', N'12', N'demo', 1, CAST(159 AS Decimal(18, 0)), N'~/Members/45/13/PRACTICAL2(ab).pdf', NULL, NULL, CAST(N'2021-03-07T15:25:11.983' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UID], [Status], [CategoryID], [TypeID], [CountryID], [NoteTitle], [DisplayPicture], [NumberOfPages], [Description], [University], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotesPreview], [Action], [Remark], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (14, 45, 4, 2, 1, 1, N'Data Science3', N'C:/Users/abc/source/repos/NotesMarketPlace/NotesMarketPlace/DefaultImg/customer-3.png', N'123', N'demo', NULL, N'demo', N'demo', N'12', N'demo', 1, CAST(159 AS Decimal(18, 0)), N'~/Members/45/14/PRACTICAL1(.pdf', NULL, NULL, CAST(N'2021-03-07T15:28:49.953' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UID], [Status], [CategoryID], [TypeID], [CountryID], [NoteTitle], [DisplayPicture], [NumberOfPages], [Description], [University], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotesPreview], [Action], [Remark], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (15, 45, 4, 2, 1, 1, N'CD', N'~/Members/45/15/07-03-2021_16-08-04.png', N'123', N'demo', NULL, N'demo', N'demo', N'12', N'demo', 0, CAST(0 AS Decimal(18, 0)), NULL, NULL, NULL, CAST(N'2021-03-07T16:08:03.707' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UID], [Status], [CategoryID], [TypeID], [CountryID], [NoteTitle], [DisplayPicture], [NumberOfPages], [Description], [University], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotesPreview], [Action], [Remark], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (17, 45, 4, 3, 1, 2, N'demo2', N'C:/Users/abc/source/repos/NotesMarketPlace/NotesMarketPlace/DefaultImg/customer-3.png', N'123', N'demo', NULL, N'demo', N'demo', N'12', N'demo', 0, CAST(0 AS Decimal(18, 0)), NULL, NULL, NULL, CAST(N'2021-03-07T16:30:51.277' AS DateTime), 45, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[NoteDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[ReferenceData] ON 

INSERT [dbo].[ReferenceData] ([ID], [Value], [DataValue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'Male', N'M', N'Gender', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [DataValue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'Female', N'Fe', N'Gender', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [DataValue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'Unknown', N'U', N'Gender', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [DataValue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, N'Draft', N'Draft', N'NotesStatus', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [DataValue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, N'SubmittedForReview', N'SubmittedForReview', N'NotesStatus', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [DataValue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, N'InReview', N'InReview', N'NotesStatus', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [DataValue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (7, N'Published', N'Published', N'NotesStatus', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [DataValue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (8, N'Rejected', N'Rejected', N'NotesStatus', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [DataValue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (9, N'Removed', N'Removed', N'NotesStatus', NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[ReferenceData] OFF
GO
SET IDENTITY_INSERT [dbo].[SellerNotesAttachments] ON 

INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (12, 1, N'Attachment1_05022021.pdf;Attachment2_05022021.pdf;', N'/Members/45/1/Attachment/Attachment1_05022021.pdf/Members/45/1/Attachment/Attachment2_05022021.pdf', CAST(N'2021-03-05T15:02:12.640' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (13, 2, N'Attachment1_05252021.pdf;', N'/Members/45/2/Attachment/Attachment1_05252021.pdf', CAST(N'2021-03-05T16:25:22.480' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (14, 3, N'Attachment1_05312021.pdf;', N'/Members/45/3/Attachment/Attachment1_05312021.pdf', CAST(N'2021-03-05T17:31:13.820' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (15, 4, N'Attachment1_06312021.pdf;', N'/Members/45/4/Attachment/Attachment1_06312021.pdf', CAST(N'2021-03-06T17:31:35.467' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (16, 5, N'Attachment1_06342021.pdf;', N'/Members/45/5/Attachment/Attachment1_06342021.pdf', CAST(N'2021-03-06T17:34:42.947' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (17, 6, N'Attachment1_06382021.pdf;', N'/Members/45/6/Attachment/Attachment1_06382021.pdf', CAST(N'2021-03-06T17:38:06.870' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (18, 7, N'Attachment1_07592021.pdf;', N'/Members/45/7/Attachment/Attachment1_07592021.pdf', CAST(N'2021-03-07T11:59:03.603' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (19, 8, N'Attachment1_07072021.pdf;', N'/Members/45/8/Attachment/Attachment1_07072021.pdf', CAST(N'2021-03-07T12:07:39.857' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (20, 9, N'Attachment1_07472021.pdf;', N'/Members/45/9/Attachment/Attachment1_07472021.pdf', CAST(N'2021-03-07T14:47:04.520' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (21, 10, N'Attachment1_07162021.pdf;', N'/Members/45/10/Attachment/Attachment1_07162021.pdf', CAST(N'2021-03-07T15:16:55.380' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (22, 11, N'Attachment1_07192021.pdf;', N'/Members/45/11/Attachment/Attachment1_07192021.pdf', CAST(N'2021-03-07T15:19:47.973' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (23, 12, N'Attachment1_07202021.pdf;', N'/Members/45/12/Attachment/Attachment1_07202021.pdf', CAST(N'2021-03-07T15:20:31.113' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (24, 13, N'Attachment1_07252021.pdf;', N'/Members/45/13/Attachment/Attachment1_07252021.pdf', CAST(N'2021-03-07T15:25:12.447' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (25, 14, N'Attachment1_07282021.pdf;', N'/Members/45/14/Attachment/Attachment1_07282021.pdf', CAST(N'2021-03-07T15:28:50.563' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (26, 15, N'Attachment1_07082021.pdf;', N'/Members/45/15/Attachment/Attachment1_07082021.pdf', CAST(N'2021-03-07T16:08:04.600' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (28, 17, N'Attachment1_07272021.pdf;', N'/Members/45/17/Attachment/Attachment1_07272021.pdf', CAST(N'2021-03-07T16:27:02.973' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (29, 17, N'Attachment1_07282021.pdf;', N'/Members/45/17/Attachment/Attachment1_07282021.pdf', CAST(N'2021-03-07T16:28:40.140' AS DateTime), 45, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (30, 17, N'Attachment1_07302021.pdf;', N'/Members/45/17/Attachment/Attachment1_07302021.pdf', CAST(N'2021-03-07T16:30:51.817' AS DateTime), 45, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[SellerNotesAttachments] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRoles] ON 

INSERT [dbo].[UserRoles] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'superadmin', N'superadmin', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[UserRoles] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'admin', N'admin', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[UserRoles] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'member', N'member', NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[UserRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [EmailID], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (45, 3, N'bhumit', N'sheth', N'shethbhumit02@gmail.com', N'36237ee4e2207807adde67236d2dc7bd', 1, CAST(N'2021-03-05T14:55:11.437' AS DateTime), NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__7ED91AEE4DFF0773]    Script Date: 09-03-2021 21:34:31 ******/
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
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_NoteDetails] FOREIGN KEY([ID])
REFERENCES [dbo].[NoteDetails] ([ID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_NoteDetails]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_NoteDetails1] FOREIGN KEY([ID])
REFERENCES [dbo].[NoteDetails] ([ID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_NoteDetails1]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_ReferenceData] FOREIGN KEY([Status])
REFERENCES [dbo].[ReferenceData] ([ID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_ReferenceData]
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
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_ReferenceData] FOREIGN KEY([Gender])
REFERENCES [dbo].[ReferenceData] ([ID])
GO
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_ReferenceData]
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
