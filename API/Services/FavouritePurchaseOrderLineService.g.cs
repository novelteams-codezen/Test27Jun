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
    /// The favouritepurchaseorderlineService responsible for managing favouritepurchaseorderline related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting favouritepurchaseorderline information.
    /// </remarks>
    public interface IFavouritePurchaseOrderLineService
    {
        /// <summary>Retrieves a specific favouritepurchaseorderline by its primary key</summary>
        /// <param name="id">The primary key of the favouritepurchaseorderline</param>
        /// <returns>The favouritepurchaseorderline data</returns>
        FavouritePurchaseOrderLine GetById(Guid id);

        /// <summary>Retrieves a list of favouritepurchaseorderlines based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of favouritepurchaseorderlines</returns>
        List<FavouritePurchaseOrderLine> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new favouritepurchaseorderline</summary>
        /// <param name="model">The favouritepurchaseorderline data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(FavouritePurchaseOrderLine model);

        /// <summary>Updates a specific favouritepurchaseorderline by its primary key</summary>
        /// <param name="id">The primary key of the favouritepurchaseorderline</param>
        /// <param name="updatedEntity">The favouritepurchaseorderline data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, FavouritePurchaseOrderLine updatedEntity);

        /// <summary>Updates a specific favouritepurchaseorderline by its primary key</summary>
        /// <param name="id">The primary key of the favouritepurchaseorderline</param>
        /// <param name="updatedEntity">The favouritepurchaseorderline data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<FavouritePurchaseOrderLine> updatedEntity);

        /// <summary>Deletes a specific favouritepurchaseorderline by its primary key</summary>
        /// <param name="id">The primary key of the favouritepurchaseorderline</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The favouritepurchaseorderlineService responsible for managing favouritepurchaseorderline related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting favouritepurchaseorderline information.
    /// </remarks>
    public class FavouritePurchaseOrderLineService : IFavouritePurchaseOrderLineService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the FavouritePurchaseOrderLine class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public FavouritePurchaseOrderLineService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific favouritepurchaseorderline by its primary key</summary>
        /// <param name="id">The primary key of the favouritepurchaseorderline</param>
        /// <returns>The favouritepurchaseorderline data</returns>
        public FavouritePurchaseOrderLine GetById(Guid id)
        {
            var entityData = _dbContext.FavouritePurchaseOrderLine.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of favouritepurchaseorderlines based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of favouritepurchaseorderlines</returns>/// <exception cref="Exception"></exception>
        public List<FavouritePurchaseOrderLine> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetFavouritePurchaseOrderLine(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new favouritepurchaseorderline</summary>
        /// <param name="model">The favouritepurchaseorderline data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(FavouritePurchaseOrderLine model)
        {
            model.Id = CreateFavouritePurchaseOrderLine(model);
            return model.Id;
        }

        /// <summary>Updates a specific favouritepurchaseorderline by its primary key</summary>
        /// <param name="id">The primary key of the favouritepurchaseorderline</param>
        /// <param name="updatedEntity">The favouritepurchaseorderline data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, FavouritePurchaseOrderLine updatedEntity)
        {
            UpdateFavouritePurchaseOrderLine(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific favouritepurchaseorderline by its primary key</summary>
        /// <param name="id">The primary key of the favouritepurchaseorderline</param>
        /// <param name="updatedEntity">The favouritepurchaseorderline data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<FavouritePurchaseOrderLine> updatedEntity)
        {
            PatchFavouritePurchaseOrderLine(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific favouritepurchaseorderline by its primary key</summary>
        /// <param name="id">The primary key of the favouritepurchaseorderline</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteFavouritePurchaseOrderLine(id);
            return true;
        }
        #region
        private List<FavouritePurchaseOrderLine> GetFavouritePurchaseOrderLine(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.FavouritePurchaseOrderLine.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<FavouritePurchaseOrderLine>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(FavouritePurchaseOrderLine), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<FavouritePurchaseOrderLine, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateFavouritePurchaseOrderLine(FavouritePurchaseOrderLine model)
        {
            _dbContext.FavouritePurchaseOrderLine.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateFavouritePurchaseOrderLine(Guid id, FavouritePurchaseOrderLine updatedEntity)
        {
            _dbContext.FavouritePurchaseOrderLine.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteFavouritePurchaseOrderLine(Guid id)
        {
            var entityData = _dbContext.FavouritePurchaseOrderLine.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.FavouritePurchaseOrderLine.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchFavouritePurchaseOrderLine(Guid id, JsonPatchDocument<FavouritePurchaseOrderLine> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.FavouritePurchaseOrderLine.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.FavouritePurchaseOrderLine.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}