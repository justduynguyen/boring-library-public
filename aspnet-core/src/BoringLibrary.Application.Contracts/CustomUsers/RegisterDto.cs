﻿using System.ComponentModel.DataAnnotations;

namespace BoringLibrary.CustomUsers;

public class RegisterDto
{
    [Required] public string UserName { get; set; }
    [Required] public string Password { get; set; }
}