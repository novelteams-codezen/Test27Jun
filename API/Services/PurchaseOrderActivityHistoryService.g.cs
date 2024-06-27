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
    /// The purchaseorderactivityhistoryService responsible for managing purchaseorderactivityhistory related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting purchaseorderactivityhistory information.
    /// </remarks>
    public interface IPurchaseOrderActivityHistoryService
    {
        /// <summary>Retrieves a specific purchaseorderactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the purchaseorderactivityhistory</param>
        /// <returns>The purchaseorderactivityhistory data</returns>
        PurchaseOrderActivityHistory GetById(Guid id);

        /// <summary>Retrieves a list of purchaseorderactivityhistorys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of purchaseorderactivityhistorys</returns>
        List<PurchaseOrderActivityHistory> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new purchaseorderactivityhistory</summary>
        /// <param name="model">The purchaseorderactivityhistory data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(PurchaseOrderActivityHistory model);

        /// <summary>Updates a specific purchaseorderactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the purchaseorderactivityhistory</param>
        /// <param name="updatedEntity">The purchaseorderactivityhistory data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, PurchaseOrderActivityHistory updatedEntity);

        /// <summary>Updates a specific purchaseorderactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the purchaseorderactivityhistory</param>
        /// <param name="updatedEntity">The purchaseorderactivityhistory data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<PurchaseOrderActivityHistory> updatedEntity);

        /// <summary>Deletes a specific purchaseorderactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the purchaseorderactivityhistory</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The purchaseorderactivityhistoryService responsible for managing purchaseorderactivityhistory related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting purchaseorderactivityhistory information.
    /// </remarks>
    public class PurchaseOrderActivityHistoryService : IPurchaseOrderActivityHistoryService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the PurchaseOrderActivityHistory class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public PurchaseOrderActivityHistoryService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific purchaseorderactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the purchaseorderactivityhistory</param>
        /// <returns>The purchaseorderactivityhistory data</returns>
        public PurchaseOrderActivityHistory GetById(Guid id)
        {
            var entityData = _dbContext.PurchaseOrderActivityHistory.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of purchaseorderactivityhistorys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of purchaseorderactivityhistorys</returns>/// <exception cref="Exception"></exception>
        public List<PurchaseOrderActivityHistory> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetPurchaseOrderActivityHistory(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new purchaseorderactivityhistory</summary>
        /// <param name="model">The purchaseorderactivityhistory data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(PurchaseOrderActivityHistory model)
        {
            model.Id = CreatePurchaseOrderActivityHistory(model);
            return model.Id;
        }

        /// <summary>Updates a specific purchaseorderactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the purchaseorderactivityhistory</param>
        /// <param name="updatedEntity">The purchaseorderactivityhistory data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, PurchaseOrderActivityHistory updatedEntity)
        {
            UpdatePurchaseOrderActivityHistory(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific purchaseorderactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the purchaseorderactivityhistory</param>
        /// <param name="updatedEntity">The purchaseorderactivityhistory data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<PurchaseOrderActivityHistory> updatedEntity)
        {
            PatchPurchaseOrderActivityHistory(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific purchaseorderactivityhistory by its primary key</summary>
        /// <param name="id">The primary key of the purchaseorderactivityhistory</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeletePurchaseOrderActivityHistory(id);
            return true;
        }
        #region
        private List<PurchaseOrderActivityHistory> GetPurchaseOrderActivityHistory(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.PurchaseOrderActivityHistory.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<PurchaseOrderActivityHistory>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(PurchaseOrderActivityHistory), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<PurchaseOrderActivityHistory, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreatePurchaseOrderActivityHistory(PurchaseOrderActivityHistory model)
        {
            _dbContext.PurchaseOrderActivityHistory.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdatePurchaseOrderActivityHistory(Guid id, PurchaseOrderActivityHistory updatedEntity)
        {
            _dbContext.PurchaseOrderActivityHistory.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeletePurchaseOrderActivityHistory(Guid id)
        {
            var entityData = _dbContext.PurchaseOrderActivityHistory.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.PurchaseOrderActivityHistory.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchPurchaseOrderActivityHistory(Guid id, JsonPatchDocument<PurchaseOrderActivityHistory> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.PurchaseOrderActivityHistory.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.PurchaseOrderActivityHistory.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}