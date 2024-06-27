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
    /// The stocktakeitembatchesService responsible for managing stocktakeitembatches related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktakeitembatches information.
    /// </remarks>
    public interface IStockTakeItemBatchesService
    {
        /// <summary>Retrieves a specific stocktakeitembatches by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeitembatches</param>
        /// <returns>The stocktakeitembatches data</returns>
        StockTakeItemBatches GetById(Guid id);

        /// <summary>Retrieves a list of stocktakeitembatchess based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktakeitembatchess</returns>
        List<StockTakeItemBatches> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new stocktakeitembatches</summary>
        /// <param name="model">The stocktakeitembatches data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(StockTakeItemBatches model);

        /// <summary>Updates a specific stocktakeitembatches by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeitembatches</param>
        /// <param name="updatedEntity">The stocktakeitembatches data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, StockTakeItemBatches updatedEntity);

        /// <summary>Updates a specific stocktakeitembatches by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeitembatches</param>
        /// <param name="updatedEntity">The stocktakeitembatches data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<StockTakeItemBatches> updatedEntity);

        /// <summary>Deletes a specific stocktakeitembatches by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeitembatches</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The stocktakeitembatchesService responsible for managing stocktakeitembatches related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktakeitembatches information.
    /// </remarks>
    public class StockTakeItemBatchesService : IStockTakeItemBatchesService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the StockTakeItemBatches class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public StockTakeItemBatchesService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific stocktakeitembatches by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeitembatches</param>
        /// <returns>The stocktakeitembatches data</returns>
        public StockTakeItemBatches GetById(Guid id)
        {
            var entityData = _dbContext.StockTakeItemBatches.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of stocktakeitembatchess based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktakeitembatchess</returns>/// <exception cref="Exception"></exception>
        public List<StockTakeItemBatches> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetStockTakeItemBatches(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new stocktakeitembatches</summary>
        /// <param name="model">The stocktakeitembatches data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(StockTakeItemBatches model)
        {
            model.Id = CreateStockTakeItemBatches(model);
            return model.Id;
        }

        /// <summary>Updates a specific stocktakeitembatches by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeitembatches</param>
        /// <param name="updatedEntity">The stocktakeitembatches data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, StockTakeItemBatches updatedEntity)
        {
            UpdateStockTakeItemBatches(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific stocktakeitembatches by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeitembatches</param>
        /// <param name="updatedEntity">The stocktakeitembatches data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<StockTakeItemBatches> updatedEntity)
        {
            PatchStockTakeItemBatches(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific stocktakeitembatches by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeitembatches</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteStockTakeItemBatches(id);
            return true;
        }
        #region
        private List<StockTakeItemBatches> GetStockTakeItemBatches(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.StockTakeItemBatches.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<StockTakeItemBatches>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(StockTakeItemBatches), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<StockTakeItemBatches, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateStockTakeItemBatches(StockTakeItemBatches model)
        {
            _dbContext.StockTakeItemBatches.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateStockTakeItemBatches(Guid id, StockTakeItemBatches updatedEntity)
        {
            _dbContext.StockTakeItemBatches.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteStockTakeItemBatches(Guid id)
        {
            var entityData = _dbContext.StockTakeItemBatches.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.StockTakeItemBatches.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchStockTakeItemBatches(Guid id, JsonPatchDocument<StockTakeItemBatches> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.StockTakeItemBatches.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.StockTakeItemBatches.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}