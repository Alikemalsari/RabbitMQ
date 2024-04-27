// Producer Kodu
using RabbitMQ.Client;
using System.Text;


for (int i = 0; i < 150; i++)
{
    mesajilet();
}

Console.ReadLine();


void mesajilet()
{
    var factory = new ConnectionFactory
    {
        HostName = "localhost",
    };
    using var connection = factory.CreateConnection();
    var channel = connection.CreateModel();
    channel.QueueDeclare("mesajKuyruk", durable: true, exclusive: false, autoDelete: false);
    var mesaj = "İlk mesaj";

    var body = Encoding.UTF8.GetBytes($"{mesaj} - Gönderme zamanı: {DateTime.Now}");
    channel.BasicPublish(exchange: "", routingKey: "mesajKuyruk", body: body);

    Console.WriteLine("Mesaj Kuyruğa iletildi.");
}
