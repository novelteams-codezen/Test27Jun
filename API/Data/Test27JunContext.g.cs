using Microsoft.EntityFrameworkCore;
using Test27Jun.Entities;

namespace Test27Jun.Data
{
    /// <summary>
    /// Represents the database context for the application.
    /// </summary>
    public class Test27JunContext : DbContext
    {
        /// <summary>
        /// Configures the database connection options.
        /// </summary>
        /// <param name="optionsBuilder">The options builder used to configure the database connection.</param>
        protected override void OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=codezen.database.windows.net;Initial Catalog=codezen;Persist Security Info=True;user id=Lowcodeadmin;password=NtLowCode^123*;Integrated Security=false;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=true;");
        }

        /// <summary>
        /// Configures the model relationships and entity mappings.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure the database model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInRole>().HasKey(a => a.Id);
            modelBuilder.Entity<UserToken>().HasKey(a => a.Id);
            modelBuilder.Entity<RoleEntitlement>().HasKey(a => a.Id);
            modelBuilder.Entity<Entity>().HasKey(a => a.Id);
            modelBuilder.Entity<Tenant>().HasKey(a => a.Id);
            modelBuilder.Entity<User>().HasKey(a => a.Id);
            modelBuilder.Entity<Role>().HasKey(a => a.Id);
            modelBuilder.Entity<Product>().HasKey(a => a.Id);
            modelBuilder.Entity<ProductBatch>().HasKey(a => a.Id);
            modelBuilder.Entity<Supplier>().HasKey(a => a.Id);
            modelBuilder.Entity<Requisition>().HasKey(a => a.Id);
            modelBuilder.Entity<RequisitionLine>().HasKey(a => a.Id);
            modelBuilder.Entity<PurchaseOrder>().HasKey(a => a.Id);
            modelBuilder.Entity<PurchaseOrderLine>().HasKey(a => a.Id);
            modelBuilder.Entity<GoodsReceipt>().HasKey(a => a.Id);
            modelBuilder.Entity<GoodsReceiptItem>().HasKey(a => a.Id);
            modelBuilder.Entity<GoodsReceiptPurchaseOrderRelation>().HasKey(a => a.Id);
            modelBuilder.Entity<GoodsReturn>().HasKey(a => a.Id);
            modelBuilder.Entity<GoodsReturnItem>().HasKey(a => a.Id);
            modelBuilder.Entity<StockTransfer>().HasKey(a => a.Id);
            modelBuilder.Entity<StockTransferItem>().HasKey(a => a.Id);
            modelBuilder.Entity<StockAdjustment>().HasKey(a => a.Id);
            modelBuilder.Entity<StockAdjustmentItem>().HasKey(a => a.Id);
            modelBuilder.Entity<StockTake>().HasKey(a => a.Id);
            modelBuilder.Entity<StockTakeItem>().HasKey(a => a.Id);
            modelBuilder.Entity<PriceList>().HasKey(a => a.Id);
            modelBuilder.Entity<PriceListItem>().HasKey(a => a.Id);
            modelBuilder.Entity<PriceListVersion>().HasKey(a => a.Id);
            modelBuilder.Entity<PriceListVersionItem>().HasKey(a => a.Id);
            modelBuilder.Entity<OpeningBalance>().HasKey(a => a.Id);
            modelBuilder.Entity<OpeningBalanceItem>().HasKey(a => a.Id);
            modelBuilder.Entity<Location>().HasKey(a => a.Id);
            modelBuilder.Entity<SubLocation>().HasKey(a => a.Id);
            modelBuilder.Entity<DrugSchedule>().HasKey(a => a.Id);
            modelBuilder.Entity<FavouriteGoodsReceiptLine>().HasKey(a => a.Id);
            modelBuilder.Entity<FavouritePurchaseOrderLine>().HasKey(a => a.Id);
            modelBuilder.Entity<FevouriteRequisitionLine>().HasKey(a => a.Id);
            modelBuilder.Entity<GoodsReceiptActivityHistory>().HasKey(a => a.Id);
            modelBuilder.Entity<GoodsReceiptFile>().HasKey(a => a.Id);
            modelBuilder.Entity<GoodsReceiptSuggestion>().HasKey(a => a.Id);
            modelBuilder.Entity<GoodsReceiptSummary>().HasKey(a => a.Id);
            modelBuilder.Entity<GoodsReceiptWorkFlowStep>().HasKey(a => a.Id);
            modelBuilder.Entity<GoodsReturnActivityHistory>().HasKey(a => a.Id);
            modelBuilder.Entity<GoodsReturnFile>().HasKey(a => a.Id);
            modelBuilder.Entity<GoodsReturnSummary>().HasKey(a => a.Id);
            modelBuilder.Entity<GoodsReturnWorkFlowStep>().HasKey(a => a.Id);
            modelBuilder.Entity<InventorySettings>().HasKey(a => a.Id);
            modelBuilder.Entity<OrganisationStockBalance>().HasKey(a => a.Id);
            modelBuilder.Entity<PriceListComponent>().HasKey(a => a.Id);
            modelBuilder.Entity<PriceListVersionComponent>().HasKey(a => a.Id);
            modelBuilder.Entity<ProductCategory>().HasKey(a => a.Id);
            modelBuilder.Entity<ProductClassification>().HasKey(a => a.Id);
            modelBuilder.Entity<ProductCustomUOM>().HasKey(a => a.Id);
            modelBuilder.Entity<ProductFormulation>().HasKey(a => a.Id);
            modelBuilder.Entity<ProductGenderRule>().HasKey(a => a.Id);
            modelBuilder.Entity<ProductGeneric>().HasKey(a => a.Id);
            modelBuilder.Entity<ProductLocationPrice>().HasKey(a => a.Id);
            modelBuilder.Entity<ProductManufacture>().HasKey(a => a.Id);
            modelBuilder.Entity<ProductPatientCategoryRule>().HasKey(a => a.Id);
            modelBuilder.Entity<ProductPurchaseUOM>().HasKey(a => a.Id);
            modelBuilder.Entity<ProductTheraputicClassification>().HasKey(a => a.Id);
            modelBuilder.Entity<ProductTheraputicSubClassification>().HasKey(a => a.Id);
            modelBuilder.Entity<ProductUom>().HasKey(a => a.Id);
            modelBuilder.Entity<PurchaseOrderActivityHistory>().HasKey(a => a.Id);
            modelBuilder.Entity<PurchaseOrderFile>().HasKey(a => a.Id);
            modelBuilder.Entity<PurchaseOrderSuggestion>().HasKey(a => a.Id);
            modelBuilder.Entity<PurchaseOrderWorkFlowStep>().HasKey(a => a.Id);
            modelBuilder.Entity<RequisitionActivityHistory>().HasKey(a => a.Id);
            modelBuilder.Entity<RequisitionFile>().HasKey(a => a.Id);
            modelBuilder.Entity<RequisitionSuggestion>().HasKey(a => a.Id);
            modelBuilder.Entity<RequisitionWorkFlowStep>().HasKey(a => a.Id);
            modelBuilder.Entity<StockAdjustmentActivityHistory>().HasKey(a => a.Id);
            modelBuilder.Entity<StockAdjustmentFile>().HasKey(a => a.Id);
            modelBuilder.Entity<StockAdjustmentSummary>().HasKey(a => a.Id);
            modelBuilder.Entity<StockAdjustmentWorkFlowStep>().HasKey(a => a.Id);
            modelBuilder.Entity<StockInvoiceSummary>().HasKey(a => a.Id);
            modelBuilder.Entity<StockSummary>().HasKey(a => a.Id);
            modelBuilder.Entity<StockTakeActivityHistory>().HasKey(a => a.Id);
            modelBuilder.Entity<StockTakeFile>().HasKey(a => a.Id);
            modelBuilder.Entity<StockTakeInitiated>().HasKey(a => a.Id);
            modelBuilder.Entity<StockTakeItemBatches>().HasKey(a => a.Id);
            modelBuilder.Entity<StockTakeWorkflowStep>().HasKey(a => a.Id);
            modelBuilder.Entity<StockTransferActivityHistory>().HasKey(a => a.Id);
            modelBuilder.Entity<StockTransferFile>().HasKey(a => a.Id);
            modelBuilder.Entity<StockTransferSummary>().HasKey(a => a.Id);
            modelBuilder.Entity<StockTransferWorkFlowStep>().HasKey(a => a.Id);
            modelBuilder.Entity<SubReason>().HasKey(a => a.Id);
            modelBuilder.Entity<Order>().HasKey(a => a.Id);
            modelBuilder.Entity<Transaction>().HasKey(a => a.Id);
            modelBuilder.Entity<UOM>().HasKey(a => a.Id);
            modelBuilder.Entity<UserInRole>().HasOne(a => a.TenantId_Tenant).WithMany().HasForeignKey(c => c.TenantId);
            modelBuilder.Entity<UserInRole>().HasOne(a => a.RoleId_Role).WithMany().HasForeignKey(c => c.RoleId);
            modelBuilder.Entity<UserInRole>().HasOne(a => a.UserId_User).WithMany().HasForeignKey(c => c.UserId);
            modelBuilder.Entity<UserInRole>().HasOne(a => a.CreatedBy_User).WithMany().HasForeignKey(c => c.CreatedBy);
            modelBuilder.Entity<UserInRole>().HasOne(a => a.UpdatedBy_User).WithMany().HasForeignKey(c => c.UpdatedBy);
            modelBuilder.Entity<UserToken>().HasOne(a => a.TenantId_Tenant).WithMany().HasForeignKey(c => c.TenantId);
            modelBuilder.Entity<UserToken>().HasOne(a => a.UserId_User).WithMany().HasForeignKey(c => c.UserId);
            modelBuilder.Entity<RoleEntitlement>().HasOne(a => a.TenantId_Tenant).WithMany().HasForeignKey(c => c.TenantId);
            modelBuilder.Entity<RoleEntitlement>().HasOne(a => a.RoleId_Role).WithMany().HasForeignKey(c => c.RoleId);
            modelBuilder.Entity<RoleEntitlement>().HasOne(a => a.EntityId_Entity).WithMany().HasForeignKey(c => c.EntityId);
            modelBuilder.Entity<RoleEntitlement>().HasOne(a => a.CreatedBy_User).WithMany().HasForeignKey(c => c.CreatedBy);
            modelBuilder.Entity<RoleEntitlement>().HasOne(a => a.UpdatedBy_User).WithMany().HasForeignKey(c => c.UpdatedBy);
            modelBuilder.Entity<Entity>().HasOne(a => a.TenantId_Tenant).WithMany().HasForeignKey(c => c.TenantId);
            modelBuilder.Entity<Entity>().HasOne(a => a.CreatedBy_User).WithMany().HasForeignKey(c => c.CreatedBy);
            modelBuilder.Entity<Entity>().HasOne(a => a.UpdatedBy_User).WithMany().HasForeignKey(c => c.UpdatedBy);
            modelBuilder.Entity<User>().HasOne(a => a.TenantId_Tenant).WithMany().HasForeignKey(c => c.TenantId);
            modelBuilder.Entity<Role>().HasOne(a => a.TenantId_Tenant).WithMany().HasForeignKey(c => c.TenantId);
            modelBuilder.Entity<Role>().HasOne(a => a.CreatedBy_User).WithMany().HasForeignKey(c => c.CreatedBy);
            modelBuilder.Entity<Role>().HasOne(a => a.UpdatedBy_User).WithMany().HasForeignKey(c => c.UpdatedBy);
            modelBuilder.Entity<Product>().HasOne(a => a.ProductManufactureId_ProductManufacture).WithMany().HasForeignKey(c => c.ProductManufactureId);
            modelBuilder.Entity<Product>().HasOne(a => a.ProductCategoryId_ProductCategory).WithMany().HasForeignKey(c => c.ProductCategoryId);
            modelBuilder.Entity<Product>().HasOne(a => a.ProductFormulationId_ProductFormulation).WithMany().HasForeignKey(c => c.ProductFormulationId);
            modelBuilder.Entity<Product>().HasOne(a => a.ProductClassificationId_ProductClassification).WithMany().HasForeignKey(c => c.ProductClassificationId);
            modelBuilder.Entity<Product>().HasOne(a => a.LowestUOM_UOM).WithMany().HasForeignKey(c => c.LowestUOM);
            modelBuilder.Entity<Product>().HasOne(a => a.ReOrderQuantityUom_UOM).WithMany().HasForeignKey(c => c.ReOrderQuantityUom);
            modelBuilder.Entity<Product>().HasOne(a => a.DrugScheduleId_DrugSchedule).WithMany().HasForeignKey(c => c.DrugScheduleId);
            modelBuilder.Entity<ProductBatch>().HasOne(a => a.LocationId_Location).WithMany().HasForeignKey(c => c.LocationId);
            modelBuilder.Entity<ProductBatch>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<ProductBatch>().HasOne(a => a.ProductUomId_ProductUom).WithMany().HasForeignKey(c => c.ProductUomId);
            modelBuilder.Entity<Requisition>().HasOne(a => a.LocationId_Location).WithMany().HasForeignKey(c => c.LocationId);
            modelBuilder.Entity<RequisitionLine>().HasOne(a => a.RequisitionId_Requisition).WithMany().HasForeignKey(c => c.RequisitionId);
            modelBuilder.Entity<RequisitionLine>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<RequisitionLine>().HasOne(a => a.ProductUomId_ProductUom).WithMany().HasForeignKey(c => c.ProductUomId);
            modelBuilder.Entity<RequisitionLine>().HasOne(a => a.SupplierId_Supplier).WithMany().HasForeignKey(c => c.SupplierId);
            modelBuilder.Entity<RequisitionLine>().HasOne(a => a.PurchaseOrderId_PurchaseOrder).WithMany().HasForeignKey(c => c.PurchaseOrderId);
            modelBuilder.Entity<RequisitionLine>().HasOne(a => a.PurchaseOrderLineId_PurchaseOrderLine).WithMany().HasForeignKey(c => c.PurchaseOrderLineId);
            modelBuilder.Entity<PurchaseOrder>().HasOne(a => a.SupplierId_Supplier).WithMany().HasForeignKey(c => c.SupplierId);
            modelBuilder.Entity<PurchaseOrder>().HasOne(a => a.LocationId_Location).WithMany().HasForeignKey(c => c.LocationId);
            modelBuilder.Entity<PurchaseOrderLine>().HasOne(a => a.PurchaseOrderId_PurchaseOrder).WithMany().HasForeignKey(c => c.PurchaseOrderId);
            modelBuilder.Entity<PurchaseOrderLine>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<PurchaseOrderLine>().HasOne(a => a.ProductUomId_ProductUom).WithMany().HasForeignKey(c => c.ProductUomId);
            modelBuilder.Entity<PurchaseOrderLine>().HasOne(a => a.RequisitionId_Requisition).WithMany().HasForeignKey(c => c.RequisitionId);
            modelBuilder.Entity<PurchaseOrderLine>().HasOne(a => a.GoodsReceiptId_GoodsReceipt).WithMany().HasForeignKey(c => c.GoodsReceiptId);
            modelBuilder.Entity<PurchaseOrderLine>().HasOne(a => a.GoodsReceiptItemId_GoodsReceiptItem).WithMany().HasForeignKey(c => c.GoodsReceiptItemId);
            modelBuilder.Entity<GoodsReceipt>().HasOne(a => a.LocationId_Location).WithMany().HasForeignKey(c => c.LocationId);
            modelBuilder.Entity<GoodsReceipt>().HasOne(a => a.SupplierId_Supplier).WithMany().HasForeignKey(c => c.SupplierId);
            modelBuilder.Entity<GoodsReceipt>().HasOne(a => a.SubLocationId_SubLocation).WithMany().HasForeignKey(c => c.SubLocationId);
            modelBuilder.Entity<GoodsReceiptItem>().HasOne(a => a.GoodsReceiptId_GoodsReceipt).WithMany().HasForeignKey(c => c.GoodsReceiptId);
            modelBuilder.Entity<GoodsReceiptItem>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<GoodsReceiptItem>().HasOne(a => a.ProductBatchId_ProductBatch).WithMany().HasForeignKey(c => c.ProductBatchId);
            modelBuilder.Entity<GoodsReceiptItem>().HasOne(a => a.ProductUomId_ProductUom).WithMany().HasForeignKey(c => c.ProductUomId);
            modelBuilder.Entity<GoodsReceiptItem>().HasOne(a => a.PurchaseOrderId_PurchaseOrder).WithMany().HasForeignKey(c => c.PurchaseOrderId);
            modelBuilder.Entity<GoodsReceiptItem>().HasOne(a => a.PurchaseOrderLineId_PurchaseOrderLine).WithMany().HasForeignKey(c => c.PurchaseOrderLineId);
            modelBuilder.Entity<GoodsReceiptItem>().HasOne(a => a.ReplacedProductId_Product).WithMany().HasForeignKey(c => c.ReplacedProductId);
            modelBuilder.Entity<GoodsReceiptPurchaseOrderRelation>().HasOne(a => a.GoodsReceiptId_GoodsReceipt).WithMany().HasForeignKey(c => c.GoodsReceiptId);
            modelBuilder.Entity<GoodsReceiptPurchaseOrderRelation>().HasOne(a => a.PurchaseOrderId_PurchaseOrder).WithMany().HasForeignKey(c => c.PurchaseOrderId);
            modelBuilder.Entity<GoodsReturn>().HasOne(a => a.GoodsReceiptId_GoodsReceipt).WithMany().HasForeignKey(c => c.GoodsReceiptId);
            modelBuilder.Entity<GoodsReturn>().HasOne(a => a.SupplierId_Supplier).WithMany().HasForeignKey(c => c.SupplierId);
            modelBuilder.Entity<GoodsReturn>().HasOne(a => a.LocationId_Location).WithMany().HasForeignKey(c => c.LocationId);
            modelBuilder.Entity<GoodsReturn>().HasOne(a => a.SubReasonId_SubReason).WithMany().HasForeignKey(c => c.SubReasonId);
            modelBuilder.Entity<GoodsReturn>().HasOne(a => a.SubLocationId_SubLocation).WithMany().HasForeignKey(c => c.SubLocationId);
            modelBuilder.Entity<GoodsReturnItem>().HasOne(a => a.GoodsReturnId_GoodsReturn).WithMany().HasForeignKey(c => c.GoodsReturnId);
            modelBuilder.Entity<GoodsReturnItem>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<GoodsReturnItem>().HasOne(a => a.ProductBatchId_ProductBatch).WithMany().HasForeignKey(c => c.ProductBatchId);
            modelBuilder.Entity<GoodsReturnItem>().HasOne(a => a.GoodsReceiptItemId_GoodsReceiptItem).WithMany().HasForeignKey(c => c.GoodsReceiptItemId);
            modelBuilder.Entity<GoodsReturnItem>().HasOne(a => a.SubReasonId_SubReason).WithMany().HasForeignKey(c => c.SubReasonId);
            modelBuilder.Entity<GoodsReturnItem>().HasOne(a => a.ProductUomId_ProductUom).WithMany().HasForeignKey(c => c.ProductUomId);
            modelBuilder.Entity<StockTransfer>().HasOne(a => a.LocationFromId_Location).WithMany().HasForeignKey(c => c.LocationFromId);
            modelBuilder.Entity<StockTransfer>().HasOne(a => a.LocationToId_Location).WithMany().HasForeignKey(c => c.LocationToId);
            modelBuilder.Entity<StockTransfer>().HasOne(a => a.SubLocationFromId_SubLocation).WithMany().HasForeignKey(c => c.SubLocationFromId);
            modelBuilder.Entity<StockTransfer>().HasOne(a => a.SubLocationToId_SubLocation).WithMany().HasForeignKey(c => c.SubLocationToId);
            modelBuilder.Entity<StockTransferItem>().HasOne(a => a.StockTransferId_StockTransfer).WithMany().HasForeignKey(c => c.StockTransferId);
            modelBuilder.Entity<StockTransferItem>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<StockTransferItem>().HasOne(a => a.ProductBatchId_ProductBatch).WithMany().HasForeignKey(c => c.ProductBatchId);
            modelBuilder.Entity<StockTransferItem>().HasOne(a => a.ProductUomId_ProductUom).WithMany().HasForeignKey(c => c.ProductUomId);
            modelBuilder.Entity<StockAdjustment>().HasOne(a => a.LocationId_Location).WithMany().HasForeignKey(c => c.LocationId);
            modelBuilder.Entity<StockAdjustment>().HasOne(a => a.SubLocationId_SubLocation).WithMany().HasForeignKey(c => c.SubLocationId);
            modelBuilder.Entity<StockAdjustmentItem>().HasOne(a => a.StockAdjustmentId_StockAdjustment).WithMany().HasForeignKey(c => c.StockAdjustmentId);
            modelBuilder.Entity<StockAdjustmentItem>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<StockAdjustmentItem>().HasOne(a => a.ProductBatchId_ProductBatch).WithMany().HasForeignKey(c => c.ProductBatchId);
            modelBuilder.Entity<StockAdjustmentItem>().HasOne(a => a.ProductUomId_ProductUom).WithMany().HasForeignKey(c => c.ProductUomId);
            modelBuilder.Entity<StockAdjustmentItem>().HasOne(a => a.SubReasonId_SubReason).WithMany().HasForeignKey(c => c.SubReasonId);
            modelBuilder.Entity<StockTake>().HasOne(a => a.LocationId_Location).WithMany().HasForeignKey(c => c.LocationId);
            modelBuilder.Entity<StockTake>().HasOne(a => a.SubLocationId_SubLocation).WithMany().HasForeignKey(c => c.SubLocationId);
            modelBuilder.Entity<StockTakeItem>().HasOne(a => a.StockTakeId_StockTake).WithMany().HasForeignKey(c => c.StockTakeId);
            modelBuilder.Entity<StockTakeItem>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<PriceListItem>().HasOne(a => a.PriceListId_PriceList).WithMany().HasForeignKey(c => c.PriceListId);
            modelBuilder.Entity<PriceListItem>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<PriceListItem>().HasOne(a => a.ProductUomId_ProductUom).WithMany().HasForeignKey(c => c.ProductUomId);
            modelBuilder.Entity<PriceListVersion>().HasOne(a => a.PriceListId_PriceList).WithMany().HasForeignKey(c => c.PriceListId);
            modelBuilder.Entity<PriceListVersionItem>().HasOne(a => a.PriceListId_PriceList).WithMany().HasForeignKey(c => c.PriceListId);
            modelBuilder.Entity<PriceListVersionItem>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<PriceListVersionItem>().HasOne(a => a.PriceListVersionId_PriceListVersion).WithMany().HasForeignKey(c => c.PriceListVersionId);
            modelBuilder.Entity<PriceListVersionItem>().HasOne(a => a.ProductUomId_ProductUom).WithMany().HasForeignKey(c => c.ProductUomId);
            modelBuilder.Entity<OpeningBalance>().HasOne(a => a.LocationId_Location).WithMany().HasForeignKey(c => c.LocationId);
            modelBuilder.Entity<OpeningBalance>().HasOne(a => a.SubLocationId_SubLocation).WithMany().HasForeignKey(c => c.SubLocationId);
            modelBuilder.Entity<OpeningBalanceItem>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<SubLocation>().HasOne(a => a.LocationId_Location).WithMany().HasForeignKey(c => c.LocationId);
            modelBuilder.Entity<FavouriteGoodsReceiptLine>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<FavouritePurchaseOrderLine>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<FevouriteRequisitionLine>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<GoodsReceiptActivityHistory>().HasOne(a => a.GoodsReceiptId_GoodsReceipt).WithMany().HasForeignKey(c => c.GoodsReceiptId);
            modelBuilder.Entity<GoodsReceiptActivityHistory>().HasOne(a => a.TransactionBy_Transaction).WithMany().HasForeignKey(c => c.TransactionBy);
            modelBuilder.Entity<GoodsReceiptFile>().HasOne(a => a.GoodsReceiptId_GoodsReceipt).WithMany().HasForeignKey(c => c.GoodsReceiptId);
            modelBuilder.Entity<GoodsReceiptSuggestion>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<GoodsReceiptSuggestion>().HasOne(a => a.LocationId_Location).WithMany().HasForeignKey(c => c.LocationId);
            modelBuilder.Entity<GoodsReceiptSummary>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<GoodsReceiptSummary>().HasOne(a => a.ProductBatchId_ProductBatch).WithMany().HasForeignKey(c => c.ProductBatchId);
            modelBuilder.Entity<GoodsReceiptSummary>().HasOne(a => a.LocationId_Location).WithMany().HasForeignKey(c => c.LocationId);
            modelBuilder.Entity<GoodsReturnActivityHistory>().HasOne(a => a.GoodsReturnId_GoodsReturn).WithMany().HasForeignKey(c => c.GoodsReturnId);
            modelBuilder.Entity<GoodsReturnActivityHistory>().HasOne(a => a.TransactionBy_Transaction).WithMany().HasForeignKey(c => c.TransactionBy);
            modelBuilder.Entity<GoodsReturnFile>().HasOne(a => a.GoodsReturnId_GoodsReturn).WithMany().HasForeignKey(c => c.GoodsReturnId);
            modelBuilder.Entity<GoodsReturnSummary>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<GoodsReturnSummary>().HasOne(a => a.ProductBatchId_ProductBatch).WithMany().HasForeignKey(c => c.ProductBatchId);
            modelBuilder.Entity<GoodsReturnSummary>().HasOne(a => a.LocationId_Location).WithMany().HasForeignKey(c => c.LocationId);
            modelBuilder.Entity<OrganisationStockBalance>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<PriceListComponent>().HasOne(a => a.PriceListId_PriceList).WithMany().HasForeignKey(c => c.PriceListId);
            modelBuilder.Entity<PriceListComponent>().HasOne(a => a.LocationId_Location).WithMany().HasForeignKey(c => c.LocationId);
            modelBuilder.Entity<PriceListComponent>().HasOne(a => a.SupplierId_Supplier).WithMany().HasForeignKey(c => c.SupplierId);
            modelBuilder.Entity<PriceListVersionComponent>().HasOne(a => a.PriceListId_PriceList).WithMany().HasForeignKey(c => c.PriceListId);
            modelBuilder.Entity<PriceListVersionComponent>().HasOne(a => a.LocationId_Location).WithMany().HasForeignKey(c => c.LocationId);
            modelBuilder.Entity<PriceListVersionComponent>().HasOne(a => a.PriceListVersionId_PriceListVersion).WithMany().HasForeignKey(c => c.PriceListVersionId);
            modelBuilder.Entity<PriceListVersionComponent>().HasOne(a => a.SupplierId_Supplier).WithMany().HasForeignKey(c => c.SupplierId);
            modelBuilder.Entity<ProductGeneric>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<ProductGeneric>().HasOne(a => a.UOM_UOM).WithMany().HasForeignKey(c => c.UOM);
            modelBuilder.Entity<ProductLocationPrice>().HasOne(a => a.LocationId_Location).WithMany().HasForeignKey(c => c.LocationId);
            modelBuilder.Entity<ProductLocationPrice>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<ProductPurchaseUOM>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<ProductPurchaseUOM>().HasOne(a => a.ProductCustomUOMId_UOM).WithMany().HasForeignKey(c => c.ProductCustomUOMId);
            modelBuilder.Entity<ProductPurchaseUOM>().HasOne(a => a.UomId_UOM).WithMany().HasForeignKey(c => c.UomId);
            modelBuilder.Entity<ProductTheraputicClassification>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<ProductTheraputicSubClassification>().HasOne(a => a.ProductTheraputicClassificationId_ProductTheraputicClassification).WithMany().HasForeignKey(c => c.ProductTheraputicClassificationId);
            modelBuilder.Entity<ProductUom>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<ProductUom>().HasOne(a => a.UomId_UOM).WithMany().HasForeignKey(c => c.UomId);
            modelBuilder.Entity<PurchaseOrderActivityHistory>().HasOne(a => a.PurchaseOrderId_PurchaseOrder).WithMany().HasForeignKey(c => c.PurchaseOrderId);
            modelBuilder.Entity<PurchaseOrderFile>().HasOne(a => a.PurchaseOrderId_PurchaseOrder).WithMany().HasForeignKey(c => c.PurchaseOrderId);
            modelBuilder.Entity<PurchaseOrderSuggestion>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<PurchaseOrderSuggestion>().HasOne(a => a.LocationId_Location).WithMany().HasForeignKey(c => c.LocationId);
            modelBuilder.Entity<RequisitionActivityHistory>().HasOne(a => a.RequisitionId_Requisition).WithMany().HasForeignKey(c => c.RequisitionId);
            modelBuilder.Entity<RequisitionFile>().HasOne(a => a.RequisitionId_Requisition).WithMany().HasForeignKey(c => c.RequisitionId);
            modelBuilder.Entity<RequisitionSuggestion>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<RequisitionSuggestion>().HasOne(a => a.LocationId_Location).WithMany().HasForeignKey(c => c.LocationId);
            modelBuilder.Entity<StockAdjustmentActivityHistory>().HasOne(a => a.StockAdjustmentId_StockAdjustment).WithMany().HasForeignKey(c => c.StockAdjustmentId);
            modelBuilder.Entity<StockAdjustmentFile>().HasOne(a => a.StockAdjustmentId_StockAdjustment).WithMany().HasForeignKey(c => c.StockAdjustmentId);
            modelBuilder.Entity<StockAdjustmentSummary>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<StockAdjustmentSummary>().HasOne(a => a.ProductBatchId_ProductBatch).WithMany().HasForeignKey(c => c.ProductBatchId);
            modelBuilder.Entity<StockAdjustmentSummary>().HasOne(a => a.LocationId_Location).WithMany().HasForeignKey(c => c.LocationId);
            modelBuilder.Entity<StockInvoiceSummary>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<StockInvoiceSummary>().HasOne(a => a.ProductBatchId_ProductBatch).WithMany().HasForeignKey(c => c.ProductBatchId);
            modelBuilder.Entity<StockInvoiceSummary>().HasOne(a => a.LocationId_Location).WithMany().HasForeignKey(c => c.LocationId);
            modelBuilder.Entity<StockSummary>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<StockSummary>().HasOne(a => a.ProductBatchId_ProductBatch).WithMany().HasForeignKey(c => c.ProductBatchId);
            modelBuilder.Entity<StockSummary>().HasOne(a => a.LocationId_Location).WithMany().HasForeignKey(c => c.LocationId);
            modelBuilder.Entity<StockTakeActivityHistory>().HasOne(a => a.StockTakeId_StockTake).WithMany().HasForeignKey(c => c.StockTakeId);
            modelBuilder.Entity<StockTakeFile>().HasOne(a => a.StockTakeId_StockTake).WithMany().HasForeignKey(c => c.StockTakeId);
            modelBuilder.Entity<StockTakeInitiated>().HasOne(a => a.StockTakeId_StockTake).WithMany().HasForeignKey(c => c.StockTakeId);
            modelBuilder.Entity<StockTakeInitiated>().HasOne(a => a.LocationId_Location).WithMany().HasForeignKey(c => c.LocationId);
            modelBuilder.Entity<StockTakeItemBatches>().HasOne(a => a.ProductBatchId_ProductBatch).WithMany().HasForeignKey(c => c.ProductBatchId);
            modelBuilder.Entity<StockTransferActivityHistory>().HasOne(a => a.StockTransferId_StockTransfer).WithMany().HasForeignKey(c => c.StockTransferId);
            modelBuilder.Entity<StockTransferFile>().HasOne(a => a.StockTransferId_StockTransfer).WithMany().HasForeignKey(c => c.StockTransferId);
            modelBuilder.Entity<StockTransferSummary>().HasOne(a => a.ProductId_Product).WithMany().HasForeignKey(c => c.ProductId);
            modelBuilder.Entity<StockTransferSummary>().HasOne(a => a.ProductBatchId_ProductBatch).WithMany().HasForeignKey(c => c.ProductBatchId);
            modelBuilder.Entity<StockTransferSummary>().HasOne(a => a.LocationId_Location).WithMany().HasForeignKey(c => c.LocationId);
            modelBuilder.Entity<Order>().HasOne(a => a.SupplierId_Supplier).WithMany().HasForeignKey(c => c.SupplierId);
            modelBuilder.Entity<Transaction>().HasOne(a => a.OrderId_Order).WithMany().HasForeignKey(c => c.OrderId);
        }

        /// <summary>
        /// Represents the database set for the UserInRole entity.
        /// </summary>
        public DbSet<UserInRole> UserInRole { get; set; }

        /// <summary>
        /// Represents the database set for the UserToken entity.
        /// </summary>
        public DbSet<UserToken> UserToken { get; set; }

        /// <summary>
        /// Represents the database set for the RoleEntitlement entity.
        /// </summary>
        public DbSet<RoleEntitlement> RoleEntitlement { get; set; }

        /// <summary>
        /// Represents the database set for the Entity entity.
        /// </summary>
        public DbSet<Entity> Entity { get; set; }

        /// <summary>
        /// Represents the database set for the Tenant entity.
        /// </summary>
        public DbSet<Tenant> Tenant { get; set; }

        /// <summary>
        /// Represents the database set for the User entity.
        /// </summary>
        public DbSet<User> User { get; set; }

        /// <summary>
        /// Represents the database set for the Role entity.
        /// </summary>
        public DbSet<Role> Role { get; set; }

        /// <summary>
        /// Represents the database set for the Product entity.
        /// </summary>
        public DbSet<Product> Product { get; set; }

        /// <summary>
        /// Represents the database set for the ProductBatch entity.
        /// </summary>
        public DbSet<ProductBatch> ProductBatch { get; set; }

        /// <summary>
        /// Represents the database set for the Supplier entity.
        /// </summary>
        public DbSet<Supplier> Supplier { get; set; }

        /// <summary>
        /// Represents the database set for the Requisition entity.
        /// </summary>
        public DbSet<Requisition> Requisition { get; set; }

        /// <summary>
        /// Represents the database set for the RequisitionLine entity.
        /// </summary>
        public DbSet<RequisitionLine> RequisitionLine { get; set; }

        /// <summary>
        /// Represents the database set for the PurchaseOrder entity.
        /// </summary>
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }

        /// <summary>
        /// Represents the database set for the PurchaseOrderLine entity.
        /// </summary>
        public DbSet<PurchaseOrderLine> PurchaseOrderLine { get; set; }

        /// <summary>
        /// Represents the database set for the GoodsReceipt entity.
        /// </summary>
        public DbSet<GoodsReceipt> GoodsReceipt { get; set; }

        /// <summary>
        /// Represents the database set for the GoodsReceiptItem entity.
        /// </summary>
        public DbSet<GoodsReceiptItem> GoodsReceiptItem { get; set; }

        /// <summary>
        /// Represents the database set for the GoodsReceiptPurchaseOrderRelation entity.
        /// </summary>
        public DbSet<GoodsReceiptPurchaseOrderRelation> GoodsReceiptPurchaseOrderRelation { get; set; }

        /// <summary>
        /// Represents the database set for the GoodsReturn entity.
        /// </summary>
        public DbSet<GoodsReturn> GoodsReturn { get; set; }

        /// <summary>
        /// Represents the database set for the GoodsReturnItem entity.
        /// </summary>
        public DbSet<GoodsReturnItem> GoodsReturnItem { get; set; }

        /// <summary>
        /// Represents the database set for the StockTransfer entity.
        /// </summary>
        public DbSet<StockTransfer> StockTransfer { get; set; }

        /// <summary>
        /// Represents the database set for the StockTransferItem entity.
        /// </summary>
        public DbSet<StockTransferItem> StockTransferItem { get; set; }

        /// <summary>
        /// Represents the database set for the StockAdjustment entity.
        /// </summary>
        public DbSet<StockAdjustment> StockAdjustment { get; set; }

        /// <summary>
        /// Represents the database set for the StockAdjustmentItem entity.
        /// </summary>
        public DbSet<StockAdjustmentItem> StockAdjustmentItem { get; set; }

        /// <summary>
        /// Represents the database set for the StockTake entity.
        /// </summary>
        public DbSet<StockTake> StockTake { get; set; }

        /// <summary>
        /// Represents the database set for the StockTakeItem entity.
        /// </summary>
        public DbSet<StockTakeItem> StockTakeItem { get; set; }

        /// <summary>
        /// Represents the database set for the PriceList entity.
        /// </summary>
        public DbSet<PriceList> PriceList { get; set; }

        /// <summary>
        /// Represents the database set for the PriceListItem entity.
        /// </summary>
        public DbSet<PriceListItem> PriceListItem { get; set; }

        /// <summary>
        /// Represents the database set for the PriceListVersion entity.
        /// </summary>
        public DbSet<PriceListVersion> PriceListVersion { get; set; }

        /// <summary>
        /// Represents the database set for the PriceListVersionItem entity.
        /// </summary>
        public DbSet<PriceListVersionItem> PriceListVersionItem { get; set; }

        /// <summary>
        /// Represents the database set for the OpeningBalance entity.
        /// </summary>
        public DbSet<OpeningBalance> OpeningBalance { get; set; }

        /// <summary>
        /// Represents the database set for the OpeningBalanceItem entity.
        /// </summary>
        public DbSet<OpeningBalanceItem> OpeningBalanceItem { get; set; }

        /// <summary>
        /// Represents the database set for the Location entity.
        /// </summary>
        public DbSet<Location> Location { get; set; }

        /// <summary>
        /// Represents the database set for the SubLocation entity.
        /// </summary>
        public DbSet<SubLocation> SubLocation { get; set; }

        /// <summary>
        /// Represents the database set for the DrugSchedule entity.
        /// </summary>
        public DbSet<DrugSchedule> DrugSchedule { get; set; }

        /// <summary>
        /// Represents the database set for the FavouriteGoodsReceiptLine entity.
        /// </summary>
        public DbSet<FavouriteGoodsReceiptLine> FavouriteGoodsReceiptLine { get; set; }

        /// <summary>
        /// Represents the database set for the FavouritePurchaseOrderLine entity.
        /// </summary>
        public DbSet<FavouritePurchaseOrderLine> FavouritePurchaseOrderLine { get; set; }

        /// <summary>
        /// Represents the database set for the FevouriteRequisitionLine entity.
        /// </summary>
        public DbSet<FevouriteRequisitionLine> FevouriteRequisitionLine { get; set; }

        /// <summary>
        /// Represents the database set for the GoodsReceiptActivityHistory entity.
        /// </summary>
        public DbSet<GoodsReceiptActivityHistory> GoodsReceiptActivityHistory { get; set; }

        /// <summary>
        /// Represents the database set for the GoodsReceiptFile entity.
        /// </summary>
        public DbSet<GoodsReceiptFile> GoodsReceiptFile { get; set; }

        /// <summary>
        /// Represents the database set for the GoodsReceiptSuggestion entity.
        /// </summary>
        public DbSet<GoodsReceiptSuggestion> GoodsReceiptSuggestion { get; set; }

        /// <summary>
        /// Represents the database set for the GoodsReceiptSummary entity.
        /// </summary>
        public DbSet<GoodsReceiptSummary> GoodsReceiptSummary { get; set; }

        /// <summary>
        /// Represents the database set for the GoodsReceiptWorkFlowStep entity.
        /// </summary>
        public DbSet<GoodsReceiptWorkFlowStep> GoodsReceiptWorkFlowStep { get; set; }

        /// <summary>
        /// Represents the database set for the GoodsReturnActivityHistory entity.
        /// </summary>
        public DbSet<GoodsReturnActivityHistory> GoodsReturnActivityHistory { get; set; }

        /// <summary>
        /// Represents the database set for the GoodsReturnFile entity.
        /// </summary>
        public DbSet<GoodsReturnFile> GoodsReturnFile { get; set; }

        /// <summary>
        /// Represents the database set for the GoodsReturnSummary entity.
        /// </summary>
        public DbSet<GoodsReturnSummary> GoodsReturnSummary { get; set; }

        /// <summary>
        /// Represents the database set for the GoodsReturnWorkFlowStep entity.
        /// </summary>
        public DbSet<GoodsReturnWorkFlowStep> GoodsReturnWorkFlowStep { get; set; }

        /// <summary>
        /// Represents the database set for the InventorySettings entity.
        /// </summary>
        public DbSet<InventorySettings> InventorySettings { get; set; }

        /// <summary>
        /// Represents the database set for the OrganisationStockBalance entity.
        /// </summary>
        public DbSet<OrganisationStockBalance> OrganisationStockBalance { get; set; }

        /// <summary>
        /// Represents the database set for the PriceListComponent entity.
        /// </summary>
        public DbSet<PriceListComponent> PriceListComponent { get; set; }

        /// <summary>
        /// Represents the database set for the PriceListVersionComponent entity.
        /// </summary>
        public DbSet<PriceListVersionComponent> PriceListVersionComponent { get; set; }

        /// <summary>
        /// Represents the database set for the ProductCategory entity.
        /// </summary>
        public DbSet<ProductCategory> ProductCategory { get; set; }

        /// <summary>
        /// Represents the database set for the ProductClassification entity.
        /// </summary>
        public DbSet<ProductClassification> ProductClassification { get; set; }

        /// <summary>
        /// Represents the database set for the ProductCustomUOM entity.
        /// </summary>
        public DbSet<ProductCustomUOM> ProductCustomUOM { get; set; }

        /// <summary>
        /// Represents the database set for the ProductFormulation entity.
        /// </summary>
        public DbSet<ProductFormulation> ProductFormulation { get; set; }

        /// <summary>
        /// Represents the database set for the ProductGenderRule entity.
        /// </summary>
        public DbSet<ProductGenderRule> ProductGenderRule { get; set; }

        /// <summary>
        /// Represents the database set for the ProductGeneric entity.
        /// </summary>
        public DbSet<ProductGeneric> ProductGeneric { get; set; }

        /// <summary>
        /// Represents the database set for the ProductLocationPrice entity.
        /// </summary>
        public DbSet<ProductLocationPrice> ProductLocationPrice { get; set; }

        /// <summary>
        /// Represents the database set for the ProductManufacture entity.
        /// </summary>
        public DbSet<ProductManufacture> ProductManufacture { get; set; }

        /// <summary>
        /// Represents the database set for the ProductPatientCategoryRule entity.
        /// </summary>
        public DbSet<ProductPatientCategoryRule> ProductPatientCategoryRule { get; set; }

        /// <summary>
        /// Represents the database set for the ProductPurchaseUOM entity.
        /// </summary>
        public DbSet<ProductPurchaseUOM> ProductPurchaseUOM { get; set; }

        /// <summary>
        /// Represents the database set for the ProductTheraputicClassification entity.
        /// </summary>
        public DbSet<ProductTheraputicClassification> ProductTheraputicClassification { get; set; }

        /// <summary>
        /// Represents the database set for the ProductTheraputicSubClassification entity.
        /// </summary>
        public DbSet<ProductTheraputicSubClassification> ProductTheraputicSubClassification { get; set; }

        /// <summary>
        /// Represents the database set for the ProductUom entity.
        /// </summary>
        public DbSet<ProductUom> ProductUom { get; set; }

        /// <summary>
        /// Represents the database set for the PurchaseOrderActivityHistory entity.
        /// </summary>
        public DbSet<PurchaseOrderActivityHistory> PurchaseOrderActivityHistory { get; set; }

        /// <summary>
        /// Represents the database set for the PurchaseOrderFile entity.
        /// </summary>
        public DbSet<PurchaseOrderFile> PurchaseOrderFile { get; set; }

        /// <summary>
        /// Represents the database set for the PurchaseOrderSuggestion entity.
        /// </summary>
        public DbSet<PurchaseOrderSuggestion> PurchaseOrderSuggestion { get; set; }

        /// <summary>
        /// Represents the database set for the PurchaseOrderWorkFlowStep entity.
        /// </summary>
        public DbSet<PurchaseOrderWorkFlowStep> PurchaseOrderWorkFlowStep { get; set; }

        /// <summary>
        /// Represents the database set for the RequisitionActivityHistory entity.
        /// </summary>
        public DbSet<RequisitionActivityHistory> RequisitionActivityHistory { get; set; }

        /// <summary>
        /// Represents the database set for the RequisitionFile entity.
        /// </summary>
        public DbSet<RequisitionFile> RequisitionFile { get; set; }

        /// <summary>
        /// Represents the database set for the RequisitionSuggestion entity.
        /// </summary>
        public DbSet<RequisitionSuggestion> RequisitionSuggestion { get; set; }

        /// <summary>
        /// Represents the database set for the RequisitionWorkFlowStep entity.
        /// </summary>
        public DbSet<RequisitionWorkFlowStep> RequisitionWorkFlowStep { get; set; }

        /// <summary>
        /// Represents the database set for the StockAdjustmentActivityHistory entity.
        /// </summary>
        public DbSet<StockAdjustmentActivityHistory> StockAdjustmentActivityHistory { get; set; }

        /// <summary>
        /// Represents the database set for the StockAdjustmentFile entity.
        /// </summary>
        public DbSet<StockAdjustmentFile> StockAdjustmentFile { get; set; }

        /// <summary>
        /// Represents the database set for the StockAdjustmentSummary entity.
        /// </summary>
        public DbSet<StockAdjustmentSummary> StockAdjustmentSummary { get; set; }

        /// <summary>
        /// Represents the database set for the StockAdjustmentWorkFlowStep entity.
        /// </summary>
        public DbSet<StockAdjustmentWorkFlowStep> StockAdjustmentWorkFlowStep { get; set; }

        /// <summary>
        /// Represents the database set for the StockInvoiceSummary entity.
        /// </summary>
        public DbSet<StockInvoiceSummary> StockInvoiceSummary { get; set; }

        /// <summary>
        /// Represents the database set for the StockSummary entity.
        /// </summary>
        public DbSet<StockSummary> StockSummary { get; set; }

        /// <summary>
        /// Represents the database set for the StockTakeActivityHistory entity.
        /// </summary>
        public DbSet<StockTakeActivityHistory> StockTakeActivityHistory { get; set; }

        /// <summary>
        /// Represents the database set for the StockTakeFile entity.
        /// </summary>
        public DbSet<StockTakeFile> StockTakeFile { get; set; }

        /// <summary>
        /// Represents the database set for the StockTakeInitiated entity.
        /// </summary>
        public DbSet<StockTakeInitiated> StockTakeInitiated { get; set; }

        /// <summary>
        /// Represents the database set for the StockTakeItemBatches entity.
        /// </summary>
        public DbSet<StockTakeItemBatches> StockTakeItemBatches { get; set; }

        /// <summary>
        /// Represents the database set for the StockTakeWorkflowStep entity.
        /// </summary>
        public DbSet<StockTakeWorkflowStep> StockTakeWorkflowStep { get; set; }

        /// <summary>
        /// Represents the database set for the StockTransferActivityHistory entity.
        /// </summary>
        public DbSet<StockTransferActivityHistory> StockTransferActivityHistory { get; set; }

        /// <summary>
        /// Represents the database set for the StockTransferFile entity.
        /// </summary>
        public DbSet<StockTransferFile> StockTransferFile { get; set; }

        /// <summary>
        /// Represents the database set for the StockTransferSummary entity.
        /// </summary>
        public DbSet<StockTransferSummary> StockTransferSummary { get; set; }

        /// <summary>
        /// Represents the database set for the StockTransferWorkFlowStep entity.
        /// </summary>
        public DbSet<StockTransferWorkFlowStep> StockTransferWorkFlowStep { get; set; }

        /// <summary>
        /// Represents the database set for the SubReason entity.
        /// </summary>
        public DbSet<SubReason> SubReason { get; set; }

        /// <summary>
        /// Represents the database set for the Order entity.
        /// </summary>
        public DbSet<Order> Order { get; set; }

        /// <summary>
        /// Represents the database set for the Transaction entity.
        /// </summary>
        public DbSet<Transaction> Transaction { get; set; }

        /// <summary>
        /// Represents the database set for the UOM entity.
        /// </summary>
        public DbSet<UOM> UOM { get; set; }
    }
}