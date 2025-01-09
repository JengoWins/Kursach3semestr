using ArchiveFiles.Models.Template;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


namespace ArchiveFiles.RabbitMQ;

public class RabbitMq
{
    private ConnectionFactory factory;
    private bool result;

    public RabbitMq()
    {
        factory = new ConnectionFactory() { HostName = "localhost" };
        result = false;
    }
    public async Task<List<Guid>> ReceiveMessage(string TypeWay)
    {
        List<Guid> myObject = new List<Guid>();

        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: TypeWay,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
        Console.WriteLine(" [*] Waiting for messages.");

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            await Task.Delay(10000);
            var body = ea.Body.ToArray();
            myObject.Add(JsonConvert.DeserializeObject<Guid>(Encoding.UTF8.GetString(body)));

            await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
        };

        await channel.BasicConsumeAsync(TypeWay, autoAck: true, consumer: consumer);
        return myObject;
    }
}