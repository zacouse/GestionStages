using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionStages.Models;
using System.Data.SqlClient;

namespace GestionStages.Repositories
{
    public class repoMSSQL : Repository
    {
        private static SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=GestionStage;Integrated Security=True");
        private SqlDataReader dr;
        private SqlCommand sql;
        public override List<MilieuStage> GetAllMilieuStage()
        {
            List<MilieuStage> lesMilieus = new List<MilieuStage>();
            sql = new SqlCommand();
            try
            {
                conn.Open();
                sql.Connection = conn;
                sql.CommandText = "EXEC pGetAllMilieuStage";
                dr = sql.ExecuteReader();
                while (dr.Read())
                {
                    MilieuStage milieu = new MilieuStage();
                    milieu.IDMilieuStage = (int)dr.GetValue(0);
                    milieu.Titre = (string)dr.GetValue(1);
                    milieu.Description = (string)dr.GetValue(2);
                    milieu.NoCivique = (string)dr.GetValue(3);
                    milieu.Rue = (string)dr.GetValue(4);
                    milieu.CodePostal = (string)dr.GetValue(5);
                    milieu.Ville = (string)dr.GetValue(6);
                    milieu.Province = (string)dr.GetValue(7);
                    milieu.Pays = (string)dr.GetValue(8);
                    milieu.NoTelephone = (string)dr.GetValue(9);
                    milieu.Etat = (bool)dr.GetValue(10);
                    lesMilieus.Add(milieu);
                }
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
            return lesMilieus;
        }

        public override List<Etudiant> GetAllEtudiants()
        {
            List<Etudiant> lesEtudiants = new List<Etudiant>();
            sql = new SqlCommand();
            try
            {
                conn.Open();
                sql.Connection = conn;
                sql.CommandText = "EXEC pGetAllActiveEtudiant";
                dr = sql.ExecuteReader();
                while (dr.Read())
                {
                    Etudiant etudiant = new Etudiant();
                    etudiant.IDEtudiant = (int)dr.GetValue(0);
                    etudiant.Programme = (string)dr.GetValue(1);
                    etudiant.NoDA = (int)dr.GetValue(2);
                    etudiant.Prenom = (string)dr.GetValue(3);
                    etudiant.Nom = (string)dr.GetValue(4);
                    etudiant.Courriel = (string)dr.GetValue(5);
                    lesEtudiants.Add(etudiant);
                }
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
            return lesEtudiants;
        }

        public override Stage pAddSetStage(int idStage, int idMilieuStage, string titre, string description, int nbPoste, int statut, int periodeTravail, int nbHeureSemaine, DateTime dateDebut, DateTime dateFin, bool etat, DateTime dateHeureCreation)
        {
            Stage LeStage = new Stage();
            sql = new SqlCommand();
            try
            {
                sql = new SqlCommand("pAddSetStage", conn);
                sql.CommandType = System.Data.CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@IDStage_IN", idStage);
                sql.Parameters.AddWithValue("@IDMilieuStage_IN", idMilieuStage);
                sql.Parameters.AddWithValue("@Titre_IN", titre);
                sql.Parameters.AddWithValue("@Description_IN", description);
                sql.Parameters.AddWithValue("@NbPoste_IN", nbPoste);
                sql.Parameters.AddWithValue("@Statut_IN", statut);
                sql.Parameters.AddWithValue("@PeriodeTravail_IN", periodeTravail);
                sql.Parameters.AddWithValue("@NbHeureSemaine_IN", nbHeureSemaine);
                sql.Parameters.AddWithValue("@DateDebut_IN", dateDebut);
                sql.Parameters.AddWithValue("@DateFin_IN", dateFin);
                sql.Parameters.AddWithValue("@Etat_IN", etat);
                sql.Parameters.AddWithValue("@DateHeureCreation", dateHeureCreation);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
            return LeStage;
        }
    }
}
