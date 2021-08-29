using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Services.Interfaces
{
    public interface IHelper
    {
        string FilterString(string sourse);
        bool ReadAuthorization(string key);
        bool WriteAuthorization(string key);
    }
}
