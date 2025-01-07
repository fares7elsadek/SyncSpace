﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SyncSpace.Application.ApplicationUser.Commands.ConfirmEmail;
using SyncSpace.Application.ApplicationUser.Commands.CreateRefreshToken;
using SyncSpace.Application.ApplicationUser.Commands.LoginUser;
using SyncSpace.Application.ApplicationUser.Commands.RegisterUser;
using SyncSpace.Application.ApplicationUser.Commands.ResendConfirmationLink;
using SyncSpace.Domain.Exceptions;
using SyncSpace.Domain.Helpers;
using System.Net;

namespace SyncSpace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private ApiResponse apiResponse;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
            apiResponse = new ApiResponse();
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserCommand command)
        {
            var authResponse = await _mediator.Send(command);

            var response = new
            {
                message = authResponse.Message,
                email = authResponse.Email,
            };

            apiResponse.IsSuccess = true;
            apiResponse.Errors = null;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = response;
            return Ok(apiResponse);
        }

        [HttpGet("confirmEmail", Name = "ConfirmEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> ConfirmEmail([FromQuery] string UserId, [FromQuery] string Code)
        {
            var response = await _mediator.Send(new ConfirmEmailCommand(UserId, Code));
            apiResponse.IsSuccess = true;
            apiResponse.Errors = null;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = response;
            return Ok(apiResponse);
        }

        [HttpPost("resendConfirmationEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> ResendConfirmationEmail([FromBody] ResendConfirmationLinkCommand command)
        {
            var response = await _mediator.Send(command);
            apiResponse.IsSuccess = true;
            apiResponse.Errors = null;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = response;
            return Ok(apiResponse);
        }


        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> LoginUser([FromBody] LoginUserCommand command)
        {
            var authResponse = await _mediator.Send(command);
            if (!authResponse.IsAuthenticated)
                return BadRequest(authResponse);
            if (!string.IsNullOrEmpty(authResponse.RefreshToken))
                SetRefreshTokenInCookie(authResponse.RefreshToken, authResponse.RefreshTokenExpiration);

            apiResponse.IsSuccess = true;
            apiResponse.Errors = null;
            apiResponse.StatusCode = HttpStatusCode.OK;
            apiResponse.Result = authResponse;
            return Ok(apiResponse);
        }

        [HttpGet("refreshToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthResponse>> CreateRefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                throw new CustomeException("Invalid token");
            var result = await _mediator.Send(new CreateRefreshTokenCommand(refreshToken));
            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
            return Ok(result);
        }

        [SwaggerIgnore]
        public void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
