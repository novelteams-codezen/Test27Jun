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
    /// The stocktransferitemService responsible for managing stocktransferitem related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktransferitem information.
    /// </remarks>
    public interface IStockTransferItemService
    {
        /// <summary>Retrieves a specific stocktransferitem by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferitem</param>
        /// <returns>The stocktransferitem data</returns>
        StockTransferItem GetById(Guid id);

        /// <summary>Retrieves a list of stocktransferitems based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktransferitems</returns>
        List<StockTransferItem> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new stocktransferitem</summary>
        /// <param name="model">The stocktransferitem data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(StockTransferItem model);

        /// <summary>Updates a specific stocktransferitem by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferitem</param>
        /// <param name="updatedEntity">The stocktransferitem data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, StockTransferItem updatedEntity);

        /// <summary>Updates a specific stocktransferitem by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferitem</param>
        /// <param name="updatedEntity">The stocktransferitem data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<StockTransferItem> updatedEntity);

        /// <summary>Deletes a specific stocktransferitem by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferitem</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The stocktransferitemService responsible for managing stocktransferitem related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktransferitem information.
    /// </remarks>
    public class StockTransferItemService : IStockTransferItemService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the StockTransferItem class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public StockTransferItemService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific stocktransferitem by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferitem</param>
        /// <returns>The stocktransferitem data</returns>
        public StockTransferItem GetById(Guid id)
        {
            var entityData = _dbContext.StockTransferItem.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of stocktransferitems based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktransferitems</returns>/// <exception cref="Exception"></exception>
        public List<StockTransferItem> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetStockTransferItem(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new stocktransferitem</summary>
        /// <param name="model">The stocktransferitem data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(StockTransferItem model)
        {
            model.Id = CreateStockTransferItem(model);
            return model.Id;
        }

        /// <summary>Updates a specific stocktransferitem by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferitem</param>
        /// <param name="updatedEntity">The stocktransferitem data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, StockTransferItem updatedEntity)
        {
            UpdateStockTransferItem(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific stocktransferitem by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferitem</param>
        /// <param name="updatedEntity">The stocktransferitem data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<StockTransferItem> updatedEntity)
        {
            PatchStockTransferItem(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific stocktransferitem by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferitem</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteStockTransferItem(id);
            return true;
        }
        #region
        private List<StockTransferItem> GetStockTransferItem(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.StockTransferItem.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<StockTransferItem>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(StockTransferItem), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<StockTransferItem, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateStockTransferItem(StockTransferItem model)
        {
            _dbContext.StockTransferItem.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateStockTransferItem(Guid id, StockTransferItem updatedEntity)
        {
            _dbContext.StockTransferItem.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteStockTransferItem(Guid id)
        {
            var entityData = _dbContext.StockTransferItem.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.StockTransferItem.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchStockTransferItem(Guid id, JsonPatchDocument<StockTransferItem> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.StockTransferItem.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.StockTransferItem.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}