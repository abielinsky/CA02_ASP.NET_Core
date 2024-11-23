﻿using CA02_ASP.NET_Core.Data.Entity;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CA02_ASP.NET_Core.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : CustomControllerBase<UsersEntity, UsersEntity>
    {
        public UsersController(IGenericService<UsersEntity> service) : base(service)
        {
        }
    }

}
