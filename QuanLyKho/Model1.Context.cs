﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyKho
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QuanLyKhoHangEntities : DbContext
    {
        public QuanLyKhoHangEntities()
            : base("name=QuanLyKhoHangEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
    }
}
