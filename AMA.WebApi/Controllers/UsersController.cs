namespace AMA.WebApi.Controllers
{
    using AMA.Common.Controllers;
    using AMA.Users.Application.Queries.GetUser;
    using AMA.Users.Domain.Constants;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route(RouteConstants.ApiV1DefaultRoute)]
    public class UsersController : BaseController
    {
        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(GetUserQueryModel))]
        public async Task<IActionResult> GetUser([FromRoute] GetUserQuery request) =>
            Ok(await Mediator.Send(request));
    }
}
