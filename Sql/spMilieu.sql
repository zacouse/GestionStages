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
	@Etat_IN BIT
AS
	IF @Id_IN = 0
	BEGIN
		INSERT INTO [dbo].[MilieuStage]
           ([Titre],[Description],[NoCivique],[Rue],[CodePostal],[Ville],[Province],[Pays],[NoTelephone],[Etat])
     VALUES
           (@Titre_IN,@Description_IN,@NoCivique_IN,@Rue_IN,@CodePostal_IN,@Ville_IN,@Province_IN,@Pays_IN,@NoTelephone_IN,@Etat_IN) 
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
	END
GO

--exec pAddSetMilieuStage'3','Test','test','213','Avenue','G0M 0G4','ROKE','SOLYD','MEME','6(969)-696-9696','0';

CREATE PROC pGetMilieuStage(
@Titre_IN VARCHAR(100) = '',@Adresse_IN VARCHAR(1000) =''
)AS
	
DECLARE @SQL NVARCHAR(4000)

SET @SQL = 'SELECT [IDMilieuStage], [Titre], [Description], [NoCivique], [Rue], [CodePostal], [Ville], [Province], [Pays], [NoTelephone], [Etat] '
         + 'FROM MilieuStage '

IF @Titre_IN <> '' OR @Adresse_IN <> ''
    SET @SQL = @SQL + ' WHERE 0 = 0 '
	
IF @Titre_IN <> ''
    SET @SQL = @SQL + ' AND Titre LIKE ''%' + @Titre_IN + '%'' '

IF @Adresse_IN <> ''
    SET @SQL = @SQL + ' AND NoCivique + '' '' + Rue + '', '' + Ville + ''   '' + Province + '', '' + Pays LIKE ''%' + @Adresse_IN + '%'' '

EXEC sp_executesql @SQL
GO
