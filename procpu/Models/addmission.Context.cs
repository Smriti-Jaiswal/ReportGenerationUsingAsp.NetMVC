﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace procpu.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class db_luccpuEntities : DbContext
    {
        public db_luccpuEntities()
            : base("name=db_luccpuEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblcpugeninfo> tblcpugeninfoes { get; set; }
        public virtual DbSet<tblcpupartmoudtl> tblcpupartmoudtls { get; set; }
        public virtual DbSet<tblstdacademicgradedtl> tblstdacademicgradedtls { get; set; }
        public virtual DbSet<tblstdacademicrecord> tblstdacademicrecords { get; set; }
        public virtual DbSet<tblstdacademicsummary> tblstdacademicsummaries { get; set; }
        public virtual DbSet<tblstdapplicationdtl> tblstdapplicationdtls { get; set; }
        public virtual DbSet<tblstdattachment> tblstdattachments { get; set; }
        public virtual DbSet<tblstddocupload> tblstddocuploads { get; set; }
        public virtual DbSet<tblstdparentdtl> tblstdparentdtls { get; set; }
        public virtual DbSet<tblcountrymast> tblcountrymasts { get; set; }
        public virtual DbSet<tbldoctypemast> tbldoctypemasts { get; set; }
        public virtual DbSet<tblstdtranscript> tblstdtranscripts { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<tblattachmenttype> tblattachmenttypes { get; set; }
    }
}