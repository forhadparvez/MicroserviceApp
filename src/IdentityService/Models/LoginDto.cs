using System;

namespace IdentityService.Models;

public class LoginDto
{
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";

}
