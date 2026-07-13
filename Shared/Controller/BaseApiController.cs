using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses;

namespace FlashDrop.Shared.Controller
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        /// <summary>
        /// HTTP 200 OK
        /// </summary>
        protected IActionResult Success<T>(
            T data,
            string message = "Success")
        {
            return Ok(
                ApiResponse<T>.SuccessResponse(data, message));
        }

        /// <summary>
        /// HTTP 201 Created
        /// </summary>
        protected IActionResult CreatedResponse<T>(
            T data,
            string message = "Created successfully")
        {
            return StatusCode(
                StatusCodes.Status201Created,
                ApiResponse<T>.SuccessResponse(data, message));
        }

        /// <summary>
        /// HTTP 204 No Content
        /// </summary>
        protected IActionResult NoContentResponse()
        {
            return NoContent();
        }
    }
}