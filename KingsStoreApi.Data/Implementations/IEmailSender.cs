namespace KingsStoreApi.Data.Implementations
{
    public interface IEmailSender
    {
        void SendMail(Message message);
    }
}
