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
    /// The productlocationpriceService responsible for managing productlocationprice related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting productlocationprice information.
    /// </remarks>
    public interface IProductLocationPriceService
    {
        /// <summary>Retrieves a specific productlocationprice by its primary key</summary>
        /// <param name="id">The primary key of the productlocationprice</param>
        /// <returns>The productlocationprice data</returns>
        ProductLocationPrice GetById(Guid id);

        /// <summary>Retrieves a list of productlocationprices based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of productlocationprices</returns>
        List<ProductLocationPrice> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new productlocationprice</summary>
        /// <param name="model">The productlocationprice data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(ProductLocationPrice model);

        /// <summary>Updates a specific productlocationprice by its primary key</summary>
        /// <param name="id">The primary key of the productlocationprice</param>
        /// <param name="updatedEntity">The productlocationprice data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, ProductLocationPrice updatedEntity);

        /// <summary>Updates a specific productlocationprice by its primary key</summary>
        /// <param name="id">The primary key of the productlocationprice</param>
        /// <param name="updatedEntity">The productlocationprice data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<ProductLocationPrice> updatedEntity);

        /// <summary>Deletes a specific productlocationprice by its primary key</summary>
        /// <param name="id">The primary key of the productlocationprice</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The productlocationpriceService responsible for managing productlocationprice related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting productlocationprice information.
    /// </remarks>
    public class ProductLocationPriceService : IProductLocationPriceService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the ProductLocationPrice class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public ProductLocationPriceService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific productlocationprice by its primary key</summary>
        /// <param name="id">The primary key of the productlocationprice</param>
        /// <returns>The productlocationprice data</returns>
        public ProductLocationPrice GetById(Guid id)
        {
            var entityData = _dbContext.ProductLocationPrice.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of productlocationprices based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of productlocationprices</returns>/// <exception cref="Exception"></exception>
        public List<ProductLocationPrice> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetProductLocationPrice(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new productlocationprice</summary>
        /// <param name="model">The productlocationprice data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(ProductLocationPrice model)
        {
            model.Id = CreateProductLocationPrice(model);
            return model.Id;
        }

        /// <summary>Updates a specific productlocationprice by its primary key</summary>
        /// <param name="id">The primary key of the productlocationprice</param>
        /// <param name="updatedEntity">The productlocationprice data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, ProductLocationPrice updatedEntity)
        {
            UpdateProductLocationPrice(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific productlocationprice by its primary key</summary>
        /// <param name="id">The primary key of the productlocationprice</param>
        /// <param name="updatedEntity">The productlocationprice data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<ProductLocationPrice> updatedEntity)
        {
            PatchProductLocationPrice(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific productlocationprice by its primary key</summary>
        /// <param name="id">The primary key of the productlocationprice</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteProductLocationPrice(id);
            return true;
        }
        #region
        private List<ProductLocationPrice> GetProductLocationPrice(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.ProductLocationPrice.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<ProductLocationPrice>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(ProductLocationPrice), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<ProductLocationPrice, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateProductLocationPrice(ProductLocationPrice model)
        {
            _dbContext.ProductLocationPrice.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateProductLocationPrice(Guid id, ProductLocationPrice updatedEntity)
        {
            _dbContext.ProductLocationPrice.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteProductLocationPrice(Guid id)
        {
            var entityData = _dbContext.ProductLocationPrice.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.ProductLocationPrice.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchProductLocationPrice(Guid id, JsonPatchDocument<ProductLocationPrice> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.ProductLocationPrice.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.ProductLocationPrice.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}