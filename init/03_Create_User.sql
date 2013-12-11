
---------------------------------------------------------------------

-- Initializationscript for an example User with mandator assignment

---------------------------------------------------------------------

-- Translation Records for Active/Inactive User Status
INSERT INTO woo.Translation VALUES ('Aktiv')
INSERT INTO woo.Translation VALUES ('Inaktiv')

INSERT INTO woo.TranslationItem VALUES ((SELECT Id FROM woo.Translation WHERE DefaultDescription = 'Aktiv'), (SELECT TOP 1 Id FROM woo.Language), 'Active')
INSERT INTO woo.TranslationItem VALUES ((SELECT Id FROM woo.Translation WHERE DefaultDescription = 'Inaktiv'), (SELECT TOP 1 Id FROM woo.Language), 'Inactive')

-- Creating Statusfield
INSERT INTO woo.StatusField VALUES('Flagactive')

-- Creating Statusvalues
INSERT INTO woo.Status VALUES('Active', (SELECT Id from woo.StatusField where Name = 'Flagactive'), (SELECT Id FROM woo.Translation where DefaultDescription = 'Aktiv')) 
INSERT INTO woo.Status VALUES('Inactive', (SELECT Id from woo.StatusField where Name = 'Flagactive'), (SELECT Id FROM woo.Translation where DefaultDescription = 'Inaktiv')) 

-- Creating User
INSERT INTO woo.[User] VALUES('user1', 'pass1', 1, GETDATE(), GETDATE(), (SELECT TOP 1 Id FROM woo.Language), (SELECT TOP 1 Id FROM woo.Status where Value = 'Active'), null, 'Firstname', 'Lastname') 

-- Creating Role
INSERT INTO woo.[Role] VALUES('Administrator', 'Administrator Role', 'ADM')

-- Assign Role to the example mandator
INSERT INTO woo.MandatorRole VALUES((SELECT TOP 1 Id FROM woo.Mandator), (SELECT TOP 1 Id FROM woo.[Role]))


-- Assign the User to the Mandator Role
INSERT INTO woo.UserMandatorRole VALUES((SELECT TOP 1 Id FROM woo.[User]), (SELECT TOP 1 Id FROM woo.MandatorRole))






