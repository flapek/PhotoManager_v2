﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PhotoManager_v2.DataBase.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PhotoManagerDataBase_v2Entities : DbContext
    {
        public PhotoManagerDataBase_v2Entities()
            : base("name=PhotoManagerDataBase_v2Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<Folder> Folder { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
    }
}
