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
    public class repoMilieuStageMSSQL : IMilieuStageRepository
    {
        protected static SqlConnection conn;
        protected SqlDataReader dr;
        protected SqlCommand sql;

        public repoMilieuStageMSSQL(IConfiguration configuration)
        {
            conn = new SqlConnection(configuration.GetConnectionString("conn"));
        }

        public List<MilieuStage> GetAllMilieuStage()
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return lesMilieus;
        }
        public MilieuStage GetMilieuStageById(int id)
        {
            MilieuStage milieu = new MilieuStage();
            sql = new SqlCommand();
            try
            {
                conn.Open();
                sql.Connection = conn;
                sql.CommandText = "EXEC pGetMilieuStageById'" + id + "'";
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
        public void SaveMilieuStage(MilieuStage milieu)
        {
            sql = new SqlCommand();
            try
            {
                conn.Open();
                sql.Connection = conn;
                sql.CommandText = "EXEC pAddSetMilieuStage'" + milieu.IDMilieuStage + "','" + milieu.Titre + "','" +
                    milieu.Description + "','" + milieu.NoCivique + "','" + milieu.Rue + "','" + milieu.CodePostal +
                    "','" + milieu.Ville + "','" + milieu.Province + "','" + milieu.Pays + "','" + milieu.NoTelephone +
                    "','" + milieu.Etat + "'";
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
        public List<MilieuStage> GetMilieuStage(string titre, string adresse)
        {
            List<MilieuStage> lesMilieus = new List<MilieuStage>();
            try
            {
                sql = new SqlCommand("pGetMilieuStage",conn);
                sql.CommandType = CommandType.StoredProcedure;

                sql.Parameters.Add("@Titre_IN", SqlDbType.VarChar).Value = titre;
                sql.Parameters.Add("@Adresse_IN", SqlDbType.VarChar).Value = adresse;

                conn.Open();
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
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return lesMilieus;
        }
    }
}
