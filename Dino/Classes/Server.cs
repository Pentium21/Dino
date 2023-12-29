//using Moon.Classes;
//using System.Collections.Generic;
//using System.Net.Sockets;
//using System.Text;


//class MoonBuggyServer
//{
//    private List<GameSession> gameSessions = new List<GameSession>();

//    private void HandleClient(Socket clientSocket)
//    {
//        // Создаем объект сессии игры для каждого клиента
//        GameSession gameSession = new GameSession();

//        // Добавляем клиента в сессию
//        gameSession.AddClient(clientSocket);

//        // Добавляем сессию в список активных сессий
//        gameSessions.Add(gameSession);

//        // Отправляем клиенту начальное состояние игры
//        SendInitialGameState(clientSocket, gameSession);

//        // ... (ваш код для обработки сообщений от клиента и обновления состояния игры)
//    }

//    private void SendInitialGameState(Socket clientSocket, GameSession gameSession)
//    {
//        // Получаем начальное состояние игры из объекта GameSession
//        GameState initialGameState = gameSession.GetInitialGameState();

//        // Преобразуем состояние в байты (например, сериализуем объект в JSON)
//        string initialStateJson = SerializeGameState(initialGameState);
//        byte[] data = Encoding.UTF8.GetBytes(initialStateJson);

//        // Отправляем начальное состояние клиенту
//        clientSocket.Send(data);
//    }
//    private void UpdateGame()
//    {
//        // ... (ваш код для обновления состояния игры, например, обработка движения объектов, столкновений и т.д.)
//    }

//    private string SerializeGameState(GameState gameState)
//    {
//        // Ваш код для сериализации объекта GameState (например, в JSON)
//        // Пример использования JSON.NET:
//        // return JsonConvert.SerializeObject(gameState);

//        // Ваш код сериализации
//        return "";
//    }
//}
//class GameSession
//{
//    private List<Socket> clients = new List<Socket>();
//    private GameState gameState = new GameState(); // Начальное состояние игры

//    public void AddClient(Socket clientSocket)
//    {
//        clients.Add(clientSocket);
//    }

//    public GameState GetInitialGameState()
//    {
//        // Возвращаем начальное состояние игры для нового клиента
//        return gameState;
//    }


//    // Другие методы для обновления состояния игры, обработки входящих сообщений и т.д.
//}

//class GameState
//{
//    // логика игры и данные состояния
//    public List<Player> Players { get; set; }
//    public List<Obstacle> Obstacles { get; set; }
//    // Другие свойства и методы, описывающие текущее состояние игры
//}