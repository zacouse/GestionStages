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
                    stage.Statut = (byte)dr.GetValue(5);
                    stage.PeriodeTravail = (byte)dr.GetValue(6);
                    stage.NbHeureSemaine = (int)dr.GetValue(7);
                    stage.DateDebut = (DateTime)dr.GetValue(8);
                    stage.DateFin = (DateTime)dr.GetValue(9);
                    stage.Etat = (bool)dr.GetValue(10);
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
        public List<Stage> GetAllStageActif()
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
                    if (stage.Etat == true)
                    {
                        stage.IDStage = (int)dr.GetValue(0);
                        stage.IDMilieuStage = (int)dr.GetValue(1);
                        stage.Titre = (string)dr.GetValue(2);
                        stage.Description = (string)dr.GetValue(3);
                        stage.NbPostes = (int)dr.GetValue(4);
                        stage.Statut = (byte)dr.GetValue(5);
                        stage.PeriodeTravail = (byte)dr.GetValue(6);
                        stage.NbHeureSemaine = (int)dr.GetValue(7);
                        stage.DateDebut = (DateTime)dr.GetValue(8);
                        stage.DateFin = (DateTime)dr.GetValue(9);
                        stage.Etat = (bool)dr.GetValue(10);
                        lesStages.Add(stage);
                    }
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
        public List<Stage> GetAllStageInactif()
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
                    if (stage.Etat == false)
                    {
                        stage.IDStage = (int)dr.GetValue(0);
                        stage.IDMilieuStage = (int)dr.GetValue(1);
                        stage.Titre = (string)dr.GetValue(2);
                        stage.Description = (string)dr.GetValue(3);
                        stage.NbPostes = (int)dr.GetValue(4);
                        stage.Statut = (byte)dr.GetValue(5);
                        stage.PeriodeTravail = (byte)dr.GetValue(6);
                        stage.NbHeureSemaine = (int)dr.GetValue(7);
                        stage.DateDebut = (DateTime)dr.GetValue(8);
                        stage.DateFin = (DateTime)dr.GetValue(9);
                        stage.Etat = (bool)dr.GetValue(10);
                        lesStages.Add(stage);
                    }
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
        public void teststage(string test)
        {
            string valueType = test;
        }
        public void SaveStage(Stage stage)
        {
            sql = new SqlCommand();
            try
            {
                conn.Open();
                sql.Connection = conn;
                sql.CommandText = "EXEC pAddSetStage'" + stage.IDStage + "','" + stage.IDMilieuStage + "','" + stage.Titre + "','" +
                    stage.Description + "','" + stage.NbPostes + "','" + stage.Statut + "','" + stage.PeriodeTravail + "','" + 
                    stage.NbHeureSemaine + "','" + stage.DateDebut + "','" + stage.DateFin + "','" + stage.Etat + "'";
                sql.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public Stage GetStageByID(int stageId)
        {
            Stage stage = new Stage();
            sql = new SqlCommand();
            try
            {
                conn.Open();
                sql.Connection = conn;
                sql.CommandText = "EXEC pGetStageByID'" + stageId +"'";
                dr = sql.ExecuteReader();
                while (dr.Read())
                {
                        stage.IDStage = (int)dr.GetValue(0);
                        stage.IDMilieuStage = (int)dr.GetValue(1);
                        stage.Titre = (string)dr.GetValue(2);
                        stage.Description = (string)dr.GetValue(3);
                        stage.NbPostes = (int)dr.GetValue(4);
                        stage.Statut = (byte)dr.GetValue(5);
                        stage.PeriodeTravail = (byte)dr.GetValue(6);
                        stage.NbHeureSemaine = (int)dr.GetValue(7);
                        stage.DateDebut = (DateTime)dr.GetValue(8);
                        stage.DateFin = (DateTime)dr.GetValue(9);
                        stage.Etat = (bool)dr.GetValue(10);
                        stage.TitreMilieuStage = (string)dr.GetValue(11);
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
            return stage;
        }

        public List<Stage> GetStagesByIdMilieu(int milieu)
        {
            List<Stage> lesStages = new List<Stage>();
            sql = new SqlCommand("pGetStagesByIdMilieu", conn);
            sql.CommandType = CommandType.StoredProcedure;

            sql.Parameters.Add("@IdMilieu_IN", SqlDbType.Int).Value = milieu;

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
                stage.Statut = (byte)dr.GetValue(5);
                stage.PeriodeTravail = (byte)dr.GetValue(6);
                stage.NbHeureSemaine = (int)dr.GetValue(7);
                stage.DateDebut = (DateTime)dr.GetValue(8);
                stage.DateFin = (DateTime)dr.GetValue(9);
                stage.Etat = (bool)dr.GetValue(10);
                lesStages.Add(stage);
            }
            conn.Close();

            return lesStages;
        }

        public List<Stage> GetStage(string titre, string descr, string milieu, int minh, int maxh, string minDate, string maxDate)
        {
            List<Stage> lesStages = new List<Stage>();
            sql = new SqlCommand("pGetStage", conn);
            sql.CommandType = CommandType.StoredProcedure;

            DateTime? varMinDate = null;
            DateTime? varMaxDate = null;

            if (minDate != null)
            {
                varMinDate = DateTime.Parse(minDate);
            }

            if (maxDate != null)
            {
                varMaxDate = DateTime.Parse(maxDate);
            }

            sql.Parameters.Add("@Titre_IN", SqlDbType.VarChar).Value = titre;
            sql.Parameters.Add("@Description_IN", SqlDbType.VarChar).Value = descr;
            sql.Parameters.Add("@Milieu_IN", SqlDbType.VarChar).Value = milieu;
            sql.Parameters.Add("@Minh_IN", SqlDbType.Int).Value = minh;
            sql.Parameters.Add("@Maxh_IN", SqlDbType.Int).Value = maxh;
            sql.Parameters.Add("@MinDate_IN", SqlDbType.DateTime).Value = varMinDate;
            sql.Parameters.Add("@MaxDate_IN", SqlDbType.DateTime).Value = varMaxDate;

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
                stage.Statut = (byte)dr.GetValue(5);
                stage.PeriodeTravail = (byte)dr.GetValue(6);
                stage.NbHeureSemaine = (int)dr.GetValue(7);
                stage.DateDebut = (DateTime)dr.GetValue(8);
                stage.DateFin = (DateTime)dr.GetValue(9);
                stage.Etat = (bool)dr.GetValue(10);
                stage.TitreMilieuStage = (string)dr.GetValue(11);
                lesStages.Add(stage);
            }
            conn.Close();

            return lesStages;
        }
    }
}
