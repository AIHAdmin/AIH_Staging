USE [AgingInHome]
GO
/****** Object:  Table [dbo].[Inbox]    Script Date: 6/1/2016 1:45:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inbox](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SenderId] [nvarchar](128) NULL,
	[RecipientId] [nvarchar](128) NOT NULL,
	[Subject] [nvarchar](100) NULL,
	[MessageBody] [text] NULL,
	[SentDate] [datetime] NULL CONSTRAINT [DF_Inbox_SentDate]  DEFAULT (getdate()),
	[ServiceRequestDetailsId] [int] NULL,
	[IsArchive] [bit] NULL,
	[IsRead] [bit] NULL,
	[IsStarred] [bit] NULL,
	[IsProviderArchive] [bit] NULL,
	[IsActive] [bit] NULL,
	[ConversationId] [int] NULL,
	[ContactUsId] [int] NULL,
 CONSTRAINT [PK_Inbox] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Inbox]  WITH CHECK ADD  CONSTRAINT [FK_Inbox_AspNetUsers] FOREIGN KEY([SenderId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Inbox] CHECK CONSTRAINT [FK_Inbox_AspNetUsers]
GO
ALTER TABLE [dbo].[Inbox]  WITH CHECK ADD  CONSTRAINT [FK_Inbox_AspNetUsers1] FOREIGN KEY([RecipientId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Inbox] CHECK CONSTRAINT [FK_Inbox_AspNetUsers1]
GO
ALTER TABLE [dbo].[Inbox]  WITH CHECK ADD  CONSTRAINT [FK_Inbox_ContactUs] FOREIGN KEY([ContactUsId])
REFERENCES [dbo].[ContactUs] ([Id])
GO
ALTER TABLE [dbo].[Inbox] CHECK CONSTRAINT [FK_Inbox_ContactUs]
GO
ALTER TABLE [dbo].[Inbox]  WITH CHECK ADD  CONSTRAINT [FK_Inbox_ServiceRequestDetails] FOREIGN KEY([ServiceRequestDetailsId])
REFERENCES [dbo].[ServiceRequestDetails] ([Id])
GO
ALTER TABLE [dbo].[Inbox] CHECK CONSTRAINT [FK_Inbox_ServiceRequestDetails]
GO
