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
    /// The supplierService responsible for managing supplier related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting supplier information.
    /// </remarks>
    public interface ISupplierService
    {
        /// <summary>Retrieves a specific supplier by its primary key</summary>
        /// <param name="id">The primary key of the supplier</param>
        /// <returns>The supplier data</returns>
        Supplier GetById(Guid id);

        /// <summary>Retrieves a list of suppliers based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of suppliers</returns>
        List<Supplier> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new supplier</summary>
        /// <param name="model">The supplier data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(Supplier model);

        /// <summary>Updates a specific supplier by its primary key</summary>
        /// <param name="id">The primary key of the supplier</param>
        /// <param name="updatedEntity">The supplier data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, Supplier updatedEntity);

        /// <summary>Updates a specific supplier by its primary key</summary>
        /// <param name="id">The primary key of the supplier</param>
        /// <param name="updatedEntity">The supplier data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<Supplier> updatedEntity);

        /// <summary>Deletes a specific supplier by its primary key</summary>
        /// <param name="id">The primary key of the supplier</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The supplierService responsible for managing supplier related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting supplier information.
    /// </remarks>
    public class SupplierService : ISupplierService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the Supplier class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public SupplierService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific supplier by its primary key</summary>
        /// <param name="id">The primary key of the supplier</param>
        /// <returns>The supplier data</returns>
        public Supplier GetById(Guid id)
        {
            var entityData = _dbContext.Supplier.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of suppliers based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of suppliers</returns>/// <exception cref="Exception"></exception>
        public List<Supplier> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetSupplier(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new supplier</summary>
        /// <param name="model">The supplier data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(Supplier model)
        {
            model.Id = CreateSupplier(model);
            return model.Id;
        }

        /// <summary>Updates a specific supplier by its primary key</summary>
        /// <param name="id">The primary key of the supplier</param>
        /// <param name="updatedEntity">The supplier data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, Supplier updatedEntity)
        {
            UpdateSupplier(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific supplier by its primary key</summary>
        /// <param name="id">The primary key of the supplier</param>
        /// <param name="updatedEntity">The supplier data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<Supplier> updatedEntity)
        {
            PatchSupplier(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific supplier by its primary key</summary>
        /// <param name="id">The primary key of the supplier</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteSupplier(id);
            return true;
        }
        #region
        private List<Supplier> GetSupplier(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.Supplier.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<Supplier>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(Supplier), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<Supplier, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateSupplier(Supplier model)
        {
            _dbContext.Supplier.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateSupplier(Guid id, Supplier updatedEntity)
        {
            _dbContext.Supplier.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteSupplier(Guid id)
        {
            var entityData = _dbContext.Supplier.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.Supplier.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchSupplier(Guid id, JsonPatchDocument<Supplier> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.Supplier.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.Supplier.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}