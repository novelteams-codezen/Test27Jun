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
    /// The stocktransferactivityhistoryService responsible for managing stocktransferactivityhistory related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktransferactivityhistory information.
    /// </remarks>
    public interface IStockTransferActivityHistoryService
    {
        /// <summary>Retrieves a specific stocktransferactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferactivityhistory</param>
        /// <returns>The stocktransferactivityhistory data</returns>
        StockTransferActivityHistory GetById(Guid id);

        /// <summary>Retrieves a list of stocktransferactivityhistorys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktransferactivityhistorys</returns>
        List<StockTransferActivityHistory> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new stocktransferactivityhistory</summary>
        /// <param name="model">The stocktransferactivityhistory data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(StockTransferActivityHistory model);

        /// <summary>Updates a specific stocktransferactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferactivityhistory</param>
        /// <param name="updatedEntity">The stocktransferactivityhistory data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, StockTransferActivityHistory updatedEntity);

        /// <summary>Updates a specific stocktransferactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferactivityhistory</param>
        /// <param name="updatedEntity">The stocktransferactivityhistory data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<StockTransferActivityHistory> updatedEntity);

        /// <summary>Deletes a specific stocktransferactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferactivityhistory</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The stocktransferactivityhistoryService responsible for managing stocktransferactivityhistory related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktransferactivityhistory information.
    /// </remarks>
    public class StockTransferActivityHistoryService : IStockTransferActivityHistoryService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the StockTransferActivityHistory class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public StockTransferActivityHistoryService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific stocktransferactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferactivityhistory</param>
        /// <returns>The stocktransferactivityhistory data</returns>
        public StockTransferActivityHistory GetById(Guid id)
        {
            var entityData = _dbContext.StockTransferActivityHistory.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of stocktransferactivityhistorys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktransferactivityhistorys</returns>/// <exception cref="Exception"></exception>
        public List<StockTransferActivityHistory> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetStockTransferActivityHistory(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new stocktransferactivityhistory</summary>
        /// <param name="model">The stocktransferactivityhistory data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(StockTransferActivityHistory model)
        {
            model.Id = CreateStockTransferActivityHistory(model);
            return model.Id;
        }

        /// <summary>Updates a specific stocktransferactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferactivityhistory</param>
        /// <param name="updatedEntity">The stocktransferactivityhistory data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, StockTransferActivityHistory updatedEntity)
        {
            UpdateStockTransferActivityHistory(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific stocktransferactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferactivityhistory</param>
        /// <param name="updatedEntity">The stocktransferactivityhistory data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<StockTransferActivityHistory> updatedEntity)
        {
            PatchStockTransferActivityHistory(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific stocktransferactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferactivityhistory</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteStockTransferActivityHistory(id);
            return true;
        }
        #region
        private List<StockTransferActivityHistory> GetStockTransferActivityHistory(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.StockTransferActivityHistory.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<StockTransferActivityHistory>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(StockTransferActivityHistory), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<StockTransferActivityHistory, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateStockTransferActivityHistory(StockTransferActivityHistory model)
        {
            _dbContext.StockTransferActivityHistory.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateStockTransferActivityHistory(Guid id, StockTransferActivityHistory updatedEntity)
        {
            _dbContext.StockTransferActivityHistory.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteStockTransferActivityHistory(Guid id)
        {
            var entityData = _dbContext.StockTransferActivityHistory.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.StockTransferActivityHistory.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchStockTransferActivityHistory(Guid id, JsonPatchDocument<StockTransferActivityHistory> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.StockTransferActivityHistory.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.StockTransferActivityHistory.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}