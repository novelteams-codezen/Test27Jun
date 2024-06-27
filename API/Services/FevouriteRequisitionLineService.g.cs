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
    /// The fevouriterequisitionlineService responsible for managing fevouriterequisitionline related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting fevouriterequisitionline information.
    /// </remarks>
    public interface IFevouriteRequisitionLineService
    {
        /// <summary>Retrieves a specific fevouriterequisitionline by its primary key</summary>
        /// <param name="id">The primary key of the fevouriterequisitionline</param>
        /// <returns>The fevouriterequisitionline data</returns>
        FevouriteRequisitionLine GetById(Guid id);

        /// <summary>Retrieves a list of fevouriterequisitionlines based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of fevouriterequisitionlines</returns>
        List<FevouriteRequisitionLine> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new fevouriterequisitionline</summary>
        /// <param name="model">The fevouriterequisitionline data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(FevouriteRequisitionLine model);

        /// <summary>Updates a specific fevouriterequisitionline by its primary key</summary>
        /// <param name="id">The primary key of the fevouriterequisitionline</param>
        /// <param name="updatedEntity">The fevouriterequisitionline data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, FevouriteRequisitionLine updatedEntity);

        /// <summary>Updates a specific fevouriterequisitionline by its primary key</summary>
        /// <param name="id">The primary key of the fevouriterequisitionline</param>
        /// <param name="updatedEntity">The fevouriterequisitionline data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<FevouriteRequisitionLine> updatedEntity);

        /// <summary>Deletes a specific fevouriterequisitionline by its primary key</summary>
        /// <param name="id">The primary key of the fevouriterequisitionline</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The fevouriterequisitionlineService responsible for managing fevouriterequisitionline related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting fevouriterequisitionline information.
    /// </remarks>
    public class FevouriteRequisitionLineService : IFevouriteRequisitionLineService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the FevouriteRequisitionLine class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public FevouriteRequisitionLineService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific fevouriterequisitionline by its primary key</summary>
        /// <param name="id">The primary key of the fevouriterequisitionline</param>
        /// <returns>The fevouriterequisitionline data</returns>
        public FevouriteRequisitionLine GetById(Guid id)
        {
            var entityData = _dbContext.FevouriteRequisitionLine.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of fevouriterequisitionlines based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of fevouriterequisitionlines</returns>/// <exception cref="Exception"></exception>
        public List<FevouriteRequisitionLine> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetFevouriteRequisitionLine(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new fevouriterequisitionline</summary>
        /// <param name="model">The fevouriterequisitionline data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(FevouriteRequisitionLine model)
        {
            model.Id = CreateFevouriteRequisitionLine(model);
            return model.Id;
        }

        /// <summary>Updates a specific fevouriterequisitionline by its primary key</summary>
        /// <param name="id">The primary key of the fevouriterequisitionline</param>
        /// <param name="updatedEntity">The fevouriterequisitionline data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, FevouriteRequisitionLine updatedEntity)
        {
            UpdateFevouriteRequisitionLine(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific fevouriterequisitionline by its primary key</summary>
        /// <param name="id">The primary key of the fevouriterequisitionline</param>
        /// <param name="updatedEntity">The fevouriterequisitionline data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<FevouriteRequisitionLine> updatedEntity)
        {
            PatchFevouriteRequisitionLine(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific fevouriterequisitionline by its primary key</summary>
        /// <param name="id">The primary key of the fevouriterequisitionline</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteFevouriteRequisitionLine(id);
            return true;
        }
        #region
        private List<FevouriteRequisitionLine> GetFevouriteRequisitionLine(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.FevouriteRequisitionLine.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<FevouriteRequisitionLine>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(FevouriteRequisitionLine), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<FevouriteRequisitionLine, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateFevouriteRequisitionLine(FevouriteRequisitionLine model)
        {
            _dbContext.FevouriteRequisitionLine.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateFevouriteRequisitionLine(Guid id, FevouriteRequisitionLine updatedEntity)
        {
            _dbContext.FevouriteRequisitionLine.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteFevouriteRequisitionLine(Guid id)
        {
            var entityData = _dbContext.FevouriteRequisitionLine.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.FevouriteRequisitionLine.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchFevouriteRequisitionLine(Guid id, JsonPatchDocument<FevouriteRequisitionLine> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.FevouriteRequisitionLine.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.FevouriteRequisitionLine.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}