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

        public override List<Stage> GetAllStage()
        {
            List<Stage> lesStages = new List<Stage>();
            sql = new SqlCommand();
            try
            {
                conn.Open();
                sql.Connection = conn;
                sql.CommandText = "EXEC pGetAllStage";
                dr = sql.ExecuteReader();
                while (dr.Read())
                {
                    Stage stage = new Stage();
                    stage.IDStage = (int)dr.GetValue(0);
                    stage.IDMilieuStage = (int)dr.GetValue(1);
                    stage.Titre = (string)dr.GetValue(2);
                    stage.Description = (string)dr.GetValue(3);
                    stage.nbPostes = (int)dr.GetValue(4);
                    stage.Status = (int)dr.GetValue(5);
                    stage.PeriodeTravail = (int)dr.GetValue(6);
                    stage.NbHeureSemaine = (int)dr.GetValue(7);
                    stage.DateDebut = (DateTime)dr.GetValue(8);
                    stage.DateFin = (DateTime)dr.GetValue(9);
                    stage.Etat = (bool)dr.GetValue(10);
                    lesStages.Add(stage);
                }
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
            return lesStages;
        }
    }
}
