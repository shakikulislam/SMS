USE [master]
GO
/****** Object:  Database [StockManagementSystemDb]    Script Date: 26-Feb-19 10:30:01 PM ******/
CREATE DATABASE [StockManagementSystemDb] ON  PRIMARY 
( NAME = N'StockManagementSystemDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\StockManagementSystemDb.mdf' , SIZE = 2304KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'StockManagementSystemDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\StockManagementSystemDb_log.LDF' , SIZE = 504KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [StockManagementSystemDb] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [StockManagementSystemDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [StockManagementSystemDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [StockManagementSystemDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [StockManagementSystemDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [StockManagementSystemDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [StockManagementSystemDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [StockManagementSystemDb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [StockManagementSystemDb] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [StockManagementSystemDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [StockManagementSystemDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [StockManagementSystemDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [StockManagementSystemDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [StockManagementSystemDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [StockManagementSystemDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [StockManagementSystemDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [StockManagementSystemDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [StockManagementSystemDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [StockManagementSystemDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [StockManagementSystemDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [StockManagementSystemDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [StockManagementSystemDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [StockManagementSystemDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [StockManagementSystemDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [StockManagementSystemDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [StockManagementSystemDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [StockManagementSystemDb] SET  MULTI_USER 
GO
ALTER DATABASE [StockManagementSystemDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [StockManagementSystemDb] SET DB_CHAINING OFF 
GO
USE [StockManagementSystemDb]
GO
/****** Object:  Table [dbo].[CategorieS]    Script Date: 26-Feb-19 10:30:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CategorieS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_Categorie] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CompanyS]    Script Date: 26-Feb-19 10:30:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CompanyS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_CompanyS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Items]    Script Date: 26-Feb-19 10:30:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Items](
	[ID] [int] NOT NULL,
	[Name] [varchar](20) NULL,
	[ReorderLevel] [varchar](20) NULL,
	[CompanyId] [int] NULL,
	[CategoryId] [int] NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StockIn]    Script Date: 26-Feb-19 10:30:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StockIn](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Available Quantity] [varchar](50) NULL,
	[Stock In Quantity] [varchar](50) NULL,
	[Date] [varchar](50) NULL,
	[Company ID] [int] NULL,
	[Item ID] [int] NULL,
 CONSTRAINT [PK_StockIn] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StockOut]    Script Date: 26-Feb-19 10:30:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StockOut](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StockId] [int] NULL,
	[ItemId] [int] NULL,
	[StockOutQuantity] [int] NULL,
	[StockOutType] [varchar](50) NULL,
	[Date] [date] NULL,
 CONSTRAINT [PK_StockOut] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[CompanyItemView]    Script Date: 26-Feb-19 10:30:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CompanyItemView]
AS
SELECT        dbo.Items.ID, dbo.Items.Name, dbo.Items.ReorderLevel, dbo.Items.CategoryId, dbo.CompanyS.Name AS Company
FROM            dbo.CompanyS INNER JOIN
                         dbo.Items ON dbo.CompanyS.ID = dbo.Items.CompanyId


GO
/****** Object:  View [dbo].[ItemsStockInView]    Script Date: 26-Feb-19 10:30:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ItemsStockInView]
AS
SELECT        dbo.StockIn.ID, dbo.StockIn.[Available Quantity], dbo.StockIn.[Stock In Quantity], dbo.StockIn.Date, dbo.StockIn.[Company ID], dbo.Items.Name, dbo.Items.ReorderLevel
FROM            dbo.Items INNER JOIN
                         dbo.StockIn ON dbo.Items.ID = dbo.StockIn.[Item ID]


GO
/****** Object:  View [dbo].[SalesView]    Script Date: 26-Feb-19 10:30:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[SalesView]
AS
SELECT        dbo.Items.ID AS ItemID, dbo.Items.Name, dbo.StockOut.Date, dbo.StockOut.StockOutQuantity
FROM            dbo.Items INNER JOIN
                         dbo.StockOut ON dbo.Items.ID = dbo.StockOut.ItemId
WHERE        (dbo.StockOut.StockOutType = 'Sales')

GO
SET IDENTITY_INSERT [dbo].[CategorieS] ON 

INSERT [dbo].[CategorieS] ([ID], [Name]) VALUES (1, N'Stationary')
INSERT [dbo].[CategorieS] ([ID], [Name]) VALUES (2, N'Cosmetics')
INSERT [dbo].[CategorieS] ([ID], [Name]) VALUES (3, N'Electronics')
INSERT [dbo].[CategorieS] ([ID], [Name]) VALUES (4, N'Kids Items')
INSERT [dbo].[CategorieS] ([ID], [Name]) VALUES (5, N'Furniture')
INSERT [dbo].[CategorieS] ([ID], [Name]) VALUES (6, N'Kids ')
INSERT [dbo].[CategorieS] ([ID], [Name]) VALUES (7, N'Kitchen')
INSERT [dbo].[CategorieS] ([ID], [Name]) VALUES (8, N'Kids Toy')
INSERT [dbo].[CategorieS] ([ID], [Name]) VALUES (9, N'Home Deco')
SET IDENTITY_INSERT [dbo].[CategorieS] OFF
SET IDENTITY_INSERT [dbo].[CompanyS] ON 

INSERT [dbo].[CompanyS] ([ID], [Name]) VALUES (1, N'Unilever')
INSERT [dbo].[CompanyS] ([ID], [Name]) VALUES (2, N'RFL')
INSERT [dbo].[CompanyS] ([ID], [Name]) VALUES (3, N'Walton')
INSERT [dbo].[CompanyS] ([ID], [Name]) VALUES (4, N'RPL')
INSERT [dbo].[CompanyS] ([ID], [Name]) VALUES (5, N'Intel')
INSERT [dbo].[CompanyS] ([ID], [Name]) VALUES (6, N'Sony')
INSERT [dbo].[CompanyS] ([ID], [Name]) VALUES (7, N'Symphony')
INSERT [dbo].[CompanyS] ([ID], [Name]) VALUES (8, N'Lenovo')
INSERT [dbo].[CompanyS] ([ID], [Name]) VALUES (9, N'Samsung')
INSERT [dbo].[CompanyS] ([ID], [Name]) VALUES (10, N'Regal')
INSERT [dbo].[CompanyS] ([ID], [Name]) VALUES (11, N'Casio')
SET IDENTITY_INSERT [dbo].[CompanyS] OFF
INSERT [dbo].[Items] ([ID], [Name], [ReorderLevel], [CompanyId], [CategoryId]) VALUES (1, N'Sofa', N'10', 10, NULL)
INSERT [dbo].[Items] ([ID], [Name], [ReorderLevel], [CompanyId], [CategoryId]) VALUES (2, N'Almira', N'20', 10, NULL)
INSERT [dbo].[Items] ([ID], [Name], [ReorderLevel], [CompanyId], [CategoryId]) VALUES (3, N'Television', N'20', 3, NULL)
INSERT [dbo].[Items] ([ID], [Name], [ReorderLevel], [CompanyId], [CategoryId]) VALUES (4, N'Refrigerator', N'15', 3, NULL)
INSERT [dbo].[Items] ([ID], [Name], [ReorderLevel], [CompanyId], [CategoryId]) VALUES (5, N'Generator', N'15', 3, NULL)
INSERT [dbo].[Items] ([ID], [Name], [ReorderLevel], [CompanyId], [CategoryId]) VALUES (6, N'Microwave oven', N'10', 3, NULL)
SET IDENTITY_INSERT [dbo].[StockIn] ON 

INSERT [dbo].[StockIn] ([ID], [Available Quantity], [Stock In Quantity], [Date], [Company ID], [Item ID]) VALUES (1, N'0', N'20', NULL, 10, 2)
INSERT [dbo].[StockIn] ([ID], [Available Quantity], [Stock In Quantity], [Date], [Company ID], [Item ID]) VALUES (8, N'0', N'50', NULL, 10, 2)
INSERT [dbo].[StockIn] ([ID], [Available Quantity], [Stock In Quantity], [Date], [Company ID], [Item ID]) VALUES (9, N'0', N'50', NULL, 10, 1)
INSERT [dbo].[StockIn] ([ID], [Available Quantity], [Stock In Quantity], [Date], [Company ID], [Item ID]) VALUES (10, N'0', N'25', NULL, 10, 2)
INSERT [dbo].[StockIn] ([ID], [Available Quantity], [Stock In Quantity], [Date], [Company ID], [Item ID]) VALUES (11, N'0', N'30', NULL, 10, 2)
INSERT [dbo].[StockIn] ([ID], [Available Quantity], [Stock In Quantity], [Date], [Company ID], [Item ID]) VALUES (12, N'30', N'30', NULL, 10, 2)
INSERT [dbo].[StockIn] ([ID], [Available Quantity], [Stock In Quantity], [Date], [Company ID], [Item ID]) VALUES (13, N'30', N'10', NULL, 10, 2)
INSERT [dbo].[StockIn] ([ID], [Available Quantity], [Stock In Quantity], [Date], [Company ID], [Item ID]) VALUES (14, N'30', N'20', NULL, 10, 2)
INSERT [dbo].[StockIn] ([ID], [Available Quantity], [Stock In Quantity], [Date], [Company ID], [Item ID]) VALUES (15, N'30', N'20', NULL, 10, 2)
INSERT [dbo].[StockIn] ([ID], [Available Quantity], [Stock In Quantity], [Date], [Company ID], [Item ID]) VALUES (16, N'30', N'20', NULL, 10, 2)
INSERT [dbo].[StockIn] ([ID], [Available Quantity], [Stock In Quantity], [Date], [Company ID], [Item ID]) VALUES (17, N'0', N'50', NULL, 3, 6)
INSERT [dbo].[StockIn] ([ID], [Available Quantity], [Stock In Quantity], [Date], [Company ID], [Item ID]) VALUES (18, N'0', N'50', NULL, 3, 6)
INSERT [dbo].[StockIn] ([ID], [Available Quantity], [Stock In Quantity], [Date], [Company ID], [Item ID]) VALUES (19, N'0', N'20', NULL, 3, 5)
INSERT [dbo].[StockIn] ([ID], [Available Quantity], [Stock In Quantity], [Date], [Company ID], [Item ID]) VALUES (20, N'20', N'10', NULL, 3, 5)
SET IDENTITY_INSERT [dbo].[StockIn] OFF
SET IDENTITY_INSERT [dbo].[StockOut] ON 

INSERT [dbo].[StockOut] ([ID], [StockId], [ItemId], [StockOutQuantity], [StockOutType], [Date]) VALUES (1, 8, 2, 10, N'Sales', CAST(0x763F0B00 AS Date))
INSERT [dbo].[StockOut] ([ID], [StockId], [ItemId], [StockOutQuantity], [StockOutType], [Date]) VALUES (2, 9, 1, 5, N'Sales', CAST(0x5A3F0B00 AS Date))
INSERT [dbo].[StockOut] ([ID], [StockId], [ItemId], [StockOutQuantity], [StockOutType], [Date]) VALUES (3, 18, 6, 10, N'Sales', CAST(0x5A3F0B00 AS Date))
INSERT [dbo].[StockOut] ([ID], [StockId], [ItemId], [StockOutQuantity], [StockOutType], [Date]) VALUES (4, 10, 2, 20, N'Sales', CAST(0x3B3F0B00 AS Date))
SET IDENTITY_INSERT [dbo].[StockOut] OFF
ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK_Items_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[CategorieS] ([ID])
GO
ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK_Items_Categories]
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK_Items_CompanyS1] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[CompanyS] ([ID])
GO
ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK_Items_CompanyS1]
GO
ALTER TABLE [dbo].[StockIn]  WITH CHECK ADD  CONSTRAINT [FK_StockIn_CompanyS] FOREIGN KEY([Company ID])
REFERENCES [dbo].[CompanyS] ([ID])
GO
ALTER TABLE [dbo].[StockIn] CHECK CONSTRAINT [FK_StockIn_CompanyS]
GO
ALTER TABLE [dbo].[StockIn]  WITH CHECK ADD  CONSTRAINT [FK_StockIn_Items] FOREIGN KEY([Item ID])
REFERENCES [dbo].[Items] ([ID])
GO
ALTER TABLE [dbo].[StockIn] CHECK CONSTRAINT [FK_StockIn_Items]
GO
ALTER TABLE [dbo].[StockOut]  WITH CHECK ADD  CONSTRAINT [FK_StockOut_Items] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Items] ([ID])
GO
ALTER TABLE [dbo].[StockOut] CHECK CONSTRAINT [FK_StockOut_Items]
GO
ALTER TABLE [dbo].[StockOut]  WITH CHECK ADD  CONSTRAINT [FK_StockOut_StockIn] FOREIGN KEY([StockId])
REFERENCES [dbo].[StockIn] ([ID])
GO
ALTER TABLE [dbo].[StockOut] CHECK CONSTRAINT [FK_StockOut_StockIn]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Items"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CompanyS"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 102
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CompanyItemView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CompanyItemView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Items"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "StockIn"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 432
            End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ItemsStockInView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ItemsStockInView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[27] 2[14] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Items"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "StockOut"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 135
               Right = 430
            End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'SalesView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'SalesView'
GO
USE [master]
GO
ALTER DATABASE [StockManagementSystemDb] SET  READ_WRITE 
GO
