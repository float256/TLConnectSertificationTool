IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'request_info')
BEGIN
	CREATE TABLE [dbo].[request_info](
		[id_request_info] [int] IDENTITY(1,1) NOT NULL,
		[request_body] [nvarchar](max) NOT NULL,
        [response_body] [nvarchar](max) NOT NULL,
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
