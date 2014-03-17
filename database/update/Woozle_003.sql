BEGIN TRY
	BEGIN TRANSACTION MigrationTransaction

	--Declare the version of this migration script. 
	--The script migrates the version automatically after a successful execution
	DECLARE @Version int;
	SET @Version = 3;
           
	--Perform migration
	IF exists(Select [Version] from [woo].[Version] where [Version] = @Version - 1)
	BEGIN
		--========================== START MIGRATION ==========================
		
		-- ATTENTION: Execute this script only when you migrated your existing users from plain password to password hash and salt! All still null salts and hashes get defaulted 
		-- with this script because these columns are mandatory afterwards.
		
		ALTER TABLE [woo].[User]
		DROP COLUMN [Password]

		Update [woo].[User] SET PasswordSalt='Default' where PasswordSalt is null
		
		Update [woo].[User] SET PasswordHash='Default' where PasswordHash is null

		ALTER TABLE [woo].[User]
		ALTER COLUMN PasswordSalt varchar(50) NOT NULL
		
		ALTER TABLE [woo].[User]
		ALTER COLUMN PasswordHash varchar(100) NOT NULL
		
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