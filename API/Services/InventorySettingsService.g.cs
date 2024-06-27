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
    /// The inventorysettingsService responsible for managing inventorysettings related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting inventorysettings information.
    /// </remarks>
    public interface IInventorySettingsService
    {
        /// <summary>Retrieves a specific inventorysettings by its primary key</summary>
        /// <param name="id">The primary key of the inventorysettings</param>
        /// <returns>The inventorysettings data</returns>
        InventorySettings GetById(Guid id);

        /// <summary>Retrieves a list of inventorysettingss based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of inventorysettingss</returns>
        List<InventorySettings> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new inventorysettings</summary>
        /// <param name="model">The inventorysettings data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(InventorySettings model);

        /// <summary>Updates a specific inventorysettings by its primary key</summary>
        /// <param name="id">The primary key of the inventorysettings</param>
        /// <param name="updatedEntity">The inventorysettings data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, InventorySettings updatedEntity);

        /// <summary>Updates a specific inventorysettings by its primary key</summary>
        /// <param name="id">The primary key of the inventorysettings</param>
        /// <param name="updatedEntity">The inventorysettings data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<InventorySettings> updatedEntity);

        /// <summary>Deletes a specific inventorysettings by its primary key</summary>
        /// <param name="id">The primary key of the inventorysettings</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The inventorysettingsService responsible for managing inventorysettings related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting inventorysettings information.
    /// </remarks>
    public class InventorySettingsService : IInventorySettingsService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the InventorySettings class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public InventorySettingsService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific inventorysettings by its primary key</summary>
        /// <param name="id">The primary key of the inventorysettings</param>
        /// <returns>The inventorysettings data</returns>
        public InventorySettings GetById(Guid id)
        {
            var entityData = _dbContext.InventorySettings.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of inventorysettingss based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of inventorysettingss</returns>/// <exception cref="Exception"></exception>
        public List<InventorySettings> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetInventorySettings(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new inventorysettings</summary>
        /// <param name="model">The inventorysettings data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(InventorySettings model)
        {
            model.Id = CreateInventorySettings(model);
            return model.Id;
        }

        /// <summary>Updates a specific inventorysettings by its primary key</summary>
        /// <param name="id">The primary key of the inventorysettings</param>
        /// <param name="updatedEntity">The inventorysettings data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, InventorySettings updatedEntity)
        {
            UpdateInventorySettings(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific inventorysettings by its primary key</summary>
        /// <param name="id">The primary key of the inventorysettings</param>
        /// <param name="updatedEntity">The inventorysettings data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<InventorySettings> updatedEntity)
        {
            PatchInventorySettings(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific inventorysettings by its primary key</summary>
        /// <param name="id">The primary key of the inventorysettings</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteInventorySettings(id);
            return true;
        }
        #region
        private List<InventorySettings> GetInventorySettings(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.InventorySettings.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<InventorySettings>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(InventorySettings), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<InventorySettings, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateInventorySettings(InventorySettings model)
        {
            _dbContext.InventorySettings.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateInventorySettings(Guid id, InventorySettings updatedEntity)
        {
            _dbContext.InventorySettings.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteInventorySettings(Guid id)
        {
            var entityData = _dbContext.InventorySettings.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.InventorySettings.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchInventorySettings(Guid id, JsonPatchDocument<InventorySettings> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.InventorySettings.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.InventorySettings.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}