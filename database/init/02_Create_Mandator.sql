
------------------------------------------------------------

-- Initializationscript for an example mandator

------------------------------------------------------------

-- Creating Language
INSERT INTO woo.[Language] VALUES(1,'en', 'English')

-- Creating Translation Record for Country Name with the fallback description
INSERT INTO woo.Translation VALUES('Schweiz')

-- Creating Translation Item for the Country Name for the Language 'en'
INSERT INTO woo.TranslationItem VALUES((SELECT TOP 1 Id FROM woo.Translation), (SELECT TOP 1 Id FROM woo.[Language]), 'Switzerland')

-- Creating Country with the translated name
INSERT INTO woo.Country VALUES((SELECT TOP 1 Id FROM woo.Translation))

-- Creating City
INSERT INTO woo.City VALUES('6003', 'Lucerne',(SELECT TOP 1 Id FROM woo.Country))

-- Creating Mandator Group
INSERT INTO woo.MandatorGroup VALUES('Mandator Group 1')

-- Creating Example Mandator
INSERT INTO woo.Mandator VALUES('Mandator 1', 'Test street 1', '044 445 45 45', (SELECT TOP 1 Id FROM woo.City), null, 'info@mandator1.com', null, (select TOP 1 Id from woo.MandatorGroup))