BEGIN TRY
	BEGIN TRANSACTION MigrationTransaction

	--Declare the version of this migration script. 
	--The script migrates the version automatically after a successful execution
	DECLARE @Version int;
	SET @Version = 7;
           
	--Perform migration
	IF exists(Select [Version] from [woo].[Version] where [Version] = @Version - 1)
	BEGIN
		--========================== START MIGRATION ==========================
		
	CREATE TABLE [woo].[TextFieldPlaceHolder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[DescriptionTranslationId] [int] NOT NULL,
	[FieldValueTranslationId] [int] NOT NULL,
	[PlaceHolderType] [varchar](50) NULL,
	PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	))

	ALTER TABLE [woo].[TextFieldPlaceHolder]  WITH CHECK ADD  CONSTRAINT [FK_DTextFieldPlaceHolder_Translation] FOREIGN KEY([DescriptionTranslationId])
	REFERENCES [woo].[Translation] ([Id])

	ALTER TABLE [woo].[TextFieldPlaceHolder] CHECK CONSTRAINT [FK_DTextFieldPlaceHolder_Translation]


	ALTER TABLE [woo].[TextFieldPlaceHolder]  WITH CHECK ADD  CONSTRAINT [FK_FTextFieldPlaceHolder_Translation] FOREIGN KEY([FieldValueTranslationId])
	REFERENCES [woo].[Translation] ([Id])


	ALTER TABLE [woo].[TextFieldPlaceHolder] CHECK CONSTRAINT [FK_FTextFieldPlaceHolder_Translation]
		
		--========================== END MIGRATION ==========================
	END
	ELSE
	BEGIN
		RAISERROR ('The database cannot be migrated because its version is too old.', 17, 1)
	END

	--Increment version
	Update [woo].[Version] set [Version] = @Version

	COMMIT TRANSACTION MigrationTransaction
	PRINT 'The Migration was successful.'
	
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION MigrationTransaction
	Declare @ErrorMessage nvarchar(300)
	SELECT @ErrorMessage=ERROR_MESSAGE()
	PRINT 'ERROR: ' + @ErrorMessage;   
END CATCH