﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStages.Models
{
    public class Stage
    {
        public int IDStage { get; set; }
        public int IDMilieuStage { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public int nbPostes { get; set; }
        public int Status { get; set; }//Peut être changé dans le futur
        public int PeriodeTravail { get; set; }//Peut être changé dans le futur
        public int NbHeureSemaine { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public bool Etat { get; set; }
        public string TitreMilieuStage { get; set; }

        private string[] periode = {""};
        private string[] status = {""};

        public Stage()
        {
            IDStage = 0;
            IDMilieuStage = 0;
            Titre = "";
            Description = "";
            nbPostes = 0;
            Status = 0;
            PeriodeTravail = 0;
            NbHeureSemaine = 0;
            DateDebut = DateTime.MinValue;
            DateFin = DateTime.MinValue;
            Etat = false;
        }

        public Stage(int idstage, int idmilieu, string titre, string descr, int nbpostes, int status, int periode, int nbheure, DateTime datedebut, DateTime datefin, bool etat)
        {
            IDStage = idstage;
            IDMilieuStage = idmilieu;
            Titre = titre;
            Description = descr;
            nbPostes = nbpostes;
            Status = status;
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
                statusString = status[Status];
            }
            catch
            {
                statusString = "";
            }

            return statusString;
        }
    }
}
