SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

IF OBJECT_ID('mes.StructuredLog', 'U') IS NULL
BEGIN

    CREATE TABLE [StructuredLog](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [Message] [nvarchar](max) NULL,
        [MessageTemplate] [nvarchar](max) NULL,
        [Level] [nvarchar](128) NULL,
        [TimeStamp] [datetime] NOT NULL,
        [Exception] [nvarchar](max) NULL,
        [Properties] [xml] NULL,
    CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED
    (
        [Id] ASC
    ) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]

END
GO
SET ANSI_PADDING OFF
GO