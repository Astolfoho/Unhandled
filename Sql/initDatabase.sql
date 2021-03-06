﻿
CREATE SCHEMA unhandled

CREATE TABLE [unhandled].[UnhandledError]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[Message] VARCHAR(200) NULL, 
	[StackTrace] VARCHAR(2000) NULL, 
	[Type] VARCHAR(200) NULL, 
	[Source] VARCHAR(255) NULL, 
	[LineNumber] INT NULL, 
	[FileName] VARCHAR(255) NULL, 
	[SourceCode] VARCHAR(2000) NULL,
	[ParentErrorId] BIGINT NULL
)
GO


CREATE PROCEDURE [unhandled].[UnhandledErrorRepository_Create]
(
	@Id AS BIGINT,  
	@Message AS VARCHAR(200), 
	@StackTrace AS VARCHAR(2000), 
	@Type AS VARCHAR(200), 
	@Source AS VARCHAR(255), 
	@LineNumber AS INT, 
	@FileName AS VARCHAR(255), 
	@SourceCode AS VARCHAR(2000),
	@ParentErrorId BIGINT,
	@ChildError AS BIGINT
)
AS
BEGIN
	
	INSERT INTO
		[unhandled].[UnhandledError]
		VALUES
		(@Message,
		@StackTrace,
		@Type,
		@Source,
		@LineNumber,
		@FileName,
		@SourceCode,
		@ParentErrorId)

		SELECT CAST(SCOPE_IDENTITY() AS BIGINT) Id
END
GO

CREATE PROCEDURE [unhandled].[UnhandledErrorRepository_GetAll]
AS
BEGIN
	
	SELECT 
		PE.Id,
		PE.[Message],
		PE.StackTrace,
		PE.[Type],
		PE.[Source],
		PE.LineNumber,
		PE.[FileName],
		PE.SourceCode,
		PE.ParentErrorId,
		CE.Id AS ChildError
	FROM
		[unhandled].[UnhandledError] PE
	LEFT JOIN [unhandled].[UnhandledError] CE ON PE.Id = CE.ParentErrorId

END
GO

CREATE PROCEDURE [unhandled].[UnhandledErrorRepository_GetMainErrors]
AS
BEGIN
	
	SELECT 
		PE.Id,
		PE.[Message],
		PE.StackTrace,
		PE.[Type],
		PE.[Source],
		PE.LineNumber,
		PE.[FileName],
		PE.SourceCode,
		PE.ParentErrorId,
		CE.Id AS ChildError
	FROM
		[unhandled].[UnhandledError] PE
	LEFT JOIN [unhandled].[UnhandledError] CE ON PE.Id = CE.ParentErrorId
	WHERE PE.ParentErrorId IS NULL

END
GO

CREATE PROCEDURE [unhandled].[UnhandledErrorRepository_GetById]
(
	@Id AS BIGINT
)
AS
BEGIN
	
	SELECT 
		PE.Id,
		PE.[Message],
		PE.StackTrace,
		PE.[Type],
		PE.[Source],
		PE.LineNumber,
		PE.[FileName],
		PE.SourceCode,
		PE.ParentErrorId,
		CE.Id AS ChildError
	FROM
		[unhandled].[UnhandledError] PE
	LEFT JOIN [unhandled].[UnhandledError] CE ON PE.Id = CE.ParentErrorId
	WHERE
		PE.Id = @Id

END
GO

CREATE TABLE [unhandled].[UnhandledCookie]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	UnhandledErrorId BIGINT NOT NULL,
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
	@Id AS BIGINT,  
	@UnhandledErrorId AS BIGINT,
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
		(@UnhandledErrorId,
		 @Name,
		 @Path,
		 @Expires,
		 @Domain,
		 @Secure,
		 @Value
		 )

		 SELECT CAST(SCOPE_IDENTITY() AS BIGINT) Id
END
GO

CREATE PROCEDURE [unhandled].[UnhandledCookieRepository_GetByErrorId]
(
	@ErrorId AS BIGINT
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