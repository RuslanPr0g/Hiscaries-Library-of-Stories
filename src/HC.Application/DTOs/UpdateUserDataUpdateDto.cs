using System;

namespace HC.Application.DTOs;

public class UpdateUserDataUpdateDto
{
    public string UsernameUpdate { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public bool Banned { get; set; } = false;

    public string PreviousPassword { get; set; }
    public string NewPassword { get; set; }
}