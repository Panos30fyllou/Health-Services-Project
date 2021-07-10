USE [master]
GO
/****** Object:  Database [HealthServices]    Script Date: 10/7/2021 16:26:24 ******/
CREATE DATABASE [HealthServices]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HealthServices', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\HealthServices.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HealthServices_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\HealthServices_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [HealthServices] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HealthServices].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HealthServices] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HealthServices] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HealthServices] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HealthServices] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HealthServices] SET ARITHABORT OFF 
GO
ALTER DATABASE [HealthServices] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HealthServices] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HealthServices] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HealthServices] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HealthServices] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HealthServices] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HealthServices] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HealthServices] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HealthServices] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HealthServices] SET  DISABLE_BROKER 
GO
ALTER DATABASE [HealthServices] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HealthServices] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HealthServices] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HealthServices] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HealthServices] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HealthServices] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HealthServices] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HealthServices] SET RECOVERY FULL 
GO
ALTER DATABASE [HealthServices] SET  MULTI_USER 
GO
ALTER DATABASE [HealthServices] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HealthServices] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HealthServices] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HealthServices] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HealthServices] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HealthServices] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'HealthServices', N'ON'
GO
ALTER DATABASE [HealthServices] SET QUERY_STORE = OFF
GO
USE [HealthServices]
GO
/****** Object:  Table [dbo].[Appointments]    Script Date: 10/7/2021 16:26:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointments](
	[Id] [int] NOT NULL,
	[DateOfAppointment] [datetime2](7) NOT NULL,
	[SetDate] [datetime2](7) NOT NULL,
	[PatientId] [int] NOT NULL,
	[Priority] [nvarchar](100) NOT NULL,
	[Reason] [nvarchar](100) NULL,
	[XRayType] [nvarchar](100) NOT NULL,
	[DoctorId] [int] NOT NULL,
 CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctors]    Script Date: 10/7/2021 16:26:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctors](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Surename] [nvarchar](100) NOT NULL,
	[Department] [nvarchar](100) NOT NULL,
	[NumberOfAppointments] [int] NOT NULL,
 CONSTRAINT [PK_Doctors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 10/7/2021 16:26:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Surename] [nvarchar](100) NOT NULL,
	[FathersName] [nvarchar](100) NOT NULL,
	[MothersName] [nvarchar](100) NOT NULL,
	[HealthID] [nvarchar](100) NOT NULL,
	[Gender] [nchar](100) NOT NULL,
	[DateOfBirth] [datetime2](7) NOT NULL,
	[Street] [nvarchar](100) NOT NULL,
	[Number] [int] NOT NULL,
	[PostalCode] [int] NOT NULL,
	[NumbersOfContact] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [FK_Doc]    Script Date: 10/7/2021 16:26:24 ******/
CREATE NONCLUSTERED INDEX [FK_Doc] ON [dbo].[Patient]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Patient] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Patient] ([Id])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Patient]
GO
ALTER TABLE [dbo].[Doctors]  WITH CHECK ADD  CONSTRAINT [FK_Doctors_Doctors] FOREIGN KEY([Id])
REFERENCES [dbo].[Doctors] ([Id])
GO
ALTER TABLE [dbo].[Doctors] CHECK CONSTRAINT [FK_Doctors_Doctors]
GO
ALTER TABLE [dbo].[Patient]  WITH CHECK ADD  CONSTRAINT [FK] FOREIGN KEY([Id])
REFERENCES [dbo].[Patient] ([Id])
GO
ALTER TABLE [dbo].[Patient] CHECK CONSTRAINT [FK]
GO
USE [master]
GO
ALTER DATABASE [HealthServices] SET  READ_WRITE 
GO
