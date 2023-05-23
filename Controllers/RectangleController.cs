using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RectangleCoordinatesApi.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebApplication1.Data;

namespace RectangleCoordinatesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   
    public class RectangleController : ControllerBase
    {
        private readonly RectangleDbContext _dbContext;

        public RectangleController(RectangleDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [Authorize]
        [HttpPost("GetRectanglesInCoordinates")]
        public ActionResult<List<Rectangle>> GetRectanglesInCoordinates([FromBody] CoordinateRequest request)
        {
            List<Rectangle> rectangles = _dbContext.Rectangles.ToList();

            List<Rectangle> rectanglesInCoordinates = new List<Rectangle>();

            foreach (Coordinate coordinate in request.Coordinates)
            {
                foreach (var rectangle in rectangles)
                {
                    if (IsCoordinateInsideRectangle(coordinate, rectangle))
                    {
                        rectanglesInCoordinates.Add(rectangle);
                    }
                }
            }

            return rectanglesInCoordinates;
        }
        [HttpGet]
        public string GenrateJWTToken()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKey@345fghhhhhhhhhhhhhhhhhhhhhhhhhhhhhfggggggg"));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "John Doe"),
                new Claim(ClaimTypes.Email, "john.doe@example.com")
            };

            var token = new JwtSecurityToken(
                issuer: "false",
                audience: "false",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7), // Set the token expiration date
                signingCredentials: signingCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
        private bool IsCoordinateInsideRectangle(Coordinate coordinate, Rectangle rectangle)
        {
            return coordinate.X >= rectangle.X && coordinate.X <= rectangle.X + rectangle.Width &&
                   coordinate.Y >= rectangle.Y && coordinate.Y <= rectangle.Y + rectangle.Height;
        }

    }
}
