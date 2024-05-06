using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Services.Interfaces
{
    public interface IRedisService
    {
        void SetValue(string key, string value, TimeSpan? expiry = null);
        string GetValue(string key);
        void RemoveValue(string key);
    }
}
