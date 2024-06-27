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
    /// Controller responsible for managing productcustomuom related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting productcustomuom information.
    /// </remarks>
    [Route("api/productcustomuom")]
    [Authorize]
    public class ProductCustomUOMController : ControllerBase
    {
        private readonly IProductCustomUOMService _productCustomUOMService;

        /// <summary>
        /// Initializes a new instance of the ProductCustomUOMController class with the specified context.
        /// </summary>
        /// <param name="iproductcustomuomservice">The iproductcustomuomservice to be used by the controller.</param>
        public ProductCustomUOMController(IProductCustomUOMService iproductcustomuomservice)
        {
            _productCustomUOMService = iproductcustomuomservice;
        }

        /// <summary>Adds a new productcustomuom</summary>
        /// <param name="model">The productcustomuom data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("ProductCustomUOM",Entitlements.Create)]
        public IActionResult Post([FromBody] ProductCustomUOM model)
        {
            var id = _productCustomUOMService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of productcustomuoms based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of productcustomuoms</returns>
        [HttpGet]
        [UserAuthorize("ProductCustomUOM",Entitlements.Read)]
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

            var result = _productCustomUOMService.Get(filterCriteria, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific productcustomuom by its primary key</summary>
        /// <param name="id">The primary key of the productcustomuom</param>
        /// <returns>The productcustomuom data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [UserAuthorize("ProductCustomUOM",Entitlements.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _productCustomUOMService.GetById(id);
            return Ok(result);
        }

        /// <summary>Deletes a specific productcustomuom by its primary key</summary>
        /// <param name="id">The primary key of the productcustomuom</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [UserAuthorize("ProductCustomUOM",Entitlements.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var status = _productCustomUOMService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific productcustomuom by its primary key</summary>
        /// <param name="id">The primary key of the productcustomuom</param>
        /// <param name="updatedEntity">The productcustomuom data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [UserAuthorize("ProductCustomUOM",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] ProductCustomUOM updatedEntity)
        {
            if (id != updatedEntity.Id)
            {
                return BadRequest("Mismatched Id");
            }

            var status = _productCustomUOMService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific productcustomuom by its primary key</summary>
        /// <param name="id">The primary key of the productcustomuom</param>
        /// <param name="updatedEntity">The productcustomuom data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [UserAuthorize("ProductCustomUOM",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] JsonPatchDocument<ProductCustomUOM> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = _productCustomUOMService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}