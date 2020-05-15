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
    public class repoSuperviseurMSSQL: ISuperviseurRepository
    {
        protected static SqlConnection conn;
        protected SqlDataReader dr;
        protected SqlCommand sql;

        public repoSuperviseurMSSQL(IConfiguration configuration)
        {
            conn = new SqlConnection(configuration.GetConnectionString("conn"));
        }

        public List<Superviseur> GetAllActiveSuperviseur()
        {
            List<Superviseur> lesSuperviseurs = new List<Superviseur>();
            sql = new SqlCommand();

            conn.Open();
            sql.Connection = conn;
            sql.CommandText = "EXEC pGetAllSuperviseur";
            dr = sql.ExecuteReader();
            while (dr.Read())
            {
                Superviseur superviseur = new Superviseur();
                superviseur.IDSuperviseur = (int)dr.GetValue(0);
                superviseur.Nom = (string)dr.GetValue(1);
                superviseur.Prenom = (string)dr.GetValue(2);
                superviseur.Courriel = (string)dr.GetValue(3);
                superviseur.Etat = (bool)dr.GetValue(4);
                lesSuperviseurs.Add(superviseur);
            }
            conn.Close();
            return lesSuperviseurs;
        }

        public Superviseur GetPersonneContactByStageID(int stageID)
        {
            Superviseur personneContact = new Superviseur();

            //sql = new SqlCommand("pGetPersonneContactByStageID", conn);

            //sql.CommandType = CommandType.StoredProcedure;

            //sql.Parameters.Add("@IDStage_IN", SqlDbType.Int).Value = stageID;
            //conn.Open();
            //dr = sql.ExecuteReader();
            //while (dr.Read())
            //{
            //    personneContact.IDPersonneContact = (int)dr.GetValue(0);
            //    personneContact.Nom = (string)dr.GetValue(1);
            //    personneContact.Prenom = (string)dr.GetValue(2);
            //    personneContact.Courriel = (string)dr.GetValue(3);
            //    personneContact.Etat = (bool)dr.GetValue(4);
            //}
            //conn.Close();

            return personneContact;
        }
    }
}
