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
    /// The stockadjustmentsummaryService responsible for managing stockadjustmentsummary related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stockadjustmentsummary information.
    /// </remarks>
    public interface IStockAdjustmentSummaryService
    {
        /// <summary>Retrieves a specific stockadjustmentsummary by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustmentsummary</param>
        /// <returns>The stockadjustmentsummary data</returns>
        StockAdjustmentSummary GetById(Guid id);

        /// <summary>Retrieves a list of stockadjustmentsummarys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stockadjustmentsummarys</returns>
        List<StockAdjustmentSummary> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new stockadjustmentsummary</summary>
        /// <param name="model">The stockadjustmentsummary data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(StockAdjustmentSummary model);

        /// <summary>Updates a specific stockadjustmentsummary by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustmentsummary</param>
        /// <param name="updatedEntity">The stockadjustmentsummary data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, StockAdjustmentSummary updatedEntity);

        /// <summary>Updates a specific stockadjustmentsummary by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustmentsummary</param>
        /// <param name="updatedEntity">The stockadjustmentsummary data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<StockAdjustmentSummary> updatedEntity);

        /// <summary>Deletes a specific stockadjustmentsummary by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustmentsummary</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The stockadjustmentsummaryService responsible for managing stockadjustmentsummary related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stockadjustmentsummary information.
    /// </remarks>
    public class StockAdjustmentSummaryService : IStockAdjustmentSummaryService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the StockAdjustmentSummary class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public StockAdjustmentSummaryService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific stockadjustmentsummary by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustmentsummary</param>
        /// <returns>The stockadjustmentsummary data</returns>
        public StockAdjustmentSummary GetById(Guid id)
        {
            var entityData = _dbContext.StockAdjustmentSummary.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of stockadjustmentsummarys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stockadjustmentsummarys</returns>/// <exception cref="Exception"></exception>
        public List<StockAdjustmentSummary> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetStockAdjustmentSummary(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new stockadjustmentsummary</summary>
        /// <param name="model">The stockadjustmentsummary data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(StockAdjustmentSummary model)
        {
            model.Id = CreateStockAdjustmentSummary(model);
            return model.Id;
        }

        /// <summary>Updates a specific stockadjustmentsummary by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustmentsummary</param>
        /// <param name="updatedEntity">The stockadjustmentsummary data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, StockAdjustmentSummary updatedEntity)
        {
            UpdateStockAdjustmentSummary(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific stockadjustmentsummary by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustmentsummary</param>
        /// <param name="updatedEntity">The stockadjustmentsummary data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<StockAdjustmentSummary> updatedEntity)
        {
            PatchStockAdjustmentSummary(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific stockadjustmentsummary by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustmentsummary</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteStockAdjustmentSummary(id);
            return true;
        }
        #region
        private List<StockAdjustmentSummary> GetStockAdjustmentSummary(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.StockAdjustmentSummary.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<StockAdjustmentSummary>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(StockAdjustmentSummary), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<StockAdjustmentSummary, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateStockAdjustmentSummary(StockAdjustmentSummary model)
        {
            _dbContext.StockAdjustmentSummary.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateStockAdjustmentSummary(Guid id, StockAdjustmentSummary updatedEntity)
        {
            _dbContext.StockAdjustmentSummary.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteStockAdjustmentSummary(Guid id)
        {
            var entityData = _dbContext.StockAdjustmentSummary.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.StockAdjustmentSummary.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchStockAdjustmentSummary(Guid id, JsonPatchDocument<StockAdjustmentSummary> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.StockAdjustmentSummary.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.StockAdjustmentSummary.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}