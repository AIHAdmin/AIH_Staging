USE [AgingInHome]
GO

/****** Object:  Table [dbo].[ContactUs]    Script Date: 4/27/2016 12:18:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ContactUs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[IsCall] [bit] NULL,
	[IsEmail] [bit] NULL,
	[AppointmentSchedule] [bit] NULL,
	[CustomeMsg] [bit] NULL,
	[ProviderListingId] [int] NOT NULL,
 CONSTRAINT [PK_ContactUs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ContactUs]  WITH CHECK ADD  CONSTRAINT [FK_ContactUs_ProviderListing] FOREIGN KEY([ProviderListingId])
REFERENCES [dbo].[ProviderListing] ([ProviderListingId])
GO

ALTER TABLE [dbo].[ContactUs] CHECK CONSTRAINT [FK_ContactUs_ProviderListing]
GO


