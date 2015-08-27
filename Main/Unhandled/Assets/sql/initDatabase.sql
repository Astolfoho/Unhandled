
CREATE SCHEMA unhandled

CREATE TABLE [unhandled].[UnhandledError]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
	[Message] VARCHAR(200) NULL, 
	[StackTrace] VARCHAR(2000) NULL, 
	[Type] VARCHAR(200) NULL, 
	[Source] VARCHAR(255) NULL, 
	[LineNumber] INT NULL, 
	[FileName] VARCHAR(255) NULL, 
	[SourceCode] VARCHAR(2000) NULL
)
GO


CREATE PROCEDURE [unhandled].[UnhandledErrorRepository_Create]
(
	@Id AS UNIQUEIDENTIFIER,  
	@Message AS VARCHAR(200), 
	@StackTrace AS VARCHAR(2000), 
	@Type AS VARCHAR(200), 
	@Source AS VARCHAR(255), 
	@LineNumber AS INT, 
	@FileName AS VARCHAR(255), 
	@SourceCode AS VARCHAR(2000)
)
AS
BEGIN
	
	INSERT INTO
		[unhandled].[UnhandledError]
		VALUES
		(@Id,
		@Message,
		@StackTrace,
		@Type,
		@Source,
		@LineNumber,
		@FileName,
		@SourceCode)
END
GO

CREATE PROCEDURE [unhandled].[UnhandledErrorRepository_GetAll]
AS
BEGIN
	
	SELECT 
		Id,
		[Message],
		StackTrace,
		[Type],
		[Source],
		LineNumber,
		[FileName],
		SourceCode
	FROM
		[unhandled].[UnhandledError]

END
GO

CREATE PROCEDURE [unhandled].[UnhandledErrorRepository_GetById]
(
	@Id AS UNIQUEIDENTIFIER
)
AS
BEGIN
	
	SELECT 
		Id,
		[Message],
		StackTrace,
		[Type],
		[Source],
		LineNumber,
		[FileName],
		SourceCode
	FROM
		[unhandled].[UnhandledError]
	WHERE
		Id = @Id

END
GO

CREATE TABLE [unhandled].[UnhandledCookie]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
	UnhandledErrorId UNIQUEIDENTIFIER NOT NULL,
	Name VARCHAR(200) NULL, 
	[Path] VARCHAR(255) NULL, 
	Expires DATETIME NULL, 
	[Domain] VARCHAR(255) NULL, 
	Secure BIT NULL, 
	[Value] VARCHAR(4093) NULL, 
)
GO

CREATE PROCEDURE [unhandled].[UnhandledCookieRepository_Create]
(
	@Id AS UNIQUEIDENTIFIER,  
	@UnhandledErrorId AS UNIQUEIDENTIFIER,
	@Name AS VARCHAR(200),
	@Path AS VARCHAR(255),
	@Expires AS DATETIME,
	@Domain AS VARCHAR(255),
	@Secure AS BIT,
	@Value AS VARCHAR(4093)
)
AS
BEGIN
	
	INSERT INTO
		[unhandled].[UnhandledCookie]
		VALUES
		(@Id,
	     @UnhandledErrorId,
		 @Name,
		 @Path,
		 @Expires,
		 @Domain,
		 @Secure,
		 @Value
		 )
END
GO

CREATE PROCEDURE [unhandled].[UnhandledCookieRepository_GetByErrorId]
(
	@ErrorId AS UNIQUEIDENTIFIER
)
AS
BEGIN
	
	SELECT 
		Id,
		UnhandledErrorId,
		Name,
		[Path],
		Expires,
		Domain,
		Secure,
		Value
	FROM
		[unhandled].[UnhandledCookie]
	WHERE
		UnhandledErrorId = @ErrorId
END
GO