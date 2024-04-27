// Consumer Kodu
using RabbitMQ.Client.Events;
using System.Text;
using RabbitMQ.Client;


var factory = new ConnectionFactory
{
    HostName = "localhost",
};
using var connection = factory.CreateConnection();
var channel = connection.CreateModel();
channel.QueueDeclare("mesajKuyruk", durable: true, exclusive: false, autoDelete: false);
var consumer = new EventingBasicConsumer(channel);
channel.BasicConsume("mesajKuyruk", true, consumer);
consumer.Received += Consumer_Received;
Console.ReadLine();



void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    var alinanMesaj = Encoding.UTF8.GetString(e.Body.ToArray());
    var zamanDamgasiIndex = alinanMesaj.LastIndexOf("- Gönderme zamanı:");
    var mesaj = alinanMesaj.Substring(0, zamanDamgasiIndex).Trim();
    var zamanDamgasi = alinanMesaj.Substring(zamanDamgasiIndex + 1).Trim();

    Console.WriteLine($"Gelen mesaj: {mesaj}");
    Console.WriteLine($"Mesajın gönderme zamanı: {zamanDamgasi}");
}
