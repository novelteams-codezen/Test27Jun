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
    /// The stocktakeService responsible for managing stocktake related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktake information.
    /// </remarks>
    public interface IStockTakeService
    {
        /// <summary>Retrieves a specific stocktake by its primary key</summary>
        /// <param name="id">The primary key of the stocktake</param>
        /// <returns>The stocktake data</returns>
        StockTake GetById(Guid id);

        /// <summary>Retrieves a list of stocktakes based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktakes</returns>
        List<StockTake> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new stocktake</summary>
        /// <param name="model">The stocktake data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(StockTake model);

        /// <summary>Updates a specific stocktake by its primary key</summary>
        /// <param name="id">The primary key of the stocktake</param>
        /// <param name="updatedEntity">The stocktake data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, StockTake updatedEntity);

        /// <summary>Updates a specific stocktake by its primary key</summary>
        /// <param name="id">The primary key of the stocktake</param>
        /// <param name="updatedEntity">The stocktake data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<StockTake> updatedEntity);

        /// <summary>Deletes a specific stocktake by its primary key</summary>
        /// <param name="id">The primary key of the stocktake</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The stocktakeService responsible for managing stocktake related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktake information.
    /// </remarks>
    public class StockTakeService : IStockTakeService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the StockTake class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public StockTakeService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific stocktake by its primary key</summary>
        /// <param name="id">The primary key of the stocktake</param>
        /// <returns>The stocktake data</returns>
        public StockTake GetById(Guid id)
        {
            var entityData = _dbContext.StockTake.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of stocktakes based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktakes</returns>/// <exception cref="Exception"></exception>
        public List<StockTake> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetStockTake(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new stocktake</summary>
        /// <param name="model">The stocktake data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(StockTake model)
        {
            model.Id = CreateStockTake(model);
            return model.Id;
        }

        /// <summary>Updates a specific stocktake by its primary key</summary>
        /// <param name="id">The primary key of the stocktake</param>
        /// <param name="updatedEntity">The stocktake data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, StockTake updatedEntity)
        {
            UpdateStockTake(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific stocktake by its primary key</summary>
        /// <param name="id">The primary key of the stocktake</param>
        /// <param name="updatedEntity">The stocktake data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<StockTake> updatedEntity)
        {
            PatchStockTake(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific stocktake by its primary key</summary>
        /// <param name="id">The primary key of the stocktake</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteStockTake(id);
            return true;
        }
        #region
        private List<StockTake> GetStockTake(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.StockTake.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<StockTake>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(StockTake), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<StockTake, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateStockTake(StockTake model)
        {
            _dbContext.StockTake.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateStockTake(Guid id, StockTake updatedEntity)
        {
            _dbContext.StockTake.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteStockTake(Guid id)
        {
            var entityData = _dbContext.StockTake.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.StockTake.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchStockTake(Guid id, JsonPatchDocument<StockTake> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.StockTake.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.StockTake.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}