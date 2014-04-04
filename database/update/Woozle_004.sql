BEGIN TRY
	BEGIN TRANSACTION MigrationTransaction

	--Declare the version of this migration script. 
	--The script migrates the version automatically after a successful execution
	DECLARE @Version int;
	SET @Version = 4;
           
	--Perform migration
	IF exists(Select [Version] from [woo].[Version] where [Version] = @Version - 1)
	BEGIN
		--========================== START MIGRATION ==========================
		
	print('Creating new Table [NumberRange]')
		
	CREATE TABLE [woo].[NumberRange](
		[Id] [int] IDENTITY(1,1) PRIMARY KEY,
		[From] int NOT NULL,
		[Till] int NULL,
		[Current] int NULL,
		[MandatorId] int NULL,
		[ChangeCounter] timestamp NOT NULL
		)
			
	ALTER TABLE [woo].[NumberRange]  WITH CHECK ADD  CONSTRAINT [FK_NumberRange_Mandator] FOREIGN KEY([MandatorId])
	REFERENCES [woo].[Mandator] ([Id])	
		
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