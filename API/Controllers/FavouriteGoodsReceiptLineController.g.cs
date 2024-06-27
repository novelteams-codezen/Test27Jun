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
    /// Controller responsible for managing favouritegoodsreceiptline related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting favouritegoodsreceiptline information.
    /// </remarks>
    [Route("api/favouritegoodsreceiptline")]
    [Authorize]
    public class FavouriteGoodsReceiptLineController : ControllerBase
    {
        private readonly IFavouriteGoodsReceiptLineService _favouriteGoodsReceiptLineService;

        /// <summary>
        /// Initializes a new instance of the FavouriteGoodsReceiptLineController class with the specified context.
        /// </summary>
        /// <param name="ifavouritegoodsreceiptlineservice">The ifavouritegoodsreceiptlineservice to be used by the controller.</param>
        public FavouriteGoodsReceiptLineController(IFavouriteGoodsReceiptLineService ifavouritegoodsreceiptlineservice)
        {
            _favouriteGoodsReceiptLineService = ifavouritegoodsreceiptlineservice;
        }

        /// <summary>Adds a new favouritegoodsreceiptline</summary>
        /// <param name="model">The favouritegoodsreceiptline data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("FavouriteGoodsReceiptLine",Entitlements.Create)]
        public IActionResult Post([FromBody] FavouriteGoodsReceiptLine model)
        {
            var id = _favouriteGoodsReceiptLineService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of favouritegoodsreceiptlines based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of favouritegoodsreceiptlines</returns>
        [HttpGet]
        [UserAuthorize("FavouriteGoodsReceiptLine",Entitlements.Read)]
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

            var result = _favouriteGoodsReceiptLineService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific favouritegoodsreceiptline by its primary key</summary>
        /// <param name="id">The primary key of the favouritegoodsreceiptline</param>
        /// <returns>The favouritegoodsreceiptline data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [UserAuthorize("FavouriteGoodsReceiptLine",Entitlements.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _favouriteGoodsReceiptLineService.GetById(id);
            return Ok(result);
        }

        /// <summary>Deletes a specific favouritegoodsreceiptline by its primary key</summary>
        /// <param name="id">The primary key of the favouritegoodsreceiptline</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [UserAuthorize("FavouriteGoodsReceiptLine",Entitlements.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var status = _favouriteGoodsReceiptLineService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific favouritegoodsreceiptline by its primary key</summary>
        /// <param name="id">The primary key of the favouritegoodsreceiptline</param>
        /// <param name="updatedEntity">The favouritegoodsreceiptline data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [UserAuthorize("FavouriteGoodsReceiptLine",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] FavouriteGoodsReceiptLine updatedEntity)
        {
            if (id != updatedEntity.Id)
            {
                return BadRequest("Mismatched Id");
            }

            var status = _favouriteGoodsReceiptLineService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific favouritegoodsreceiptline by its primary key</summary>
        /// <param name="id">The primary key of the favouritegoodsreceiptline</param>
        /// <param name="updatedEntity">The favouritegoodsreceiptline data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [UserAuthorize("FavouriteGoodsReceiptLine",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] JsonPatchDocument<FavouriteGoodsReceiptLine> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = _favouriteGoodsReceiptLineService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}