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
    /// The openingbalanceService responsible for managing openingbalance related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting openingbalance information.
    /// </remarks>
    public interface IOpeningBalanceService
    {
        /// <summary>Retrieves a specific openingbalance by its primary key</summary>
        /// <param name="id">The primary key of the openingbalance</param>
        /// <returns>The openingbalance data</returns>
        OpeningBalance GetById(Guid id);

        /// <summary>Retrieves a list of openingbalances based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of openingbalances</returns>
        List<OpeningBalance> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new openingbalance</summary>
        /// <param name="model">The openingbalance data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(OpeningBalance model);

        /// <summary>Updates a specific openingbalance by its primary key</summary>
        /// <param name="id">The primary key of the openingbalance</param>
        /// <param name="updatedEntity">The openingbalance data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, OpeningBalance updatedEntity);

        /// <summary>Updates a specific openingbalance by its primary key</summary>
        /// <param name="id">The primary key of the openingbalance</param>
        /// <param name="updatedEntity">The openingbalance data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<OpeningBalance> updatedEntity);

        /// <summary>Deletes a specific openingbalance by its primary key</summary>
        /// <param name="id">The primary key of the openingbalance</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The openingbalanceService responsible for managing openingbalance related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting openingbalance information.
    /// </remarks>
    public class OpeningBalanceService : IOpeningBalanceService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the OpeningBalance class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public OpeningBalanceService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific openingbalance by its primary key</summary>
        /// <param name="id">The primary key of the openingbalance</param>
        /// <returns>The openingbalance data</returns>
        public OpeningBalance GetById(Guid id)
        {
            var entityData = _dbContext.OpeningBalance.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of openingbalances based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of openingbalances</returns>/// <exception cref="Exception"></exception>
        public List<OpeningBalance> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetOpeningBalance(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new openingbalance</summary>
        /// <param name="model">The openingbalance data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(OpeningBalance model)
        {
            model.Id = CreateOpeningBalance(model);
            return model.Id;
        }

        /// <summary>Updates a specific openingbalance by its primary key</summary>
        /// <param name="id">The primary key of the openingbalance</param>
        /// <param name="updatedEntity">The openingbalance data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, OpeningBalance updatedEntity)
        {
            UpdateOpeningBalance(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific openingbalance by its primary key</summary>
        /// <param name="id">The primary key of the openingbalance</param>
        /// <param name="updatedEntity">The openingbalance data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<OpeningBalance> updatedEntity)
        {
            PatchOpeningBalance(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific openingbalance by its primary key</summary>
        /// <param name="id">The primary key of the openingbalance</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteOpeningBalance(id);
            return true;
        }
        #region
        private List<OpeningBalance> GetOpeningBalance(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.OpeningBalance.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<OpeningBalance>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(OpeningBalance), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<OpeningBalance, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateOpeningBalance(OpeningBalance model)
        {
            _dbContext.OpeningBalance.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateOpeningBalance(Guid id, OpeningBalance updatedEntity)
        {
            _dbContext.OpeningBalance.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteOpeningBalance(Guid id)
        {
            var entityData = _dbContext.OpeningBalance.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.OpeningBalance.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchOpeningBalance(Guid id, JsonPatchDocument<OpeningBalance> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.OpeningBalance.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.OpeningBalance.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}