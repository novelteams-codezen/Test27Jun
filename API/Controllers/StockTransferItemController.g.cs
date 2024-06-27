using Microsoft.AspNetCore.Mvc;
using Test27Jun.Models;
using Test27Jun.Services;
using Test27Jun.Entities;
using Test27Jun.Filter;
using Test27Jun.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

namespace Test27Jun.Controllers
{
    /// <summary>
    /// Controller responsible for managing stocktransferitem related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting stocktransferitem information.
    /// </remarks>
    [Route("api/stocktransferitem")]
    [Authorize]
    public class StockTransferItemController : ControllerBase
    {
        private readonly IStockTransferItemService _stockTransferItemService;

        /// <summary>
        /// Initializes a new instance of the StockTransferItemController class with the specified context.
        /// </summary>
        /// <param name="istocktransferitemservice">The istocktransferitemservice to be used by the controller.</param>
        public StockTransferItemController(IStockTransferItemService istocktransferitemservice)
        {
            _stockTransferItemService = istocktransferitemservice;
        }

        /// <summary>Adds a new stocktransferitem</summary>
        /// <param name="model">The stocktransferitem data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("StockTransferItem",Entitlements.Create)]
        public IActionResult Post([FromBody] StockTransferItem model)
        {
            var id = _stockTransferItemService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of stocktransferitems based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of stocktransferitems</returns>
        [HttpGet]
        [UserAuthorize("StockTransferItem",Entitlements.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult Get([FromQuery] string filters, string searchTerm, int pageNumber = 1, int pageSize = 10, string sortField = null, string sortOrder = "asc")
        {
            List<FilterCriteria> filterCriteria = null;
            if (pageSize < 1)
            {
                return BadRequest("Page size invalid.");
            }

            if (pageNumber < 1)
            {
                return BadRequest("Page mumber invalid.");
            }

            if (!string.IsNullOrEmpty(filters))
            {
                filterCriteria = JsonHelper.Deserialize<List<FilterCriteria>>(filters);
            }

            var result = _stockTransferItemService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific stocktransferitem by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferitem</param>
        /// <returns>The stocktransferitem data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [UserAuthorize("StockTransferItem",Entitlements.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _stockTransferItemService.GetById(id);
            return Ok(result);
        }

        /// <summary>Deletes a specific stocktransferitem by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferitem</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [UserAuthorize("StockTransferItem",Entitlements.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var status = _stockTransferItemService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific stocktransferitem by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferitem</param>
        /// <param name="updatedEntity">The stocktransferitem data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [UserAuthorize("StockTransferItem",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] StockTransferItem updatedEntity)
        {
            if (id != updatedEntity.Id)
            {
                return BadRequest("Mismatched Id");
            }

            var status = _stockTransferItemService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific stocktransferitem by its primary key</summary>
        /// <param name="id">The primary key of the stocktransferitem</param>
        /// <param name="updatedEntity">The stocktransferitem data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [UserAuthorize("StockTransferItem",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] JsonPatchDocument<StockTransferItem> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = _stockTransferItemService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}