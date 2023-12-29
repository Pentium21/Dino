using Moon.Classes;
using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;

class MoonBuggyServer
{
    private Socket serverSocket;
    IPAddress ipAddress = IPAddress.Parse("192.168.1.72");
    private const int PORT = 11000;
    private const int MAX_CONNECTIONS = 3;
    private int activeConnections = 0;
    private static readonly ManualResetEvent allDone = new ManualResetEvent(false);

    private List<GameSession> gameSessions = new List<GameSession>();
    public MoonBuggyServer()
    {
        StartServer();
    }

    private void StartServer()
    {

        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, PORT);

        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        serverSocket.Bind(localEndPoint);
        serverSocket.Listen(10);

        //Console.WriteLine($"Сервер запущен на порту {PORT}");

        while (true)
        {
            allDone.Reset();
            serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), serverSocket);
            allDone.WaitOne();
        }
    }

    private void AcceptCallback(IAsyncResult ar)
    {
        allDone.Set();

        Socket listener = (Socket)ar.AsyncState;
        Socket clientSocket = listener.EndAccept(ar);

        try
        {
            clientSocket = listener.EndAccept(ar);

            if (activeConnections < MAX_CONNECTIONS)
            {
                // Если не достигнуто максимальное количество подключений, принимаем новое подключение
                //Console.WriteLine($"Новое подключение: {((IPEndPoint)clientSocket.RemoteEndPoint).Address}");

                // Создаем новую игровую сессию для клиента
                HandleClient(clientSocket);


                //Console.WriteLine($"Подключение {((IPEndPoint)clientSocket.RemoteEndPoint).Address} закрыто");
                clientSocket.Close();
            }
            else
            {
                // Если достигнуто максимальное количество подключений, игнорируем новое подключение
                //Console.WriteLine($"Превышено максимальное количество подключений. Подключение {((IPEndPoint)clientSocket.RemoteEndPoint).Address} игнорируется.");
                clientSocket.Close();
            }
        }
        catch (Exception ex)
        {
            //Console.WriteLine($"Ошибка при обработке подключения: {ex.Message}");
            if (clientSocket != null)
                clientSocket.Close();
        }
    }


    private void HandleClient(Socket clientSocket)
    {
        // Создаем объект сессии игры для каждого клиента
        GameSession gameSession = new GameSession();

        // Добавляем клиента в сессию
        gameSession.AddClient(clientSocket);

        // Добавляем сессию в список активных сессий
        gameSessions.Add(gameSession);

        gameSession.SendGameState(clientSocket);

        // Отправляем клиенту начальное состояние игры
        //SendInitialGameState(clientSocket, gameSession);

        // ... (ваш код для обработки сообщений от клиента и обновления состояния игры)
    }

}
class GameSession
{
    private List<Socket> clients = new List<Socket>();
    private GameController game = new GameController(); // Начальное состояние игры

    public void AddClient(Socket clientSocket)
    {
        clients.Add(clientSocket);
    }

    public void SendGameState(Socket clientSocket)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (MemoryStream memoryStream = new MemoryStream())
        {
            binaryFormatter.Serialize(memoryStream, game);
            byte[] dataBytes = memoryStream.ToArray();
            clientSocket.Send(dataBytes);
        }
    }

}
