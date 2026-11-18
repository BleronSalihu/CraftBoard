using System;
using System.Collections.Generic;
using System.Text;


namespace CraftBoard.Application.Interfaces;

public interface ICurrentUser
{
    Task<string> GetRequiredUserIdAsync();
}
