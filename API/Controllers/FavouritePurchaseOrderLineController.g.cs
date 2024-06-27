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
    /// Controller responsible for managing favouritepurchaseorderline related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting favouritepurchaseorderline information.
    /// </remarks>
    [Route("api/favouritepurchaseorderline")]
    [Authorize]
    public class FavouritePurchaseOrderLineController : ControllerBase
    {
        private readonly IFavouritePurchaseOrderLineService _favouritePurchaseOrderLineService;

        /// <summary>
        /// Initializes a new instance of the FavouritePurchaseOrderLineController class with the specified context.
        /// </summary>
        /// <param name="ifavouritepurchaseorderlineservice">The ifavouritepurchaseorderlineservice to be used by the controller.</param>
        public FavouritePurchaseOrderLineController(IFavouritePurchaseOrderLineService ifavouritepurchaseorderlineservice)
        {
            _favouritePurchaseOrderLineService = ifavouritepurchaseorderlineservice;
        }

        /// <summary>Adds a new favouritepurchaseorderline</summary>
        /// <param name="model">The favouritepurchaseorderline data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("FavouritePurchaseOrderLine",Entitlements.Create)]
        public IActionResult Post([FromBody] FavouritePurchaseOrderLine model)
        {
            var id = _favouritePurchaseOrderLineService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of favouritepurchaseorderlines based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of favouritepurchaseorderlines</returns>
        [HttpGet]
        [UserAuthorize("FavouritePurchaseOrderLine",Entitlements.Read)]
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

            var result = _favouritePurchaseOrderLineService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific favouritepurchaseorderline by its primary key</summary>
        /// <param name="id">The primary key of the favouritepurchaseorderline</param>
        /// <returns>The favouritepurchaseorderline data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [UserAuthorize("FavouritePurchaseOrderLine",Entitlements.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _favouritePurchaseOrderLineService.GetById(id);
            return Ok(result);
        }

        /// <summary>Deletes a specific favouritepurchaseorderline by its primary key</summary>
        /// <param name="id">The primary key of the favouritepurchaseorderline</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [UserAuthorize("FavouritePurchaseOrderLine",Entitlements.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var status = _favouritePurchaseOrderLineService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific favouritepurchaseorderline by its primary key</summary>
        /// <param name="id">The primary key of the favouritepurchaseorderline</param>
        /// <param name="updatedEntity">The favouritepurchaseorderline data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [UserAuthorize("FavouritePurchaseOrderLine",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] FavouritePurchaseOrderLine updatedEntity)
        {
            if (id != updatedEntity.Id)
            {
                return BadRequest("Mismatched Id");
            }

            var status = _favouritePurchaseOrderLineService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific favouritepurchaseorderline by its primary key</summary>
        /// <param name="id">The primary key of the favouritepurchaseorderline</param>
        /// <param name="updatedEntity">The favouritepurchaseorderline data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [UserAuthorize("FavouritePurchaseOrderLine",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] JsonPatchDocument<FavouritePurchaseOrderLine> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = _favouritePurchaseOrderLineService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}