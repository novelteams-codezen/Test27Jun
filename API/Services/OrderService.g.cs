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
    /// The orderService responsible for managing order related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting order information.
    /// </remarks>
    public interface IOrderService
    {
        /// <summary>Retrieves a specific order by its primary key</summary>
        /// <param name="id">The primary key of the order</param>
        /// <returns>The order data</returns>
        Order GetById(Guid id);

        /// <summary>Retrieves a list of orders based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of orders</returns>
        List<Order> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new order</summary>
        /// <param name="model">The order data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(Order model);

        /// <summary>Updates a specific order by its primary key</summary>
        /// <param name="id">The primary key of the order</param>
        /// <param name="updatedEntity">The order data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, Order updatedEntity);

        /// <summary>Updates a specific order by its primary key</summary>
        /// <param name="id">The primary key of the order</param>
        /// <param name="updatedEntity">The order data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<Order> updatedEntity);

        /// <summary>Deletes a specific order by its primary key</summary>
        /// <param name="id">The primary key of the order</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The orderService responsible for managing order related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting order information.
    /// </remarks>
    public class OrderService : IOrderService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the Order class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public OrderService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific order by its primary key</summary>
        /// <param name="id">The primary key of the order</param>
        /// <returns>The order data</returns>
        public Order GetById(Guid id)
        {
            var entityData = _dbContext.Order.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of orders based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of orders</returns>/// <exception cref="Exception"></exception>
        public List<Order> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetOrder(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new order</summary>
        /// <param name="model">The order data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(Order model)
        {
            model.Id = CreateOrder(model);
            return model.Id;
        }

        /// <summary>Updates a specific order by its primary key</summary>
        /// <param name="id">The primary key of the order</param>
        /// <param name="updatedEntity">The order data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, Order updatedEntity)
        {
            UpdateOrder(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific order by its primary key</summary>
        /// <param name="id">The primary key of the order</param>
        /// <param name="updatedEntity">The order data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<Order> updatedEntity)
        {
            PatchOrder(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific order by its primary key</summary>
        /// <param name="id">The primary key of the order</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteOrder(id);
            return true;
        }
        #region
        private List<Order> GetOrder(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.Order.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<Order>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(Order), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<Order, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateOrder(Order model)
        {
            _dbContext.Order.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateOrder(Guid id, Order updatedEntity)
        {
            _dbContext.Order.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteOrder(Guid id)
        {
            var entityData = _dbContext.Order.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.Order.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchOrder(Guid id, JsonPatchDocument<Order> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.Order.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.Order.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}