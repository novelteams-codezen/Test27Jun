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
    /// The uomService responsible for managing uom related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting uom information.
    /// </remarks>
    public interface IUOMService
    {
        /// <summary>Retrieves a specific uom by its primary key</summary>
        /// <param name="id">The primary key of the uom</param>
        /// <returns>The uom data</returns>
        UOM GetById(Guid id);

        /// <summary>Retrieves a list of uoms based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of uoms</returns>
        List<UOM> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new uom</summary>
        /// <param name="model">The uom data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(UOM model);

        /// <summary>Updates a specific uom by its primary key</summary>
        /// <param name="id">The primary key of the uom</param>
        /// <param name="updatedEntity">The uom data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, UOM updatedEntity);

        /// <summary>Updates a specific uom by its primary key</summary>
        /// <param name="id">The primary key of the uom</param>
        /// <param name="updatedEntity">The uom data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<UOM> updatedEntity);

        /// <summary>Deletes a specific uom by its primary key</summary>
        /// <param name="id">The primary key of the uom</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The uomService responsible for managing uom related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting uom information.
    /// </remarks>
    public class UOMService : IUOMService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the UOM class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public UOMService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific uom by its primary key</summary>
        /// <param name="id">The primary key of the uom</param>
        /// <returns>The uom data</returns>
        public UOM GetById(Guid id)
        {
            var entityData = _dbContext.UOM.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of uoms based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of uoms</returns>/// <exception cref="Exception"></exception>
        public List<UOM> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetUOM(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new uom</summary>
        /// <param name="model">The uom data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(UOM model)
        {
            model.Id = CreateUOM(model);
            return model.Id;
        }

        /// <summary>Updates a specific uom by its primary key</summary>
        /// <param name="id">The primary key of the uom</param>
        /// <param name="updatedEntity">The uom data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, UOM updatedEntity)
        {
            UpdateUOM(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific uom by its primary key</summary>
        /// <param name="id">The primary key of the uom</param>
        /// <param name="updatedEntity">The uom data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<UOM> updatedEntity)
        {
            PatchUOM(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific uom by its primary key</summary>
        /// <param name="id">The primary key of the uom</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteUOM(id);
            return true;
        }
        #region
        private List<UOM> GetUOM(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.UOM.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<UOM>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(UOM), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<UOM, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateUOM(UOM model)
        {
            _dbContext.UOM.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateUOM(Guid id, UOM updatedEntity)
        {
            _dbContext.UOM.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteUOM(Guid id)
        {
            var entityData = _dbContext.UOM.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.UOM.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchUOM(Guid id, JsonPatchDocument<UOM> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.UOM.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.UOM.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}