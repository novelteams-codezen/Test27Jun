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
    /// The stocktakeactivityhistoryService responsible for managing stocktakeactivityhistory related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktakeactivityhistory information.
    /// </remarks>
    public interface IStockTakeActivityHistoryService
    {
        /// <summary>Retrieves a specific stocktakeactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeactivityhistory</param>
        /// <returns>The stocktakeactivityhistory data</returns>
        StockTakeActivityHistory GetById(Guid id);

        /// <summary>Retrieves a list of stocktakeactivityhistorys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktakeactivityhistorys</returns>
        List<StockTakeActivityHistory> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new stocktakeactivityhistory</summary>
        /// <param name="model">The stocktakeactivityhistory data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(StockTakeActivityHistory model);

        /// <summary>Updates a specific stocktakeactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeactivityhistory</param>
        /// <param name="updatedEntity">The stocktakeactivityhistory data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, StockTakeActivityHistory updatedEntity);

        /// <summary>Updates a specific stocktakeactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeactivityhistory</param>
        /// <param name="updatedEntity">The stocktakeactivityhistory data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<StockTakeActivityHistory> updatedEntity);

        /// <summary>Deletes a specific stocktakeactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeactivityhistory</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The stocktakeactivityhistoryService responsible for managing stocktakeactivityhistory related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktakeactivityhistory information.
    /// </remarks>
    public class StockTakeActivityHistoryService : IStockTakeActivityHistoryService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the StockTakeActivityHistory class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public StockTakeActivityHistoryService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific stocktakeactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeactivityhistory</param>
        /// <returns>The stocktakeactivityhistory data</returns>
        public StockTakeActivityHistory GetById(Guid id)
        {
            var entityData = _dbContext.StockTakeActivityHistory.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of stocktakeactivityhistorys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktakeactivityhistorys</returns>/// <exception cref="Exception"></exception>
        public List<StockTakeActivityHistory> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetStockTakeActivityHistory(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new stocktakeactivityhistory</summary>
        /// <param name="model">The stocktakeactivityhistory data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(StockTakeActivityHistory model)
        {
            model.Id = CreateStockTakeActivityHistory(model);
            return model.Id;
        }

        /// <summary>Updates a specific stocktakeactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeactivityhistory</param>
        /// <param name="updatedEntity">The stocktakeactivityhistory data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, StockTakeActivityHistory updatedEntity)
        {
            UpdateStockTakeActivityHistory(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific stocktakeactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeactivityhistory</param>
        /// <param name="updatedEntity">The stocktakeactivityhistory data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<StockTakeActivityHistory> updatedEntity)
        {
            PatchStockTakeActivityHistory(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific stocktakeactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeactivityhistory</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteStockTakeActivityHistory(id);
            return true;
        }
        #region
        private List<StockTakeActivityHistory> GetStockTakeActivityHistory(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.StockTakeActivityHistory.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<StockTakeActivityHistory>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(StockTakeActivityHistory), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<StockTakeActivityHistory, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateStockTakeActivityHistory(StockTakeActivityHistory model)
        {
            _dbContext.StockTakeActivityHistory.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateStockTakeActivityHistory(Guid id, StockTakeActivityHistory updatedEntity)
        {
            _dbContext.StockTakeActivityHistory.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteStockTakeActivityHistory(Guid id)
        {
            var entityData = _dbContext.StockTakeActivityHistory.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.StockTakeActivityHistory.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchStockTakeActivityHistory(Guid id, JsonPatchDocument<StockTakeActivityHistory> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.StockTakeActivityHistory.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.StockTakeActivityHistory.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}