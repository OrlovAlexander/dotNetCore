USE [master]
GO
/****** Object:  Database [employees]    Script Date: 10/17/2017 09:40:54 ******/
CREATE DATABASE [employees]
GO
USE [employees]
GO
/****** Object:  Table [dbo].[EmployeeSalary]    Script Date: 10/17/2017 09:40:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeSalary](
	[EmployeeId] [int] NOT NULL,
	[Salary] [numeric](15, 3) NOT NULL
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_EmployeeSalaryPay] ON [dbo].[EmployeeSalary] 
(
	[EmployeeId] ASC
)ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeHourlyPay]    Script Date: 10/17/2017 09:40:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeHourlyPay](
	[EmployeeId] [int] NOT NULL,
	[HourlyPay] [numeric](15, 3) NOT NULL
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_EmployeeHourlyPay] ON [dbo].[EmployeeHourlyPay] 
(
	[EmployeeId] ASC
)ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 10/17/2017 09:40:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[Patronymic] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO
