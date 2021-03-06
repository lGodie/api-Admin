USE [TestRappi]
GO
/****** Object:  Table [dbo].[IdentificationTypes]    Script Date: 10/25/2020 9:38:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentificationTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[Code] [nvarchar](8) NOT NULL,
 CONSTRAINT [PK_IdentificationTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](32) NOT NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](16) NOT NULL,
	[name] [varchar](64) NULL,
	[password] [varchar](16) NOT NULL,
	[email] [varchar](128) NOT NULL,
	[document] [nvarchar](32) NOT NULL,
	[firstName] [nvarchar](50) NOT NULL,
	[lastName] [nvarchar](50) NOT NULL,
	[idRole] [int] NOT NULL,
	[idIdentificationType] [int] NULL,
	[idWorkSubArea] [int] NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkAreas]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkAreas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_WorkAreas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkSubAreas]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkSubAreas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[idWorkArea] [int] NOT NULL,
 CONSTRAINT [PK_WorkSubAreas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_IdentificationTypes] FOREIGN KEY([idIdentificationType])
REFERENCES [dbo].[IdentificationTypes] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_IdentificationTypes]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([idRole])
REFERENCES [dbo].[Roles] ([id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_WorkAreas] FOREIGN KEY([idWorkSubArea])
REFERENCES [dbo].[WorkAreas] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_WorkAreas]
GO
ALTER TABLE [dbo].[WorkSubAreas]  WITH CHECK ADD  CONSTRAINT [FK_WorkSubAreas_WorkAreas] FOREIGN KEY([idWorkArea])
REFERENCES [dbo].[WorkAreas] ([Id])
GO
ALTER TABLE [dbo].[WorkSubAreas] CHECK CONSTRAINT [FK_WorkSubAreas_WorkAreas]
GO
/****** Object:  StoredProcedure [dbo].[usp_IdentificationTypes_Create]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_IdentificationTypes_Create] 
	@Name	nvarchar(64),
	@Code	nvarchar(8)
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT OFF; 

	DECLARE @strMsg VARCHAR(2000)

	BEGIN TRY
		BEGIN TRAN
			BEGIN
				INSERT INTO [dbo].IdentificationTypes
				(Name,Code)
				VALUES
				(@Name,@Code)
			END
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN

		SET @strMsg = 'Se presentó un error en el procedimiento: dbo.usp_IdentificationTypes_Create ' + ERROR_MESSAGE()
		RAISERROR(@strMsg,16,1)
	END CATCH

	SET NOCOUNT OFF; 
END

GO
/****** Object:  StoredProcedure [dbo].[usp_IdentificationTypes_Delete]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_IdentificationTypes_Delete]
	@Id int
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  

	BEGIN
		DELETE FROM [dbo].IdentificationTypes
		WHERE Id = @Id
	END
END

GO
/****** Object:  StoredProcedure [dbo].[usp_IdentificationTypes_FindAll]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_IdentificationTypes_FindAll]
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT OFF; 

	SELECT 
		 id
		,Name,Code
	FROM [dbo].IdentificationTypes

	SET NOCOUNT OFF; 
END

GO
/****** Object:  StoredProcedure [dbo].[usp_IdentificationTypes_FindById]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_IdentificationTypes_FindById]
	@Id int
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT ON; 

	SELECT 
		 Id
		,name,Code
	FROM [dbo].IdentificationTypes
	WHERE Id = @Id

	SET NOCOUNT OFF; 
END

GO
/****** Object:  StoredProcedure [dbo].[usp_IdentificationTypes_Update]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_IdentificationTypes_Update] 
	@Id	int,
	@Name nvarchar(64),
	@code nvarchar(8)
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT OFF; 

	DECLARE @strMsg VARCHAR(1024)

	BEGIN TRY
		BEGIN TRAN
			
			BEGIN
				UPDATE [dbo].IdentificationTypes
					SET Name = @Name,
					Code = @code
				WHERE Id = @Id		
            END
		COMMIT TRAN

		SELECT @Id
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN

		SET @strMsg = 'Error en el procedimiento: dbo.usp_IdentificationTypes_Update ' + ERROR_MESSAGE()
		RAISERROR(@strMsg,16,1)
	END CATCH

	SET NOCOUNT OFF; 
END

GO
/****** Object:  StoredProcedure [dbo].[usp_Role_Create]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Role_Create] 
	@Name	nvarchar(32)
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT OFF; 

	DECLARE @strMsg VARCHAR(1000)

	BEGIN TRY
		BEGIN TRAN
			BEGIN
				INSERT INTO [dbo].Roles
				(Name,active)
				VALUES
				(@Name,1)
			END
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN

		SET @strMsg = 'Se presentó un error en el procedimiento: dbo.usp_Role_Actualizar ' + ERROR_MESSAGE()
		RAISERROR(@strMsg,16,1)
	END CATCH

	SET NOCOUNT OFF; 
END

GO
/****** Object:  StoredProcedure [dbo].[usp_Role_Delete]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_Role_Delete]
	@Id int
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	IF EXISTS (SELECT TOP 1 1 FROM [dbo].Users WHERE idRole = @Id)
	BEGIN
		RAISERROR('Existen usuario con el role, no se puede eliminar.',16,1)
	END
	ELSE
	BEGIN
		DELETE FROM [dbo].Roles
		WHERE Id = @Id
	END
END

GO
/****** Object:  StoredProcedure [dbo].[usp_Role_FindAll]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_Role_FindAll]
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT OFF; 

	SELECT 
		 id
		,Name,
		active
	FROM [dbo].Roles

	SET NOCOUNT OFF; 
END

GO
/****** Object:  StoredProcedure [dbo].[usp_Role_FindById]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_Role_FindById]
	@Id int
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT ON; 

	SELECT 
		 Id
		,name
	FROM [dbo].Roles
	WHERE Id = @Id

	SET NOCOUNT OFF; 
END

GO
/****** Object:  StoredProcedure [dbo].[usp_Role_Update]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_Role_Update] 
	@Id	int,
	@Name varchar(50),
	@Active bit
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT OFF; 

	DECLARE @strMsg VARCHAR(1024)

	BEGIN TRY
		BEGIN TRAN
			
			BEGIN
				UPDATE [dbo].Roles
					SET Name = @Name,
					 active = @Active
				WHERE Id = @Id		
            END
		COMMIT TRAN

		SELECT @Id
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN

		SET @strMsg = 'Error en el procedimiento: dbo.usp_Role_Update ' + ERROR_MESSAGE()
		RAISERROR(@strMsg,16,1)
	END CATCH

	SET NOCOUNT OFF; 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Users_Create]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create  PROCEDURE [dbo].[usp_Users_Create]
	(  
	@username varchar(16),
	@name varchar(64),
	@password varchar(16) ,
	@email varchar(128) ,
	@document nvarchar(32) ,
	@firstName nvarchar(50) ,
	@lastName nvarchar (50) ,
	@idRole int,
	@idIdentificationType int ,
	@idWorkSubArea int
	)
  AS
  BEGIN
	BEGIN TRANSACTION;
		BEGIN TRY
		  INSERT INTO Users ([username],[name],[password] ,[email],[document] ,[firstName] ,
	[lastName] ,[idRole] ,[idIdentificationType],[idWorkSubArea],[active])
		  VALUES (@username, @name,@password,@email,@document,@firstName,@lastName,@idRole,@idIdentificationType,@idWorkSubArea,1);
			COMMIT TRANSACTION;
		 END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION;
		END CATCH;
  END
GO
/****** Object:  StoredProcedure [dbo].[usp_Users_Delete]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[usp_Users_Delete] 
	@Id int
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  

	DELETE FROM [dbo].Users
	WHERE Id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[usp_Users_FindAll]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[usp_Users_FindAll]
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT OFF; 

	SELECT 
		 u.Id
		,u.Name
		,[username]
        ,[email]
        ,[document]
        ,[firstName]
        ,[lastName]
        ,[idRole]
        ,[idIdentificationType]
        ,[idWorkSubArea]
        ,u.[active],
		I.Id as idIdentificationType,
		I.Name as NameIdentificationType,
		w.Name SubArea,
		r.Id as idRole,
		w.Id as idSubArea,
		r.name role
	FROM [dbo].Users u
	INNER JOIN [dbo].IdentificationTypes I
		ON u.idIdentificationType = I.id
	INNER JOIN [dbo].WorkSubAreas w
		ON u.idWorkSubArea = w.id
	INNER JOIN [dbo].Roles r
		ON u.idRole = r.id
END

GO
/****** Object:  StoredProcedure [dbo].[usp_Users_Login]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_Users_Login]
	@Email nvarchar(128),
	@Password varchar(16)
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	DECLARE @strMessage NVARCHAR(1024) = ''

	IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.Users WHERE email = @Email )
	BEGIN
		SET @strMessage += 'Usuario o Contraseña inválido \n'
		RAISERROR(@strMessage, 16, 1)
	END
	ELSE
	BEGIN
		IF NOT EXISTS (SELECT TOP 1 id FROM dbo.Users WHERE email = @Email AND password = @Password)
		BEGIN
			SET @strMessage += 'Usuario o Contraseña inválida \n'
			RAISERROR(@strMessage, 16, 1)
		END
	END

	SELECT CONVERT(BIT,1)

	SET NOCOUNT OFF;
END

GO
/****** Object:  StoredProcedure [dbo].[usp_Users_Search]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[usp_Users_Search]
	@ActualPage int = 1,
	@PageSize int = 10,
	@searchString nvarchar(240) = NULL,
	@PageQuatity int output,
	@idrole int
AS
BEGIN 
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT ON; 

	DECLARE @NumberRecords int

	DECLARE @tmpEmployees TABLE 
	(	 Id int
		,Name varchar(64)
		,username varchar(16)
        ,email varchar(128)
        ,document nvarchar(32)
        ,firstName nvarchar(50)
        ,lastName nvarchar(50)
        ,idRole int
        ,idIdentification int
        ,idWorkSubArea int
		,[row] INT
	)

	INSERT INTO @tmpEmployees(
	 Id
	,Name
	,[username]
	,[email]
	,[document]
	,[firstName]
	,[lastName]
	,[idRole]
	,[idIdentification]
	,[idWorkSubArea]
	,[row]
	)
	SELECT 
			 TMP.id
			,TMP.Name
			,TMP.username
			,TMP.email
			,TMP.document
			,TMP.firstName
			,TMP.lastName
			,TMP.idRole
			,TMP.idIdentificationType
			,TMP.idWorkSubArea
			,TMP.[row]
	FROM (
		SELECT
			 u.Id
		    ,u.Name
		    ,[username]
            ,[email]
            ,[document]
            ,[firstName]
            ,[lastName]
            ,[idRole]
            ,[idIdentificationType]
            ,[idWorkSubArea]
            ,u.[active],
			ROW_NUMBER() OVER (ORDER BY u.id) AS [row]
		FROM [dbo].Users u
	   INNER JOIN [dbo].IdentificationTypes I
		ON u.idIdentificationType = I.id
	   INNER JOIN [dbo].WorkSubAreas w
		ON u.idWorkSubArea = w.id
	    INNER JOIN [dbo].Roles r
		ON u.idRole = r.id
		WHERE r.id = @idrole
		and (( u.document LIKE ( CASE WHEN @searchString IS NULL THEN u.document ELSE '%' + @searchString + '%' END ) )
		OR ( (u.firstName) LIKE ( CASE WHEN @searchString IS NULL THEN ( u.firstName) ELSE '%' + @searchString + '%' END ) ))
		
	) AS TMP 
	
	SET @NumberRecords = (SELECT COUNT(*) FROM @tmpEmployees)

	SELECT *
	FROM @tmpEmployees TMP
	WHERE ( TMP.row BETWEEN (((@ActualPage - 1) * @PageSize)+1) AND (@ActualPage * @PageSize ))

	IF ( @NumberRecords % @PageSize ) > 0
	BEGIN
		SET @PageQuatity = (@NumberRecords / @PageSize ) + 1
	END
	ELSE
	BEGIN
		SET @PageQuatity = (@NumberRecords / @PageSize )
	END

	PRINT @PageQuatity

	RETURN @PageQuatity

	SET NOCOUNT OFF; 
END

GO
/****** Object:  StoredProcedure [dbo].[usp_Users_SelectByEmail]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[usp_Users_SelectByEmail]
 @email varchar(128)
AS
BEGIN 
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SELECT
		  u.Id
		,u.Name
		,[username]
        ,[email]
        ,[document]
        ,[firstName]
        ,[lastName]
        ,[idRole]
        ,[idIdentificationType]
        ,[idWorkSubArea]
        ,u.[active],
		I.Id as idIdentificationType,
		I.Name as NameIdentificationType,
		w.Name SubArea,
		r.Id as idRole,
		w.Id as idSubArea,
		r.name role
	FROM [dbo].Users u
	INNER JOIN [dbo].IdentificationTypes I
		ON u.idIdentificationType = I.id
	INNER JOIN [dbo].WorkSubAreas w
		ON u.idWorkSubArea = w.id
	INNER JOIN [dbo].Roles r
		ON u.idRole = r.id
		where u.email = @email

	SET NOCOUNT OFF; 
END

GO
/****** Object:  StoredProcedure [dbo].[usp_Users_SelectById]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_Users_SelectById]
 @Id int
AS
BEGIN 
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SELECT
		  u.Id
		,u.Name
		,[username]
        ,[email]
        ,[document]
        ,[firstName]
        ,[lastName]
        ,[idRole]
        ,[idIdentificationType]
        ,[idWorkSubArea]
        ,u.[active],
		I.Id as idIdentificationType,
		I.Name as NameIdentificationType,
		w.Name SubArea,
		r.Id as idRole,
		w.Id as idSubArea,
		r.name role
	FROM [dbo].Users u
	INNER JOIN [dbo].IdentificationTypes I
		ON u.idIdentificationType = I.id
	INNER JOIN [dbo].WorkSubAreas w
		ON u.idWorkSubArea = w.id
	INNER JOIN [dbo].Roles r
		ON u.idRole = r.id
		where u.Id = @Id

	SET NOCOUNT OFF; 
END

GO
/****** Object:  StoredProcedure [dbo].[usp_Users_Update]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[usp_Users_Update] 
	@Id	int,
	@username varchar(16),
	@name varchar(64),
	@password varchar(16),
	@email varchar(128) ,
	@document nvarchar(32) ,
	@firstName nvarchar(50) ,
	@lastName nvarchar (50) ,
	@idRole int,
	@idIdentificationType int ,
	@idWorkSubArea int
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT ON; 

	DECLARE @strMsg VARCHAR(1024)

	BEGIN TRY
		BEGIN TRAN
			
			BEGIN
				UPDATE [dbo].Users
					SET Name = @Name,
					username = @username,
					password = @password,
					email = @email,
					document = @document,
					firstName = @firstName,
					lastName = @lastName,
					idRole = @idRole,
					idWorkSubArea = @idWorkSubArea,
					idIdentificationType = @idIdentificationType
				WHERE Id = @Id		
            END
		COMMIT TRAN

		SELECT @Id
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN

		SET @strMsg = 'Error en el procedimiento: dbo.usp_Users_Update ' + ERROR_MESSAGE()
		RAISERROR(@strMsg,16,1)
	END CATCH

	SET NOCOUNT OFF; 
END

GO
/****** Object:  StoredProcedure [dbo].[usp_WorkArea_Create]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_WorkArea_Create] 
	@Name nvarchar(50)
AS

  BEGIN
	BEGIN TRANSACTION;
		BEGIN TRY
		 INSERT INTO [dbo].WorkAreas
				(Name)
				VALUES
				(@Name)
				COMMIT TRANSACTION;
		 END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION;
		END CATCH;
  END
GO
/****** Object:  StoredProcedure [dbo].[usp_WorkArea_Delete]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_WorkArea_Delete]
	@Id int
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  

	IF EXISTS (SELECT TOP 1 1 FROM [dbo].WorkSubAreas WHERE idWorkArea = @Id)
	BEGIN
		RAISERROR('El area tiene subáreas asociadas no se puede eliminar.',16,1)
	END
	ELSE
	BEGIN
		DELETE FROM [dbo].WorkAreas
		WHERE Id = @Id
	END
END

GO
/****** Object:  StoredProcedure [dbo].[usp_WorkArea_FindById]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_WorkArea_FindById]
	@IdArea NUMERIC
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT ON; 

	SELECT 
		 Id
		,name
	FROM [dbo].WorkAreas
	WHERE Id = @IdArea

	SET NOCOUNT OFF; 
END

GO
/****** Object:  StoredProcedure [dbo].[usp_WorkArea_Update]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_WorkArea_Update] 
	@Id	int,
	@Name varchar(50)
AS
 BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT OFF; 

	DECLARE @strMsg VARCHAR(1024)
	BEGIN TRANSACTION
	BEGIN TRY
		
				UPDATE [dbo].WorkAreas
					SET Name = @Name
				WHERE Id = @Id		
            
		COMMIT TRANSACTION

		SELECT @Id
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION

		SET @strMsg = 'Error en el procedimiento: dbo.usp_Role_Update ' + ERROR_MESSAGE()
		RAISERROR(@strMsg,16,1)
	END CATCH

	SET NOCOUNT OFF; 
END

GO
/****** Object:  StoredProcedure [dbo].[usp_WorkAreas_FindAll]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_WorkAreas_FindAll]
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT OFF; 

	SELECT 
		 id
		,Name
	FROM [dbo].WorkAreas

	SET NOCOUNT OFF; 
END

GO
/****** Object:  StoredProcedure [dbo].[usp_WorkSubArea_Create]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_WorkSubArea_Create] 
	@Name	nvarchar(50),
	@idArea int
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT OFF; 

	DECLARE @strMsg VARCHAR(2000)

	BEGIN TRY
		BEGIN TRAN
			BEGIN
				INSERT INTO [dbo].WorkSubAreas
				(Name,idWorkArea)
				VALUES
				(@Name,@idArea)
			END
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN

		SET @strMsg = 'Se presentó un error en el procedimiento: dbo.usp_WorkSubAreas_Create ' + ERROR_MESSAGE()
		RAISERROR(@strMsg,16,1)
	END CATCH

	SET NOCOUNT OFF; 
END

GO
/****** Object:  StoredProcedure [dbo].[usp_WorkSubArea_Delete]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_WorkSubArea_Delete]
	@Id int
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  

	BEGIN
		DELETE FROM [dbo].WorkSubAreas
		WHERE Id = @Id
	END
END

GO
/****** Object:  StoredProcedure [dbo].[usp_WorkSubArea_FindById]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_WorkSubArea_FindById]
	@Id int
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT ON; 

	SELECT 
		  a.id
		,a.Name,
		w.Id as idWorkArea,
		w.Name as AreaName
	FROM [dbo].WorkSubAreas as a
	INNER JOIN [dbo].WorkAreas w
		ON a.idWorkArea = w.id
	WHERE a.Id = @Id

	SET NOCOUNT OFF; 
END

GO
/****** Object:  StoredProcedure [dbo].[usp_WorksubArea_Search]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 CREATE PROCEDURE [dbo].[usp_WorksubArea_Search] 
	(  
		@id int
	)
	
  AS
  BEGIN
	BEGIN TRANSACTION;
		BEGIN TRY
			SELECT * FROM  WorkSubAreas 
				WHERE id=@id;
			COMMIT TRANSACTION;
		 END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION;
		END CATCH;
  END
 -- execute Search_WorksubArea 		@@id =1
GO
/****** Object:  StoredProcedure [dbo].[usp_WorkSubArea_Update]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_WorkSubArea_Update] 
	@Id	int,
	@idArea int,
	@Name varchar(50)
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT OFF; 

	DECLARE @strMsg VARCHAR(1024)

	BEGIN TRY
		BEGIN TRAN
			
			BEGIN
				UPDATE [dbo].WorkSubAreas
					SET Name = @Name,
					    idWorkArea= @idArea
				WHERE Id = @Id		
            END
		COMMIT TRAN

		SELECT @Id
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN

		SET @strMsg = 'Error en el procedimiento: dbo.usp_WorkSubArea_Update ' + ERROR_MESSAGE()
		RAISERROR(@strMsg,16,1)
	END CATCH

	SET NOCOUNT OFF; 
END

GO
/****** Object:  StoredProcedure [dbo].[usp_WorkSubAreas_FindAll]    Script Date: 10/25/2020 9:38:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_WorkSubAreas_FindAll]
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
	SET NOCOUNT OFF; 

	SELECT 
		 a.id
		,a.Name,
		w.Id as idWorkArea,
		w.Name as AreaName
	FROM [dbo].WorkSubAreas as a
	INNER JOIN [dbo].WorkAreas w
		ON a.idWorkArea = w.id

	SET NOCOUNT OFF; 
END

GO
