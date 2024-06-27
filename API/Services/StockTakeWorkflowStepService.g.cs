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
    /// The stocktakeworkflowstepService responsible for managing stocktakeworkflowstep related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktakeworkflowstep information.
    /// </remarks>
    public interface IStockTakeWorkflowStepService
    {
        /// <summary>Retrieves a specific stocktakeworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeworkflowstep</param>
        /// <returns>The stocktakeworkflowstep data</returns>
        StockTakeWorkflowStep GetById(Guid id);

        /// <summary>Retrieves a list of stocktakeworkflowsteps based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktakeworkflowsteps</returns>
        List<StockTakeWorkflowStep> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new stocktakeworkflowstep</summary>
        /// <param name="model">The stocktakeworkflowstep data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(StockTakeWorkflowStep model);

        /// <summary>Updates a specific stocktakeworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeworkflowstep</param>
        /// <param name="updatedEntity">The stocktakeworkflowstep data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, StockTakeWorkflowStep updatedEntity);

        /// <summary>Updates a specific stocktakeworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeworkflowstep</param>
        /// <param name="updatedEntity">The stocktakeworkflowstep data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<StockTakeWorkflowStep> updatedEntity);

        /// <summary>Deletes a specific stocktakeworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeworkflowstep</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The stocktakeworkflowstepService responsible for managing stocktakeworkflowstep related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktakeworkflowstep information.
    /// </remarks>
    public class StockTakeWorkflowStepService : IStockTakeWorkflowStepService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the StockTakeWorkflowStep class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public StockTakeWorkflowStepService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific stocktakeworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeworkflowstep</param>
        /// <returns>The stocktakeworkflowstep data</returns>
        public StockTakeWorkflowStep GetById(Guid id)
        {
            var entityData = _dbContext.StockTakeWorkflowStep.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of stocktakeworkflowsteps based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktakeworkflowsteps</returns>/// <exception cref="Exception"></exception>
        public List<StockTakeWorkflowStep> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetStockTakeWorkflowStep(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new stocktakeworkflowstep</summary>
        /// <param name="model">The stocktakeworkflowstep data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(StockTakeWorkflowStep model)
        {
            model.Id = CreateStockTakeWorkflowStep(model);
            return model.Id;
        }

        /// <summary>Updates a specific stocktakeworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeworkflowstep</param>
        /// <param name="updatedEntity">The stocktakeworkflowstep data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, StockTakeWorkflowStep updatedEntity)
        {
            UpdateStockTakeWorkflowStep(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific stocktakeworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeworkflowstep</param>
        /// <param name="updatedEntity">The stocktakeworkflowstep data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<StockTakeWorkflowStep> updatedEntity)
        {
            PatchStockTakeWorkflowStep(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific stocktakeworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the stocktakeworkflowstep</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteStockTakeWorkflowStep(id);
            return true;
        }
        #region
        private List<StockTakeWorkflowStep> GetStockTakeWorkflowStep(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.StockTakeWorkflowStep.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<StockTakeWorkflowStep>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(StockTakeWorkflowStep), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<StockTakeWorkflowStep, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateStockTakeWorkflowStep(StockTakeWorkflowStep model)
        {
            _dbContext.StockTakeWorkflowStep.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateStockTakeWorkflowStep(Guid id, StockTakeWorkflowStep updatedEntity)
        {
            _dbContext.StockTakeWorkflowStep.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteStockTakeWorkflowStep(Guid id)
        {
            var entityData = _dbContext.StockTakeWorkflowStep.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.StockTakeWorkflowStep.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchStockTakeWorkflowStep(Guid id, JsonPatchDocument<StockTakeWorkflowStep> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.StockTakeWorkflowStep.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.StockTakeWorkflowStep.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}