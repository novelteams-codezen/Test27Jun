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
    /// The stocktransfersummaryService responsible for managing stocktransfersummary related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktransfersummary information.
    /// </remarks>
    public interface IStockTransferSummaryService
    {
        /// <summary>Retrieves a specific stocktransfersummary by its primary key</summary>
        /// <param name="id">The primary key of the stocktransfersummary</param>
        /// <returns>The stocktransfersummary data</returns>
        StockTransferSummary GetById(Guid id);

        /// <summary>Retrieves a list of stocktransfersummarys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktransfersummarys</returns>
        List<StockTransferSummary> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new stocktransfersummary</summary>
        /// <param name="model">The stocktransfersummary data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(StockTransferSummary model);

        /// <summary>Updates a specific stocktransfersummary by its primary key</summary>
        /// <param name="id">The primary key of the stocktransfersummary</param>
        /// <param name="updatedEntity">The stocktransfersummary data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, StockTransferSummary updatedEntity);

        /// <summary>Updates a specific stocktransfersummary by its primary key</summary>
        /// <param name="id">The primary key of the stocktransfersummary</param>
        /// <param name="updatedEntity">The stocktransfersummary data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<StockTransferSummary> updatedEntity);

        /// <summary>Deletes a specific stocktransfersummary by its primary key</summary>
        /// <param name="id">The primary key of the stocktransfersummary</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The stocktransfersummaryService responsible for managing stocktransfersummary related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktransfersummary information.
    /// </remarks>
    public class StockTransferSummaryService : IStockTransferSummaryService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the StockTransferSummary class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public StockTransferSummaryService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific stocktransfersummary by its primary key</summary>
        /// <param name="id">The primary key of the stocktransfersummary</param>
        /// <returns>The stocktransfersummary data</returns>
        public StockTransferSummary GetById(Guid id)
        {
            var entityData = _dbContext.StockTransferSummary.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of stocktransfersummarys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktransfersummarys</returns>/// <exception cref="Exception"></exception>
        public List<StockTransferSummary> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetStockTransferSummary(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new stocktransfersummary</summary>
        /// <param name="model">The stocktransfersummary data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(StockTransferSummary model)
        {
            model.Id = CreateStockTransferSummary(model);
            return model.Id;
        }

        /// <summary>Updates a specific stocktransfersummary by its primary key</summary>
        /// <param name="id">The primary key of the stocktransfersummary</param>
        /// <param name="updatedEntity">The stocktransfersummary data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, StockTransferSummary updatedEntity)
        {
            UpdateStockTransferSummary(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific stocktransfersummary by its primary key</summary>
        /// <param name="id">The primary key of the stocktransfersummary</param>
        /// <param name="updatedEntity">The stocktransfersummary data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<StockTransferSummary> updatedEntity)
        {
            PatchStockTransferSummary(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific stocktransfersummary by its primary key</summary>
        /// <param name="id">The primary key of the stocktransfersummary</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteStockTransferSummary(id);
            return true;
        }
        #region
        private List<StockTransferSummary> GetStockTransferSummary(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.StockTransferSummary.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<StockTransferSummary>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(StockTransferSummary), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<StockTransferSummary, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateStockTransferSummary(StockTransferSummary model)
        {
            _dbContext.StockTransferSummary.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateStockTransferSummary(Guid id, StockTransferSummary updatedEntity)
        {
            _dbContext.StockTransferSummary.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteStockTransferSummary(Guid id)
        {
            var entityData = _dbContext.StockTransferSummary.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.StockTransferSummary.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchStockTransferSummary(Guid id, JsonPatchDocument<StockTransferSummary> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.StockTransferSummary.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.StockTransferSummary.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}