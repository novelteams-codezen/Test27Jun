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
    /// The stocktakefileService responsible for managing stocktakefile related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktakefile information.
    /// </remarks>
    public interface IStockTakeFileService
    {
        /// <summary>Retrieves a specific stocktakefile by its primary key</summary>
        /// <param name="id">The primary key of the stocktakefile</param>
        /// <returns>The stocktakefile data</returns>
        StockTakeFile GetById(Guid id);

        /// <summary>Retrieves a list of stocktakefiles based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktakefiles</returns>
        List<StockTakeFile> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new stocktakefile</summary>
        /// <param name="model">The stocktakefile data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(StockTakeFile model);

        /// <summary>Updates a specific stocktakefile by its primary key</summary>
        /// <param name="id">The primary key of the stocktakefile</param>
        /// <param name="updatedEntity">The stocktakefile data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, StockTakeFile updatedEntity);

        /// <summary>Updates a specific stocktakefile by its primary key</summary>
        /// <param name="id">The primary key of the stocktakefile</param>
        /// <param name="updatedEntity">The stocktakefile data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<StockTakeFile> updatedEntity);

        /// <summary>Deletes a specific stocktakefile by its primary key</summary>
        /// <param name="id">The primary key of the stocktakefile</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The stocktakefileService responsible for managing stocktakefile related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting stocktakefile information.
    /// </remarks>
    public class StockTakeFileService : IStockTakeFileService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the StockTakeFile class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public StockTakeFileService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific stocktakefile by its primary key</summary>
        /// <param name="id">The primary key of the stocktakefile</param>
        /// <returns>The stocktakefile data</returns>
        public StockTakeFile GetById(Guid id)
        {
            var entityData = _dbContext.StockTakeFile.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of stocktakefiles based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktakefiles</returns>/// <exception cref="Exception"></exception>
        public List<StockTakeFile> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetStockTakeFile(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new stocktakefile</summary>
        /// <param name="model">The stocktakefile data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(StockTakeFile model)
        {
            model.Id = CreateStockTakeFile(model);
            return model.Id;
        }

        /// <summary>Updates a specific stocktakefile by its primary key</summary>
        /// <param name="id">The primary key of the stocktakefile</param>
        /// <param name="updatedEntity">The stocktakefile data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, StockTakeFile updatedEntity)
        {
            UpdateStockTakeFile(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific stocktakefile by its primary key</summary>
        /// <param name="id">The primary key of the stocktakefile</param>
        /// <param name="updatedEntity">The stocktakefile data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<StockTakeFile> updatedEntity)
        {
            PatchStockTakeFile(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific stocktakefile by its primary key</summary>
        /// <param name="id">The primary key of the stocktakefile</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteStockTakeFile(id);
            return true;
        }
        #region
        private List<StockTakeFile> GetStockTakeFile(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.StockTakeFile.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<StockTakeFile>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(StockTakeFile), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<StockTakeFile, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateStockTakeFile(StockTakeFile model)
        {
            _dbContext.StockTakeFile.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateStockTakeFile(Guid id, StockTakeFile updatedEntity)
        {
            _dbContext.StockTakeFile.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteStockTakeFile(Guid id)
        {
            var entityData = _dbContext.StockTakeFile.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.StockTakeFile.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchStockTakeFile(Guid id, JsonPatchDocument<StockTakeFile> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.StockTakeFile.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.StockTakeFile.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}