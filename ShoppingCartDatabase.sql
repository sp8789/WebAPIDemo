USE [DemoDatabase]
GO
/****** Object:  Table [dbo].[CouponMaster]    Script Date: 12/13/2018 02:34:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CouponMaster](
	[CouponId] [int] IDENTITY(1,1) NOT NULL,
	[CouponCode] [nvarchar](50) NULL,
	[Percentage] [int] NULL,
 CONSTRAINT [PK_CouponMaster] PRIMARY KEY CLUSTERED 
(
	[CouponId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserMaster]    Script Date: 12/13/2018 02:34:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Address] [nvarchar](500) NULL,
	[Phone] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductMaster]    Script Date: 12/13/2018 02:34:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductMaster](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](50) NULL,
	[ProductPrice] [decimal](10, 2) NULL,
 CONSTRAINT [PK_ItemDetails] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderSummary]    Script Date: 12/13/2018 02:34:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderSummary](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[OrderNumber] [nvarchar](100) NULL,
	[UserId] [int] NULL,
	[CouponId] [int] NULL,
	[OrderTotal] [decimal](18, 2) NULL,
 CONSTRAINT [PK_OderHeader] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 12/13/2018 02:34:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderDetailId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NULL,
	[ProductId] [int] NULL,
	[Quantity] [int] NULL,
	[Total] [decimal](10, 2) NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[OrderDetailId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_OrderDetail_OrderSummary]    Script Date: 12/13/2018 02:34:09 ******/
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_OrderSummary] FOREIGN KEY([OrderId])
REFERENCES [dbo].[OrderSummary] ([OrderId])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_OrderSummary]
GO
/****** Object:  ForeignKey [FK_OrderDetail_ProductMaster]    Script Date: 12/13/2018 02:34:09 ******/
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_ProductMaster] FOREIGN KEY([ProductId])
REFERENCES [dbo].[ProductMaster] ([ProductId])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_ProductMaster]
GO
/****** Object:  ForeignKey [FK_OrderSummary_CouponMaster]    Script Date: 12/13/2018 02:34:09 ******/
ALTER TABLE [dbo].[OrderSummary]  WITH CHECK ADD  CONSTRAINT [FK_OrderSummary_CouponMaster] FOREIGN KEY([CouponId])
REFERENCES [dbo].[CouponMaster] ([CouponId])
GO
ALTER TABLE [dbo].[OrderSummary] CHECK CONSTRAINT [FK_OrderSummary_CouponMaster]
GO
/****** Object:  ForeignKey [FK_OrderSummary_UserMaster]    Script Date: 12/13/2018 02:34:09 ******/
ALTER TABLE [dbo].[OrderSummary]  WITH CHECK ADD  CONSTRAINT [FK_OrderSummary_UserMaster] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserMaster] ([Id])
GO
ALTER TABLE [dbo].[OrderSummary] CHECK CONSTRAINT [FK_OrderSummary_UserMaster]
GO
