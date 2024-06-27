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
    /// The stockadjustmentworkflowstepService responsible for managing stockadjustmentworkflowstep related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stockadjustmentworkflowstep information.
    /// </remarks>
    public interface IStockAdjustmentWorkFlowStepService
    {
        /// <summary>Retrieves a specific stockadjustmentworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustmentworkflowstep</param>
        /// <returns>The stockadjustmentworkflowstep data</returns>
        StockAdjustmentWorkFlowStep GetById(Guid id);

        /// <summary>Retrieves a list of stockadjustmentworkflowsteps based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stockadjustmentworkflowsteps</returns>
        List<StockAdjustmentWorkFlowStep> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new stockadjustmentworkflowstep</summary>
        /// <param name="model">The stockadjustmentworkflowstep data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(StockAdjustmentWorkFlowStep model);

        /// <summary>Updates a specific stockadjustmentworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustmentworkflowstep</param>
        /// <param name="updatedEntity">The stockadjustmentworkflowstep data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, StockAdjustmentWorkFlowStep updatedEntity);

        /// <summary>Updates a specific stockadjustmentworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustmentworkflowstep</param>
        /// <param name="updatedEntity">The stockadjustmentworkflowstep data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<StockAdjustmentWorkFlowStep> updatedEntity);

        /// <summary>Deletes a specific stockadjustmentworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustmentworkflowstep</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The stockadjustmentworkflowstepService responsible for managing stockadjustmentworkflowstep related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stockadjustmentworkflowstep information.
    /// </remarks>
    public class StockAdjustmentWorkFlowStepService : IStockAdjustmentWorkFlowStepService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the StockAdjustmentWorkFlowStep class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public StockAdjustmentWorkFlowStepService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific stockadjustmentworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustmentworkflowstep</param>
        /// <returns>The stockadjustmentworkflowstep data</returns>
        public StockAdjustmentWorkFlowStep GetById(Guid id)
        {
            var entityData = _dbContext.StockAdjustmentWorkFlowStep.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of stockadjustmentworkflowsteps based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stockadjustmentworkflowsteps</returns>/// <exception cref="Exception"></exception>
        public List<StockAdjustmentWorkFlowStep> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetStockAdjustmentWorkFlowStep(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new stockadjustmentworkflowstep</summary>
        /// <param name="model">The stockadjustmentworkflowstep data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(StockAdjustmentWorkFlowStep model)
        {
            model.Id = CreateStockAdjustmentWorkFlowStep(model);
            return model.Id;
        }

        /// <summary>Updates a specific stockadjustmentworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustmentworkflowstep</param>
        /// <param name="updatedEntity">The stockadjustmentworkflowstep data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, StockAdjustmentWorkFlowStep updatedEntity)
        {
            UpdateStockAdjustmentWorkFlowStep(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific stockadjustmentworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustmentworkflowstep</param>
        /// <param name="updatedEntity">The stockadjustmentworkflowstep data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<StockAdjustmentWorkFlowStep> updatedEntity)
        {
            PatchStockAdjustmentWorkFlowStep(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific stockadjustmentworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the stockadjustmentworkflowstep</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteStockAdjustmentWorkFlowStep(id);
            return true;
        }
        #region
        private List<StockAdjustmentWorkFlowStep> GetStockAdjustmentWorkFlowStep(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.StockAdjustmentWorkFlowStep.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<StockAdjustmentWorkFlowStep>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(StockAdjustmentWorkFlowStep), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<StockAdjustmentWorkFlowStep, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateStockAdjustmentWorkFlowStep(StockAdjustmentWorkFlowStep model)
        {
            _dbContext.StockAdjustmentWorkFlowStep.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateStockAdjustmentWorkFlowStep(Guid id, StockAdjustmentWorkFlowStep updatedEntity)
        {
            _dbContext.StockAdjustmentWorkFlowStep.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteStockAdjustmentWorkFlowStep(Guid id)
        {
            var entityData = _dbContext.StockAdjustmentWorkFlowStep.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.StockAdjustmentWorkFlowStep.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchStockAdjustmentWorkFlowStep(Guid id, JsonPatchDocument<StockAdjustmentWorkFlowStep> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.StockAdjustmentWorkFlowStep.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.StockAdjustmentWorkFlowStep.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}