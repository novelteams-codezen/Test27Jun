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
    /// The stockinvoicesummaryService responsible for managing stockinvoicesummary related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stockinvoicesummary information.
    /// </remarks>
    public interface IStockInvoiceSummaryService
    {
        /// <summary>Retrieves a specific stockinvoicesummary by its primary key</summary>
        /// <param name="id">The primary key of the stockinvoicesummary</param>
        /// <returns>The stockinvoicesummary data</returns>
        StockInvoiceSummary GetById(Guid id);

        /// <summary>Retrieves a list of stockinvoicesummarys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stockinvoicesummarys</returns>
        List<StockInvoiceSummary> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new stockinvoicesummary</summary>
        /// <param name="model">The stockinvoicesummary data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(StockInvoiceSummary model);

        /// <summary>Updates a specific stockinvoicesummary by its primary key</summary>
        /// <param name="id">The primary key of the stockinvoicesummary</param>
        /// <param name="updatedEntity">The stockinvoicesummary data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, StockInvoiceSummary updatedEntity);

        /// <summary>Updates a specific stockinvoicesummary by its primary key</summary>
        /// <param name="id">The primary key of the stockinvoicesummary</param>
        /// <param name="updatedEntity">The stockinvoicesummary data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<StockInvoiceSummary> updatedEntity);

        /// <summary>Deletes a specific stockinvoicesummary by its primary key</summary>
        /// <param name="id">The primary key of the stockinvoicesummary</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The stockinvoicesummaryService responsible for managing stockinvoicesummary related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stockinvoicesummary information.
    /// </remarks>
    public class StockInvoiceSummaryService : IStockInvoiceSummaryService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the StockInvoiceSummary class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public StockInvoiceSummaryService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific stockinvoicesummary by its primary key</summary>
        /// <param name="id">The primary key of the stockinvoicesummary</param>
        /// <returns>The stockinvoicesummary data</returns>
        public StockInvoiceSummary GetById(Guid id)
        {
            var entityData = _dbContext.StockInvoiceSummary.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of stockinvoicesummarys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stockinvoicesummarys</returns>/// <exception cref="Exception"></exception>
        public List<StockInvoiceSummary> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetStockInvoiceSummary(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new stockinvoicesummary</summary>
        /// <param name="model">The stockinvoicesummary data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(StockInvoiceSummary model)
        {
            model.Id = CreateStockInvoiceSummary(model);
            return model.Id;
        }

        /// <summary>Updates a specific stockinvoicesummary by its primary key</summary>
        /// <param name="id">The primary key of the stockinvoicesummary</param>
        /// <param name="updatedEntity">The stockinvoicesummary data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, StockInvoiceSummary updatedEntity)
        {
            UpdateStockInvoiceSummary(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific stockinvoicesummary by its primary key</summary>
        /// <param name="id">The primary key of the stockinvoicesummary</param>
        /// <param name="updatedEntity">The stockinvoicesummary data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<StockInvoiceSummary> updatedEntity)
        {
            PatchStockInvoiceSummary(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific stockinvoicesummary by its primary key</summary>
        /// <param name="id">The primary key of the stockinvoicesummary</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteStockInvoiceSummary(id);
            return true;
        }
        #region
        private List<StockInvoiceSummary> GetStockInvoiceSummary(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.StockInvoiceSummary.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<StockInvoiceSummary>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(StockInvoiceSummary), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<StockInvoiceSummary, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateStockInvoiceSummary(StockInvoiceSummary model)
        {
            _dbContext.StockInvoiceSummary.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateStockInvoiceSummary(Guid id, StockInvoiceSummary updatedEntity)
        {
            _dbContext.StockInvoiceSummary.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteStockInvoiceSummary(Guid id)
        {
            var entityData = _dbContext.StockInvoiceSummary.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.StockInvoiceSummary.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchStockInvoiceSummary(Guid id, JsonPatchDocument<StockInvoiceSummary> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.StockInvoiceSummary.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.StockInvoiceSummary.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}