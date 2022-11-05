using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nvprojet.Models
{
    public class offre
    {
        public int id { get; set; }
        public string nom_s { get; set; }
        public string img { get; set; }
        public Nullable<bool> wifi { get; set; }
        public Nullable<int> Nbr_place { get; set; }
        public string ville_depart { get; set; }
        public string ville_arriver { get; set; }
        public string heure_dep { get; set; }
        public string heur_arv { get; set; }
    }
}