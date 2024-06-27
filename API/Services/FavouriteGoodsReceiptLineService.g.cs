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
    /// The favouritegoodsreceiptlineService responsible for managing favouritegoodsreceiptline related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting favouritegoodsreceiptline information.
    /// </remarks>
    public interface IFavouriteGoodsReceiptLineService
    {
        /// <summary>Retrieves a specific favouritegoodsreceiptline by its primary key</summary>
        /// <param name="id">The primary key of the favouritegoodsreceiptline</param>
        /// <returns>The favouritegoodsreceiptline data</returns>
        FavouriteGoodsReceiptLine GetById(Guid id);

        /// <summary>Retrieves a list of favouritegoodsreceiptlines based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of favouritegoodsreceiptlines</returns>
        List<FavouriteGoodsReceiptLine> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new favouritegoodsreceiptline</summary>
        /// <param name="model">The favouritegoodsreceiptline data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(FavouriteGoodsReceiptLine model);

        /// <summary>Updates a specific favouritegoodsreceiptline by its primary key</summary>
        /// <param name="id">The primary key of the favouritegoodsreceiptline</param>
        /// <param name="updatedEntity">The favouritegoodsreceiptline data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, FavouriteGoodsReceiptLine updatedEntity);

        /// <summary>Updates a specific favouritegoodsreceiptline by its primary key</summary>
        /// <param name="id">The primary key of the favouritegoodsreceiptline</param>
        /// <param name="updatedEntity">The favouritegoodsreceiptline data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<FavouriteGoodsReceiptLine> updatedEntity);

        /// <summary>Deletes a specific favouritegoodsreceiptline by its primary key</summary>
        /// <param name="id">The primary key of the favouritegoodsreceiptline</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The favouritegoodsreceiptlineService responsible for managing favouritegoodsreceiptline related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting favouritegoodsreceiptline information.
    /// </remarks>
    public class FavouriteGoodsReceiptLineService : IFavouriteGoodsReceiptLineService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the FavouriteGoodsReceiptLine class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public FavouriteGoodsReceiptLineService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific favouritegoodsreceiptline by its primary key</summary>
        /// <param name="id">The primary key of the favouritegoodsreceiptline</param>
        /// <returns>The favouritegoodsreceiptline data</returns>
        public FavouriteGoodsReceiptLine GetById(Guid id)
        {
            var entityData = _dbContext.FavouriteGoodsReceiptLine.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of favouritegoodsreceiptlines based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of favouritegoodsreceiptlines</returns>/// <exception cref="Exception"></exception>
        public List<FavouriteGoodsReceiptLine> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetFavouriteGoodsReceiptLine(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new favouritegoodsreceiptline</summary>
        /// <param name="model">The favouritegoodsreceiptline data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(FavouriteGoodsReceiptLine model)
        {
            model.Id = CreateFavouriteGoodsReceiptLine(model);
            return model.Id;
        }

        /// <summary>Updates a specific favouritegoodsreceiptline by its primary key</summary>
        /// <param name="id">The primary key of the favouritegoodsreceiptline</param>
        /// <param name="updatedEntity">The favouritegoodsreceiptline data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, FavouriteGoodsReceiptLine updatedEntity)
        {
            UpdateFavouriteGoodsReceiptLine(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific favouritegoodsreceiptline by its primary key</summary>
        /// <param name="id">The primary key of the favouritegoodsreceiptline</param>
        /// <param name="updatedEntity">The favouritegoodsreceiptline data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<FavouriteGoodsReceiptLine> updatedEntity)
        {
            PatchFavouriteGoodsReceiptLine(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific favouritegoodsreceiptline by its primary key</summary>
        /// <param name="id">The primary key of the favouritegoodsreceiptline</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteFavouriteGoodsReceiptLine(id);
            return true;
        }
        #region
        private List<FavouriteGoodsReceiptLine> GetFavouriteGoodsReceiptLine(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.FavouriteGoodsReceiptLine.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<FavouriteGoodsReceiptLine>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(FavouriteGoodsReceiptLine), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<FavouriteGoodsReceiptLine, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateFavouriteGoodsReceiptLine(FavouriteGoodsReceiptLine model)
        {
            _dbContext.FavouriteGoodsReceiptLine.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateFavouriteGoodsReceiptLine(Guid id, FavouriteGoodsReceiptLine updatedEntity)
        {
            _dbContext.FavouriteGoodsReceiptLine.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteFavouriteGoodsReceiptLine(Guid id)
        {
            var entityData = _dbContext.FavouriteGoodsReceiptLine.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.FavouriteGoodsReceiptLine.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchFavouriteGoodsReceiptLine(Guid id, JsonPatchDocument<FavouriteGoodsReceiptLine> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.FavouriteGoodsReceiptLine.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.FavouriteGoodsReceiptLine.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}