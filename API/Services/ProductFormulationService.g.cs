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
    /// The productformulationService responsible for managing productformulation related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting productformulation information.
    /// </remarks>
    public interface IProductFormulationService
    {
        /// <summary>Retrieves a specific productformulation by its primary key</summary>
        /// <param name="id">The primary key of the productformulation</param>
        /// <returns>The productformulation data</returns>
        ProductFormulation GetById(Guid id);

        /// <summary>Retrieves a list of productformulations based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of productformulations</returns>
        List<ProductFormulation> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new productformulation</summary>
        /// <param name="model">The productformulation data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(ProductFormulation model);

        /// <summary>Updates a specific productformulation by its primary key</summary>
        /// <param name="id">The primary key of the productformulation</param>
        /// <param name="updatedEntity">The productformulation data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, ProductFormulation updatedEntity);

        /// <summary>Updates a specific productformulation by its primary key</summary>
        /// <param name="id">The primary key of the productformulation</param>
        /// <param name="updatedEntity">The productformulation data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<ProductFormulation> updatedEntity);

        /// <summary>Deletes a specific productformulation by its primary key</summary>
        /// <param name="id">The primary key of the productformulation</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The productformulationService responsible for managing productformulation related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting productformulation information.
    /// </remarks>
    public class ProductFormulationService : IProductFormulationService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the ProductFormulation class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public ProductFormulationService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific productformulation by its primary key</summary>
        /// <param name="id">The primary key of the productformulation</param>
        /// <returns>The productformulation data</returns>
        public ProductFormulation GetById(Guid id)
        {
            var entityData = _dbContext.ProductFormulation.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of productformulations based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of productformulations</returns>/// <exception cref="Exception"></exception>
        public List<ProductFormulation> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetProductFormulation(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new productformulation</summary>
        /// <param name="model">The productformulation data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(ProductFormulation model)
        {
            model.Id = CreateProductFormulation(model);
            return model.Id;
        }

        /// <summary>Updates a specific productformulation by its primary key</summary>
        /// <param name="id">The primary key of the productformulation</param>
        /// <param name="updatedEntity">The productformulation data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, ProductFormulation updatedEntity)
        {
            UpdateProductFormulation(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific productformulation by its primary key</summary>
        /// <param name="id">The primary key of the productformulation</param>
        /// <param name="updatedEntity">The productformulation data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<ProductFormulation> updatedEntity)
        {
            PatchProductFormulation(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific productformulation by its primary key</summary>
        /// <param name="id">The primary key of the productformulation</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteProductFormulation(id);
            return true;
        }
        #region
        private List<ProductFormulation> GetProductFormulation(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.ProductFormulation.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<ProductFormulation>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(ProductFormulation), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<ProductFormulation, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateProductFormulation(ProductFormulation model)
        {
            _dbContext.ProductFormulation.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateProductFormulation(Guid id, ProductFormulation updatedEntity)
        {
            _dbContext.ProductFormulation.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteProductFormulation(Guid id)
        {
            var entityData = _dbContext.ProductFormulation.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.ProductFormulation.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchProductFormulation(Guid id, JsonPatchDocument<ProductFormulation> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.ProductFormulation.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.ProductFormulation.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}