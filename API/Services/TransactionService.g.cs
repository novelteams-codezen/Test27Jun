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
    /// The transactionService responsible for managing transaction related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting transaction information.
    /// </remarks>
    public interface ITransactionService
    {
        /// <summary>Retrieves a specific transaction by its primary key</summary>
        /// <param name="id">The primary key of the transaction</param>
        /// <returns>The transaction data</returns>
        Transaction GetById(Guid id);

        /// <summary>Retrieves a list of transactions based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of transactions</returns>
        List<Transaction> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new transaction</summary>
        /// <param name="model">The transaction data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(Transaction model);

        /// <summary>Updates a specific transaction by its primary key</summary>
        /// <param name="id">The primary key of the transaction</param>
        /// <param name="updatedEntity">The transaction data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, Transaction updatedEntity);

        /// <summary>Updates a specific transaction by its primary key</summary>
        /// <param name="id">The primary key of the transaction</param>
        /// <param name="updatedEntity">The transaction data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<Transaction> updatedEntity);

        /// <summary>Deletes a specific transaction by its primary key</summary>
        /// <param name="id">The primary key of the transaction</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The transactionService responsible for managing transaction related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting transaction information.
    /// </remarks>
    public class TransactionService : ITransactionService
    {
        private Test27JunContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the Transaction class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public TransactionService(Test27JunContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific transaction by its primary key</summary>
        /// <param name="id">The primary key of the transaction</param>
        /// <returns>The transaction data</returns>
        public Transaction GetById(Guid id)
        {
            var entityData = _dbContext.Transaction.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of transactions based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of transactions</returns>/// <exception cref="Exception"></exception>
        public List<Transaction> Get(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            var result = GetTransaction(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return result;
        }

        /// <summary>Adds a new transaction</summary>
        /// <param name="model">The transaction data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(Transaction model)
        {
            model.Id = CreateTransaction(model);
            return model.Id;
        }

        /// <summary>Updates a specific transaction by its primary key</summary>
        /// <param name="id">The primary key of the transaction</param>
        /// <param name="updatedEntity">The transaction data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, Transaction updatedEntity)
        {
            UpdateTransaction(id, updatedEntity);
            return true;
        }

        /// <summary>Updates a specific transaction by its primary key</summary>
        /// <param name="id">The primary key of the transaction</param>
        /// <param name="updatedEntity">The transaction data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<Transaction> updatedEntity)
        {
            PatchTransaction(id, updatedEntity);
            return true;
        }

        /// <summary>Deletes a specific transaction by its primary key</summary>
        /// <param name="id">The primary key of the transaction</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            DeleteTransaction(id);
            return true;
        }
        #region
        private List<Transaction> GetTransaction(List<FilterCriteria> filters = null, string searchTerm = "", int pageNumber = 1, int pageSize = 1, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                throw new ApplicationException("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new ApplicationException("Page mumber invalid!");
            }

            var query = _dbContext.Transaction.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<Transaction>.ApplyFilter(query, filters, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(Transaction), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<Transaction, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        private Guid CreateTransaction(Transaction model)
        {
            _dbContext.Transaction.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        private void UpdateTransaction(Guid id, Transaction updatedEntity)
        {
            _dbContext.Transaction.Update(updatedEntity);
            _dbContext.SaveChanges();
        }

        private bool DeleteTransaction(Guid id)
        {
            var entityData = _dbContext.Transaction.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new ApplicationException("No data found!");
            }

            _dbContext.Transaction.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }

        private void PatchTransaction(Guid id, JsonPatchDocument<Transaction> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new ApplicationException("Patch document is missing!");
            }

            var existingEntity = _dbContext.Transaction.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new ApplicationException("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.Transaction.Update(existingEntity);
            _dbContext.SaveChanges();
        }
        #endregion
    }
}