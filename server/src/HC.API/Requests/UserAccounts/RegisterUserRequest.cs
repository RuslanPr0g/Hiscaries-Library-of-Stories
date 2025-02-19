﻿using System;

namespace HC.API.Requests.Users;

public class RegisterUserRequest
{
    public string Username { get; set; }

    public string Email { get; set; }

    public DateTime BirthDate { get; set; }

    public string Password { get; set; }
}