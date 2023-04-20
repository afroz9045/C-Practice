using System.Text;
// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

Console.WriteLine("Welcome to ticketing service.");

var factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };
        var conn = factory.CreateConnection();

        using var channel = conn.CreateModel();

        channel.QueueDeclare("bookings",durable:false,exclusive:false,autoDelete:false,arguments:null);


        var consumer = new EventingBasicConsumer(channel);

        consumer.Received+= (model,eventArgs)=>
        {
            var body = eventArgs.Body.ToArray();

            var message = Encoding.UTF8.GetString(body);
             Console.WriteLine($"A message has been received - {message}");
        };

        channel.BasicConsume("bookings",true,consumer);

        Console.ReadKey();