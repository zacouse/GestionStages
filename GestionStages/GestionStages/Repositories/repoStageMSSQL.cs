using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionStages.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace GestionStages.Repositories
{
    public class repoStageMSSQL : IStageRepository
    {
        protected static SqlConnection conn;
        protected SqlDataReader dr;
        protected SqlCommand sql;

        public repoStageMSSQL(IConfiguration configuration)
        {
            conn = new SqlConnection(configuration.GetConnectionString("conn"));
        }

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
        public List<Stage> GetAllStageActif()
        {
            List<Stage> lesStages = new List<Stage>();
            sql = new SqlCommand();
            try
            {
                conn.Open();
                sql.Connection = conn;
                sql.CommandText = "EXEC pGetAllStageActif";
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
                        stage.TitreMilieuStage = (string)dr.GetValue(11);
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
        public void SaveStage(Stage stage, string idRestriction)
        {
            sql = new SqlCommand("pAddSetStage", conn);
            try
            {
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.Add("@IDStage_IN", SqlDbType.Int).Value = stage.IDStage;
                sql.Parameters.Add("@IDMilieuStage_IN", SqlDbType.Int).Value = stage.IDMilieuStage;
                sql.Parameters.Add("@Titre_IN", SqlDbType.VarChar).Value = stage.Titre;
                sql.Parameters.Add("@Description_IN", SqlDbType.VarChar).Value = stage.Description;
                sql.Parameters.Add("@NbPostes_IN", SqlDbType.Int).Value = stage.NbPostes;
                sql.Parameters.Add("@Statut_IN", SqlDbType.Int).Value = stage.Statut;
                sql.Parameters.Add("@PeriodeTravail_IN", SqlDbType.Int).Value = stage.PeriodeTravail;
                sql.Parameters.Add("@NbHeureSemaine_IN", SqlDbType.Int).Value = stage.NbHeureSemaine;
                sql.Parameters.Add("@DateDebut_IN", SqlDbType.DateTime).Value = stage.DateDebut;
                sql.Parameters.Add("@DateFin_IN", SqlDbType.DateTime).Value = stage.DateFin;
                sql.Parameters.Add("@Etat_IN", SqlDbType.Bit).Value = stage.Etat;
                sql.Parameters.Add("@IDRestriction_IN", SqlDbType.VarChar).Value = idRestriction;
                conn.Open();
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
                sql.CommandText = "EXEC pGetStageByID'" + stageId + "'";
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

        public List<Stage> GetStage(string titre, string descr, string milieu, int minh, int maxh, string minDate, string maxDate, bool chkIsJour, bool chkIsSoir, bool chkIsNuit, bool chkIsActive, bool chkIsInactive)
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
            sql.Parameters.Add("@isJour_IN", SqlDbType.Bit).Value = chkIsJour;
            sql.Parameters.Add("@isSoir_IN", SqlDbType.Bit).Value = chkIsSoir;
            sql.Parameters.Add("@isNuit_IN", SqlDbType.Bit).Value = chkIsNuit;
            sql.Parameters.Add("@isActive_IN", SqlDbType.Bit).Value = chkIsActive;
            sql.Parameters.Add("@isInactive_IN", SqlDbType.Bit).Value = chkIsInactive;

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

        public void SaveChoixStage(ChoixStageEtudiant choixStageEtudiant)
        {

            try
            {
                sql = new SqlCommand("pAddSetChoixStage", conn);
                sql.CommandType = CommandType.StoredProcedure;

                sql.Parameters.Add("@IdStageEtudiant_IN", SqlDbType.Int).Value = choixStageEtudiant.IDChoixStageEtudiant;
                sql.Parameters.Add("@IdStage_IN", SqlDbType.Int).Value = choixStageEtudiant.IDStage;
                sql.Parameters.Add("@IdEtudiant_IN", SqlDbType.Int).Value = choixStageEtudiant.IDEtudiant;
                sql.Parameters.Add("@NumeroChoix_IN", SqlDbType.Int).Value = choixStageEtudiant.NumeroChoix;
                sql.Parameters.Add("@ChoixFinal_IN", SqlDbType.Bit).Value = choixStageEtudiant.ChoixFinal;
                sql.Parameters.Add("@Etat_IN", SqlDbType.Bit).Value = choixStageEtudiant.Etat;


                conn.Open();
                int row = sql.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
        }

        public List<ChoixStageEtudiant> getChoixStage(string idEtudiant)
        {
            List<ChoixStageEtudiant> lesChoix = new List<ChoixStageEtudiant>();
            sql = new SqlCommand("pGetChoixStageByIdEtudiant", conn);
            sql.CommandType = CommandType.StoredProcedure;

            sql.Parameters.Add("@IdEtudiant_IN", SqlDbType.Int).Value = idEtudiant;

            conn.Open();
            dr = sql.ExecuteReader();
            while (dr.Read())
            {
                ChoixStageEtudiant choix = new ChoixStageEtudiant();
                choix.IDChoixStageEtudiant = (int)dr.GetValue(0);
                choix.IDStage = (int)dr.GetValue(1);
                choix.IDEtudiant = (int)dr.GetValue(2);
                choix.NumeroChoix = (int)dr.GetValue(3);
                choix.ChoixFinal = (bool)dr.GetValue(4);
                choix.Etat = (bool)dr.GetValue(5);
                lesChoix.Add(choix);
            }
            conn.Close();

            return lesChoix;
        }

        public void RemoveChoixStage(int idEtudiant, int numeroChoix)
        {

            sql = new SqlCommand("pAddSetRetirerChoixStage", conn);
            sql.CommandType = CommandType.StoredProcedure;

            sql.Parameters.Add("@IdEtudiant_IN", SqlDbType.Int).Value = idEtudiant;
            sql.Parameters.Add("@NumeroChoix_IN", SqlDbType.Int).Value = numeroChoix;

            conn.Open();
            int row = sql.ExecuteNonQuery();

            conn.Close();


        }
    }
}
