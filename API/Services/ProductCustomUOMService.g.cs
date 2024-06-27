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
    /// The productcustomuomService responsible for managing productcustomuom related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting productcustomuom information.
    /// </remarks>
    public interface IProductCustomUOMService
    {
        /// <summary>Retrieves a specific productcustomuom by its primary key</summary>
        /// <param name="id">The primary key of the productcustomuom</param>
        /// <returns>The productcustomuom data</returns>
        ProductCustomUOM GetById(Guid id);

        /// <summary>Retrieves a list of productcustomuoms based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of productcustomuoms</returns>
        List<ProductCustomUOM> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new productcustomuom</summary>
        /// <param name="model">The productcustomuom data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(ProductCustomUOM model);

        /// <summary>Updates a specific productcustomuom by its primary key</summary>
        /// <param name="id">The primary key of the productcustomuom</param>
        /// <param name="updatedEntity">The productcustomuom data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, ProductCustomUOM updatedEntity);

        /// <summary>Updates a specific productcustomuom by its primary key</summary>
        /// <param name="id">The primary key of the productcustomuom</param>
        /// <param name="updatedEntity">The productcustomuom data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<ProductCustomUOM> updatedEntity);

        /// <summary>Deletes a specific productcustomuom by its primary key</summary>
        /// <param name="id">The primary key of the productcustomuom</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The productcustomuomService responsible for managing productcustomuom related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting productcustomuom information.
    /// </remarks>
    public class ProductCustomUOMService : IProductCustomUOMService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the ProductCustomUOM class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public ProductCustomUOMService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific productcustomuom by its primary key</summary>
        /// <param name="id">The primary key of the productcustomuom</param>
        /// <returns>The productcustomuom data</returns>
        public ProductCustomUOM GetById(Guid id)
        {
            var entityData = _dbContext.ProductCustomUOM.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of productcustomuoms based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of productcustomuoms</returns>/// <exception cref="Exception"></exception>
        public List<ProductCustomUOM> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetProductCustomUOM(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new productcustomuom</summary>
        /// <param name="model">The productcustomuom data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(ProductCustomUOM model)
        {
            model.Id = CreateProductCustomUOM(model);
            return model.Id;
        }

        /// <summary>Updates a specific productcustomuom by its primary key</summary>
        /// <param name="id">The primary key of the productcustomuom</param>
        /// <param name="updatedEntity">The productcustomuom data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, ProductCustomUOM updatedEntity)
        {
            UpdateProductCustomUOM(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific productcustomuom by its primary key</summary>
        /// <param name="id">The primary key of the productcustomuom</param>
        /// <param name="updatedEntity">The productcustomuom data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<ProductCustomUOM> updatedEntity)
        {
            PatchProductCustomUOM(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific productcustomuom by its primary key</summary>
        /// <param name="id">The primary key of the productcustomuom</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteProductCustomUOM(id);
            return true;
        }
        #region
        private List<ProductCustomUOM> GetProductCustomUOM(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.ProductCustomUOM.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<ProductCustomUOM>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(ProductCustomUOM), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<ProductCustomUOM, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateProductCustomUOM(ProductCustomUOM model)
        {
            _dbContext.ProductCustomUOM.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateProductCustomUOM(Guid id, ProductCustomUOM updatedEntity)
        {
            _dbContext.ProductCustomUOM.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteProductCustomUOM(Guid id)
        {
            var entityData = _dbContext.ProductCustomUOM.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.ProductCustomUOM.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchProductCustomUOM(Guid id, JsonPatchDocument<ProductCustomUOM> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.ProductCustomUOM.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.ProductCustomUOM.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}