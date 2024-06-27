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
    /// The stockadjustmentService responsible for managing stockadjustment related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stockadjustment information.
    /// </remarks>
    public interface IStockAdjustmentService
    {
        /// <summary>Retrieves a specific stockadjustment by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustment</param>
        /// <returns>The stockadjustment data</returns>
        StockAdjustment GetById(Guid id);

        /// <summary>Retrieves a list of stockadjustments based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stockadjustments</returns>
        List<StockAdjustment> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new stockadjustment</summary>
        /// <param name="model">The stockadjustment data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(StockAdjustment model);

        /// <summary>Updates a specific stockadjustment by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustment</param>
        /// <param name="updatedEntity">The stockadjustment data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, StockAdjustment updatedEntity);

        /// <summary>Updates a specific stockadjustment by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustment</param>
        /// <param name="updatedEntity">The stockadjustment data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<StockAdjustment> updatedEntity);

        /// <summary>Deletes a specific stockadjustment by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustment</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The stockadjustmentService responsible for managing stockadjustment related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stockadjustment information.
    /// </remarks>
    public class StockAdjustmentService : IStockAdjustmentService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the StockAdjustment class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public StockAdjustmentService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific stockadjustment by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustment</param>
        /// <returns>The stockadjustment data</returns>
        public StockAdjustment GetById(Guid id)
        {
            var entityData = _dbContext.StockAdjustment.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of stockadjustments based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stockadjustments</returns>/// <exception cref="Exception"></exception>
        public List<StockAdjustment> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetStockAdjustment(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new stockadjustment</summary>
        /// <param name="model">The stockadjustment data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(StockAdjustment model)
        {
            model.Id = CreateStockAdjustment(model);
            return model.Id;
        }

        /// <summary>Updates a specific stockadjustment by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustment</param>
        /// <param name="updatedEntity">The stockadjustment data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, StockAdjustment updatedEntity)
        {
            UpdateStockAdjustment(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific stockadjustment by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustment</param>
        /// <param name="updatedEntity">The stockadjustment data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<StockAdjustment> updatedEntity)
        {
            PatchStockAdjustment(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific stockadjustment by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustment</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteStockAdjustment(id);
            return true;
        }
        #region
        private List<StockAdjustment> GetStockAdjustment(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.StockAdjustment.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<StockAdjustment>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(StockAdjustment), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<StockAdjustment, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateStockAdjustment(StockAdjustment model)
        {
            _dbContext.StockAdjustment.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateStockAdjustment(Guid id, StockAdjustment updatedEntity)
        {
            _dbContext.StockAdjustment.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteStockAdjustment(Guid id)
        {
            var entityData = _dbContext.StockAdjustment.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.StockAdjustment.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchStockAdjustment(Guid id, JsonPatchDocument<StockAdjustment> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.StockAdjustment.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.StockAdjustment.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}