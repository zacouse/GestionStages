using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionStages.Models;
using System.Data.SqlClient;

namespace GestionStages.Repositories
{
    public class RepoMSSQL : Repository
    {
        private static SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=GestionStage;Integrated Security=True");
        private SqlDataReader dr;
        private SqlCommand sql;
        public override List<MilieuStage> getAllMilieuStage()
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

        public override List<Etudiant> getAllEtudiants()
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

        public override MilieuStage getMilieuStage(int id)
        {
            MilieuStage milieu = new MilieuStage();
            sql = new SqlCommand();
            try
            {
                conn.Open();
                sql.Connection = conn;
                sql.CommandText = "EXEC pGetMilieuStageById'"+id+"'";
                dr = sql.ExecuteReader();
                while (dr.Read())
                {
                    
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
                }
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
            return milieu;
        }
    }
}
