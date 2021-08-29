using Product.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Services
{
    public class Helper
        :IHelper
    {
        private readonly string _writeKey = "dJL*(Dkfje89345";
        private readonly string _readKey = "49fkJfi0e!9d";
        public string FilterString(string sourse)
        {
            return sourse.Replace('\'', '\"').Replace("<", "").Replace(">", "");
        }
        public bool ReadAuthorization(string key)
        {
            if (key == _readKey)
                return true;
            else
                return false;
        }
        public bool WriteAuthorization(string key)
        {
            if (key == _writeKey)
                return true;
            else
                return false;
        }
    }
}
