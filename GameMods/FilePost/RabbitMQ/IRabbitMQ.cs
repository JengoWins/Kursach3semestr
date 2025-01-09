using Posts.Models.Template;

namespace Posts.RabbitMQ;

public interface IRabbitMQ
{
    public void SendMessage(Guid Account, string TypeWay);
    public Task<List<Guid>> ReceiveMessage(string TypeWay);
}
