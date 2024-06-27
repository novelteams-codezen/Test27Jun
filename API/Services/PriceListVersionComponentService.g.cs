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
    /// The pricelistversioncomponentService responsible for managing pricelistversioncomponent related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting pricelistversioncomponent information.
    /// </remarks>
    public interface IPriceListVersionComponentService
    {
        /// <summary>Retrieves a specific pricelistversioncomponent by its primary key</summary>
        /// <param name="id">The primary key of the pricelistversioncomponent</param>
        /// <returns>The pricelistversioncomponent data</returns>
        PriceListVersionComponent GetById(Guid id);

        /// <summary>Retrieves a list of pricelistversioncomponents based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of pricelistversioncomponents</returns>
        List<PriceListVersionComponent> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new pricelistversioncomponent</summary>
        /// <param name="model">The pricelistversioncomponent data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(PriceListVersionComponent model);

        /// <summary>Updates a specific pricelistversioncomponent by its primary key</summary>
        /// <param name="id">The primary key of the pricelistversioncomponent</param>
        /// <param name="updatedEntity">The pricelistversioncomponent data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, PriceListVersionComponent updatedEntity);

        /// <summary>Updates a specific pricelistversioncomponent by its primary key</summary>
        /// <param name="id">The primary key of the pricelistversioncomponent</param>
        /// <param name="updatedEntity">The pricelistversioncomponent data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<PriceListVersionComponent> updatedEntity);

        /// <summary>Deletes a specific pricelistversioncomponent by its primary key</summary>
        /// <param name="id">The primary key of the pricelistversioncomponent</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The pricelistversioncomponentService responsible for managing pricelistversioncomponent related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting pricelistversioncomponent information.
    /// </remarks>
    public class PriceListVersionComponentService : IPriceListVersionComponentService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the PriceListVersionComponent class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public PriceListVersionComponentService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific pricelistversioncomponent by its primary key</summary>
        /// <param name="id">The primary key of the pricelistversioncomponent</param>
        /// <returns>The pricelistversioncomponent data</returns>
        public PriceListVersionComponent GetById(Guid id)
        {
            var entityData = _dbContext.PriceListVersionComponent.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of pricelistversioncomponents based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of pricelistversioncomponents</returns>/// <exception cref="Exception"></exception>
        public List<PriceListVersionComponent> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetPriceListVersionComponent(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new pricelistversioncomponent</summary>
        /// <param name="model">The pricelistversioncomponent data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(PriceListVersionComponent model)
        {
            model.Id = CreatePriceListVersionComponent(model);
            return model.Id;
        }

        /// <summary>Updates a specific pricelistversioncomponent by its primary key</summary>
        /// <param name="id">The primary key of the pricelistversioncomponent</param>
        /// <param name="updatedEntity">The pricelistversioncomponent data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, PriceListVersionComponent updatedEntity)
        {
            UpdatePriceListVersionComponent(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific pricelistversioncomponent by its primary key</summary>
        /// <param name="id">The primary key of the pricelistversioncomponent</param>
        /// <param name="updatedEntity">The pricelistversioncomponent data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<PriceListVersionComponent> updatedEntity)
        {
            PatchPriceListVersionComponent(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific pricelistversioncomponent by its primary key</summary>
        /// <param name="id">The primary key of the pricelistversioncomponent</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeletePriceListVersionComponent(id);
            return true;
        }
        #region
        private List<PriceListVersionComponent> GetPriceListVersionComponent(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.PriceListVersionComponent.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<PriceListVersionComponent>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(PriceListVersionComponent), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<PriceListVersionComponent, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreatePriceListVersionComponent(PriceListVersionComponent model)
        {
            _dbContext.PriceListVersionComponent.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdatePriceListVersionComponent(Guid id, PriceListVersionComponent updatedEntity)
        {
            _dbContext.PriceListVersionComponent.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeletePriceListVersionComponent(Guid id)
        {
            var entityData = _dbContext.PriceListVersionComponent.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.PriceListVersionComponent.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchPriceListVersionComponent(Guid id, JsonPatchDocument<PriceListVersionComponent> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.PriceListVersionComponent.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.PriceListVersionComponent.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}