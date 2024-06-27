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
    /// The requisitionactivityhistoryService responsible for managing requisitionactivityhistory related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting requisitionactivityhistory information.
    /// </remarks>
    public interface IRequisitionActivityHistoryService
    {
        /// <summary>Retrieves a specific requisitionactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the requisitionactivityhistory</param>
        /// <returns>The requisitionactivityhistory data</returns>
        RequisitionActivityHistory GetById(Guid id);

        /// <summary>Retrieves a list of requisitionactivityhistorys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of requisitionactivityhistorys</returns>
        List<RequisitionActivityHistory> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new requisitionactivityhistory</summary>
        /// <param name="model">The requisitionactivityhistory data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(RequisitionActivityHistory model);

        /// <summary>Updates a specific requisitionactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the requisitionactivityhistory</param>
        /// <param name="updatedEntity">The requisitionactivityhistory data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, RequisitionActivityHistory updatedEntity);

        /// <summary>Updates a specific requisitionactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the requisitionactivityhistory</param>
        /// <param name="updatedEntity">The requisitionactivityhistory data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<RequisitionActivityHistory> updatedEntity);

        /// <summary>Deletes a specific requisitionactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the requisitionactivityhistory</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The requisitionactivityhistoryService responsible for managing requisitionactivityhistory related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting requisitionactivityhistory information.
    /// </remarks>
    public class RequisitionActivityHistoryService : IRequisitionActivityHistoryService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the RequisitionActivityHistory class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public RequisitionActivityHistoryService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific requisitionactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the requisitionactivityhistory</param>
        /// <returns>The requisitionactivityhistory data</returns>
        public RequisitionActivityHistory GetById(Guid id)
        {
            var entityData = _dbContext.RequisitionActivityHistory.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of requisitionactivityhistorys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of requisitionactivityhistorys</returns>/// <exception cref="Exception"></exception>
        public List<RequisitionActivityHistory> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetRequisitionActivityHistory(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new requisitionactivityhistory</summary>
        /// <param name="model">The requisitionactivityhistory data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(RequisitionActivityHistory model)
        {
            model.Id = CreateRequisitionActivityHistory(model);
            return model.Id;
        }

        /// <summary>Updates a specific requisitionactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the requisitionactivityhistory</param>
        /// <param name="updatedEntity">The requisitionactivityhistory data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, RequisitionActivityHistory updatedEntity)
        {
            UpdateRequisitionActivityHistory(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific requisitionactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the requisitionactivityhistory</param>
        /// <param name="updatedEntity">The requisitionactivityhistory data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<RequisitionActivityHistory> updatedEntity)
        {
            PatchRequisitionActivityHistory(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific requisitionactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the requisitionactivityhistory</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteRequisitionActivityHistory(id);
            return true;
        }
        #region
        private List<RequisitionActivityHistory> GetRequisitionActivityHistory(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.RequisitionActivityHistory.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<RequisitionActivityHistory>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(RequisitionActivityHistory), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<RequisitionActivityHistory, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateRequisitionActivityHistory(RequisitionActivityHistory model)
        {
            _dbContext.RequisitionActivityHistory.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateRequisitionActivityHistory(Guid id, RequisitionActivityHistory updatedEntity)
        {
            _dbContext.RequisitionActivityHistory.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteRequisitionActivityHistory(Guid id)
        {
            var entityData = _dbContext.RequisitionActivityHistory.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.RequisitionActivityHistory.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchRequisitionActivityHistory(Guid id, JsonPatchDocument<RequisitionActivityHistory> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.RequisitionActivityHistory.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.RequisitionActivityHistory.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}