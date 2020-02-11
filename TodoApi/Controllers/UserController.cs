using System;
using System.Collections.Generic;
using TodoApi.Shared.Generics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.DataAccess.Abstract.Admin;
namespace TodoApi.Controllers
{
    [ApiController]   
     [Route("[controller]")]
    public class UserController : ControllerBase
    {
        #region Private Properties
        private readonly IUserRepository _userRepo;

        #endregion

        #region Constructor
        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        #endregion
        #region  publicMethods
        #region get
        [HttpGet]
       
        public async Task<ActionResult> GetUserData()
        {
            try
            {              
                return Ok(new Envelope<string>(true,"",await _userRepo.getUser()));
            }
            catch (Exception ex)
            {
               return Ok(new Envelope<string>(false, "exception-message", ex));
            }
        }
        #endregion
        #endregion
    }
}