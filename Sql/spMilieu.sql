use [GestionStage]
GO

CREATE PROC pGetAllMilieuStage
AS

SELECT [IDMilieuStage], [Titre], [Description], [NoCivique], [Rue], [CodePostal], [Ville], [Province], [Pays], [NoTelephone], [Etat]
FROM MilieuStage

GO