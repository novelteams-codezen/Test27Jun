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
    /// The stockadjustmentactivityhistoryService responsible for managing stockadjustmentactivityhistory related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stockadjustmentactivityhistory information.
    /// </remarks>
    public interface IStockAdjustmentActivityHistoryService
    {
        /// <summary>Retrieves a specific stockadjustmentactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustmentactivityhistory</param>
        /// <returns>The stockadjustmentactivityhistory data</returns>
        StockAdjustmentActivityHistory GetById(Guid id);

        /// <summary>Retrieves a list of stockadjustmentactivityhistorys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stockadjustmentactivityhistorys</returns>
        List<StockAdjustmentActivityHistory> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new stockadjustmentactivityhistory</summary>
        /// <param name="model">The stockadjustmentactivityhistory data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(StockAdjustmentActivityHistory model);

        /// <summary>Updates a specific stockadjustmentactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustmentactivityhistory</param>
        /// <param name="updatedEntity">The stockadjustmentactivityhistory data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, StockAdjustmentActivityHistory updatedEntity);

        /// <summary>Updates a specific stockadjustmentactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustmentactivityhistory</param>
        /// <param name="updatedEntity">The stockadjustmentactivityhistory data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<StockAdjustmentActivityHistory> updatedEntity);

        /// <summary>Deletes a specific stockadjustmentactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustmentactivityhistory</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The stockadjustmentactivityhistoryService responsible for managing stockadjustmentactivityhistory related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stockadjustmentactivityhistory information.
    /// </remarks>
    public class StockAdjustmentActivityHistoryService : IStockAdjustmentActivityHistoryService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the StockAdjustmentActivityHistory class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public StockAdjustmentActivityHistoryService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific stockadjustmentactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustmentactivityhistory</param>
        /// <returns>The stockadjustmentactivityhistory data</returns>
        public StockAdjustmentActivityHistory GetById(Guid id)
        {
            var entityData = _dbContext.StockAdjustmentActivityHistory.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of stockadjustmentactivityhistorys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stockadjustmentactivityhistorys</returns>/// <exception cref="Exception"></exception>
        public List<StockAdjustmentActivityHistory> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetStockAdjustmentActivityHistory(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new stockadjustmentactivityhistory</summary>
        /// <param name="model">The stockadjustmentactivityhistory data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(StockAdjustmentActivityHistory model)
        {
            model.Id = CreateStockAdjustmentActivityHistory(model);
            return model.Id;
        }

        /// <summary>Updates a specific stockadjustmentactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustmentactivityhistory</param>
        /// <param name="updatedEntity">The stockadjustmentactivityhistory data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, StockAdjustmentActivityHistory updatedEntity)
        {
            UpdateStockAdjustmentActivityHistory(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific stockadjustmentactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustmentactivityhistory</param>
        /// <param name="updatedEntity">The stockadjustmentactivityhistory data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<StockAdjustmentActivityHistory> updatedEntity)
        {
            PatchStockAdjustmentActivityHistory(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific stockadjustmentactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustmentactivityhistory</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteStockAdjustmentActivityHistory(id);
            return true;
        }
        #region
        private List<StockAdjustmentActivityHistory> GetStockAdjustmentActivityHistory(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.StockAdjustmentActivityHistory.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<StockAdjustmentActivityHistory>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(StockAdjustmentActivityHistory), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<StockAdjustmentActivityHistory, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateStockAdjustmentActivityHistory(StockAdjustmentActivityHistory model)
        {
            _dbContext.StockAdjustmentActivityHistory.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateStockAdjustmentActivityHistory(Guid id, StockAdjustmentActivityHistory updatedEntity)
        {
            _dbContext.StockAdjustmentActivityHistory.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteStockAdjustmentActivityHistory(Guid id)
        {
            var entityData = _dbContext.StockAdjustmentActivityHistory.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.StockAdjustmentActivityHistory.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchStockAdjustmentActivityHistory(Guid id, JsonPatchDocument<StockAdjustmentActivityHistory> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.StockAdjustmentActivityHistory.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.StockAdjustmentActivityHistory.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}