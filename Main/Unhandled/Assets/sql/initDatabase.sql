
CREATE SCHEMA unhandled


CREATE TABLE [unhandled].[Application]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	MachineName VARCHAR(20) NOT NULL,
	ApplicationName VARCHAR(200) NOT NULL
)
GO


CREATE PROCEDURE [unhandled].[ApplicationRepository_Create]
(
	@Id AS BIGINT,  
	@MachineName AS VARCHAR(20), 
	@ApplicationName AS VARCHAR(2000)
)
AS
BEGIN
	
	INSERT INTO
		[unhandled].[Application]
		VALUES
		(@MachineName,
		@ApplicationName)

		SELECT CAST(SCOPE_IDENTITY() AS BIGINT) Id
END
GO


CREATE PROCEDURE [unhandled].[ApplicationRepository_GetByMachineNameAndApplicationName]
(
	@MachineName AS VARCHAR(20), 
	@ApplicationName AS VARCHAR(2000)
)
AS
BEGIN
	
	SELECT 
		Id,  
		MachineName, 
		ApplicationName
	FROM [unhandled].[Application]
	WHERE 
		MachineName = @MachineName AND 
		ApplicationName = @ApplicationName

END
GO


CREATE TABLE [unhandled].[Error]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	ApplicationId BIGINT NULL,
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


CREATE PROCEDURE [unhandled].[ErrorRepository_Create]
(
	@Id AS BIGINT,  
	@ApplicationId AS BIGINT,
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
		[unhandled].[Error]
		VALUES
		(@ApplicationId,
		@Message,
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

CREATE PROCEDURE [unhandled].[ErrorRepository_GetAll]
AS
BEGIN
	
	SELECT 
		PE.Id,
		PE.ApplicationId,
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
		[unhandled].[Error] PE
	LEFT JOIN [unhandled].[UnhandledError] CE ON PE.Id = CE.ParentErrorId

END
GO

CREATE PROCEDURE [unhandled].[ErrorRepository_GetMainErrors]
AS
BEGIN
	
	SELECT 
		PE.Id,
		PE.ApplicationId,
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
		[unhandled].[Error] PE
	LEFT JOIN [unhandled].[Error] CE ON PE.Id = CE.ParentErrorId
	WHERE PE.ParentErrorId IS NULL

END
GO

CREATE PROCEDURE [unhandled].[ErrorRepository_GetById]
(
	@Id AS BIGINT
)
AS
BEGIN
	
	SELECT 
		PE.Id,
		PE.ApplicationId,
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
		[unhandled].[Error] PE
	LEFT JOIN [unhandled].[Error] CE ON PE.Id = CE.ParentErrorId
	WHERE
		PE.Id = @Id

END
GO

CREATE TABLE [unhandled].[Cookie]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	ErrorId BIGINT NOT NULL,
	Name VARCHAR(200) NULL, 
	[Path] VARCHAR(255) NULL, 
	Expires DATETIME NULL, 
	[Domain] VARCHAR(255) NULL, 
	Secure BIT NULL, 
	[Value] VARCHAR(4093) NULL, 
)
GO

CREATE PROCEDURE [unhandled].[CookieRepository_Create]
(
	@Id AS BIGINT,  
	@ErrorId AS BIGINT,
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
		[unhandled].[Cookie]
		VALUES
		(@ErrorId,
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

CREATE PROCEDURE [unhandled].[CookieRepository_GetByErrorId]
(
	@ErrorId AS BIGINT
)
AS
BEGIN
	
	SELECT 
		Id,
		ErrorId,
		Name,
		[Path],
		Expires,
		Domain,
		Secure,
		Value
	FROM
		[unhandled].[Cookie]
	WHERE
		ErrorId = @ErrorId
END
GO