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
    /// The productgenericService responsible for managing productgeneric related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting productgeneric information.
    /// </remarks>
    public interface IProductGenericService
    {
        /// <summary>Retrieves a specific productgeneric by its primary key</summary>
        /// <param name="id">The primary key of the productgeneric</param>
        /// <returns>The productgeneric data</returns>
        ProductGeneric GetById(Guid id);

        /// <summary>Retrieves a list of productgenerics based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of productgenerics</returns>
        List<ProductGeneric> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new productgeneric</summary>
        /// <param name="model">The productgeneric data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(ProductGeneric model);

        /// <summary>Updates a specific productgeneric by its primary key</summary>
        /// <param name="id">The primary key of the productgeneric</param>
        /// <param name="updatedEntity">The productgeneric data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, ProductGeneric updatedEntity);

        /// <summary>Updates a specific productgeneric by its primary key</summary>
        /// <param name="id">The primary key of the productgeneric</param>
        /// <param name="updatedEntity">The productgeneric data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<ProductGeneric> updatedEntity);

        /// <summary>Deletes a specific productgeneric by its primary key</summary>
        /// <param name="id">The primary key of the productgeneric</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The productgenericService responsible for managing productgeneric related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting productgeneric information.
    /// </remarks>
    public class ProductGenericService : IProductGenericService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the ProductGeneric class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public ProductGenericService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific productgeneric by its primary key</summary>
        /// <param name="id">The primary key of the productgeneric</param>
        /// <returns>The productgeneric data</returns>
        public ProductGeneric GetById(Guid id)
        {
            var entityData = _dbContext.ProductGeneric.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of productgenerics based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of productgenerics</returns>/// <exception cref="Exception"></exception>
        public List<ProductGeneric> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetProductGeneric(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new productgeneric</summary>
        /// <param name="model">The productgeneric data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(ProductGeneric model)
        {
            model.Id = CreateProductGeneric(model);
            return model.Id;
        }

        /// <summary>Updates a specific productgeneric by its primary key</summary>
        /// <param name="id">The primary key of the productgeneric</param>
        /// <param name="updatedEntity">The productgeneric data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, ProductGeneric updatedEntity)
        {
            UpdateProductGeneric(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific productgeneric by its primary key</summary>
        /// <param name="id">The primary key of the productgeneric</param>
        /// <param name="updatedEntity">The productgeneric data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<ProductGeneric> updatedEntity)
        {
            PatchProductGeneric(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific productgeneric by its primary key</summary>
        /// <param name="id">The primary key of the productgeneric</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteProductGeneric(id);
            return true;
        }
        #region
        private List<ProductGeneric> GetProductGeneric(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.ProductGeneric.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<ProductGeneric>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(ProductGeneric), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<ProductGeneric, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateProductGeneric(ProductGeneric model)
        {
            _dbContext.ProductGeneric.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateProductGeneric(Guid id, ProductGeneric updatedEntity)
        {
            _dbContext.ProductGeneric.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteProductGeneric(Guid id)
        {
            var entityData = _dbContext.ProductGeneric.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.ProductGeneric.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchProductGeneric(Guid id, JsonPatchDocument<ProductGeneric> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.ProductGeneric.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.ProductGeneric.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}