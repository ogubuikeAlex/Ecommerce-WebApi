using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingsStoreApi.Model.ModelHelpers.Mail
{
    internal interface IEmailSender
    {
        void SendEmail(Message message);
    }
}
