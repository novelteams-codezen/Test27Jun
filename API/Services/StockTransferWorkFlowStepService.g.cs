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
    /// The stocktransferworkflowstepService responsible for managing stocktransferworkflowstep related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktransferworkflowstep information.
    /// </remarks>
    public interface IStockTransferWorkFlowStepService
    {
        /// <summary>Retrieves a specific stocktransferworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferworkflowstep</param>
        /// <returns>The stocktransferworkflowstep data</returns>
        StockTransferWorkFlowStep GetById(Guid id);

        /// <summary>Retrieves a list of stocktransferworkflowsteps based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktransferworkflowsteps</returns>
        List<StockTransferWorkFlowStep> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new stocktransferworkflowstep</summary>
        /// <param name="model">The stocktransferworkflowstep data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(StockTransferWorkFlowStep model);

        /// <summary>Updates a specific stocktransferworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferworkflowstep</param>
        /// <param name="updatedEntity">The stocktransferworkflowstep data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, StockTransferWorkFlowStep updatedEntity);

        /// <summary>Updates a specific stocktransferworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferworkflowstep</param>
        /// <param name="updatedEntity">The stocktransferworkflowstep data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<StockTransferWorkFlowStep> updatedEntity);

        /// <summary>Deletes a specific stocktransferworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferworkflowstep</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The stocktransferworkflowstepService responsible for managing stocktransferworkflowstep related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktransferworkflowstep information.
    /// </remarks>
    public class StockTransferWorkFlowStepService : IStockTransferWorkFlowStepService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the StockTransferWorkFlowStep class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public StockTransferWorkFlowStepService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific stocktransferworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferworkflowstep</param>
        /// <returns>The stocktransferworkflowstep data</returns>
        public StockTransferWorkFlowStep GetById(Guid id)
        {
            var entityData = _dbContext.StockTransferWorkFlowStep.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of stocktransferworkflowsteps based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktransferworkflowsteps</returns>/// <exception cref="Exception"></exception>
        public List<StockTransferWorkFlowStep> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetStockTransferWorkFlowStep(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new stocktransferworkflowstep</summary>
        /// <param name="model">The stocktransferworkflowstep data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(StockTransferWorkFlowStep model)
        {
            model.Id = CreateStockTransferWorkFlowStep(model);
            return model.Id;
        }

        /// <summary>Updates a specific stocktransferworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferworkflowstep</param>
        /// <param name="updatedEntity">The stocktransferworkflowstep data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, StockTransferWorkFlowStep updatedEntity)
        {
            UpdateStockTransferWorkFlowStep(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific stocktransferworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferworkflowstep</param>
        /// <param name="updatedEntity">The stocktransferworkflowstep data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<StockTransferWorkFlowStep> updatedEntity)
        {
            PatchStockTransferWorkFlowStep(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific stocktransferworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferworkflowstep</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteStockTransferWorkFlowStep(id);
            return true;
        }
        #region
        private List<StockTransferWorkFlowStep> GetStockTransferWorkFlowStep(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.StockTransferWorkFlowStep.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<StockTransferWorkFlowStep>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(StockTransferWorkFlowStep), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<StockTransferWorkFlowStep, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateStockTransferWorkFlowStep(StockTransferWorkFlowStep model)
        {
            _dbContext.StockTransferWorkFlowStep.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateStockTransferWorkFlowStep(Guid id, StockTransferWorkFlowStep updatedEntity)
        {
            _dbContext.StockTransferWorkFlowStep.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteStockTransferWorkFlowStep(Guid id)
        {
            var entityData = _dbContext.StockTransferWorkFlowStep.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.StockTransferWorkFlowStep.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchStockTransferWorkFlowStep(Guid id, JsonPatchDocument<StockTransferWorkFlowStep> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.StockTransferWorkFlowStep.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.StockTransferWorkFlowStep.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}