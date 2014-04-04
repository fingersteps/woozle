BEGIN TRY
	BEGIN TRANSACTION MigrationTransaction

	--Declare the version of this migration script. 
	--The script migrates the version automatically after a successful execution
	DECLARE @Version int;
	SET @Version = 5;
           
	--Perform migration
	IF exists(Select [Version] from [woo].[Version] where [Version] = @Version - 1)
	BEGIN
		--========================== START MIGRATION ==========================
		
	ALTER TABLE [woo].[NumberRange] ADD Name nvarchar(50) not null
		
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