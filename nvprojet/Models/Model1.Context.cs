﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TransportEntities : DbContext
    {
        public TransportEntities()
            : base("name=TransportEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<abennement> abennements { get; set; }
        public virtual DbSet<administrateur> administrateurs { get; set; }
        public virtual DbSet<autocar> autocars { get; set; }
        public virtual DbSet<client> clients { get; set; }
        public virtual DbSet<demande> demandes { get; set; }
        public virtual DbSet<societe> societes { get; set; }
        public virtual DbSet<trajet> trajets { get; set; }
    }
}
