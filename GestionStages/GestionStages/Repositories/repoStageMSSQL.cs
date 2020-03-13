using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionStages.Models;
using System.Data.SqlClient;

namespace GestionStages.Repositories
{
    public class repoStageMSSQL : IStageRepository
    {
        protected static SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=GestionStage;Integrated Security=True");
        protected SqlDataReader dr;
        protected SqlCommand sql;
        public List<Stage> GetAllStage()
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
