using System;

namespace Orkaris_Back.Services;

public class SMTPService
{
    public string? Server { get; set; }
    public int Port { get; set; }
    public string? User { get; set; }
    public string? Password { get; set; }
}
