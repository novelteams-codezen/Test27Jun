using Test27Jun.Models;
using Test27Jun.Data;
using Test27Jun.Filter;
using Test27Jun.Entities;
using Test27Jun.Logger;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;

namespace Test27Jun.Services
{
    /// <summary>
    /// The productpurchaseuomService responsible for managing productpurchaseuom related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting productpurchaseuom information.
    /// </remarks>
    public interface IProductPurchaseUOMService
    {
        /// <summary>Retrieves a specific productpurchaseuom by its primary key</summary>
        /// <param name="id">The primary key of the productpurchaseuom</param>
        /// <returns>The productpurchaseuom data</returns>
        ProductPurchaseUOM GetById(Guid id);

        /// <summary>Retrieves a list of productpurchaseuoms based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of productpurchaseuoms</returns>
        List<ProductPurchaseUOM> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new productpurchaseuom</summary>
        /// <param name="model">The productpurchaseuom data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(ProductPurchaseUOM model);

        /// <summary>Updates a specific productpurchaseuom by its primary key</summary>
        /// <param name="id">The primary key of the productpurchaseuom</param>
        /// <param name="updatedEntity">The productpurchaseuom data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, ProductPurchaseUOM updatedEntity);

        /// <summary>Updates a specific productpurchaseuom by its primary key</summary>
        /// <param name="id">The primary key of the productpurchaseuom</param>
        /// <param name="updatedEntity">The productpurchaseuom data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<ProductPurchaseUOM> updatedEntity);

        /// <summary>Deletes a specific productpurchaseuom by its primary key</summary>
        /// <param name="id">The primary key of the productpurchaseuom</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The productpurchaseuomService responsible for managing productpurchaseuom related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting productpurchaseuom information.
    /// </remarks>
    public class ProductPurchaseUOMService : IProductPurchaseUOMService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the ProductPurchaseUOM class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public ProductPurchaseUOMService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific productpurchaseuom by its primary key</summary>
        /// <param name="id">The primary key of the productpurchaseuom</param>
        /// <returns>The productpurchaseuom data</returns>
        public ProductPurchaseUOM GetById(Guid id)
        {
            var entityData = _dbContext.ProductPurchaseUOM.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of productpurchaseuoms based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of productpurchaseuoms</returns>/// <exception cref="Exception"></exception>
        public List<ProductPurchaseUOM> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetProductPurchaseUOM(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new productpurchaseuom</summary>
        /// <param name="model">The productpurchaseuom data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(ProductPurchaseUOM model)
        {
            model.Id = CreateProductPurchaseUOM(model);
            return model.Id;
        }

        /// <summary>Updates a specific productpurchaseuom by its primary key</summary>
        /// <param name="id">The primary key of the productpurchaseuom</param>
        /// <param name="updatedEntity">The productpurchaseuom data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, ProductPurchaseUOM updatedEntity)
        {
            UpdateProductPurchaseUOM(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific productpurchaseuom by its primary key</summary>
        /// <param name="id">The primary key of the productpurchaseuom</param>
        /// <param name="updatedEntity">The productpurchaseuom data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<ProductPurchaseUOM> updatedEntity)
        {
            PatchProductPurchaseUOM(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific productpurchaseuom by its primary key</summary>
        /// <param name="id">The primary key of the productpurchaseuom</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteProductPurchaseUOM(id);
            return true;
        }
        #region
        private List<ProductPurchaseUOM> GetProductPurchaseUOM(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.ProductPurchaseUOM.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<ProductPurchaseUOM>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(ProductPurchaseUOM), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<ProductPurchaseUOM, object>>(Expression.Convert(property, typeof(object)), parameter);
                if (sortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase))
                {
                    result = result.OrderBy(lambda);
                }
                else if (sortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase))
                {
                    result = result.OrderByDescending(lambda);
                }
                else
                {
                    throw new ApplicationException("Invalid sort order. Use 'asc' or 'desc'");
                }
            }

            var paginatedResult = result.Skip(skip).Take(pageSize).ToList();
            return paginatedResult;
        }

        private Guid CreateProductPurchaseUOM(ProductPurchaseUOM model)
        {
            _dbContext.ProductPurchaseUOM.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateProductPurchaseUOM(Guid id, ProductPurchaseUOM updatedEntity)
        {
            _dbContext.ProductPurchaseUOM.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteProductPurchaseUOM(Guid id)
        {
            var entityData = _dbContext.ProductPurchaseUOM.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.ProductPurchaseUOM.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchProductPurchaseUOM(Guid id, JsonPatchDocument<ProductPurchaseUOM> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.ProductPurchaseUOM.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.ProductPurchaseUOM.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}