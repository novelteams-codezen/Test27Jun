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
    /// The organisationstockbalanceService responsible for managing organisationstockbalance related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting organisationstockbalance information.
    /// </remarks>
    public interface IOrganisationStockBalanceService
    {
        /// <summary>Retrieves a specific organisationstockbalance by its primary key</summary>
        /// <param name="id">The primary key of the organisationstockbalance</param>
        /// <returns>The organisationstockbalance data</returns>
        OrganisationStockBalance GetById(Guid id);

        /// <summary>Retrieves a list of organisationstockbalances based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of organisationstockbalances</returns>
        List<OrganisationStockBalance> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new organisationstockbalance</summary>
        /// <param name="model">The organisationstockbalance data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(OrganisationStockBalance model);

        /// <summary>Updates a specific organisationstockbalance by its primary key</summary>
        /// <param name="id">The primary key of the organisationstockbalance</param>
        /// <param name="updatedEntity">The organisationstockbalance data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, OrganisationStockBalance updatedEntity);

        /// <summary>Updates a specific organisationstockbalance by its primary key</summary>
        /// <param name="id">The primary key of the organisationstockbalance</param>
        /// <param name="updatedEntity">The organisationstockbalance data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<OrganisationStockBalance> updatedEntity);

        /// <summary>Deletes a specific organisationstockbalance by its primary key</summary>
        /// <param name="id">The primary key of the organisationstockbalance</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The organisationstockbalanceService responsible for managing organisationstockbalance related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting organisationstockbalance information.
    /// </remarks>
    public class OrganisationStockBalanceService : IOrganisationStockBalanceService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the OrganisationStockBalance class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public OrganisationStockBalanceService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific organisationstockbalance by its primary key</summary>
        /// <param name="id">The primary key of the organisationstockbalance</param>
        /// <returns>The organisationstockbalance data</returns>
        public OrganisationStockBalance GetById(Guid id)
        {
            var entityData = _dbContext.OrganisationStockBalance.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of organisationstockbalances based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of organisationstockbalances</returns>/// <exception cref="Exception"></exception>
        public List<OrganisationStockBalance> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetOrganisationStockBalance(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new organisationstockbalance</summary>
        /// <param name="model">The organisationstockbalance data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(OrganisationStockBalance model)
        {
            model.Id = CreateOrganisationStockBalance(model);
            return model.Id;
        }

        /// <summary>Updates a specific organisationstockbalance by its primary key</summary>
        /// <param name="id">The primary key of the organisationstockbalance</param>
        /// <param name="updatedEntity">The organisationstockbalance data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, OrganisationStockBalance updatedEntity)
        {
            UpdateOrganisationStockBalance(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific organisationstockbalance by its primary key</summary>
        /// <param name="id">The primary key of the organisationstockbalance</param>
        /// <param name="updatedEntity">The organisationstockbalance data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<OrganisationStockBalance> updatedEntity)
        {
            PatchOrganisationStockBalance(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific organisationstockbalance by its primary key</summary>
        /// <param name="id">The primary key of the organisationstockbalance</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteOrganisationStockBalance(id);
            return true;
        }
        #region
        private List<OrganisationStockBalance> GetOrganisationStockBalance(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.OrganisationStockBalance.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<OrganisationStockBalance>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(OrganisationStockBalance), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<OrganisationStockBalance, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateOrganisationStockBalance(OrganisationStockBalance model)
        {
            _dbContext.OrganisationStockBalance.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateOrganisationStockBalance(Guid id, OrganisationStockBalance updatedEntity)
        {
            _dbContext.OrganisationStockBalance.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteOrganisationStockBalance(Guid id)
        {
            var entityData = _dbContext.OrganisationStockBalance.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.OrganisationStockBalance.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchOrganisationStockBalance(Guid id, JsonPatchDocument<OrganisationStockBalance> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.OrganisationStockBalance.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.OrganisationStockBalance.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}