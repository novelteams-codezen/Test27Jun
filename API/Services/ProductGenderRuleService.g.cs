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
    /// The productgenderruleService responsible for managing productgenderrule related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting productgenderrule information.
    /// </remarks>
    public interface IProductGenderRuleService
    {
        /// <summary>Retrieves a specific productgenderrule by its primary key</summary>
        /// <param name="id">The primary key of the productgenderrule</param>
        /// <returns>The productgenderrule data</returns>
        ProductGenderRule GetById(Guid id);

        /// <summary>Retrieves a list of productgenderrules based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of productgenderrules</returns>
        List<ProductGenderRule> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new productgenderrule</summary>
        /// <param name="model">The productgenderrule data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(ProductGenderRule model);

        /// <summary>Updates a specific productgenderrule by its primary key</summary>
        /// <param name="id">The primary key of the productgenderrule</param>
        /// <param name="updatedEntity">The productgenderrule data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, ProductGenderRule updatedEntity);

        /// <summary>Updates a specific productgenderrule by its primary key</summary>
        /// <param name="id">The primary key of the productgenderrule</param>
        /// <param name="updatedEntity">The productgenderrule data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<ProductGenderRule> updatedEntity);

        /// <summary>Deletes a specific productgenderrule by its primary key</summary>
        /// <param name="id">The primary key of the productgenderrule</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The productgenderruleService responsible for managing productgenderrule related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting productgenderrule information.
    /// </remarks>
    public class ProductGenderRuleService : IProductGenderRuleService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the ProductGenderRule class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public ProductGenderRuleService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific productgenderrule by its primary key</summary>
        /// <param name="id">The primary key of the productgenderrule</param>
        /// <returns>The productgenderrule data</returns>
        public ProductGenderRule GetById(Guid id)
        {
            var entityData = _dbContext.ProductGenderRule.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of productgenderrules based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of productgenderrules</returns>/// <exception cref="Exception"></exception>
        public List<ProductGenderRule> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetProductGenderRule(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new productgenderrule</summary>
        /// <param name="model">The productgenderrule data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(ProductGenderRule model)
        {
            model.Id = CreateProductGenderRule(model);
            return model.Id;
        }

        /// <summary>Updates a specific productgenderrule by its primary key</summary>
        /// <param name="id">The primary key of the productgenderrule</param>
        /// <param name="updatedEntity">The productgenderrule data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, ProductGenderRule updatedEntity)
        {
            UpdateProductGenderRule(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific productgenderrule by its primary key</summary>
        /// <param name="id">The primary key of the productgenderrule</param>
        /// <param name="updatedEntity">The productgenderrule data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<ProductGenderRule> updatedEntity)
        {
            PatchProductGenderRule(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific productgenderrule by its primary key</summary>
        /// <param name="id">The primary key of the productgenderrule</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteProductGenderRule(id);
            return true;
        }
        #region
        private List<ProductGenderRule> GetProductGenderRule(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.ProductGenderRule.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<ProductGenderRule>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(ProductGenderRule), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<ProductGenderRule, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateProductGenderRule(ProductGenderRule model)
        {
            _dbContext.ProductGenderRule.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateProductGenderRule(Guid id, ProductGenderRule updatedEntity)
        {
            _dbContext.ProductGenderRule.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteProductGenderRule(Guid id)
        {
            var entityData = _dbContext.ProductGenderRule.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.ProductGenderRule.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchProductGenderRule(Guid id, JsonPatchDocument<ProductGenderRule> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.ProductGenderRule.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.ProductGenderRule.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}