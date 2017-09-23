using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace city_info_api.Services
{
    public interface IMailService
    {
        void Send(string subject, string message);

    }

}
