BEGIN TRY
	BEGIN TRANSACTION MigrationTransaction

	--Declare the version of this migration script. 
	--The script migrates the version automatically after a successful execution
	DECLARE @Version int;
	SET @Version = 6;
           
	--Perform migration
	IF exists(Select [Version] from [woo].[Version] where [Version] = @Version - 1)
	BEGIN
		--========================== START MIGRATION ==========================
		
	CREATE TABLE [woo].[TextField](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[DescriptionTranslationId] [int] NOT NULL,
	[FieldValueTranslationId] [int] NOT NULL,
	[MandId] [int] NULL,
	[TextType] [varchar](20) NULL,
	PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
	) 


		ALTER TABLE [woo].[TextField]  WITH CHECK ADD  CONSTRAINT [FK_NDescriptionTranslationId_Translation] FOREIGN KEY([DescriptionTranslationId])
		REFERENCES [woo].[Translation] ([Id])


		ALTER TABLE [woo].[TextField] CHECK CONSTRAINT [FK_NDescriptionTranslationId_Translation]


		ALTER TABLE [woo].[TextField]  WITH CHECK ADD  CONSTRAINT [FK_NFieldValueTranslationId_Translation] FOREIGN KEY([FieldValueTranslationId])
		REFERENCES [woo].[Translation] ([Id])


		ALTER TABLE [woo].[TextField] CHECK CONSTRAINT [FK_NFieldValueTranslationId_Translation]


		ALTER TABLE [woo].[TextField]  WITH CHECK ADD  CONSTRAINT [FK_NotificationField_Mandator] FOREIGN KEY([MandId])
		REFERENCES [woo].[Mandator] ([Id])


		ALTER TABLE [woo].[TextField] CHECK CONSTRAINT [FK_NotificationField_Mandator]
		
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