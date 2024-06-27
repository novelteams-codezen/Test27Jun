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
    /// The goodsreturnworkflowstepService responsible for managing goodsreturnworkflowstep related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting goodsreturnworkflowstep information.
    /// </remarks>
    public interface IGoodsReturnWorkFlowStepService
    {
        /// <summary>Retrieves a specific goodsreturnworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the goodsreturnworkflowstep</param>
        /// <returns>The goodsreturnworkflowstep data</returns>
        GoodsReturnWorkFlowStep GetById(Guid id);

        /// <summary>Retrieves a list of goodsreturnworkflowsteps based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of goodsreturnworkflowsteps</returns>
        List<GoodsReturnWorkFlowStep> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new goodsreturnworkflowstep</summary>
        /// <param name="model">The goodsreturnworkflowstep data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(GoodsReturnWorkFlowStep model);

        /// <summary>Updates a specific goodsreturnworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the goodsreturnworkflowstep</param>
        /// <param name="updatedEntity">The goodsreturnworkflowstep data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, GoodsReturnWorkFlowStep updatedEntity);

        /// <summary>Updates a specific goodsreturnworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the goodsreturnworkflowstep</param>
        /// <param name="updatedEntity">The goodsreturnworkflowstep data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<GoodsReturnWorkFlowStep> updatedEntity);

        /// <summary>Deletes a specific goodsreturnworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the goodsreturnworkflowstep</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The goodsreturnworkflowstepService responsible for managing goodsreturnworkflowstep related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting goodsreturnworkflowstep information.
    /// </remarks>
    public class GoodsReturnWorkFlowStepService : IGoodsReturnWorkFlowStepService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the GoodsReturnWorkFlowStep class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public GoodsReturnWorkFlowStepService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific goodsreturnworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the goodsreturnworkflowstep</param>
        /// <returns>The goodsreturnworkflowstep data</returns>
        public GoodsReturnWorkFlowStep GetById(Guid id)
        {
            var entityData = _dbContext.GoodsReturnWorkFlowStep.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of goodsreturnworkflowsteps based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of goodsreturnworkflowsteps</returns>/// <exception cref="Exception"></exception>
        public List<GoodsReturnWorkFlowStep> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetGoodsReturnWorkFlowStep(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new goodsreturnworkflowstep</summary>
        /// <param name="model">The goodsreturnworkflowstep data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(GoodsReturnWorkFlowStep model)
        {
            model.Id = CreateGoodsReturnWorkFlowStep(model);
            return model.Id;
        }

        /// <summary>Updates a specific goodsreturnworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the goodsreturnworkflowstep</param>
        /// <param name="updatedEntity">The goodsreturnworkflowstep data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, GoodsReturnWorkFlowStep updatedEntity)
        {
            UpdateGoodsReturnWorkFlowStep(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific goodsreturnworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the goodsreturnworkflowstep</param>
        /// <param name="updatedEntity">The goodsreturnworkflowstep data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<GoodsReturnWorkFlowStep> updatedEntity)
        {
            PatchGoodsReturnWorkFlowStep(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific goodsreturnworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the goodsreturnworkflowstep</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteGoodsReturnWorkFlowStep(id);
            return true;
        }
        #region
        private List<GoodsReturnWorkFlowStep> GetGoodsReturnWorkFlowStep(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.GoodsReturnWorkFlowStep.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<GoodsReturnWorkFlowStep>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(GoodsReturnWorkFlowStep), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<GoodsReturnWorkFlowStep, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateGoodsReturnWorkFlowStep(GoodsReturnWorkFlowStep model)
        {
            _dbContext.GoodsReturnWorkFlowStep.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateGoodsReturnWorkFlowStep(Guid id, GoodsReturnWorkFlowStep updatedEntity)
        {
            _dbContext.GoodsReturnWorkFlowStep.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteGoodsReturnWorkFlowStep(Guid id)
        {
            var entityData = _dbContext.GoodsReturnWorkFlowStep.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.GoodsReturnWorkFlowStep.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchGoodsReturnWorkFlowStep(Guid id, JsonPatchDocument<GoodsReturnWorkFlowStep> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.GoodsReturnWorkFlowStep.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.GoodsReturnWorkFlowStep.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}