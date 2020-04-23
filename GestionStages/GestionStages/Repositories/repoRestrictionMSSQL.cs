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

            return lesRestrictions;
        }
    }
}
