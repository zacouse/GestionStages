use [GestionStage]
GO

CREATE PROC pGetAllMilieuStage
AS

SELECT [IDMilieuStage], [Titre], [Description], [NoCivique], [Rue], [CodePostal], [Ville], [Province], [Pays], [NoTelephone], [Etat]
FROM MilieuStage

GO
CREATE PROC pGetMilieuStageById
	@Id_IN INT
AS
	SELECT [IDMilieuStage],[Titre],[Description],[NoCivique],[Rue],[CodePostal],[Ville],[Province],[Pays],[NoTelephone],[Etat]
	FROM MilieuStage WHERE [IDMilieuStage]= @Id_IN
GO

CREATE PROC pAddSetMilieuStageRestriction @IDMilieuStage_IN INT, @IDRestriction_IN Varchar(4000),@New_IN BIT
AS
IF @New_IN = 1
BEGIN
INSERT INTO MilieuStageRestriction([IDMilieuStage],[IDRestriction],[Etat])
SELECT  @IDMilieuStage_IN as 'IDMilieuStage', IDRestriction, 1 as 'Etat' From Restriction where IDRestriction IN(select value from STRING_SPLIT(@IDRestriction_IN,',')) /*Ajout des restrictions*/
END
ELSE
BEGIN
UPDATE MilieuStageRestriction
SET [Etat] = 0 ,[DateHeureModification] =GETDATE()/*Enlever les ancients qui ne sont pas dans la nouvelle liste*/
WHERE [IDRestriction] not in (select value from STRING_SPLIT(@IDRestriction_IN,',')) and IDMilieuStage = @IDMilieuStage_IN 

Insert into MilieuStageRestriction([IDMilieuStage],[IDRestriction],[Etat])
select  @IDMilieuStage_IN as 'IDMilieuStage', IDRestriction, 1 as 'Etat' From Restriction where IDRestriction IN(select value from STRING_SPLIT(@IDRestriction_IN,',')) /*Ajout des nouvelles restrictions*/
and IDRestriction not in (select IDRestriction  from MilieuStageRestriction where IDRestriction in(select value from STRING_SPLIT(@IDRestriction_IN,',')) and [IDMilieuStage] = @IDMilieuStage_IN)

UPDATE MilieuStageRestriction
SET [Etat] = 1 ,[DateHeureModification] =GETDATE()/*Actualiser l'état des restrictions*/
WHERE [IDRestriction] in (select value from STRING_SPLIT(@IDRestriction_IN,',')) and IDMilieuStage = @IDMilieuStage_IN
END
GO

CREATE PROC pAddSetMilieuStage
	@Id_IN INT,
	@Titre_IN VARCHAR(100),
	@Description_IN VARCHAR(1000),
	@NoCivique_IN VARCHAR(10),
	@Rue_IN VARCHAR(100),
	@CodePostal_IN VARCHAR(7),
	@Ville_IN VARCHAR(100),
	@Province_IN VARCHAR(100),
	@Pays_IN VARCHAR(100),
	@NoTelephone_IN VARCHAR(20),
	@Etat_IN BIT,
	@IDRestriction_IN Varchar(4000)
AS
	DECLARE @isNew Bit;
	IF @Id_IN = 0
	BEGIN
		INSERT INTO [dbo].[MilieuStage]
           ([Titre],[Description],[NoCivique],[Rue],[CodePostal],[Ville],[Province],[Pays],[NoTelephone],[Etat])
		 VALUES
           (@Titre_IN,@Description_IN,@NoCivique_IN,@Rue_IN,@CodePostal_IN,@Ville_IN,@Province_IN,@Pays_IN,@NoTelephone_IN,@Etat_IN) 
		SET @Id_IN =SCOPE_IDENTITY()
		SET @isNew = 1
	END
	ELSE
	BEGIN
		UPDATE  [dbo].[MilieuStage]
		SET [Titre]					= @Titre_IN,
			[Description]			= @Description_IN,
			[NoCivique]				= @NoCivique_IN,
			[Rue]					= @Rue_IN,
			[CodePostal]			= @CodePostal_IN,
			[Ville]					= @Ville_IN,
			[Province]				= @Province_IN,
			[Pays]					= @Pays_IN,
			[NoTelephone]			= @NoTelephone_IN,
			[Etat]					= @Etat_IN,
			[DateHeureModification] = GETDATE()
		WHERE [IDMilieuStage] = @Id_IN
		SET @isNew = 0
	END
	EXEC pAddSetMilieuStageRestriction @Id_IN,@IDRestriction_IN,@isNew
GO

--exec pAddSetMilieuStage'3','Test','test','213','Avenue','G0M 0G4','ROKE','SOLYD','MEME','6(969)-696-9696','0';

CREATE PROC [dbo].[pGetMilieuStage](
@Titre_IN VARCHAR(100) = '',@Adresse_IN VARCHAR(1000) ='',@isActive_IN BIT = 0,@isInactive_IN BIT = 0
)AS
	
DECLARE @SQL NVARCHAR(4000)

SET @SQL = 'SELECT [IDMilieuStage], [Titre], [Description], [NoCivique], [Rue], [CodePostal], [Ville], [Province], [Pays], [NoTelephone], [Etat] '
         + 'FROM MilieuStage WHERE 0 = 0 '
	
IF @Titre_IN <> ''
    SET @SQL = @SQL + ' AND Titre LIKE ''%' + @Titre_IN + '%'' '

IF @Adresse_IN <> ''
    SET @SQL = @SQL + ' AND NoCivique + '' '' + Rue + '', '' + Ville + ''   '' + Province + '', '' + Pays LIKE ''%' + @Adresse_IN + '%'' '

IF @isActive_IN = 1 OR @isInactive_IN = 1
BEGIN
    SET @SQL = @SQL + ' AND ( 1 = 0 '

    IF @isActive_IN = 1
        SET @SQL = @SQL + ' OR MilieuStage.Etat = 1 '

    IF @isInactive_IN = 1
        SET @SQL = @SQL + ' OR MilieuStage.Etat = 0 '

    SET @SQL = @SQL + ' ) '
END
EXEC sp_executesql @SQL
GO

CREATE PROC pGetMilieuStageByTitle(@Titre_IN VARCHAR(100))
AS
BEGIN
SELECT DISTINCT IDMilieuStage FROM MilieuStage WHERE @Titre_IN = Titre
END
GO
