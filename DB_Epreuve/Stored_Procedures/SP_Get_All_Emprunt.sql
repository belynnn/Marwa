CREATE PROCEDURE [dbo].[SP_Get_All_Emprunt]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Emprunt_Id,
        Jeux_Utilisateur_Id,
        Emprunteur_Id,
        DateEmprunt,
        DateRetour,
        EvaluationPreteur,
        EvaluationEmprunteur
    FROM dbo.Emprunt;
END;
