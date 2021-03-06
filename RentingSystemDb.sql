USE [master]
GO
/****** Object:  Database [RentingSystem]    Script Date: 9/4/2020 10:54:53 AM ******/
CREATE DATABASE [RentingSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RentingSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\RentingSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RentingSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\RentingSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [RentingSystem] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RentingSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RentingSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RentingSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RentingSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RentingSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RentingSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [RentingSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RentingSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RentingSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RentingSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RentingSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RentingSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RentingSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RentingSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RentingSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RentingSystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RentingSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RentingSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RentingSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RentingSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RentingSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RentingSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RentingSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RentingSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [RentingSystem] SET  MULTI_USER 
GO
ALTER DATABASE [RentingSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RentingSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RentingSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RentingSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RentingSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RentingSystem] SET QUERY_STORE = OFF
GO
USE [RentingSystem]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 9/4/2020 10:54:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[Phone] [nvarchar](255) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movies]    Script Date: 9/4/2020 10:54:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movies](
	[MovieID] [int] IDENTITY(1,1) NOT NULL,
	[Rating] [nvarchar](50) NULL,
	[Title] [nvarchar](255) NULL,
	[Year] [nvarchar](255) NULL,
	[Rental_Cost] [money] NULL,
	[Copies] [nvarchar](50) NULL,
	[Plot] [ntext] NULL,
	[Genre] [nvarchar](50) NULL,
 CONSTRAINT [PK_Movies] PRIMARY KEY CLUSTERED 
(
	[MovieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RentedMovies]    Script Date: 9/4/2020 10:54:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RentedMovies](
	[RMID] [int] IDENTITY(1,1) NOT NULL,
	[MovieIDFK] [int] NULL,
	[CustIDFK] [int] NULL,
	[DateRented] [datetime] NULL,
	[DateReturned] [datetime] NULL,
	[Rented] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 9/4/2020 10:54:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[user_name] [varchar](15) NULL,
	[password] [varchar](15) NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([CustID], [FirstName], [LastName], [Address], [Phone]) VALUES (1, N'robin', N'sharma', N'1b beaumont street', N'0273095779')
INSERT [dbo].[Customer] ([CustID], [FirstName], [LastName], [Address], [Phone]) VALUES (2, N'sukhman', N'singh', N'33,gibson road ', N'0279035779')
INSERT [dbo].[Customer] ([CustID], [FirstName], [LastName], [Address], [Phone]) VALUES (3, N'navjot', N'singh', N'32,chrtwell', N'077896384')
INSERT [dbo].[Customer] ([CustID], [FirstName], [LastName], [Address], [Phone]) VALUES (4, N'gaurav ', N'bhatia', N'1b beaomont steet', N'0211889668')
INSERT [dbo].[Customer] ([CustID], [FirstName], [LastName], [Address], [Phone]) VALUES (5, N'Anmol', N'singh', N'lake road', N'02230697896')
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[Movies] ON 

INSERT [dbo].[Movies] ([MovieID], [Rating], [Title], [Year], [Rental_Cost], [Copies], [Plot], [Genre]) VALUES (1, N'5', N'shoole', N'1998', 2.0000, N'10', N'this is action movie', N'action')
INSERT [dbo].[Movies] ([MovieID], [Rating], [Title], [Year], [Rental_Cost], [Copies], [Plot], [Genre]) VALUES (2, N'4.5', N'gangster', N'2002', 2.0000, N'9', N'romanctic', N'romanctic')
INSERT [dbo].[Movies] ([MovieID], [Rating], [Title], [Year], [Rental_Cost], [Copies], [Plot], [Genre]) VALUES (3, N'4', N'dabbang', N'2014', 2.0000, N'7', N'action and romantic', N'action and romantic')
INSERT [dbo].[Movies] ([MovieID], [Rating], [Title], [Year], [Rental_Cost], [Copies], [Plot], [Genre]) VALUES (4, N'5', N'titanic', N'1998', 2.0000, N'10', N'romantic', N'romantic')
INSERT [dbo].[Movies] ([MovieID], [Rating], [Title], [Year], [Rental_Cost], [Copies], [Plot], [Genre]) VALUES (5, N'4', N'krish', N'2010', 2.0000, N'9', N'comedy', N'comedy')
SET IDENTITY_INSERT [dbo].[Movies] OFF
GO
SET IDENTITY_INSERT [dbo].[RentedMovies] ON 

INSERT [dbo].[RentedMovies] ([RMID], [MovieIDFK], [CustIDFK], [DateRented], [DateReturned], [Rented]) VALUES (1, 3, 1, CAST(N'2020-09-04T10:52:54.160' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[RentedMovies] OFF
GO
INSERT [dbo].[users] ([user_name], [password]) VALUES (N'gg', N'gg')
GO
ALTER TABLE [dbo].[RentedMovies]  WITH CHECK ADD  CONSTRAINT [FK1] FOREIGN KEY([MovieIDFK])
REFERENCES [dbo].[Movies] ([MovieID])
GO
ALTER TABLE [dbo].[RentedMovies] CHECK CONSTRAINT [FK1]
GO
ALTER TABLE [dbo].[RentedMovies]  WITH CHECK ADD  CONSTRAINT [FK2] FOREIGN KEY([CustIDFK])
REFERENCES [dbo].[Customer] ([CustID])
GO
ALTER TABLE [dbo].[RentedMovies] CHECK CONSTRAINT [FK2]
GO
USE [master]
GO
ALTER DATABASE [RentingSystem] SET  READ_WRITE 
GO
