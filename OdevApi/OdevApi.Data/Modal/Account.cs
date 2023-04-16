﻿using OdevApi.Base;

namespace OdevApi.Data;

public class Account : BaseModal
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public DateTime LastActivity { get; set; }
    public virtual List<Person> Persons { get; set; }
}
