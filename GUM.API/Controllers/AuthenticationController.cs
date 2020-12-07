using GUM.DAL;
using GUM.EntityDataModel;
using GUM.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace GUM.API.Controllers
{
    //http://192.168.0.102:51023/
    [AllowAnonymous]
    public class AuthenticationController : ApiController
    {
        readonly IUserRepository userRepository = new UserRepository();

        #region Login existing user
        [HttpGet]
        [Route("api/Authentication/Login")]
        public HttpResponseMessage Login(string email, string password)
        {
            var user = userRepository.ValidateUser(email, password);
            if (user != null)
            {
                var token = userRepository.GetToken(user);
                return Request.CreateResponse(HttpStatusCode.OK, new { Token = token });
            }
            else
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Incorrect Login credentials");
        }
        #endregion

        #region Signup new user
        [HttpPost]
        [Route("api/Authentication/Signup/")]
        public HttpResponseMessage SignUp(User user)
        {
            user.RoleID = 2;
            var result = userRepository.AddUser(user);
            if (result == true)
                return Request.CreateResponse(HttpStatusCode.OK,new { Saved="True"});
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest,new { Saved="False"});
        }
        #endregion

    }
}