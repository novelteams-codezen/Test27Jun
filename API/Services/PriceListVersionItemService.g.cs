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
    /// The pricelistversionitemService responsible for managing pricelistversionitem related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting pricelistversionitem information.
    /// </remarks>
    public interface IPriceListVersionItemService
    {
        /// <summary>Retrieves a specific pricelistversionitem by its primary key</summary>
        /// <param name="id">The primary key of the pricelistversionitem</param>
        /// <returns>The pricelistversionitem data</returns>
        PriceListVersionItem GetById(Guid id);

        /// <summary>Retrieves a list of pricelistversionitems based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of pricelistversionitems</returns>
        List<PriceListVersionItem> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new pricelistversionitem</summary>
        /// <param name="model">The pricelistversionitem data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(PriceListVersionItem model);

        /// <summary>Updates a specific pricelistversionitem by its primary key</summary>
        /// <param name="id">The primary key of the pricelistversionitem</param>
        /// <param name="updatedEntity">The pricelistversionitem data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, PriceListVersionItem updatedEntity);

        /// <summary>Updates a specific pricelistversionitem by its primary key</summary>
        /// <param name="id">The primary key of the pricelistversionitem</param>
        /// <param name="updatedEntity">The pricelistversionitem data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<PriceListVersionItem> updatedEntity);

        /// <summary>Deletes a specific pricelistversionitem by its primary key</summary>
        /// <param name="id">The primary key of the pricelistversionitem</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The pricelistversionitemService responsible for managing pricelistversionitem related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting pricelistversionitem information.
    /// </remarks>
    public class PriceListVersionItemService : IPriceListVersionItemService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the PriceListVersionItem class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public PriceListVersionItemService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific pricelistversionitem by its primary key</summary>
        /// <param name="id">The primary key of the pricelistversionitem</param>
        /// <returns>The pricelistversionitem data</returns>
        public PriceListVersionItem GetById(Guid id)
        {
            var entityData = _dbContext.PriceListVersionItem.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of pricelistversionitems based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of pricelistversionitems</returns>/// <exception cref="Exception"></exception>
        public List<PriceListVersionItem> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetPriceListVersionItem(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new pricelistversionitem</summary>
        /// <param name="model">The pricelistversionitem data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(PriceListVersionItem model)
        {
            model.Id = CreatePriceListVersionItem(model);
            return model.Id;
        }

        /// <summary>Updates a specific pricelistversionitem by its primary key</summary>
        /// <param name="id">The primary key of the pricelistversionitem</param>
        /// <param name="updatedEntity">The pricelistversionitem data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, PriceListVersionItem updatedEntity)
        {
            UpdatePriceListVersionItem(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific pricelistversionitem by its primary key</summary>
        /// <param name="id">The primary key of the pricelistversionitem</param>
        /// <param name="updatedEntity">The pricelistversionitem data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<PriceListVersionItem> updatedEntity)
        {
            PatchPriceListVersionItem(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific pricelistversionitem by its primary key</summary>
        /// <param name="id">The primary key of the pricelistversionitem</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeletePriceListVersionItem(id);
            return true;
        }
        #region
        private List<PriceListVersionItem> GetPriceListVersionItem(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.PriceListVersionItem.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<PriceListVersionItem>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(PriceListVersionItem), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<PriceListVersionItem, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreatePriceListVersionItem(PriceListVersionItem model)
        {
            _dbContext.PriceListVersionItem.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdatePriceListVersionItem(Guid id, PriceListVersionItem updatedEntity)
        {
            _dbContext.PriceListVersionItem.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeletePriceListVersionItem(Guid id)
        {
            var entityData = _dbContext.PriceListVersionItem.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.PriceListVersionItem.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchPriceListVersionItem(Guid id, JsonPatchDocument<PriceListVersionItem> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.PriceListVersionItem.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.PriceListVersionItem.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}