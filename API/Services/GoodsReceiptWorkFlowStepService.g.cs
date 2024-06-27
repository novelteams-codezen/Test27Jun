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
    /// The goodsreceiptworkflowstepService responsible for managing goodsreceiptworkflowstep related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting goodsreceiptworkflowstep information.
    /// </remarks>
    public interface IGoodsReceiptWorkFlowStepService
    {
        /// <summary>Retrieves a specific goodsreceiptworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the goodsreceiptworkflowstep</param>
        /// <returns>The goodsreceiptworkflowstep data</returns>
        GoodsReceiptWorkFlowStep GetById(Guid id);

        /// <summary>Retrieves a list of goodsreceiptworkflowsteps based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of goodsreceiptworkflowsteps</returns>
        List<GoodsReceiptWorkFlowStep> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new goodsreceiptworkflowstep</summary>
        /// <param name="model">The goodsreceiptworkflowstep data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(GoodsReceiptWorkFlowStep model);

        /// <summary>Updates a specific goodsreceiptworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the goodsreceiptworkflowstep</param>
        /// <param name="updatedEntity">The goodsreceiptworkflowstep data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, GoodsReceiptWorkFlowStep updatedEntity);

        /// <summary>Updates a specific goodsreceiptworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the goodsreceiptworkflowstep</param>
        /// <param name="updatedEntity">The goodsreceiptworkflowstep data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<GoodsReceiptWorkFlowStep> updatedEntity);

        /// <summary>Deletes a specific goodsreceiptworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the goodsreceiptworkflowstep</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The goodsreceiptworkflowstepService responsible for managing goodsreceiptworkflowstep related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting goodsreceiptworkflowstep information.
    /// </remarks>
    public class GoodsReceiptWorkFlowStepService : IGoodsReceiptWorkFlowStepService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the GoodsReceiptWorkFlowStep class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public GoodsReceiptWorkFlowStepService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific goodsreceiptworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the goodsreceiptworkflowstep</param>
        /// <returns>The goodsreceiptworkflowstep data</returns>
        public GoodsReceiptWorkFlowStep GetById(Guid id)
        {
            var entityData = _dbContext.GoodsReceiptWorkFlowStep.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of goodsreceiptworkflowsteps based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of goodsreceiptworkflowsteps</returns>/// <exception cref="Exception"></exception>
        public List<GoodsReceiptWorkFlowStep> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetGoodsReceiptWorkFlowStep(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new goodsreceiptworkflowstep</summary>
        /// <param name="model">The goodsreceiptworkflowstep data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(GoodsReceiptWorkFlowStep model)
        {
            model.Id = CreateGoodsReceiptWorkFlowStep(model);
            return model.Id;
        }

        /// <summary>Updates a specific goodsreceiptworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the goodsreceiptworkflowstep</param>
        /// <param name="updatedEntity">The goodsreceiptworkflowstep data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, GoodsReceiptWorkFlowStep updatedEntity)
        {
            UpdateGoodsReceiptWorkFlowStep(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific goodsreceiptworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the goodsreceiptworkflowstep</param>
        /// <param name="updatedEntity">The goodsreceiptworkflowstep data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<GoodsReceiptWorkFlowStep> updatedEntity)
        {
            PatchGoodsReceiptWorkFlowStep(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific goodsreceiptworkflowstep by its primary key</summary>
        /// <param name="id">The primary key of the goodsreceiptworkflowstep</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteGoodsReceiptWorkFlowStep(id);
            return true;
        }
        #region
        private List<GoodsReceiptWorkFlowStep> GetGoodsReceiptWorkFlowStep(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.GoodsReceiptWorkFlowStep.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<GoodsReceiptWorkFlowStep>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(GoodsReceiptWorkFlowStep), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<GoodsReceiptWorkFlowStep, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateGoodsReceiptWorkFlowStep(GoodsReceiptWorkFlowStep model)
        {
            _dbContext.GoodsReceiptWorkFlowStep.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateGoodsReceiptWorkFlowStep(Guid id, GoodsReceiptWorkFlowStep updatedEntity)
        {
            _dbContext.GoodsReceiptWorkFlowStep.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteGoodsReceiptWorkFlowStep(Guid id)
        {
            var entityData = _dbContext.GoodsReceiptWorkFlowStep.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.GoodsReceiptWorkFlowStep.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchGoodsReceiptWorkFlowStep(Guid id, JsonPatchDocument<GoodsReceiptWorkFlowStep> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.GoodsReceiptWorkFlowStep.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.GoodsReceiptWorkFlowStep.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}