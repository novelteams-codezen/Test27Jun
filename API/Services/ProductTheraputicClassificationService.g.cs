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
    /// The producttheraputicclassificationService responsible for managing producttheraputicclassification related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting producttheraputicclassification information.
    /// </remarks>
    public interface IProductTheraputicClassificationService
    {
        /// <summary>Retrieves a specific producttheraputicclassification by its primary key</summary>
        /// <param name="id">The primary key of the producttheraputicclassification</param>
        /// <returns>The producttheraputicclassification data</returns>
        ProductTheraputicClassification GetById(Guid id);

        /// <summary>Retrieves a list of producttheraputicclassifications based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of producttheraputicclassifications</returns>
        List<ProductTheraputicClassification> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new producttheraputicclassification</summary>
        /// <param name="model">The producttheraputicclassification data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(ProductTheraputicClassification model);

        /// <summary>Updates a specific producttheraputicclassification by its primary key</summary>
        /// <param name="id">The primary key of the producttheraputicclassification</param>
        /// <param name="updatedEntity">The producttheraputicclassification data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, ProductTheraputicClassification updatedEntity);

        /// <summary>Updates a specific producttheraputicclassification by its primary key</summary>
        /// <param name="id">The primary key of the producttheraputicclassification</param>
        /// <param name="updatedEntity">The producttheraputicclassification data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<ProductTheraputicClassification> updatedEntity);

        /// <summary>Deletes a specific producttheraputicclassification by its primary key</summary>
        /// <param name="id">The primary key of the producttheraputicclassification</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The producttheraputicclassificationService responsible for managing producttheraputicclassification related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting producttheraputicclassification information.
    /// </remarks>
    public class ProductTheraputicClassificationService : IProductTheraputicClassificationService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the ProductTheraputicClassification class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public ProductTheraputicClassificationService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific producttheraputicclassification by its primary key</summary>
        /// <param name="id">The primary key of the producttheraputicclassification</param>
        /// <returns>The producttheraputicclassification data</returns>
        public ProductTheraputicClassification GetById(Guid id)
        {
            var entityData = _dbContext.ProductTheraputicClassification.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of producttheraputicclassifications based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of producttheraputicclassifications</returns>/// <exception cref="Exception"></exception>
        public List<ProductTheraputicClassification> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetProductTheraputicClassification(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new producttheraputicclassification</summary>
        /// <param name="model">The producttheraputicclassification data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(ProductTheraputicClassification model)
        {
            model.Id = CreateProductTheraputicClassification(model);
            return model.Id;
        }

        /// <summary>Updates a specific producttheraputicclassification by its primary key</summary>
        /// <param name="id">The primary key of the producttheraputicclassification</param>
        /// <param name="updatedEntity">The producttheraputicclassification data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, ProductTheraputicClassification updatedEntity)
        {
            UpdateProductTheraputicClassification(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific producttheraputicclassification by its primary key</summary>
        /// <param name="id">The primary key of the producttheraputicclassification</param>
        /// <param name="updatedEntity">The producttheraputicclassification data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<ProductTheraputicClassification> updatedEntity)
        {
            PatchProductTheraputicClassification(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific producttheraputicclassification by its primary key</summary>
        /// <param name="id">The primary key of the producttheraputicclassification</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteProductTheraputicClassification(id);
            return true;
        }
        #region
        private List<ProductTheraputicClassification> GetProductTheraputicClassification(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.ProductTheraputicClassification.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<ProductTheraputicClassification>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(ProductTheraputicClassification), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<ProductTheraputicClassification, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateProductTheraputicClassification(ProductTheraputicClassification model)
        {
            _dbContext.ProductTheraputicClassification.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateProductTheraputicClassification(Guid id, ProductTheraputicClassification updatedEntity)
        {
            _dbContext.ProductTheraputicClassification.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteProductTheraputicClassification(Guid id)
        {
            var entityData = _dbContext.ProductTheraputicClassification.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.ProductTheraputicClassification.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchProductTheraputicClassification(Guid id, JsonPatchDocument<ProductTheraputicClassification> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.ProductTheraputicClassification.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.ProductTheraputicClassification.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}