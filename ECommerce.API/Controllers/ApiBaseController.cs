using ECommerce.Application.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        public static ActionResult<T> ToActionResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
                return new OkObjectResult(result.data);

            return ToProblem(result.Errors);
        }
        public static ActionResult<T> ToActionResult<T>(Result result)
        {
            if (result.IsSuccess)
                return new OkResult();

            return ToProblem(result.Errors);
        }
        public static ObjectResult ToProblem(IReadOnlyList<Error> errors)
        {
            var First = errors[0];
            var status = First.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.UnAuthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };

            var Problem = new ProblemDetails
            {
                Status = status,
                Title = First.code,
                Detail = First.Description,
                Extensions = { ["errors"] = errors }
            };
            return new ObjectResult(Problem) { StatusCode = status };
        }
    }
}
