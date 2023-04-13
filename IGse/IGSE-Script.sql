USE [master]
GO
/****** Object:  Database [IGse_DB]    Script Date: 14/1/2023 1:38:31 am ******/
CREATE DATABASE [IGse_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'IGse_DB', FILENAME = N'C:\Users\kmafroz\IGse_DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'IGse_DB_log', FILENAME = N'C:\Users\kmafroz\IGse_DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [IGse_DB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [IGse_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [IGse_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [IGse_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [IGse_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [IGse_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [IGse_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [IGse_DB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [IGse_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [IGse_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [IGse_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [IGse_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [IGse_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [IGse_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [IGse_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [IGse_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [IGse_DB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [IGse_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [IGse_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [IGse_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [IGse_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [IGse_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [IGse_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [IGse_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [IGse_DB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [IGse_DB] SET  MULTI_USER 
GO
ALTER DATABASE [IGse_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [IGse_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [IGse_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [IGse_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [IGse_DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [IGse_DB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [IGse_DB] SET QUERY_STORE = OFF
GO
USE [IGse_DB]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 14/1/2023 1:38:31 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[CustomerId] [int] NULL,
	[DayElectricityReading] [int] NOT NULL,
	[NightElectricityReading] [int] NOT NULL,
	[GasReading] [int] NOT NULL,
	[IsPaid] [bit] NOT NULL,
	[Amount] [int] NOT NULL,
	[IsVoucherUsed] [bit] NOT NULL,
	[EvcId] [int] NULL,
	[DueDate] [datetime] NOT NULL,
	[BillId] [int] IDENTITY(1,1) NOT NULL,
	[BillMonthYear] [datetime] NULL,
	[NumberOfDays] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 14/1/2023 1:38:31 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[Address] [varchar](225) NOT NULL,
	[PropertyType] [varchar](50) NOT NULL,
	[NumberOfBedrooms] [int] NOT NULL,
	[Evc] [varchar](8) NULL,
	[WalletAmount] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerEvcHistory]    Script Date: 14/1/2023 1:38:31 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerEvcHistory](
	[CustomerId] [int] NOT NULL,
	[EvcId] [int] NOT NULL,
	[DateOfUsed] [datetime] NOT NULL,
	[HistoryId] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Evc]    Script Date: 14/1/2023 1:38:31 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evc](
	[EvcVoucher] [varchar](8) NOT NULL,
	[IsUsed] [bit] NOT NULL,
	[EvcId] [int] IDENTITY(1,1) NOT NULL,
	[UsedByCustomer] [int] NULL,
	[Amount] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EvcId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[EvcVoucher] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 14/1/2023 1:38:31 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[CustomerId] [int] NULL,
	[PaymentStatus] [bit] NOT NULL,
	[PaymentDate] [datetime] NOT NULL,
	[BillId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SetPrice]    Script Date: 14/1/2023 1:38:31 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SetPrice](
	[SetId] [int] IDENTITY(1,1) NOT NULL,
	[SetDate] [datetime] NOT NULL,
	[ElectricityPriceNight] [decimal](5, 2) NOT NULL,
	[ElectricityPriceDay] [decimal](5, 2) NOT NULL,
	[GasPrice] [decimal](5, 2) NOT NULL,
	[StandingCharge] [decimal](18, 0) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SetPriceHistory]    Script Date: 14/1/2023 1:38:31 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SetPriceHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SetType] [varchar](100) NOT NULL,
	[SetDate] [datetime] NOT NULL,
	[SetBy] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 14/1/2023 1:38:31 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NULL,
	[EmailId] [varchar](50) NOT NULL,
	[Password] [varchar](30) NOT NULL,
	[SaltedPassword] [varchar](40) NOT NULL,
	[role] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
UNIQUE NONCLUSTERED 
(
	[EmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Customer] ADD  DEFAULT ((0)) FOR [WalletAmount]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[CustomerEvcHistory]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[CustomerEvcHistory]  WITH CHECK ADD FOREIGN KEY([EvcId])
REFERENCES [dbo].[Evc] ([EvcId])
GO
ALTER TABLE [dbo].[Evc]  WITH CHECK ADD FOREIGN KEY([UsedByCustomer])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD FOREIGN KEY([BillId])
REFERENCES [dbo].[Bill] ([BillId])
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
/****** Object:  StoredProcedure [dbo].[GetAdmins]    Script Date: 14/1/2023 1:38:31 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  CREATE procedure [dbo].[GetAdmins]
  as 
begin
SELECT * ,u.EmailId,u.IsActive from Customer c
join Users u
on u.CustomerId =c.CustomerId
where u.role = 'admin'
end
GO
/****** Object:  StoredProcedure [dbo].[GetCustomerIdByUserId]    Script Date: 14/1/2023 1:38:31 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCustomerIdByUserId]
(
    @UserId int
)
AS
BEGIN
    select c.CustomerId from Customer c
join Users u
on u.CustomerId = c.CustomerId
Where u.UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[GetEvcHistoryByCustomerId]    Script Date: 14/1/2023 1:38:31 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetEvcHistoryByCustomerId]
@customerId int
as
begin 
SELECT ch.CustomerId,ch.DateOfUsed,ch.EvcId,ch.HistoryId,e.Amount
  FROM [CustomerEvcHistory] ch
  inner join Evc e
  on ch.EvcId = e.EvcId
  where ch.CustomerId = @customerId
end
GO
USE [master]
GO
ALTER DATABASE [IGse_DB] SET  READ_WRITE 
GO
