using GUM.DAL;
using GUM.Interfaces;
using GUM.EntityDataModel;
using GUM.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Helpers;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GUM.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : ApiController
    {
        readonly IUserRepository userRepository = new UserRepository();

        #region Get all users
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var users = userRepository.GetAllUsers();
            if (users != null)
                return Request.CreateResponse(HttpStatusCode.OK, users);
            else
                return Request.CreateResponse(HttpStatusCode.NotFound);
        }
        #endregion

        #region Get user by ID
        [HttpGet]
        public HttpResponseMessage Get(long id)
        {
            var user = userRepository.GetUserByID(id);
            if (user != null)
                return Request.CreateResponse(HttpStatusCode.OK, user);
            else
                return Request.CreateResponse(HttpStatusCode.NotFound);
        }
        #endregion

        #region Update Existing User
        [HttpPut]
        public HttpResponseMessage Put([FromBody] User user)
        {
            var result= userRepository.UpdateUser(user);
            if (result == true)
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
        #endregion

        #region Delete Existing user
        [HttpDelete]
        public HttpResponseMessage Delete(long id)
        {
            var result= userRepository.DeleteUser(id);
            if (result == true)
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
        #endregion
    }
}