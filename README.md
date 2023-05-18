# Propozycja rozwiązania zadania nr.1: Producer

System generowania wiadomości e-mail dla sklepu internetowego stworzony w oparciu o ASP.NET, RabbitMQ, EF CORE<br />

--Object Order:<br />
int Id <br />
string Email <br />
bool isCompleted <br />
bool isSent  <br />

## /api/OrderStatus/completing/id_zamówienia 
Nadawanie nowemu zamówieniu statusu "W trakcie realizacji" oraz wysyłanie obiektu do kolejki.

## /api/OrderStatus/sending/id_zamówienia
Nadawanie zamówieniu, które posiada już status "W trakcie realizacji" statusu "Zamówienie wysłane" oraz wysyłanie obiektu do kolejki.

## Consumer z zaimplementowanym SmtpClient do wysyłania statusu do klienta sklepu

https://github.com/tyburski/RabbitMQConsumer



