//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace nvprojet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class demande
    {
        public int id { get; set; }
        [Display(Name = "ville depart")]
        [Required(ErrorMessage ="champ obligatoire !!!!!!")]
        public string ville_d { get; set; }
        [Display(Name = "ville arriver")]
        [Required(ErrorMessage = "champ obligatoire !!!!!!")]
        public string ville_a { get; set; }
        [Display(Name = "heure depart")]
        [Required(ErrorMessage = "champ obligatoire !!!!!!")]
        public string heure_d { get; set; }
        [Display(Name = "heure arriver")]
        [Required(ErrorMessage = "champ obligatoire !!!!!!")]
        public string heur_a { get; set; }
        public Nullable<int> id_cli { get; set; }
    
        public virtual client client { get; set; }
    }
}
