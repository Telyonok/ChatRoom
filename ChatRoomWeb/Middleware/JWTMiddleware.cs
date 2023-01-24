using Azure;
using ChatRoomWeb.Models;
using ChatRoomWeb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Authentication;
using static System.Net.Mime.MediaTypeNames;

namespace ChatRoomWeb.Middleware
{
    public class JWTMiddleware
    {
        public RequestDelegate _next;
        public IUserManagementService _userManagementService;
        public JWTMiddleware(IUserManagementService userManagementService, RequestDelegate next)
        {
            _userManagementService = userManagementService;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var refreshToken = context.Request.Cookies["X-Refresh-Token"];
                TokenResponse newTokenResponse;
                try
                {
                    newTokenResponse = await _userManagementService.RefreshTokenAsync(refreshToken);

					context.Response.Cookies.Delete("X-Access-Token");
                    context.Response.Cookies.Delete("X-Refresh-Token");
					
					context.Response.Cookies.Append("X-Access-Token", newTokenResponse.Token,
                    new CookieOptions
                    {
                        HttpOnly = true,
                        SameSite = SameSiteMode.Strict
                    });
                    context.Response.Cookies.Append("X-Refresh-Token", newTokenResponse.RefreshToken,
                    new CookieOptions
                    {
                        HttpOnly = true,
                        SameSite = SameSiteMode.Strict
                    });

                    if (newTokenResponse != null)
					{
						context.Response.Redirect(context.Request.Path.Value);
				      //  await _next(context);
                    }
                }
                catch (Exception ex2)
                {
                    context.Response.Redirect("/UserManagement");
                }
            }
        }
    }
}
