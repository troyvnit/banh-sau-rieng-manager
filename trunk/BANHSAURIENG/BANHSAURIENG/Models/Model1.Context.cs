﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BANHSAURIENG.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    
    public partial class BSR_Entities : DbContext
    {
        public BSR_Entities()
            : base("name=BSR_Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<CampainDetail> CampainDetails { get; set; }
        public DbSet<EmployeePolicy> EmployeePolicies { get; set; }
        public DbSet<Product_Material> Product_Material { get; set; }
        public DbSet<Supplier_Material> Supplier_Material { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<tblAccount_tblRoles> tblAccount_tblRoles { get; set; }
        public DbSet<tblAccountGroup> tblAccountGroups { get; set; }
        public DbSet<tblAccount> tblAccounts { get; set; }
        public DbSet<tblArticleCat_Article> tblArticleCat_Article { get; set; }
        public DbSet<tblArticleCat> tblArticleCats { get; set; }
        public DbSet<tblArticle> tblArticles { get; set; }
        public DbSet<tblBillDetail> tblBillDetails { get; set; }
        public DbSet<tblBill> tblBills { get; set; }
        public DbSet<tblCampaign> tblCampaigns { get; set; }
        public DbSet<tblCity> tblCities { get; set; }
        public DbSet<tblContact> tblContacts { get; set; }
        public DbSet<tblCustomer> tblCustomers { get; set; }
        public DbSet<tblDistrict> tblDistricts { get; set; }
        public DbSet<tblEmployee> tblEmployees { get; set; }
        public DbSet<tblEmployeeType_tblEmployee> tblEmployeeType_tblEmployee { get; set; }
        public DbSet<tblEmployeeType> tblEmployeeTypes { get; set; }
        public DbSet<tblFixedAsset_Store_Shop> tblFixedAsset_Store_Shop { get; set; }
        public DbSet<tblFixedAsset> tblFixedAssets { get; set; }
        public DbSet<tblFixedCost> tblFixedCosts { get; set; }
        public DbSet<tblGroupSupplier> tblGroupSuppliers { get; set; }
        public DbSet<tblMaterial> tblMaterials { get; set; }
        public DbSet<tblMediaCat_tblMedia> tblMediaCat_tblMedia { get; set; }
        public DbSet<tblMediaCat> tblMediaCats { get; set; }
        public DbSet<tblMedia> tblMedias { get; set; }
        public DbSet<tblMenu> tblMenus { get; set; }
        public DbSet<tblObject> tblObjects { get; set; }
        public DbSet<tblProduct> tblProducts { get; set; }
        public DbSet<tblRole> tblRoles { get; set; }
        public DbSet<tblShop_tblStore_tblEmployee> tblShop_tblStore_tblEmployee { get; set; }
        public DbSet<tblShop> tblShops { get; set; }
        public DbSet<tblStatement> tblStatements { get; set; }
        public DbSet<tblStore> tblStores { get; set; }
        public DbSet<tblSupplier> tblSuppliers { get; set; }
        public DbSet<tblTable> tblTables { get; set; }
        public DbSet<tblVoucher> tblVouchers { get; set; }
        public DbSet<tblWard> tblWards { get; set; }
        public DbSet<tblComBo> tblComBoes { get; set; }
    
        public virtual ObjectResult<spGetProductByID_Result> spGetProductByID(Nullable<int> productID)
        {
            ((IObjectContextAdapter)this).ObjectContext.MetadataWorkspace.LoadFromAssembly(typeof(spGetProductByID_Result).Assembly);
    
            var productIDParameter = productID.HasValue ?
                new ObjectParameter("productID", productID) :
                new ObjectParameter("productID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetProductByID_Result>("spGetProductByID", productIDParameter);
        }
    
        public virtual ObjectResult<tblAccount> spCheckLogin(string username, string password)
        {
            ((IObjectContextAdapter)this).ObjectContext.MetadataWorkspace.LoadFromAssembly(typeof(tblAccount).Assembly);
    
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<tblAccount>("spCheckLogin", usernameParameter, passwordParameter);
        }
    
        public virtual ObjectResult<tblAccount> spCheckLogin(string username, string password, MergeOption mergeOption)
        {
            ((IObjectContextAdapter)this).ObjectContext.MetadataWorkspace.LoadFromAssembly(typeof(tblAccount).Assembly);
    
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<tblAccount>("spCheckLogin", mergeOption, usernameParameter, passwordParameter);
        }
    
        public virtual int spAddComboProduct(Nullable<int> comboID, Nullable<int> productID)
        {
            var comboIDParameter = comboID.HasValue ?
                new ObjectParameter("comboID", comboID) :
                new ObjectParameter("comboID", typeof(int));
    
            var productIDParameter = productID.HasValue ?
                new ObjectParameter("productID", productID) :
                new ObjectParameter("productID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spAddComboProduct", comboIDParameter, productIDParameter);
        }
    
        public virtual ObjectResult<tblProduct> spGetProductByComboID(Nullable<int> comboID)
        {
            ((IObjectContextAdapter)this).ObjectContext.MetadataWorkspace.LoadFromAssembly(typeof(tblProduct).Assembly);
    
            var comboIDParameter = comboID.HasValue ?
                new ObjectParameter("comboID", comboID) :
                new ObjectParameter("comboID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<tblProduct>("spGetProductByComboID", comboIDParameter);
        }
    
        public virtual ObjectResult<tblProduct> spGetProductByComboID(Nullable<int> comboID, MergeOption mergeOption)
        {
            ((IObjectContextAdapter)this).ObjectContext.MetadataWorkspace.LoadFromAssembly(typeof(tblProduct).Assembly);
    
            var comboIDParameter = comboID.HasValue ?
                new ObjectParameter("comboID", comboID) :
                new ObjectParameter("comboID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<tblProduct>("spGetProductByComboID", mergeOption, comboIDParameter);
        }
    
        public virtual int spDeleteComboProduct(Nullable<int> comboID)
        {
            var comboIDParameter = comboID.HasValue ?
                new ObjectParameter("comboID", comboID) :
                new ObjectParameter("comboID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spDeleteComboProduct", comboIDParameter);
        }
    
        public virtual ObjectResult<tblProduct> spTest()
        {
            ((IObjectContextAdapter)this).ObjectContext.MetadataWorkspace.LoadFromAssembly(typeof(tblProduct).Assembly);
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<tblProduct>("spTest");
        }
    
        public virtual ObjectResult<tblProduct> spTest(MergeOption mergeOption)
        {
            ((IObjectContextAdapter)this).ObjectContext.MetadataWorkspace.LoadFromAssembly(typeof(tblProduct).Assembly);
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<tblProduct>("spTest", mergeOption);
        }
    }
}