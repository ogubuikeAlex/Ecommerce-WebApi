using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingsStoreApi.Model.ModelHelpers.Mail
{
    public interface IEmailSender
    {
         void SendEmail (Message message); 
    }
}
