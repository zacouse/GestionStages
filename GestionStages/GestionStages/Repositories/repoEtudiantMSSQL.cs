using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionStages.Models;
using System.Data.SqlClient;

namespace GestionStages.Repositories
{
    public class repoEtudiantMSSQL: IEtudiantRepository
    {
        protected static SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=GestionStage;Integrated Security=True");
        protected SqlDataReader dr;
        protected SqlCommand sql;

        public List<Etudiant> GetAllEtudiants()
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
    }
}
