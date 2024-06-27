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
    /// The purchaseordersuggestionService responsible for managing purchaseordersuggestion related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting purchaseordersuggestion information.
    /// </remarks>
    public interface IPurchaseOrderSuggestionService
    {
        /// <summary>Retrieves a specific purchaseordersuggestion by its primary key</summary>
        /// <param name="id">The primary key of the purchaseordersuggestion</param>
        /// <returns>The purchaseordersuggestion data</returns>
        PurchaseOrderSuggestion GetById(Guid id);

        /// <summary>Retrieves a list of purchaseordersuggestions based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of purchaseordersuggestions</returns>
        List<PurchaseOrderSuggestion> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new purchaseordersuggestion</summary>
        /// <param name="model">The purchaseordersuggestion data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(PurchaseOrderSuggestion model);

        /// <summary>Updates a specific purchaseordersuggestion by its primary key</summary>
        /// <param name="id">The primary key of the purchaseordersuggestion</param>
        /// <param name="updatedEntity">The purchaseordersuggestion data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, PurchaseOrderSuggestion updatedEntity);

        /// <summary>Updates a specific purchaseordersuggestion by its primary key</summary>
        /// <param name="id">The primary key of the purchaseordersuggestion</param>
        /// <param name="updatedEntity">The purchaseordersuggestion data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<PurchaseOrderSuggestion> updatedEntity);

        /// <summary>Deletes a specific purchaseordersuggestion by its primary key</summary>
        /// <param name="id">The primary key of the purchaseordersuggestion</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The purchaseordersuggestionService responsible for managing purchaseordersuggestion related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting purchaseordersuggestion information.
    /// </remarks>
    public class PurchaseOrderSuggestionService : IPurchaseOrderSuggestionService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the PurchaseOrderSuggestion class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public PurchaseOrderSuggestionService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific purchaseordersuggestion by its primary key</summary>
        /// <param name="id">The primary key of the purchaseordersuggestion</param>
        /// <returns>The purchaseordersuggestion data</returns>
        public PurchaseOrderSuggestion GetById(Guid id)
        {
            var entityData = _dbContext.PurchaseOrderSuggestion.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of purchaseordersuggestions based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of purchaseordersuggestions</returns>/// <exception cref="Exception"></exception>
        public List<PurchaseOrderSuggestion> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetPurchaseOrderSuggestion(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new purchaseordersuggestion</summary>
        /// <param name="model">The purchaseordersuggestion data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(PurchaseOrderSuggestion model)
        {
            model.Id = CreatePurchaseOrderSuggestion(model);
            return model.Id;
        }

        /// <summary>Updates a specific purchaseordersuggestion by its primary key</summary>
        /// <param name="id">The primary key of the purchaseordersuggestion</param>
        /// <param name="updatedEntity">The purchaseordersuggestion data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, PurchaseOrderSuggestion updatedEntity)
        {
            UpdatePurchaseOrderSuggestion(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific purchaseordersuggestion by its primary key</summary>
        /// <param name="id">The primary key of the purchaseordersuggestion</param>
        /// <param name="updatedEntity">The purchaseordersuggestion data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<PurchaseOrderSuggestion> updatedEntity)
        {
            PatchPurchaseOrderSuggestion(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific purchaseordersuggestion by its primary key</summary>
        /// <param name="id">The primary key of the purchaseordersuggestion</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeletePurchaseOrderSuggestion(id);
            return true;
        }
        #region
        private List<PurchaseOrderSuggestion> GetPurchaseOrderSuggestion(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.PurchaseOrderSuggestion.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<PurchaseOrderSuggestion>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(PurchaseOrderSuggestion), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<PurchaseOrderSuggestion, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreatePurchaseOrderSuggestion(PurchaseOrderSuggestion model)
        {
            _dbContext.PurchaseOrderSuggestion.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdatePurchaseOrderSuggestion(Guid id, PurchaseOrderSuggestion updatedEntity)
        {
            _dbContext.PurchaseOrderSuggestion.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeletePurchaseOrderSuggestion(Guid id)
        {
            var entityData = _dbContext.PurchaseOrderSuggestion.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.PurchaseOrderSuggestion.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchPurchaseOrderSuggestion(Guid id, JsonPatchDocument<PurchaseOrderSuggestion> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.PurchaseOrderSuggestion.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.PurchaseOrderSuggestion.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}