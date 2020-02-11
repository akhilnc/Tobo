using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.DataAccess.Abstract.Account;
using TodoApi.Shared.Generics;

namespace TodoApi.Controllers.Account
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        #region Private Properties
        private readonly IAuthRepository _authRepo;

        #endregion

        #region Constructor
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }
        #endregion
        #region  publicMethods
        [HttpPost("Register")]
        public async Task<ActionResult> Register(string userName, string password)
        {
            try
            {
                var user = new User
                {
                    UserName = userName
                };
                return Ok(new Envelope<string>(true, "", await _authRepo.Register(user,password)));
            }
            catch (Exception ex)
            {
                return Ok(new Envelope<string>(false, "exception-message", ex));
            }
        }
        #endregion
    }
}