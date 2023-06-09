﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace THPTPhuBinh.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class thuvienEntities : DbContext
    {
        public thuvienEntities()
            : base("name=thuvienEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; }
        public virtual DbSet<DocGia> DocGias { get; set; }
        public virtual DbSet<LoaiDG> LoaiDGs { get; set; }
        public virtual DbSet<LoaiTK> LoaiTKs { get; set; }
        public virtual DbSet<LuaChon> LuaChons { get; set; }
        public virtual DbSet<Nguoi> Nguois { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<PhieuMuon> PhieuMuons { get; set; }
        public virtual DbSet<PhieuPhat> PhieuPhats { get; set; }
        public virtual DbSet<Sach> Saches { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<TheLoai> TheLoais { get; set; }
        public virtual DbSet<TheThuVien> TheThuViens { get; set; }
        public virtual DbSet<TrangThai> TrangThais { get; set; }
    
        public virtual int CreateAccount(string idAccount, string password, Nullable<int> type, string idStaff, string name, Nullable<System.DateTime> birth, string address, string phone)
        {
            var idAccountParameter = idAccount != null ?
                new ObjectParameter("IdAccount", idAccount) :
                new ObjectParameter("IdAccount", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var typeParameter = type.HasValue ?
                new ObjectParameter("Type", type) :
                new ObjectParameter("Type", typeof(int));
    
            var idStaffParameter = idStaff != null ?
                new ObjectParameter("IdStaff", idStaff) :
                new ObjectParameter("IdStaff", typeof(string));
    
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var birthParameter = birth.HasValue ?
                new ObjectParameter("Birth", birth) :
                new ObjectParameter("Birth", typeof(System.DateTime));
    
            var addressParameter = address != null ?
                new ObjectParameter("Address", address) :
                new ObjectParameter("Address", typeof(string));
    
            var phoneParameter = phone != null ?
                new ObjectParameter("Phone", phone) :
                new ObjectParameter("Phone", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateAccount", idAccountParameter, passwordParameter, typeParameter, idStaffParameter, nameParameter, birthParameter, addressParameter, phoneParameter);
        }
    }
}
