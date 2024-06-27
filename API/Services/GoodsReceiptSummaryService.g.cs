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
    /// The goodsreceiptsummaryService responsible for managing goodsreceiptsummary related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting goodsreceiptsummary information.
    /// </remarks>
    public interface IGoodsReceiptSummaryService
    {
        /// <summary>Retrieves a specific goodsreceiptsummary by its primary key</summary>
        /// <param name="id">The primary key of the goodsreceiptsummary</param>
        /// <returns>The goodsreceiptsummary data</returns>
        GoodsReceiptSummary GetById(Guid id);

        /// <summary>Retrieves a list of goodsreceiptsummarys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of goodsreceiptsummarys</returns>
        List<GoodsReceiptSummary> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new goodsreceiptsummary</summary>
        /// <param name="model">The goodsreceiptsummary data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(GoodsReceiptSummary model);

        /// <summary>Updates a specific goodsreceiptsummary by its primary key</summary>
        /// <param name="id">The primary key of the goodsreceiptsummary</param>
        /// <param name="updatedEntity">The goodsreceiptsummary data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, GoodsReceiptSummary updatedEntity);

        /// <summary>Updates a specific goodsreceiptsummary by its primary key</summary>
        /// <param name="id">The primary key of the goodsreceiptsummary</param>
        /// <param name="updatedEntity">The goodsreceiptsummary data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<GoodsReceiptSummary> updatedEntity);

        /// <summary>Deletes a specific goodsreceiptsummary by its primary key</summary>
        /// <param name="id">The primary key of the goodsreceiptsummary</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The goodsreceiptsummaryService responsible for managing goodsreceiptsummary related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting goodsreceiptsummary information.
    /// </remarks>
    public class GoodsReceiptSummaryService : IGoodsReceiptSummaryService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the GoodsReceiptSummary class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public GoodsReceiptSummaryService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific goodsreceiptsummary by its primary key</summary>
        /// <param name="id">The primary key of the goodsreceiptsummary</param>
        /// <returns>The goodsreceiptsummary data</returns>
        public GoodsReceiptSummary GetById(Guid id)
        {
            var entityData = _dbContext.GoodsReceiptSummary.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of goodsreceiptsummarys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of goodsreceiptsummarys</returns>/// <exception cref="Exception"></exception>
        public List<GoodsReceiptSummary> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetGoodsReceiptSummary(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new goodsreceiptsummary</summary>
        /// <param name="model">The goodsreceiptsummary data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(GoodsReceiptSummary model)
        {
            model.Id = CreateGoodsReceiptSummary(model);
            return model.Id;
        }

        /// <summary>Updates a specific goodsreceiptsummary by its primary key</summary>
        /// <param name="id">The primary key of the goodsreceiptsummary</param>
        /// <param name="updatedEntity">The goodsreceiptsummary data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, GoodsReceiptSummary updatedEntity)
        {
            UpdateGoodsReceiptSummary(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific goodsreceiptsummary by its primary key</summary>
        /// <param name="id">The primary key of the goodsreceiptsummary</param>
        /// <param name="updatedEntity">The goodsreceiptsummary data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<GoodsReceiptSummary> updatedEntity)
        {
            PatchGoodsReceiptSummary(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific goodsreceiptsummary by its primary key</summary>
        /// <param name="id">The primary key of the goodsreceiptsummary</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteGoodsReceiptSummary(id);
            return true;
        }
        #region
        private List<GoodsReceiptSummary> GetGoodsReceiptSummary(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.GoodsReceiptSummary.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<GoodsReceiptSummary>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(GoodsReceiptSummary), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<GoodsReceiptSummary, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateGoodsReceiptSummary(GoodsReceiptSummary model)
        {
            _dbContext.GoodsReceiptSummary.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateGoodsReceiptSummary(Guid id, GoodsReceiptSummary updatedEntity)
        {
            _dbContext.GoodsReceiptSummary.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteGoodsReceiptSummary(Guid id)
        {
            var entityData = _dbContext.GoodsReceiptSummary.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.GoodsReceiptSummary.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchGoodsReceiptSummary(Guid id, JsonPatchDocument<GoodsReceiptSummary> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.GoodsReceiptSummary.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.GoodsReceiptSummary.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}