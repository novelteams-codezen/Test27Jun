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
    /// The goodsreturnsummaryService responsible for managing goodsreturnsummary related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting goodsreturnsummary information.
    /// </remarks>
    public interface IGoodsReturnSummaryService
    {
        /// <summary>Retrieves a specific goodsreturnsummary by its primary key</summary>
        /// <param name="id">The primary key of the goodsreturnsummary</param>
        /// <returns>The goodsreturnsummary data</returns>
        GoodsReturnSummary GetById(Guid id);

        /// <summary>Retrieves a list of goodsreturnsummarys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of goodsreturnsummarys</returns>
        List<GoodsReturnSummary> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new goodsreturnsummary</summary>
        /// <param name="model">The goodsreturnsummary data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(GoodsReturnSummary model);

        /// <summary>Updates a specific goodsreturnsummary by its primary key</summary>
        /// <param name="id">The primary key of the goodsreturnsummary</param>
        /// <param name="updatedEntity">The goodsreturnsummary data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, GoodsReturnSummary updatedEntity);

        /// <summary>Updates a specific goodsreturnsummary by its primary key</summary>
        /// <param name="id">The primary key of the goodsreturnsummary</param>
        /// <param name="updatedEntity">The goodsreturnsummary data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<GoodsReturnSummary> updatedEntity);

        /// <summary>Deletes a specific goodsreturnsummary by its primary key</summary>
        /// <param name="id">The primary key of the goodsreturnsummary</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The goodsreturnsummaryService responsible for managing goodsreturnsummary related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting goodsreturnsummary information.
    /// </remarks>
    public class GoodsReturnSummaryService : IGoodsReturnSummaryService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the GoodsReturnSummary class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public GoodsReturnSummaryService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific goodsreturnsummary by its primary key</summary>
        /// <param name="id">The primary key of the goodsreturnsummary</param>
        /// <returns>The goodsreturnsummary data</returns>
        public GoodsReturnSummary GetById(Guid id)
        {
            var entityData = _dbContext.GoodsReturnSummary.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of goodsreturnsummarys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of goodsreturnsummarys</returns>/// <exception cref="Exception"></exception>
        public List<GoodsReturnSummary> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetGoodsReturnSummary(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new goodsreturnsummary</summary>
        /// <param name="model">The goodsreturnsummary data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(GoodsReturnSummary model)
        {
            model.Id = CreateGoodsReturnSummary(model);
            return model.Id;
        }

        /// <summary>Updates a specific goodsreturnsummary by its primary key</summary>
        /// <param name="id">The primary key of the goodsreturnsummary</param>
        /// <param name="updatedEntity">The goodsreturnsummary data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, GoodsReturnSummary updatedEntity)
        {
            UpdateGoodsReturnSummary(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific goodsreturnsummary by its primary key</summary>
        /// <param name="id">The primary key of the goodsreturnsummary</param>
        /// <param name="updatedEntity">The goodsreturnsummary data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<GoodsReturnSummary> updatedEntity)
        {
            PatchGoodsReturnSummary(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific goodsreturnsummary by its primary key</summary>
        /// <param name="id">The primary key of the goodsreturnsummary</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteGoodsReturnSummary(id);
            return true;
        }
        #region
        private List<GoodsReturnSummary> GetGoodsReturnSummary(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.GoodsReturnSummary.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<GoodsReturnSummary>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(GoodsReturnSummary), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<GoodsReturnSummary, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateGoodsReturnSummary(GoodsReturnSummary model)
        {
            _dbContext.GoodsReturnSummary.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateGoodsReturnSummary(Guid id, GoodsReturnSummary updatedEntity)
        {
            _dbContext.GoodsReturnSummary.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteGoodsReturnSummary(Guid id)
        {
            var entityData = _dbContext.GoodsReturnSummary.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.GoodsReturnSummary.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchGoodsReturnSummary(Guid id, JsonPatchDocument<GoodsReturnSummary> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.GoodsReturnSummary.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.GoodsReturnSummary.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}