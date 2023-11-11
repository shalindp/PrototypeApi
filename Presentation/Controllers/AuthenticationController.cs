using Application.Authentication.Commands;
using Application.Authentication.Queries;
using Contracts;
using Contracts.Authentication.Mappers;
using Contracts.Authentication.Requests;
using Contracts.Authentication.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Extensions;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [HttpPost(nameof(SignUp))]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var command = new SignUpCommand(request.Email, request.Password);

        var result = await _mediator.Send(command);
        
        return result.Resolve(AuthenticationResponseMapper.Map);
    }
    
    [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [HttpPost(nameof(SignIn))]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
    
        var query = new SignInQuery(request.Email, request.Password);
        
        var result = await _mediator.Send(query);
    
        return result.Resolve(AuthenticationResponseMapper.Map);
    }
    
    [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [HttpGet(nameof(GetRefreshToken))]
    public async Task<IActionResult> GetRefreshToken([FromBody] RefreshTokenRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var query = new RefreshTokenCommand(request.RefreshToken);
        
        var result = await _mediator.Send(query);

        return result.Resolve(AuthenticationResponseMapper.Map);
    }
}