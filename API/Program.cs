using Test27Jun.Data;
using Microsoft.OpenApi.Models;
using System.Reflection;
using NLog.Web;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Test27Jun.Models;
using Test27Jun.Services;
using Test27Jun.Logger;
using Newtonsoft.Json;
var builder = WebApplication.CreateBuilder(args);

// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Host.UseNLog();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test27Jun", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.IgnoreObsoleteActions();
    c.IgnoreObsoleteProperties();
    c.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter into field the word 'Bearer' following by space and JWT",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                    Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string> ()
                    }
                });
});
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
// Build the configuration object from appsettings.json
var config = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json", optional: false)
  .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
  .Build();
//Set value to appsetting
AppSetting.JwtIssuer = config.GetValue<string>("Jwt:Issuer");
AppSetting.JwtKey = config.GetValue<string>("Jwt:Key");
AppSetting.TokenExpirationtime = config.GetValue<int>("TokenExpirationtime");
// Add NLog as the logging service
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders(); // Remove other logging providers
    loggingBuilder.SetMinimumLevel(LogLevel.Trace);
});
// Add JWT authentication services
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = AppSetting.JwtIssuer,
        ValidAudience = AppSetting.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSetting.JwtKey ?? ""))
    };
});
//Service inject
builder.Services.AddScoped<IUOMService, UOMService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ISubReasonService, SubReasonService>();
builder.Services.AddScoped<IStockTransferWorkFlowStepService, StockTransferWorkFlowStepService>();
builder.Services.AddScoped<IStockTransferSummaryService, StockTransferSummaryService>();
builder.Services.AddScoped<IStockTransferFileService, StockTransferFileService>();
builder.Services.AddScoped<IStockTransferActivityHistoryService, StockTransferActivityHistoryService>();
builder.Services.AddScoped<IStockTakeWorkflowStepService, StockTakeWorkflowStepService>();
builder.Services.AddScoped<IStockTakeItemBatchesService, StockTakeItemBatchesService>();
builder.Services.AddScoped<IStockTakeInitiatedService, StockTakeInitiatedService>();
builder.Services.AddScoped<IStockTakeFileService, StockTakeFileService>();
builder.Services.AddScoped<IStockTakeActivityHistoryService, StockTakeActivityHistoryService>();
builder.Services.AddScoped<IStockSummaryService, StockSummaryService>();
builder.Services.AddScoped<IStockInvoiceSummaryService, StockInvoiceSummaryService>();
builder.Services.AddScoped<IStockAdjustmentWorkFlowStepService, StockAdjustmentWorkFlowStepService>();
builder.Services.AddScoped<IStockAdjustmentSummaryService, StockAdjustmentSummaryService>();
builder.Services.AddScoped<IStockAdjustmentFileService, StockAdjustmentFileService>();
builder.Services.AddScoped<IStockAdjustmentActivityHistoryService, StockAdjustmentActivityHistoryService>();
builder.Services.AddScoped<IRequisitionWorkFlowStepService, RequisitionWorkFlowStepService>();
builder.Services.AddScoped<IRequisitionSuggestionService, RequisitionSuggestionService>();
builder.Services.AddScoped<IRequisitionFileService, RequisitionFileService>();
builder.Services.AddScoped<IRequisitionActivityHistoryService, RequisitionActivityHistoryService>();
builder.Services.AddScoped<IPurchaseOrderWorkFlowStepService, PurchaseOrderWorkFlowStepService>();
builder.Services.AddScoped<IPurchaseOrderSuggestionService, PurchaseOrderSuggestionService>();
builder.Services.AddScoped<IPurchaseOrderFileService, PurchaseOrderFileService>();
builder.Services.AddScoped<IPurchaseOrderActivityHistoryService, PurchaseOrderActivityHistoryService>();
builder.Services.AddScoped<IProductUomService, ProductUomService>();
builder.Services.AddScoped<IProductTheraputicSubClassificationService, ProductTheraputicSubClassificationService>();
builder.Services.AddScoped<IProductTheraputicClassificationService, ProductTheraputicClassificationService>();
builder.Services.AddScoped<IProductPurchaseUOMService, ProductPurchaseUOMService>();
builder.Services.AddScoped<IProductPatientCategoryRuleService, ProductPatientCategoryRuleService>();
builder.Services.AddScoped<IProductManufactureService, ProductManufactureService>();
builder.Services.AddScoped<IProductLocationPriceService, ProductLocationPriceService>();
builder.Services.AddScoped<IProductGenericService, ProductGenericService>();
builder.Services.AddScoped<IProductGenderRuleService, ProductGenderRuleService>();
builder.Services.AddScoped<IProductFormulationService, ProductFormulationService>();
builder.Services.AddScoped<IProductCustomUOMService, ProductCustomUOMService>();
builder.Services.AddScoped<IProductClassificationService, ProductClassificationService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IPriceListVersionComponentService, PriceListVersionComponentService>();
builder.Services.AddScoped<IPriceListComponentService, PriceListComponentService>();
builder.Services.AddScoped<IOrganisationStockBalanceService, OrganisationStockBalanceService>();
builder.Services.AddScoped<IInventorySettingsService, InventorySettingsService>();
builder.Services.AddScoped<IGoodsReturnWorkFlowStepService, GoodsReturnWorkFlowStepService>();
builder.Services.AddScoped<IGoodsReturnSummaryService, GoodsReturnSummaryService>();
builder.Services.AddScoped<IGoodsReturnFileService, GoodsReturnFileService>();
builder.Services.AddScoped<IGoodsReturnActivityHistoryService, GoodsReturnActivityHistoryService>();
builder.Services.AddScoped<IGoodsReceiptWorkFlowStepService, GoodsReceiptWorkFlowStepService>();
builder.Services.AddScoped<IGoodsReceiptSummaryService, GoodsReceiptSummaryService>();
builder.Services.AddScoped<IGoodsReceiptSuggestionService, GoodsReceiptSuggestionService>();
builder.Services.AddScoped<IGoodsReceiptFileService, GoodsReceiptFileService>();
builder.Services.AddScoped<IGoodsReceiptActivityHistoryService, GoodsReceiptActivityHistoryService>();
builder.Services.AddScoped<IFevouriteRequisitionLineService, FevouriteRequisitionLineService>();
builder.Services.AddScoped<IFavouritePurchaseOrderLineService, FavouritePurchaseOrderLineService>();
builder.Services.AddScoped<IFavouriteGoodsReceiptLineService, FavouriteGoodsReceiptLineService>();
builder.Services.AddScoped<IDrugScheduleService, DrugScheduleService>();
builder.Services.AddScoped<ISubLocationService, SubLocationService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IOpeningBalanceItemService, OpeningBalanceItemService>();
builder.Services.AddScoped<IOpeningBalanceService, OpeningBalanceService>();
builder.Services.AddScoped<IPriceListVersionItemService, PriceListVersionItemService>();
builder.Services.AddScoped<IPriceListVersionService, PriceListVersionService>();
builder.Services.AddScoped<IPriceListItemService, PriceListItemService>();
builder.Services.AddScoped<IPriceListService, PriceListService>();
builder.Services.AddScoped<IStockTakeItemService, StockTakeItemService>();
builder.Services.AddScoped<IStockTakeService, StockTakeService>();
builder.Services.AddScoped<IStockAdjustmentItemService, StockAdjustmentItemService>();
builder.Services.AddScoped<IStockAdjustmentService, StockAdjustmentService>();
builder.Services.AddScoped<IStockTransferItemService, StockTransferItemService>();
builder.Services.AddScoped<IStockTransferService, StockTransferService>();
builder.Services.AddScoped<IGoodsReturnItemService, GoodsReturnItemService>();
builder.Services.AddScoped<IGoodsReturnService, GoodsReturnService>();
builder.Services.AddScoped<IGoodsReceiptPurchaseOrderRelationService, GoodsReceiptPurchaseOrderRelationService>();
builder.Services.AddScoped<IGoodsReceiptItemService, GoodsReceiptItemService>();
builder.Services.AddScoped<IGoodsReceiptService, GoodsReceiptService>();
builder.Services.AddScoped<IPurchaseOrderLineService, PurchaseOrderLineService>();
builder.Services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
builder.Services.AddScoped<IRequisitionLineService, RequisitionLineService>();
builder.Services.AddScoped<IRequisitionService, RequisitionService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IProductBatchService, ProductBatchService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<IEntityService, EntityService>();
builder.Services.AddScoped<IRoleEntitlementService, RoleEntitlementService>();
builder.Services.AddScoped<IUserInRoleService, UserInRoleService>();
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddTransient<ILoggerService, LoggerService>();
//Json handler
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // Configure Newtonsoft.Json settings here
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
});
//Inject context
builder.Services.AddTransient<Test27JunContext>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.SetIsOriginAllowed(_ => true)
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test27Jun API v1");
        c.RoutePrefix = string.Empty;
    });
    app.MapSwagger().RequireAuthorization();
}
app.UseAuthorization();
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();