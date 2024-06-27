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
    /// The stocksummaryService responsible for managing stocksummary related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocksummary information.
    /// </remarks>
    public interface IStockSummaryService
    {
        /// <summary>Retrieves a specific stocksummary by its primary key</summary>
        /// <param name="id">The primary key of the stocksummary</param>
        /// <returns>The stocksummary data</returns>
        StockSummary GetById(Guid id);

        /// <summary>Retrieves a list of stocksummarys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocksummarys</returns>
        List<StockSummary> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new stocksummary</summary>
        /// <param name="model">The stocksummary data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(StockSummary model);

        /// <summary>Updates a specific stocksummary by its primary key</summary>
        /// <param name="id">The primary key of the stocksummary</param>
        /// <param name="updatedEntity">The stocksummary data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, StockSummary updatedEntity);

        /// <summary>Updates a specific stocksummary by its primary key</summary>
        /// <param name="id">The primary key of the stocksummary</param>
        /// <param name="updatedEntity">The stocksummary data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<StockSummary> updatedEntity);

        /// <summary>Deletes a specific stocksummary by its primary key</summary>
        /// <param name="id">The primary key of the stocksummary</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The stocksummaryService responsible for managing stocksummary related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocksummary information.
    /// </remarks>
    public class StockSummaryService : IStockSummaryService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the StockSummary class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public StockSummaryService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific stocksummary by its primary key</summary>
        /// <param name="id">The primary key of the stocksummary</param>
        /// <returns>The stocksummary data</returns>
        public StockSummary GetById(Guid id)
        {
            var entityData = _dbContext.StockSummary.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of stocksummarys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocksummarys</returns>/// <exception cref="Exception"></exception>
        public List<StockSummary> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetStockSummary(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new stocksummary</summary>
        /// <param name="model">The stocksummary data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(StockSummary model)
        {
            model.Id = CreateStockSummary(model);
            return model.Id;
        }

        /// <summary>Updates a specific stocksummary by its primary key</summary>
        /// <param name="id">The primary key of the stocksummary</param>
        /// <param name="updatedEntity">The stocksummary data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, StockSummary updatedEntity)
        {
            UpdateStockSummary(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific stocksummary by its primary key</summary>
        /// <param name="id">The primary key of the stocksummary</param>
        /// <param name="updatedEntity">The stocksummary data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<StockSummary> updatedEntity)
        {
            PatchStockSummary(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific stocksummary by its primary key</summary>
        /// <param name="id">The primary key of the stocksummary</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteStockSummary(id);
            return true;
        }
        #region
        private List<StockSummary> GetStockSummary(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.StockSummary.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<StockSummary>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(StockSummary), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<StockSummary, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateStockSummary(StockSummary model)
        {
            _dbContext.StockSummary.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateStockSummary(Guid id, StockSummary updatedEntity)
        {
            _dbContext.StockSummary.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteStockSummary(Guid id)
        {
            var entityData = _dbContext.StockSummary.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.StockSummary.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchStockSummary(Guid id, JsonPatchDocument<StockSummary> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.StockSummary.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.StockSummary.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}