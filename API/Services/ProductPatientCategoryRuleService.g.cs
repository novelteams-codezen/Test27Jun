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
    /// The productpatientcategoryruleService responsible for managing productpatientcategoryrule related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting productpatientcategoryrule information.
    /// </remarks>
    public interface IProductPatientCategoryRuleService
    {
        /// <summary>Retrieves a specific productpatientcategoryrule by its primary key</summary>
        /// <param name="id">The primary key of the productpatientcategoryrule</param>
        /// <returns>The productpatientcategoryrule data</returns>
        ProductPatientCategoryRule GetById(Guid id);

        /// <summary>Retrieves a list of productpatientcategoryrules based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of productpatientcategoryrules</returns>
        List<ProductPatientCategoryRule> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new productpatientcategoryrule</summary>
        /// <param name="model">The productpatientcategoryrule data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(ProductPatientCategoryRule model);

        /// <summary>Updates a specific productpatientcategoryrule by its primary key</summary>
        /// <param name="id">The primary key of the productpatientcategoryrule</param>
        /// <param name="updatedEntity">The productpatientcategoryrule data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, ProductPatientCategoryRule updatedEntity);

        /// <summary>Updates a specific productpatientcategoryrule by its primary key</summary>
        /// <param name="id">The primary key of the productpatientcategoryrule</param>
        /// <param name="updatedEntity">The productpatientcategoryrule data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<ProductPatientCategoryRule> updatedEntity);

        /// <summary>Deletes a specific productpatientcategoryrule by its primary key</summary>
        /// <param name="id">The primary key of the productpatientcategoryrule</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The productpatientcategoryruleService responsible for managing productpatientcategoryrule related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting productpatientcategoryrule information.
    /// </remarks>
    public class ProductPatientCategoryRuleService : IProductPatientCategoryRuleService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the ProductPatientCategoryRule class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public ProductPatientCategoryRuleService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific productpatientcategoryrule by its primary key</summary>
        /// <param name="id">The primary key of the productpatientcategoryrule</param>
        /// <returns>The productpatientcategoryrule data</returns>
        public ProductPatientCategoryRule GetById(Guid id)
        {
            var entityData = _dbContext.ProductPatientCategoryRule.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of productpatientcategoryrules based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of productpatientcategoryrules</returns>/// <exception cref="Exception"></exception>
        public List<ProductPatientCategoryRule> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetProductPatientCategoryRule(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new productpatientcategoryrule</summary>
        /// <param name="model">The productpatientcategoryrule data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(ProductPatientCategoryRule model)
        {
            model.Id = CreateProductPatientCategoryRule(model);
            return model.Id;
        }

        /// <summary>Updates a specific productpatientcategoryrule by its primary key</summary>
        /// <param name="id">The primary key of the productpatientcategoryrule</param>
        /// <param name="updatedEntity">The productpatientcategoryrule data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, ProductPatientCategoryRule updatedEntity)
        {
            UpdateProductPatientCategoryRule(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific productpatientcategoryrule by its primary key</summary>
        /// <param name="id">The primary key of the productpatientcategoryrule</param>
        /// <param name="updatedEntity">The productpatientcategoryrule data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<ProductPatientCategoryRule> updatedEntity)
        {
            PatchProductPatientCategoryRule(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific productpatientcategoryrule by its primary key</summary>
        /// <param name="id">The primary key of the productpatientcategoryrule</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteProductPatientCategoryRule(id);
            return true;
        }
        #region
        private List<ProductPatientCategoryRule> GetProductPatientCategoryRule(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.ProductPatientCategoryRule.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<ProductPatientCategoryRule>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(ProductPatientCategoryRule), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<ProductPatientCategoryRule, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateProductPatientCategoryRule(ProductPatientCategoryRule model)
        {
            _dbContext.ProductPatientCategoryRule.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateProductPatientCategoryRule(Guid id, ProductPatientCategoryRule updatedEntity)
        {
            _dbContext.ProductPatientCategoryRule.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteProductPatientCategoryRule(Guid id)
        {
            var entityData = _dbContext.ProductPatientCategoryRule.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.ProductPatientCategoryRule.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchProductPatientCategoryRule(Guid id, JsonPatchDocument<ProductPatientCategoryRule> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.ProductPatientCategoryRule.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.ProductPatientCategoryRule.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}