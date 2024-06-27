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
    /// The drugscheduleService responsible for managing drugschedule related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting drugschedule information.
    /// </remarks>
    public interface IDrugScheduleService
    {
        /// <summary>Retrieves a specific drugschedule by its primary key</summary>
        /// <param name="id">The primary key of the drugschedule</param>
        /// <returns>The drugschedule data</returns>
        DrugSchedule GetById(Guid id);

        /// <summary>Retrieves a list of drugschedules based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of drugschedules</returns>
        List<DrugSchedule> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new drugschedule</summary>
        /// <param name="model">The drugschedule data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(DrugSchedule model);

        /// <summary>Updates a specific drugschedule by its primary key</summary>
        /// <param name="id">The primary key of the drugschedule</param>
        /// <param name="updatedEntity">The drugschedule data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, DrugSchedule updatedEntity);

        /// <summary>Updates a specific drugschedule by its primary key</summary>
        /// <param name="id">The primary key of the drugschedule</param>
        /// <param name="updatedEntity">The drugschedule data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<DrugSchedule> updatedEntity);

        /// <summary>Deletes a specific drugschedule by its primary key</summary>
        /// <param name="id">The primary key of the drugschedule</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The drugscheduleService responsible for managing drugschedule related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting drugschedule information.
    /// </remarks>
    public class DrugScheduleService : IDrugScheduleService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the DrugSchedule class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public DrugScheduleService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific drugschedule by its primary key</summary>
        /// <param name="id">The primary key of the drugschedule</param>
        /// <returns>The drugschedule data</returns>
        public DrugSchedule GetById(Guid id)
        {
            var entityData = _dbContext.DrugSchedule.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of drugschedules based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of drugschedules</returns>/// <exception cref="Exception"></exception>
        public List<DrugSchedule> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetDrugSchedule(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new drugschedule</summary>
        /// <param name="model">The drugschedule data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(DrugSchedule model)
        {
            model.Id = CreateDrugSchedule(model);
            return model.Id;
        }

        /// <summary>Updates a specific drugschedule by its primary key</summary>
        /// <param name="id">The primary key of the drugschedule</param>
        /// <param name="updatedEntity">The drugschedule data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, DrugSchedule updatedEntity)
        {
            UpdateDrugSchedule(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific drugschedule by its primary key</summary>
        /// <param name="id">The primary key of the drugschedule</param>
        /// <param name="updatedEntity">The drugschedule data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<DrugSchedule> updatedEntity)
        {
            PatchDrugSchedule(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific drugschedule by its primary key</summary>
        /// <param name="id">The primary key of the drugschedule</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteDrugSchedule(id);
            return true;
        }
        #region
        private List<DrugSchedule> GetDrugSchedule(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.DrugSchedule.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<DrugSchedule>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(DrugSchedule), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<DrugSchedule, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateDrugSchedule(DrugSchedule model)
        {
            _dbContext.DrugSchedule.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateDrugSchedule(Guid id, DrugSchedule updatedEntity)
        {
            _dbContext.DrugSchedule.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteDrugSchedule(Guid id)
        {
            var entityData = _dbContext.DrugSchedule.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.DrugSchedule.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchDrugSchedule(Guid id, JsonPatchDocument<DrugSchedule> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.DrugSchedule.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.DrugSchedule.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}