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
            //// 查找符合帳號的用戶
            //var user = await _context.User.FirstOrDefaultAsync(u => u.username == request.username);

            //// 如果找不到用戶或密碼不正確，返回失敗提示
            //if (user == null || user.password != request.password)
            //{
            //    //return BadRequest("帳號或密碼輸入錯誤");
            //    return BadRequest(new { massage = "帳號或密碼輸入錯誤，登入失敗" });
            //}

            //// 驗證成功，返回成功提示
            //return Ok(new {status=200, message= "登入成功" });
            // 檢查是否輸入了帳號和密碼
            if (string.IsNullOrEmpty(request.username) || string.IsNullOrEmpty(request.password))
            {
                return BadRequest(new { code = 400, message = "未輸入帳號或密碼！" });
            }

            try
            {
                // 查找符合帳號的用戶
                var user = await _context.User.FirstOrDefaultAsync(u => u.username == request.username);

                // 如果找不到用戶，返回無此帳號提示
                if (user == null)
                {
                    return StatusCode(422,new { code = 422, message = "無此帳號，是否要進行註冊？" });
                }

                // 檢查密碼是否正確
                if (user.password != request.password)
                {
                    return Unauthorized(new { code = 401, message = "密碼錯誤!" });
                }

                // 驗證成功，返回成功提示
                return Ok(new { code = 200, message = "登入成功！" });
            }
            catch (Exception)
            {
                // 發生異常，可能是伺服器無連線等問題
                return StatusCode(500, new { code = 500, message = "伺服器無連線，請聯絡伺服器管理人員！" });
            }
        }
    }
}


