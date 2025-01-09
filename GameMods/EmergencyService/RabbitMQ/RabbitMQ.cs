using EmergencyService.Classes;
using EnergencyService.Models.Template;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


namespace EnergencyService.RabbitMQ;

public class RabbitMq
{
    private ConnectionFactory factory;
    private bool result;

    public RabbitMq()
    {
        factory = new ConnectionFactory() { HostName = "localhost" };
        result = false;
    }
    public async void SendMessage(Guid Account, string TypeWay)
    {
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: TypeWay,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var jsonString = JsonConvert.SerializeObject(Account);

        var body = Encoding.UTF8.GetBytes(jsonString);

        var properties = new BasicProperties
        {
            Persistent = true
        };

        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: TypeWay,
            body: body
        );
    }
    
    public async Task<bool> ReceiveMessage(string TypeWay)
    {
        
        FullAccountTemplate? myObject = new FullAccountTemplate();
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
            myObject = JsonConvert.DeserializeObject<FullAccountTemplate>(Encoding.UTF8.GetString(body));
            Console.WriteLine($" [x] Received {myObject.Email}");
            Registration registration = new Registration(myObject);
            result = registration.Register();
            await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
        };

        await channel.BasicConsumeAsync(TypeWay, autoAck: true, consumer: consumer);
        return result;
    }
}