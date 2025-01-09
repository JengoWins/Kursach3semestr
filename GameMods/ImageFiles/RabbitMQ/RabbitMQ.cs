﻿using ImageFiles.Classes;
using ImageFiles.Models.Template;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


namespace ImageFiles.RabbitMQ;

public class RabbitMq
{
    private ConnectionFactory factory;
    private bool result;

    public RabbitMq()
    {
        factory = new ConnectionFactory() { HostName = "localhost" };
        result = false;
    }
    /*
    public async void SendMessage(FullAccountTemplate modelAccount)
    {
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: "User",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var jsonString = JsonConvert.SerializeObject(modelAccount);

        var body = Encoding.UTF8.GetBytes(jsonString);

        var properties = new BasicProperties
        {
            Persistent = true
        };

        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: "User",
            body: body
        );
    }
    */
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