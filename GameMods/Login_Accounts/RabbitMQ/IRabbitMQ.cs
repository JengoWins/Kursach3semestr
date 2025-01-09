using Login_Accounts.Models.Template;

namespace Login_Accounts.RabbitMQ;

public interface IRabbitMQ
{
    public void SendMessage(FullAccountTemplate modelAccount);
    public void SendMessage(Guid Account, string TypeWay);
    public Task<List<Guid>> ReceiveMessage(string TypeWay);
}
