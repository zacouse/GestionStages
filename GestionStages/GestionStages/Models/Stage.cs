using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionStages.Properties;

namespace GestionStages.Models
{
    public class Stage
    {
        public enum StatutTemps
        {
            TempsPlein,
            TempsPartiel
        }
        public enum PeriodeDeTravail
        {
            Jour,
            Soir,
            Nuit
        }

        public int IDStage { get; set; }
        public int IDMilieuStage { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public int NbPostes { get; set; }
        public int Statut { get; set; }//Peut être changé dans le futur
        public int PeriodeTravail { get; set; }//Peut être changé dans le futur
        public int NbHeureSemaine { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public bool Etat { get; set; }
        public string TitreMilieuStage { get; set; }

        private string[] periode = { lang.Jour, lang.Soiree, lang.Nuit};
        private string[] status = { lang.TempsPleins, lang.TempsPartiel };

        public Stage()
        {
            IDStage = 0;
            IDMilieuStage = 0;
            Titre = "";
            Description = "";
            NbPostes = 0;
            Statut = 0;
            PeriodeTravail = 0;
            NbHeureSemaine = 0;
            DateDebut = DateTime.MinValue;
            DateFin = DateTime.MinValue;
            Etat = false;
        }

        public Stage(int idstage, int idmilieu, string titre, string descr, int nbpostes, int statut, int periode, int nbheure, DateTime datedebut, DateTime datefin, bool etat)
        {
            IDStage = idstage;
            IDMilieuStage = idmilieu;
            Titre = titre;
            Description = descr;
            NbPostes = nbpostes;
            Statut = statut;
            PeriodeTravail = periode;
            NbHeureSemaine = nbheure;
            DateDebut = datedebut;
            DateFin = datefin;
            Etat = etat;
        }

        public string GetPeriode()
        {
            string periodeString;
            try 
            {
                periodeString = periode[PeriodeTravail];
            }
            catch
            {
                periodeString = "";
            }

            return periodeString;
        }

        public string GetStatus()
        {
            string statusString;
            try
            {
                statusString = status[Statut];
            }
            catch
            {
                statusString = "";
            }

            return statusString;
        }
    }
}
