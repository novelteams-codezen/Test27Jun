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
    /// The stocktransferService responsible for managing stocktransfer related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktransfer information.
    /// </remarks>
    public interface IStockTransferService
    {
        /// <summary>Retrieves a specific stocktransfer by its primary key</summary>
        /// <param name="id">The primary key of the stocktransfer</param>
        /// <returns>The stocktransfer data</returns>
        StockTransfer GetById(Guid id);

        /// <summary>Retrieves a list of stocktransfers based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktransfers</returns>
        List<StockTransfer> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new stocktransfer</summary>
        /// <param name="model">The stocktransfer data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(StockTransfer model);

        /// <summary>Updates a specific stocktransfer by its primary key</summary>
        /// <param name="id">The primary key of the stocktransfer</param>
        /// <param name="updatedEntity">The stocktransfer data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, StockTransfer updatedEntity);

        /// <summary>Updates a specific stocktransfer by its primary key</summary>
        /// <param name="id">The primary key of the stocktransfer</param>
        /// <param name="updatedEntity">The stocktransfer data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<StockTransfer> updatedEntity);

        /// <summary>Deletes a specific stocktransfer by its primary key</summary>
        /// <param name="id">The primary key of the stocktransfer</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The stocktransferService responsible for managing stocktransfer related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktransfer information.
    /// </remarks>
    public class StockTransferService : IStockTransferService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the StockTransfer class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public StockTransferService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific stocktransfer by its primary key</summary>
        /// <param name="id">The primary key of the stocktransfer</param>
        /// <returns>The stocktransfer data</returns>
        public StockTransfer GetById(Guid id)
        {
            var entityData = _dbContext.StockTransfer.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of stocktransfers based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktransfers</returns>/// <exception cref="Exception"></exception>
        public List<StockTransfer> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetStockTransfer(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new stocktransfer</summary>
        /// <param name="model">The stocktransfer data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(StockTransfer model)
        {
            model.Id = CreateStockTransfer(model);
            return model.Id;
        }

        /// <summary>Updates a specific stocktransfer by its primary key</summary>
        /// <param name="id">The primary key of the stocktransfer</param>
        /// <param name="updatedEntity">The stocktransfer data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, StockTransfer updatedEntity)
        {
            UpdateStockTransfer(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific stocktransfer by its primary key</summary>
        /// <param name="id">The primary key of the stocktransfer</param>
        /// <param name="updatedEntity">The stocktransfer data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<StockTransfer> updatedEntity)
        {
            PatchStockTransfer(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific stocktransfer by its primary key</summary>
        /// <param name="id">The primary key of the stocktransfer</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteStockTransfer(id);
            return true;
        }
        #region
        private List<StockTransfer> GetStockTransfer(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.StockTransfer.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<StockTransfer>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(StockTransfer), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<StockTransfer, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateStockTransfer(StockTransfer model)
        {
            _dbContext.StockTransfer.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateStockTransfer(Guid id, StockTransfer updatedEntity)
        {
            _dbContext.StockTransfer.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteStockTransfer(Guid id)
        {
            var entityData = _dbContext.StockTransfer.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.StockTransfer.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchStockTransfer(Guid id, JsonPatchDocument<StockTransfer> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.StockTransfer.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.StockTransfer.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}