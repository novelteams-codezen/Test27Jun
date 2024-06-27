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
    /// The stocktakeitemService responsible for managing stocktakeitem related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktakeitem information.
    /// </remarks>
    public interface IStockTakeItemService
    {
        /// <summary>Retrieves a specific stocktakeitem by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeitem</param>
        /// <returns>The stocktakeitem data</returns>
        StockTakeItem GetById(Guid id);

        /// <summary>Retrieves a list of stocktakeitems based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktakeitems</returns>
        List<StockTakeItem> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new stocktakeitem</summary>
        /// <param name="model">The stocktakeitem data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(StockTakeItem model);

        /// <summary>Updates a specific stocktakeitem by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeitem</param>
        /// <param name="updatedEntity">The stocktakeitem data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, StockTakeItem updatedEntity);

        /// <summary>Updates a specific stocktakeitem by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeitem</param>
        /// <param name="updatedEntity">The stocktakeitem data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<StockTakeItem> updatedEntity);

        /// <summary>Deletes a specific stocktakeitem by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeitem</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The stocktakeitemService responsible for managing stocktakeitem related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktakeitem information.
    /// </remarks>
    public class StockTakeItemService : IStockTakeItemService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the StockTakeItem class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public StockTakeItemService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific stocktakeitem by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeitem</param>
        /// <returns>The stocktakeitem data</returns>
        public StockTakeItem GetById(Guid id)
        {
            var entityData = _dbContext.StockTakeItem.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of stocktakeitems based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktakeitems</returns>/// <exception cref="Exception"></exception>
        public List<StockTakeItem> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetStockTakeItem(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new stocktakeitem</summary>
        /// <param name="model">The stocktakeitem data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(StockTakeItem model)
        {
            model.Id = CreateStockTakeItem(model);
            return model.Id;
        }

        /// <summary>Updates a specific stocktakeitem by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeitem</param>
        /// <param name="updatedEntity">The stocktakeitem data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, StockTakeItem updatedEntity)
        {
            UpdateStockTakeItem(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific stocktakeitem by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeitem</param>
        /// <param name="updatedEntity">The stocktakeitem data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<StockTakeItem> updatedEntity)
        {
            PatchStockTakeItem(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific stocktakeitem by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeitem</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteStockTakeItem(id);
            return true;
        }
        #region
        private List<StockTakeItem> GetStockTakeItem(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.StockTakeItem.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<StockTakeItem>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(StockTakeItem), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<StockTakeItem, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateStockTakeItem(StockTakeItem model)
        {
            _dbContext.StockTakeItem.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateStockTakeItem(Guid id, StockTakeItem updatedEntity)
        {
            _dbContext.StockTakeItem.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteStockTakeItem(Guid id)
        {
            var entityData = _dbContext.StockTakeItem.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.StockTakeItem.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchStockTakeItem(Guid id, JsonPatchDocument<StockTakeItem> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.StockTakeItem.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.StockTakeItem.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}