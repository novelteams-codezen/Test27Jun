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
    /// The stocktakeinitiatedService responsible for managing stocktakeinitiated related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktakeinitiated information.
    /// </remarks>
    public interface IStockTakeInitiatedService
    {
        /// <summary>Retrieves a specific stocktakeinitiated by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeinitiated</param>
        /// <returns>The stocktakeinitiated data</returns>
        StockTakeInitiated GetById(Guid id);

        /// <summary>Retrieves a list of stocktakeinitiateds based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktakeinitiateds</returns>
        List<StockTakeInitiated> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new stocktakeinitiated</summary>
        /// <param name="model">The stocktakeinitiated data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(StockTakeInitiated model);

        /// <summary>Updates a specific stocktakeinitiated by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeinitiated</param>
        /// <param name="updatedEntity">The stocktakeinitiated data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, StockTakeInitiated updatedEntity);

        /// <summary>Updates a specific stocktakeinitiated by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeinitiated</param>
        /// <param name="updatedEntity">The stocktakeinitiated data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<StockTakeInitiated> updatedEntity);

        /// <summary>Deletes a specific stocktakeinitiated by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeinitiated</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The stocktakeinitiatedService responsible for managing stocktakeinitiated related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktakeinitiated information.
    /// </remarks>
    public class StockTakeInitiatedService : IStockTakeInitiatedService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the StockTakeInitiated class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public StockTakeInitiatedService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific stocktakeinitiated by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeinitiated</param>
        /// <returns>The stocktakeinitiated data</returns>
        public StockTakeInitiated GetById(Guid id)
        {
            var entityData = _dbContext.StockTakeInitiated.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of stocktakeinitiateds based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktakeinitiateds</returns>/// <exception cref="Exception"></exception>
        public List<StockTakeInitiated> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetStockTakeInitiated(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new stocktakeinitiated</summary>
        /// <param name="model">The stocktakeinitiated data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(StockTakeInitiated model)
        {
            model.Id = CreateStockTakeInitiated(model);
            return model.Id;
        }

        /// <summary>Updates a specific stocktakeinitiated by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeinitiated</param>
        /// <param name="updatedEntity">The stocktakeinitiated data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, StockTakeInitiated updatedEntity)
        {
            UpdateStockTakeInitiated(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific stocktakeinitiated by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeinitiated</param>
        /// <param name="updatedEntity">The stocktakeinitiated data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<StockTakeInitiated> updatedEntity)
        {
            PatchStockTakeInitiated(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific stocktakeinitiated by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeinitiated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteStockTakeInitiated(id);
            return true;
        }
        #region
        private List<StockTakeInitiated> GetStockTakeInitiated(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.StockTakeInitiated.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<StockTakeInitiated>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(StockTakeInitiated), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<StockTakeInitiated, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateStockTakeInitiated(StockTakeInitiated model)
        {
            _dbContext.StockTakeInitiated.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateStockTakeInitiated(Guid id, StockTakeInitiated updatedEntity)
        {
            _dbContext.StockTakeInitiated.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteStockTakeInitiated(Guid id)
        {
            var entityData = _dbContext.StockTakeInitiated.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.StockTakeInitiated.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchStockTakeInitiated(Guid id, JsonPatchDocument<StockTakeInitiated> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.StockTakeInitiated.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.StockTakeInitiated.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}