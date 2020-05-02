using GestionStages.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStages.Repositories
{
    public class repoRestrictionMSSQL : IRestrictionRepository
    {
        protected static SqlConnection conn;
        protected SqlDataReader dr;
        protected SqlCommand sql;

        public repoRestrictionMSSQL(IConfiguration configuration)
        {
            conn = new SqlConnection(configuration.GetConnectionString("conn"));
        }

        public Restriction GetRestrictionByID(int id)
        {
            Restriction laRestriction = new Restriction();

            sql = new SqlCommand("pGetRestrictionByID", conn);
            sql.CommandType = CommandType.StoredProcedure;

            sql.Parameters.Add("@IDRestriction_IN", SqlDbType.Int).Value = id;

            conn.Open();
            dr = sql.ExecuteReader();
            while (dr.Read())
            {
                laRestriction.IDRestriction = (int)dr.GetValue(0);
                laRestriction.Titre = (string)dr.GetValue(1);
                laRestriction.Description = (string)dr.GetValue(2);
                laRestriction.Etat = (bool)dr.GetValue(3);
            }
            conn.Close();

            return laRestriction;
        }
        public List<Restriction> GetAllRestriction()
        {
            List<Restriction> lesRestrictions = new List<Restriction>();
            sql = new SqlCommand();
            try
            {
                conn.Open();
                sql.Connection = conn;
                sql.CommandText = "EXEC pGetAllRestriction";
                dr = sql.ExecuteReader();
                while (dr.Read())
                {
                    Restriction restriction = new Restriction();
                    restriction.IDRestriction = (int)dr.GetValue(0);
                    restriction.Titre = (string)dr.GetValue(1);
                    restriction.Description = (string)dr.GetValue(2);
                    restriction.Etat = (bool)dr.GetValue(3);
                    lesRestrictions.Add(restriction);
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
            return lesRestrictions;
        }
        public List<Restriction> GetRestrictions(string titre, string descr)
        {
            List<Restriction> lesRestrictions = new List<Restriction>();

            sql = new SqlCommand("pGetRestriction", conn);
            sql.CommandType = CommandType.StoredProcedure;

            sql.Parameters.Add("@Titre_IN", SqlDbType.VarChar).Value = titre;
            sql.Parameters.Add("@Descr_IN", SqlDbType.VarChar).Value = descr;

            conn.Open();
            dr = sql.ExecuteReader();
            while (dr.Read())
            {
                Restriction restriction = new Restriction();
                restriction.IDRestriction = (int)dr.GetValue(0);
                restriction.Titre = (string)dr.GetValue(1);
                restriction.Description = (string)dr.GetValue(2);
                restriction.Etat = (bool)dr.GetValue(3);
                lesRestrictions.Add(restriction);
            }
            conn.Close();

            return lesRestrictions;
        }

        public void SaveRestriction(int id, string titre, string descr, bool etat)
        {
            sql = new SqlCommand("pAddSetRestriction", conn);
            sql.CommandType = CommandType.StoredProcedure;

            sql.Parameters.Add("@IDRestriction_IN", SqlDbType.Int).Value = id;
            sql.Parameters.Add("@Titre_IN", SqlDbType.VarChar).Value = titre;
            sql.Parameters.Add("@Descr_IN", SqlDbType.VarChar).Value = descr;
            sql.Parameters.Add("@Etat_IN", SqlDbType.Bit).Value = etat;

            conn.Open();
            sql.ExecuteNonQuery();
            conn.Close();
        }

        public List<int> GetRestrictionIDFromStageID(int StageId)
        {
            List<int> lesIds = new List<int>();
            sql = new SqlCommand("pGetRestrictionIdByIdStage", conn);
            sql.CommandType = CommandType.StoredProcedure;

            sql.Parameters.Add("@IDStage_IN", SqlDbType.Int).Value = StageId;

            conn.Open();
            dr = sql.ExecuteReader();
            while (dr.Read())
            {
                lesIds.Add((int)dr.GetValue(0));
            }
            conn.Close();

            return lesIds;
        }
        public List<int> GetRestrictionIDFromMilieuStageID(int MilieuStageId)
        {
            List<int> lesIds = new List<int>();
            sql = new SqlCommand("pGetRestrictionIdByIdMilieu", conn);
            sql.CommandType = CommandType.StoredProcedure;

            sql.Parameters.Add("@IDMilieu_IN", SqlDbType.Int).Value = MilieuStageId;

            conn.Open();
            dr = sql.ExecuteReader();
            while (dr.Read())
            {
                lesIds.Add((int)dr.GetValue(0));
            }
            conn.Close();

            return lesIds;
        }

        public List<Restriction> GetAllStageRestrictionByIdStage(int idStage)
        {
            List<Restriction> lesRestriction = new List<Restriction>();
            sql = new SqlCommand("pGetAllStageRestrictionByIdStage", conn);
            sql.CommandType = CommandType.StoredProcedure;
            sql.Parameters.Add("@IDStage_IN", SqlDbType.Int).Value = idStage;
            conn.Open();
            dr = sql.ExecuteReader();
            while (dr.Read())
            {
                Restriction restriction = new Restriction();
                restriction.Titre = (string)dr.GetValue(0);
                restriction.Description = (string)dr.GetValue(1);
                lesRestriction.Add(restriction);
            }
            conn.Close();
            return lesRestriction;
        }

        public List<Restriction> GetAllMilieuStageRestrictionByIdMilieuStage(int idMilieuStage)
        {
            List<Restriction> lesRestriction = new List<Restriction>();
            sql = new SqlCommand("pGetAllMilieuStageRestrictionByIdMilieuStage", conn);
            sql.CommandType = CommandType.StoredProcedure;
            sql.Parameters.Add("@IDMilieuStage_IN", SqlDbType.Int).Value = idMilieuStage;
            conn.Open();
            dr = sql.ExecuteReader();
            while (dr.Read())
            {
                Restriction restriction = new Restriction();
                restriction.Titre = (string)dr.GetValue(0);
                restriction.Description = (string)dr.GetValue(1);
                lesRestriction.Add(restriction);
            }
            conn.Close();
            return lesRestriction;
        }
    }
}
