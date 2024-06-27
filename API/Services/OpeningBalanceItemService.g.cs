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
    /// The openingbalanceitemService responsible for managing openingbalanceitem related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting openingbalanceitem information.
    /// </remarks>
    public interface IOpeningBalanceItemService
    {
        /// <summary>Retrieves a specific openingbalanceitem by its primary key</summary>
        /// <param name="id">The primary key of the openingbalanceitem</param>
        /// <returns>The openingbalanceitem data</returns>
        OpeningBalanceItem GetById(Guid id);

        /// <summary>Retrieves a list of openingbalanceitems based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of openingbalanceitems</returns>
        List<OpeningBalanceItem> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new openingbalanceitem</summary>
        /// <param name="model">The openingbalanceitem data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(OpeningBalanceItem model);

        /// <summary>Updates a specific openingbalanceitem by its primary key</summary>
        /// <param name="id">The primary key of the openingbalanceitem</param>
        /// <param name="updatedEntity">The openingbalanceitem data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, OpeningBalanceItem updatedEntity);

        /// <summary>Updates a specific openingbalanceitem by its primary key</summary>
        /// <param name="id">The primary key of the openingbalanceitem</param>
        /// <param name="updatedEntity">The openingbalanceitem data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<OpeningBalanceItem> updatedEntity);

        /// <summary>Deletes a specific openingbalanceitem by its primary key</summary>
        /// <param name="id">The primary key of the openingbalanceitem</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The openingbalanceitemService responsible for managing openingbalanceitem related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting openingbalanceitem information.
    /// </remarks>
    public class OpeningBalanceItemService : IOpeningBalanceItemService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the OpeningBalanceItem class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public OpeningBalanceItemService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific openingbalanceitem by its primary key</summary>
        /// <param name="id">The primary key of the openingbalanceitem</param>
        /// <returns>The openingbalanceitem data</returns>
        public OpeningBalanceItem GetById(Guid id)
        {
            var entityData = _dbContext.OpeningBalanceItem.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of openingbalanceitems based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of openingbalanceitems</returns>/// <exception cref="Exception"></exception>
        public List<OpeningBalanceItem> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetOpeningBalanceItem(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new openingbalanceitem</summary>
        /// <param name="model">The openingbalanceitem data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(OpeningBalanceItem model)
        {
            model.Id = CreateOpeningBalanceItem(model);
            return model.Id;
        }

        /// <summary>Updates a specific openingbalanceitem by its primary key</summary>
        /// <param name="id">The primary key of the openingbalanceitem</param>
        /// <param name="updatedEntity">The openingbalanceitem data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, OpeningBalanceItem updatedEntity)
        {
            UpdateOpeningBalanceItem(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific openingbalanceitem by its primary key</summary>
        /// <param name="id">The primary key of the openingbalanceitem</param>
        /// <param name="updatedEntity">The openingbalanceitem data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<OpeningBalanceItem> updatedEntity)
        {
            PatchOpeningBalanceItem(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific openingbalanceitem by its primary key</summary>
        /// <param name="id">The primary key of the openingbalanceitem</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteOpeningBalanceItem(id);
            return true;
        }
        #region
        private List<OpeningBalanceItem> GetOpeningBalanceItem(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.OpeningBalanceItem.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<OpeningBalanceItem>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(OpeningBalanceItem), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<OpeningBalanceItem, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateOpeningBalanceItem(OpeningBalanceItem model)
        {
            _dbContext.OpeningBalanceItem.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateOpeningBalanceItem(Guid id, OpeningBalanceItem updatedEntity)
        {
            _dbContext.OpeningBalanceItem.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteOpeningBalanceItem(Guid id)
        {
            var entityData = _dbContext.OpeningBalanceItem.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.OpeningBalanceItem.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchOpeningBalanceItem(Guid id, JsonPatchDocument<OpeningBalanceItem> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.OpeningBalanceItem.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.OpeningBalanceItem.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}