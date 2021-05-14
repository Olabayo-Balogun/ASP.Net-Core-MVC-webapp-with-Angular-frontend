namespace DutchTreat.Services
{
    public interface IMailService
    {
        //An interface is necessary if you plan to have multiple implementation of the service using different real-life mailing services.
        void SendMessage(string to, string subject, string body);
    }
}