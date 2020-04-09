using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionStages.Models;
using System.Data.SqlClient;
using System.Data;

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
                    stage.NbPostes = (int)dr.GetValue(4);
                    stage.Status = (byte)dr.GetValue(5);
                    stage.PeriodeTravail = (byte)dr.GetValue(6);
                    stage.NbHeureSemaine = (int)dr.GetValue(7);
                    stage.DateDebut = (DateTime)dr.GetValue(8);
                    stage.DateFin = (DateTime)dr.GetValue(9);
                    stage.Etat = (bool)dr.GetValue(10);
                    stage.TitreMilieuStage = (string)dr.GetValue(11);
                    lesStages.Add(stage);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return lesStages;
        }

        public List<Stage> GetStage(string titre, string descr, string milieu, int minh, int maxh, DateTime minDate, DateTime maxDate)
        {
            List<Stage> lesStages = new List<Stage>();
            try
            {
                sql = new SqlCommand("pGetStage", conn);
                sql.CommandType = CommandType.StoredProcedure;

                sql.Parameters.Add("@Titre_IN", SqlDbType.VarChar).Value = titre;
                sql.Parameters.Add("@Description_IN", SqlDbType.VarChar).Value = descr;
                sql.Parameters.Add("@Milieu_IN", SqlDbType.VarChar).Value = milieu;
                sql.Parameters.Add("@Minh_IN", SqlDbType.Int).Value = minh;
                sql.Parameters.Add("@Maxh_IN", SqlDbType.Int).Value = maxh;
                sql.Parameters.Add("@MinDate_IN", SqlDbType.DateTime).Value = minDate;
                sql.Parameters.Add("@MaxDate_IN", SqlDbType.DateTime).Value = maxDate;

                conn.Open();
                dr = sql.ExecuteReader();
                while (dr.Read())
                {
                    Stage stage = new Stage();
                    stage.IDStage = (int)dr.GetValue(0);
                    stage.IDMilieuStage = (int)dr.GetValue(1);
                    stage.Titre = (string)dr.GetValue(2);
                    stage.Description = (string)dr.GetValue(3);
                    stage.NbPostes = (int)dr.GetValue(4);
                    stage.Status = (byte)dr.GetValue(5);
                    stage.PeriodeTravail = (byte)dr.GetValue(6);
                    stage.NbHeureSemaine = (int)dr.GetValue(7);
                    stage.DateDebut = (DateTime)dr.GetValue(8);
                    stage.DateFin = (DateTime)dr.GetValue(9);
                    stage.Etat = (bool)dr.GetValue(10);
                    stage.TitreMilieuStage = (string)dr.GetValue(11);
                    lesStages.Add(stage);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return lesStages;
        }
    }
}
