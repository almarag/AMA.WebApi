namespace AMA.WebApi.Controllers
{
    using AMA.Common.Controllers;
    using AMA.Users.Application.Queries.GetUser;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class UserController : BaseController
    {
        [HttpGet("{UserId}")]
        [ProducesResponseType(200, Type = typeof(GetUserQueryModel))]
        public async Task<IActionResult> GetUser([FromRoute] GetUserQuery request) =>
            Ok(await Mediator.Send(request));
    }
}
