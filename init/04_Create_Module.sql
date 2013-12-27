
---------------------------------------------------------------------

-- Initializationscript for an example module

---------------------------------------------------------------------

INSERT INTO [woo].[ModuleGroup]
           ([Icon]
           ,[Name]
           ,[Description])
     VALUES
           (null
           ,'Test-Group'
           ,'Contains all specific modules')
GO
INSERT INTO [woo].[Translation]
           ([DefaultDescription])
     VALUES
           ('Test-Module')
GO
INSERT INTO [woo].[TranslationItem]
           ([TranslationId]
           ,[LanguageId]
           ,[Description])
     VALUES
           ((SELECT TOP 1 Id FROM woo.Translation where DefaultDescription = 'Test-Module')
           ,(SELECT TOP 1 Id FROM woo.[Language])
           ,'Test-Module')
GO
INSERT INTO [woo].[Module]
           ([Icon]
           ,[Name]
           ,[Description]
           ,[Version]
           ,[ModuleGroupId]
           ,[LogicalId]
           ,[Sequence]
           ,[TranslationId])
     VALUES
           (null,
           'My Test Module'
           ,'Manages a specific function'
           ,'0'
           ,1
           ,'TES'
           ,0
           ,(SELECT TOP 1 Id FROM woo.Translation where DefaultDescription = 'Test-Module'))
GO

INSERT INTO [TIA].[woo].[MandatorModules]
           ([ModuleId]
           ,[MandatorId])
		VALUES
           ((select TOP 1 Id from woo.Module)
           ,(select TOP 1 Id from woo.Mandator))




