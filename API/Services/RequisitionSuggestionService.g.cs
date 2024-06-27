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
    /// The requisitionsuggestionService responsible for managing requisitionsuggestion related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting requisitionsuggestion information.
    /// </remarks>
    public interface IRequisitionSuggestionService
    {
        /// <summary>Retrieves a specific requisitionsuggestion by its primary key</summary>
        /// <param name="id">The primary key of the requisitionsuggestion</param>
        /// <returns>The requisitionsuggestion data</returns>
        RequisitionSuggestion GetById(Guid id);

        /// <summary>Retrieves a list of requisitionsuggestions based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of requisitionsuggestions</returns>
        List<RequisitionSuggestion> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new requisitionsuggestion</summary>
        /// <param name="model">The requisitionsuggestion data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(RequisitionSuggestion model);

        /// <summary>Updates a specific requisitionsuggestion by its primary key</summary>
        /// <param name="id">The primary key of the requisitionsuggestion</param>
        /// <param name="updatedEntity">The requisitionsuggestion data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, RequisitionSuggestion updatedEntity);

        /// <summary>Updates a specific requisitionsuggestion by its primary key</summary>
        /// <param name="id">The primary key of the requisitionsuggestion</param>
        /// <param name="updatedEntity">The requisitionsuggestion data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<RequisitionSuggestion> updatedEntity);

        /// <summary>Deletes a specific requisitionsuggestion by its primary key</summary>
        /// <param name="id">The primary key of the requisitionsuggestion</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The requisitionsuggestionService responsible for managing requisitionsuggestion related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting requisitionsuggestion information.
    /// </remarks>
    public class RequisitionSuggestionService : IRequisitionSuggestionService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the RequisitionSuggestion class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public RequisitionSuggestionService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific requisitionsuggestion by its primary key</summary>
        /// <param name="id">The primary key of the requisitionsuggestion</param>
        /// <returns>The requisitionsuggestion data</returns>
        public RequisitionSuggestion GetById(Guid id)
        {
            var entityData = _dbContext.RequisitionSuggestion.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of requisitionsuggestions based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of requisitionsuggestions</returns>/// <exception cref="Exception"></exception>
        public List<RequisitionSuggestion> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetRequisitionSuggestion(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new requisitionsuggestion</summary>
        /// <param name="model">The requisitionsuggestion data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(RequisitionSuggestion model)
        {
            model.Id = CreateRequisitionSuggestion(model);
            return model.Id;
        }

        /// <summary>Updates a specific requisitionsuggestion by its primary key</summary>
        /// <param name="id">The primary key of the requisitionsuggestion</param>
        /// <param name="updatedEntity">The requisitionsuggestion data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, RequisitionSuggestion updatedEntity)
        {
            UpdateRequisitionSuggestion(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific requisitionsuggestion by its primary key</summary>
        /// <param name="id">The primary key of the requisitionsuggestion</param>
        /// <param name="updatedEntity">The requisitionsuggestion data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<RequisitionSuggestion> updatedEntity)
        {
            PatchRequisitionSuggestion(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific requisitionsuggestion by its primary key</summary>
        /// <param name="id">The primary key of the requisitionsuggestion</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteRequisitionSuggestion(id);
            return true;
        }
        #region
        private List<RequisitionSuggestion> GetRequisitionSuggestion(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.RequisitionSuggestion.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<RequisitionSuggestion>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(RequisitionSuggestion), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<RequisitionSuggestion, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateRequisitionSuggestion(RequisitionSuggestion model)
        {
            _dbContext.RequisitionSuggestion.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateRequisitionSuggestion(Guid id, RequisitionSuggestion updatedEntity)
        {
            _dbContext.RequisitionSuggestion.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteRequisitionSuggestion(Guid id)
        {
            var entityData = _dbContext.RequisitionSuggestion.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.RequisitionSuggestion.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchRequisitionSuggestion(Guid id, JsonPatchDocument<RequisitionSuggestion> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.RequisitionSuggestion.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.RequisitionSuggestion.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}