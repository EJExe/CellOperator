USE [master]
GO
/****** Object:  Database [SystemOpMobComm]    Script Date: 20.12.2024 13:58:04 ******/
CREATE DATABASE [SystemOpMobComm]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SystemOpMobComm', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\SystemOpMobComm.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SystemOpMobComm_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\SystemOpMobComm_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SystemOpMobComm] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SystemOpMobComm].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SystemOpMobComm] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SystemOpMobComm] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SystemOpMobComm] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SystemOpMobComm] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SystemOpMobComm] SET ARITHABORT OFF 
GO
ALTER DATABASE [SystemOpMobComm] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SystemOpMobComm] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [SystemOpMobComm] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SystemOpMobComm] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SystemOpMobComm] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SystemOpMobComm] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SystemOpMobComm] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SystemOpMobComm] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SystemOpMobComm] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SystemOpMobComm] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SystemOpMobComm] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SystemOpMobComm] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SystemOpMobComm] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SystemOpMobComm] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SystemOpMobComm] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SystemOpMobComm] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SystemOpMobComm] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SystemOpMobComm] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SystemOpMobComm] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SystemOpMobComm] SET  MULTI_USER 
GO
ALTER DATABASE [SystemOpMobComm] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SystemOpMobComm] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SystemOpMobComm] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SystemOpMobComm] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [SystemOpMobComm]
GO
/****** Object:  Table [dbo].[BalanceHistoryTable]    Script Date: 20.12.2024 13:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BalanceHistoryTable](
	[ID] [int] NOT NULL,
	[Kod_Tarifa] [int] NULL,
	[Kod_Yslygi] [int] NULL,
	[Numbe_Of_Phone] [varchar](50) NOT NULL,
	[Data] [date] NOT NULL,
	[Amount] [int] NOT NULL,
 CONSTRAINT [PK_BalanceHistoryTable] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CallTable]    Script Date: 20.12.2024 13:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CallTable](
	[<<PK>>Kod_Call] [int] NOT NULL,
	[<<FK>>Number_Sender] [varchar](50) NOT NULL,
	[<<FK>>Number_Getter] [varchar](50) NOT NULL,
	[<<FK>>Type_Of_Call] [int] NOT NULL,
	[Data_Of_Call] [date] NOT NULL,
	[Time_Of_Call] [time](7) NOT NULL,
	[Amount] [int] NOT NULL,
	[Cost] [int] NULL,
 CONSTRAINT [PK_CallTable] PRIMARY KEY CLUSTERED 
(
	[<<PK>>Kod_Call] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DataTable]    Script Date: 20.12.2024 13:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DataTable](
	[data_nachala] [datetime] NOT NULL,
	[data_okonchania] [datetime] NULL,
	[<<FK>>Kod_Tarifa] [int] NOT NULL,
	[<<FK>>Numbe_Of_Phone] [varchar](50) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SMSTable]    Script Date: 20.12.2024 13:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SMSTable](
	[<<PK>>Kod_SMS] [int] NOT NULL,
	[<<FK>>Number_Poluchatela] [varchar](50) NOT NULL,
	[<<FK>>Number_Otpravitela] [varchar](50) NOT NULL,
	[Data_Of_Sent] [date] NOT NULL,
	[Time] [time](7) NOT NULL,
	[Text] [varchar](max) NULL,
 CONSTRAINT [PK_SMSTable] PRIMARY KEY CLUSTERED 
(
	[<<PK>>Kod_SMS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TarifTable]    Script Date: 20.12.2024 13:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TarifTable](
	[<<PK>> Kod_Tarifa] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Cost_PerMonth] [int] NULL,
	[Cost_Of_Connection] [int] NULL,
	[Price_1minn_city] [int] NULL,
	[Price_1minn_mejgorod] [int] NULL,
	[Price_1minn_mejdunarod] [int] NULL,
	[[<<FK>>]]Kod_Type_Of_Tarif] [int] NOT NULL,
	[SMS_Per_Month] [int] NULL,
	[GB_Per_Month] [int] NULL,
	[Min_Per_Month] [int] NULL,
 CONSTRAINT [PK_TarifTable] PRIMARY KEY CLUSTERED 
(
	[<<PK>> Kod_Tarifa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TypeOfCallTable]    Script Date: 20.12.2024 13:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TypeOfCallTable](
	[<<PK>>Kod_Type_Of_Call] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TypeOfCallTable] PRIMARY KEY CLUSTERED 
(
	[<<PK>>Kod_Type_Of_Call] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TypeOfTarifTable]    Script Date: 20.12.2024 13:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TypeOfTarifTable](
	[<<PK>Kod_Type_Of_Tarif] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TypeOfTarifTable] PRIMARY KEY CLUSTERED 
(
	[<<PK>Kod_Type_Of_Tarif] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TypeOfUserTable]    Script Date: 20.12.2024 13:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TypeOfUserTable](
	[<<PK>>Kod_Type_Of_User] [int] NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_TypeOfUserTable] PRIMARY KEY CLUSTERED 
(
	[<<PK>>Kod_Type_Of_User] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TypeOfYslygiTable]    Script Date: 20.12.2024 13:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeOfYslygiTable](
	[<<PK>>Kod_Type_Of_Yslygi] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CostPerMonth] [int] NOT NULL,
 CONSTRAINT [PK_TypeOfYslygiTable] PRIMARY KEY CLUSTERED 
(
	[<<PK>>Kod_Type_Of_Yslygi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserTable]    Script Date: 20.12.2024 13:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserTable](
	[<<PK>> Number_Of_Phone] [varchar](50) NOT NULL,
	[<<FK>> Kod_Tarifa] [int] NOT NULL,
	[<<FK>> Kod_Type_Of_User] [int] NOT NULL,
	[FIO] [varchar](50) NOT NULL,
	[Money_On_Bank] [int] NULL,
	[Loging] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Minuts_Left] [int] NULL,
	[Gb_Left] [int] NULL,
	[SMS_Left] [int] NULL,
 CONSTRAINT [PK_UserTable] PRIMARY KEY CLUSTERED 
(
	[<<PK>> Number_Of_Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[YslygiTable]    Script Date: 20.12.2024 13:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[YslygiTable](
	[Kod_Yslygi] [int] NOT NULL,
	[<<FK>>Number_User] [varchar](50) NOT NULL,
	[<<FK>>Kod_TypeOfYslygi] [int] NOT NULL,
 CONSTRAINT [PK_YslygiTable] PRIMARY KEY CLUSTERED 
(
	[Kod_Yslygi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[CallTable]  WITH CHECK ADD  CONSTRAINT [FK_CallTable_TypeOfCallTable] FOREIGN KEY([<<FK>>Type_Of_Call])
REFERENCES [dbo].[TypeOfCallTable] ([<<PK>>Kod_Type_Of_Call])
GO
ALTER TABLE [dbo].[CallTable] CHECK CONSTRAINT [FK_CallTable_TypeOfCallTable]
GO
ALTER TABLE [dbo].[CallTable]  WITH CHECK ADD  CONSTRAINT [FK_CallTable_UserTable] FOREIGN KEY([<<FK>>Number_Sender])
REFERENCES [dbo].[UserTable] ([<<PK>> Number_Of_Phone])
GO
ALTER TABLE [dbo].[CallTable] CHECK CONSTRAINT [FK_CallTable_UserTable]
GO
ALTER TABLE [dbo].[DataTable]  WITH CHECK ADD  CONSTRAINT [FK_DataTable_TarifTable] FOREIGN KEY([<<FK>>Kod_Tarifa])
REFERENCES [dbo].[TarifTable] ([<<PK>> Kod_Tarifa])
GO
ALTER TABLE [dbo].[DataTable] CHECK CONSTRAINT [FK_DataTable_TarifTable]
GO
ALTER TABLE [dbo].[DataTable]  WITH CHECK ADD  CONSTRAINT [FK_DataTable_UserTable] FOREIGN KEY([<<FK>>Numbe_Of_Phone])
REFERENCES [dbo].[UserTable] ([<<PK>> Number_Of_Phone])
GO
ALTER TABLE [dbo].[DataTable] CHECK CONSTRAINT [FK_DataTable_UserTable]
GO
ALTER TABLE [dbo].[SMSTable]  WITH CHECK ADD  CONSTRAINT [FK_SMSTable_UserTable] FOREIGN KEY([<<FK>>Number_Otpravitela])
REFERENCES [dbo].[UserTable] ([<<PK>> Number_Of_Phone])
GO
ALTER TABLE [dbo].[SMSTable] CHECK CONSTRAINT [FK_SMSTable_UserTable]
GO
ALTER TABLE [dbo].[TarifTable]  WITH CHECK ADD  CONSTRAINT [FK_TarifTable_TypeOfTarifTable] FOREIGN KEY([[<<FK>>]]Kod_Type_Of_Tarif])
REFERENCES [dbo].[TypeOfTarifTable] ([<<PK>Kod_Type_Of_Tarif])
GO
ALTER TABLE [dbo].[TarifTable] CHECK CONSTRAINT [FK_TarifTable_TypeOfTarifTable]
GO
ALTER TABLE [dbo].[UserTable]  WITH CHECK ADD  CONSTRAINT [FK_UserTable_TypeOfUserTable] FOREIGN KEY([<<FK>> Kod_Type_Of_User])
REFERENCES [dbo].[TypeOfUserTable] ([<<PK>>Kod_Type_Of_User])
GO
ALTER TABLE [dbo].[UserTable] CHECK CONSTRAINT [FK_UserTable_TypeOfUserTable]
GO
ALTER TABLE [dbo].[YslygiTable]  WITH CHECK ADD  CONSTRAINT [FK_YslygiTable_TypeOfYslygiTable] FOREIGN KEY([<<FK>>Kod_TypeOfYslygi])
REFERENCES [dbo].[TypeOfYslygiTable] ([<<PK>>Kod_Type_Of_Yslygi])
GO
ALTER TABLE [dbo].[YslygiTable] CHECK CONSTRAINT [FK_YslygiTable_TypeOfYslygiTable]
GO
ALTER TABLE [dbo].[YslygiTable]  WITH CHECK ADD  CONSTRAINT [FK_YslygiTable_UserTable] FOREIGN KEY([<<FK>>Number_User])
REFERENCES [dbo].[UserTable] ([<<PK>> Number_Of_Phone])
GO
ALTER TABLE [dbo].[YslygiTable] CHECK CONSTRAINT [FK_YslygiTable_UserTable]
GO
USE [master]
GO
ALTER DATABASE [SystemOpMobComm] SET  READ_WRITE 
GO
