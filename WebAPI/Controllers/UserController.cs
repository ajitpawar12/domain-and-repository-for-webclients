using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Domain.Interfaces;
using Domain.Models;
using Domain.Storage;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {
        private readonly IDataRepository _repository;
        public UserController()
        {
            _repository=new DataRepository();
            
        }

        [HttpGet]
        [Route("api/user/getalluser")]
        public HttpResponseMessage GetAllUser()
        {
            var allusers = _repository.AllUsers();

            if (allusers.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK,allusers);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound,"Data not Found");
        }

        [HttpPost]
        [Route("api/user/adduser")]
        public HttpResponseMessage AddUser(User user)
        {
            if (user != null)
            {
                _repository.AddUser(user);
                return Request.CreateResponse(HttpStatusCode.OK, "User add Successfully");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Error occurs while adding user");

        }

        [HttpGet]
        [Route("api/user/getuserdetails")]
        public HttpResponseMessage GetUserDetails(int id)
        {
            if (id != 0)
            {
                var userdetails = _repository.UserDetails(id);
                return Request.CreateResponse(HttpStatusCode.OK, userdetails);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "User not Found");
        }

        [HttpDelete]
        [Route("api/user/deleteuser")]
        public HttpResponseMessage DeleteUser(int id)
        {
            if (id != 0)
            {
               _repository.DeleteUser(id);
                return Request.CreateResponse(HttpStatusCode.OK,"User Delete Successfully");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "User not Found");
        }

        [HttpPut]
        [Route("api/user/updateuser")]
        public HttpResponseMessage UpdateteUser(User user)
        {
            if (user != null)
            {
                _repository.UpdateUser(user.UserId,user);
                return Request.CreateResponse(HttpStatusCode.OK, "User Update Successfully");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Error occurs while update user");
        }

    }
}
