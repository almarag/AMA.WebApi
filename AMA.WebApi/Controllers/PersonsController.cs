namespace AMA.WebApi.Controllers
{
    using AMA.Common.Controllers;
    using AMA.Users.Application.Persons.Commnads.CreatePerson;
    using AMA.Users.Domain.Constants;
    using FluentValidation.Results;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route(RouteConstants.ApiV1DefaultRoute)]
    public class PersonsController : BaseController
    {
        /// <summary>
        /// Retrieves a User by Identifier
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(CreatePersonCommandModel))]
        [ProducesResponseType(400, Type = typeof(List<ValidationFailure>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] CreatePersonCommand request) =>
            Ok(await Mediator.Send(request));
    }
}
