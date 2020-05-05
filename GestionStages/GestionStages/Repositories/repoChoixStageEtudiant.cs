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
    public class repoChoixStageEtudiant : IChoixStageEtudiantRepository
    {
        protected static SqlConnection conn;
        protected SqlDataReader dr;
        protected SqlCommand sql;

        public repoChoixStageEtudiant(IConfiguration configuration)
        {
            conn = new SqlConnection(configuration.GetConnectionString("conn"));
        }

        public void SaveChoixStage(int IDChoixStageEtudiant, int IDStage, int IDEtudiant, int NumeroChoix, bool ChoixFinal, bool Etat)
        {
            try
            {
                sql = new SqlCommand("pAddSetChoixStage", conn);
                sql.CommandType = CommandType.StoredProcedure;

                sql.Parameters.Add("@IdStageEtudiant_IN", SqlDbType.Int).Value = IDChoixStageEtudiant;
                sql.Parameters.Add("@IdStage_IN", SqlDbType.Int).Value = IDStage;
                sql.Parameters.Add("@IdEtudiant_IN", SqlDbType.Int).Value = IDEtudiant;
                sql.Parameters.Add("@NumeroChoix_IN", SqlDbType.Int).Value = NumeroChoix;
                sql.Parameters.Add("@ChoixFinal_IN", SqlDbType.Bit).Value = ChoixFinal;
                sql.Parameters.Add("@Etat_IN", SqlDbType.Bit).Value = Etat;

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
                choix.setIDStage((int)dr.GetValue(1));
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
