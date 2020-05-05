using GestionStages.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStages.Repositories
{
    public class repoPersonneContactMSSQL:IPersonneContactRepository
    {
        protected static SqlConnection conn;
        protected SqlDataReader dr;
        protected SqlCommand sql;

        public repoPersonneContactMSSQL(IConfiguration configuration)
        {
            conn = new SqlConnection(configuration.GetConnectionString("conn"));
        }

        public List<PersonneContact> GetAllActivePersonneContact()
        {
            List<PersonneContact> lesPersonnesContact = new List<PersonneContact>();
            sql = new SqlCommand();

            conn.Open();
            sql.Connection = conn;
            sql.CommandText = "EXEC pGetAllActivePersonneContact";
            dr = sql.ExecuteReader();
            while (dr.Read())
            {
                PersonneContact personneContact = new PersonneContact();
                personneContact.IDPersonneContact = (int)dr.GetValue(0);
                personneContact.Nom = (string)dr.GetValue(1);
                personneContact.Prenom = (string)dr.GetValue(2);
                personneContact.Courriel = (string)dr.GetValue(3);
                personneContact.Etat = (bool)dr.GetValue(4);
                lesPersonnesContact.Add(personneContact);
            }
            conn.Close();
            return lesPersonnesContact;
        }
    }
}
