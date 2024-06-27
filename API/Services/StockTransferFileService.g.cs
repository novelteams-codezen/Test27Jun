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
    /// The stocktransferfileService responsible for managing stocktransferfile related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktransferfile information.
    /// </remarks>
    public interface IStockTransferFileService
    {
        /// <summary>Retrieves a specific stocktransferfile by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferfile</param>
        /// <returns>The stocktransferfile data</returns>
        StockTransferFile GetById(Guid id);

        /// <summary>Retrieves a list of stocktransferfiles based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktransferfiles</returns>
        List<StockTransferFile> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new stocktransferfile</summary>
        /// <param name="model">The stocktransferfile data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(StockTransferFile model);

        /// <summary>Updates a specific stocktransferfile by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferfile</param>
        /// <param name="updatedEntity">The stocktransferfile data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, StockTransferFile updatedEntity);

        /// <summary>Updates a specific stocktransferfile by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferfile</param>
        /// <param name="updatedEntity">The stocktransferfile data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<StockTransferFile> updatedEntity);

        /// <summary>Deletes a specific stocktransferfile by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferfile</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The stocktransferfileService responsible for managing stocktransferfile related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktransferfile information.
    /// </remarks>
    public class StockTransferFileService : IStockTransferFileService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the StockTransferFile class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public StockTransferFileService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific stocktransferfile by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferfile</param>
        /// <returns>The stocktransferfile data</returns>
        public StockTransferFile GetById(Guid id)
        {
            var entityData = _dbContext.StockTransferFile.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of stocktransferfiles based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktransferfiles</returns>/// <exception cref="Exception"></exception>
        public List<StockTransferFile> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetStockTransferFile(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new stocktransferfile</summary>
        /// <param name="model">The stocktransferfile data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(StockTransferFile model)
        {
            model.Id = CreateStockTransferFile(model);
            return model.Id;
        }

        /// <summary>Updates a specific stocktransferfile by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferfile</param>
        /// <param name="updatedEntity">The stocktransferfile data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, StockTransferFile updatedEntity)
        {
            UpdateStockTransferFile(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific stocktransferfile by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferfile</param>
        /// <param name="updatedEntity">The stocktransferfile data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<StockTransferFile> updatedEntity)
        {
            PatchStockTransferFile(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific stocktransferfile by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferfile</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteStockTransferFile(id);
            return true;
        }
        #region
        private List<StockTransferFile> GetStockTransferFile(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.StockTransferFile.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<StockTransferFile>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(StockTransferFile), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<StockTransferFile, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateStockTransferFile(StockTransferFile model)
        {
            _dbContext.StockTransferFile.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateStockTransferFile(Guid id, StockTransferFile updatedEntity)
        {
            _dbContext.StockTransferFile.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteStockTransferFile(Guid id)
        {
            var entityData = _dbContext.StockTransferFile.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.StockTransferFile.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchStockTransferFile(Guid id, JsonPatchDocument<StockTransferFile> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.StockTransferFile.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.StockTransferFile.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}