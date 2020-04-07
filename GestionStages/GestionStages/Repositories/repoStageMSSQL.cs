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
            catch
            {

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
        public void AddSetStageV1(Stage stage)
        {
            sql = new SqlCommand("pAddSetStage", conn);
            try
            {
                conn.Open();
                sql.Connection = conn;
                sql.CommandType = System.Data.CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@IDStage_IN", stage.IDStage);
                sql.Parameters.AddWithValue("@IDMilieuStage_IN", stage.IDMilieuStage);
                sql.Parameters.AddWithValue("@Titre_IN", stage.Titre);
                sql.Parameters.AddWithValue("@Description_IN", stage.Description);
                sql.Parameters.AddWithValue("@NbPostes_IN", stage.NbPostes);
                sql.Parameters.AddWithValue("@Statut_IN", stage.Statut);
                sql.Parameters.AddWithValue("@PeriodeTravail_IN", stage.PeriodeTravail);
                sql.Parameters.AddWithValue("@NbHeureSemaine_IN", stage.NbHeureSemaine);
                sql.Parameters.AddWithValue("@DateDebut_IN", stage.DateDebut);
                sql.Parameters.AddWithValue("@DateFin_IN", stage.DateFin);
                sql.Parameters.AddWithValue("@Etat_IN", stage.Etat);
                sql.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {

            }
            finally
            {
                
            }
        }
        public void AddSetStage(Stage stage)
        {
           // sql = new SqlCommand("pAddSetStage", conn);
            sql = new SqlCommand();
            try
            {
                conn.Open();
                sql.Connection = conn;
                sql.CommandText = "EXEC pAddSetStage" + "','" + stage.IDStage + "','" + stage.IDMilieuStage + "','" + stage.Titre + "','" +
                    stage.Description + "','" + stage.NbPostes + "','" + stage.Statut + "','" + stage.PeriodeTravail + "','" + 
                    stage.NbHeureSemaine + "','" + stage.DateDebut + "','" + stage.DateFin + "','" + stage.Etat;
                sql.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
        }
        public Stage GetStageByID(int stageId)
        {
            Stage LeStage = new Stage();
            sql = new SqlCommand();
            try
            {
                conn.Open();
                sql.Connection = conn;
                sql.CommandText = "EXEC pGetStageByID";
                dr = sql.ExecuteReader();
                while (dr.Read())
                {
                    Stage stage = new Stage();
                    if (stage.IDStage == stageId)
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
            return LeStage;
        }
        public void DeleteStage(bool deleted)
        {

        }
    }
}
