namespace AMA.WebApi.Controllers
{
    using AMA.Common.Controllers;
    using AMA.Users.Application.Queries.GetUser;
    using AMA.Users.Domain.Constants;
    using FluentValidation.Results;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route(RouteConstants.ApiV1DefaultRoute)]
    public class UsersController : BaseController
    {
        /// <summary>
        /// Retrieves a User by Identifier
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(GetUserQueryModel))]
        [ProducesResponseType(400, Type = typeof(List<ValidationFailure>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUser([FromRoute] GetUserQuery request) =>
            Ok(await Mediator.Send(request));
    }
}
