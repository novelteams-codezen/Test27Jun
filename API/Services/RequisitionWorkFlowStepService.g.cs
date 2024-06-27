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
    /// The requisitionworkflowstepService responsible for managing requisitionworkflowstep related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting requisitionworkflowstep information.
    /// </remarks>
    public interface IRequisitionWorkFlowStepService
    {
        /// <summary>Retrieves a specific requisitionworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the requisitionworkflowstep</param>
        /// <returns>The requisitionworkflowstep data</returns>
        RequisitionWorkFlowStep GetById(Guid id);

        /// <summary>Retrieves a list of requisitionworkflowsteps based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of requisitionworkflowsteps</returns>
        List<RequisitionWorkFlowStep> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new requisitionworkflowstep</summary>
        /// <param name="model">The requisitionworkflowstep data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(RequisitionWorkFlowStep model);

        /// <summary>Updates a specific requisitionworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the requisitionworkflowstep</param>
        /// <param name="updatedEntity">The requisitionworkflowstep data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, RequisitionWorkFlowStep updatedEntity);

        /// <summary>Updates a specific requisitionworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the requisitionworkflowstep</param>
        /// <param name="updatedEntity">The requisitionworkflowstep data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<RequisitionWorkFlowStep> updatedEntity);

        /// <summary>Deletes a specific requisitionworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the requisitionworkflowstep</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The requisitionworkflowstepService responsible for managing requisitionworkflowstep related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting requisitionworkflowstep information.
    /// </remarks>
    public class RequisitionWorkFlowStepService : IRequisitionWorkFlowStepService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the RequisitionWorkFlowStep class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public RequisitionWorkFlowStepService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific requisitionworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the requisitionworkflowstep</param>
        /// <returns>The requisitionworkflowstep data</returns>
        public RequisitionWorkFlowStep GetById(Guid id)
        {
            var entityData = _dbContext.RequisitionWorkFlowStep.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of requisitionworkflowsteps based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of requisitionworkflowsteps</returns>/// <exception cref="Exception"></exception>
        public List<RequisitionWorkFlowStep> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetRequisitionWorkFlowStep(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new requisitionworkflowstep</summary>
        /// <param name="model">The requisitionworkflowstep data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(RequisitionWorkFlowStep model)
        {
            model.Id = CreateRequisitionWorkFlowStep(model);
            return model.Id;
        }

        /// <summary>Updates a specific requisitionworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the requisitionworkflowstep</param>
        /// <param name="updatedEntity">The requisitionworkflowstep data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, RequisitionWorkFlowStep updatedEntity)
        {
            UpdateRequisitionWorkFlowStep(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific requisitionworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the requisitionworkflowstep</param>
        /// <param name="updatedEntity">The requisitionworkflowstep data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<RequisitionWorkFlowStep> updatedEntity)
        {
            PatchRequisitionWorkFlowStep(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific requisitionworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the requisitionworkflowstep</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteRequisitionWorkFlowStep(id);
            return true;
        }
        #region
        private List<RequisitionWorkFlowStep> GetRequisitionWorkFlowStep(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.RequisitionWorkFlowStep.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<RequisitionWorkFlowStep>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(RequisitionWorkFlowStep), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<RequisitionWorkFlowStep, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateRequisitionWorkFlowStep(RequisitionWorkFlowStep model)
        {
            _dbContext.RequisitionWorkFlowStep.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateRequisitionWorkFlowStep(Guid id, RequisitionWorkFlowStep updatedEntity)
        {
            _dbContext.RequisitionWorkFlowStep.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteRequisitionWorkFlowStep(Guid id)
        {
            var entityData = _dbContext.RequisitionWorkFlowStep.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.RequisitionWorkFlowStep.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchRequisitionWorkFlowStep(Guid id, JsonPatchDocument<RequisitionWorkFlowStep> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.RequisitionWorkFlowStep.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.RequisitionWorkFlowStep.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}