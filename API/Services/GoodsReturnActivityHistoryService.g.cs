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
    /// The goodsreturnactivityhistoryService responsible for managing goodsreturnactivityhistory related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting goodsreturnactivityhistory information.
    /// </remarks>
    public interface IGoodsReturnActivityHistoryService
    {
        /// <summary>Retrieves a specific goodsreturnactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the goodsreturnactivityhistory</param>
        /// <returns>The goodsreturnactivityhistory data</returns>
        GoodsReturnActivityHistory GetById(Guid id);

        /// <summary>Retrieves a list of goodsreturnactivityhistorys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of goodsreturnactivityhistorys</returns>
        List<GoodsReturnActivityHistory> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new goodsreturnactivityhistory</summary>
        /// <param name="model">The goodsreturnactivityhistory data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(GoodsReturnActivityHistory model);

        /// <summary>Updates a specific goodsreturnactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the goodsreturnactivityhistory</param>
        /// <param name="updatedEntity">The goodsreturnactivityhistory data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, GoodsReturnActivityHistory updatedEntity);

        /// <summary>Updates a specific goodsreturnactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the goodsreturnactivityhistory</param>
        /// <param name="updatedEntity">The goodsreturnactivityhistory data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<GoodsReturnActivityHistory> updatedEntity);

        /// <summary>Deletes a specific goodsreturnactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the goodsreturnactivityhistory</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The goodsreturnactivityhistoryService responsible for managing goodsreturnactivityhistory related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting goodsreturnactivityhistory information.
    /// </remarks>
    public class GoodsReturnActivityHistoryService : IGoodsReturnActivityHistoryService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the GoodsReturnActivityHistory class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public GoodsReturnActivityHistoryService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific goodsreturnactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the goodsreturnactivityhistory</param>
        /// <returns>The goodsreturnactivityhistory data</returns>
        public GoodsReturnActivityHistory GetById(Guid id)
        {
            var entityData = _dbContext.GoodsReturnActivityHistory.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of goodsreturnactivityhistorys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of goodsreturnactivityhistorys</returns>/// <exception cref="Exception"></exception>
        public List<GoodsReturnActivityHistory> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetGoodsReturnActivityHistory(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new goodsreturnactivityhistory</summary>
        /// <param name="model">The goodsreturnactivityhistory data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(GoodsReturnActivityHistory model)
        {
            model.Id = CreateGoodsReturnActivityHistory(model);
            return model.Id;
        }

        /// <summary>Updates a specific goodsreturnactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the goodsreturnactivityhistory</param>
        /// <param name="updatedEntity">The goodsreturnactivityhistory data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, GoodsReturnActivityHistory updatedEntity)
        {
            UpdateGoodsReturnActivityHistory(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific goodsreturnactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the goodsreturnactivityhistory</param>
        /// <param name="updatedEntity">The goodsreturnactivityhistory data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<GoodsReturnActivityHistory> updatedEntity)
        {
            PatchGoodsReturnActivityHistory(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific goodsreturnactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the goodsreturnactivityhistory</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteGoodsReturnActivityHistory(id);
            return true;
        }
        #region
        private List<GoodsReturnActivityHistory> GetGoodsReturnActivityHistory(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.GoodsReturnActivityHistory.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<GoodsReturnActivityHistory>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(GoodsReturnActivityHistory), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<GoodsReturnActivityHistory, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateGoodsReturnActivityHistory(GoodsReturnActivityHistory model)
        {
            _dbContext.GoodsReturnActivityHistory.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateGoodsReturnActivityHistory(Guid id, GoodsReturnActivityHistory updatedEntity)
        {
            _dbContext.GoodsReturnActivityHistory.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteGoodsReturnActivityHistory(Guid id)
        {
            var entityData = _dbContext.GoodsReturnActivityHistory.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.GoodsReturnActivityHistory.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchGoodsReturnActivityHistory(Guid id, JsonPatchDocument<GoodsReturnActivityHistory> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.GoodsReturnActivityHistory.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.GoodsReturnActivityHistory.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}