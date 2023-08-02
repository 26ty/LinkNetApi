using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LinkNetApi.Models;
using NuGet.Protocol.Plugins;

namespace LinkNetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
	{
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
		{
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login request)
        {
            // 查找符合帳號的用戶
            var user = await _context.User.FirstOrDefaultAsync(u => u.username == request.username);

            // 如果找不到用戶或密碼不正確，返回失敗提示
            if (user == null || user.password != request.password)
            {
                return BadRequest("帳號或密碼輸入錯誤");
            }

            // 驗證成功，返回成功提示
            return Ok("登入成功");
        }
    }
}


