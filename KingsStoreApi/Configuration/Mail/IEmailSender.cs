using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingsStoreApi.Configuration.Mail
{
    public interface IEmailSender
    {
        void sendEmail (Message message); 
    }
}
