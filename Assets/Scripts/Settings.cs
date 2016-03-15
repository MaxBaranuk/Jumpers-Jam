
public static class Settings {

    public static Game game = new Game();
    public static int points = 0;
    public static float totalCoins = 0;
    public static float collectedCoins = 0;
    public static bool isInGame = true;
    public static bool isStart = true;
    public static int currentLevel = 0;
    public static int currentJumper = 0;
    public static GameType gametype;
    public static TournamentType tournamentType;
    public static string[] achievementCodes = { "CgkI-vWtg-YEEAIQAQ",
                                                "CgkI-vWtg-YEEAIQAg",
                                                "CgkI-vWtg-YEEAIQAw",
                                                "CgkI-vWtg-YEEAIQB",
                                                "CgkI-vWtg-YEEAIQBQ"};

    public enum GameType
    {
        Tournament,
        Adventure, 
        NoType
    }

   

    public enum TournamentType
    {
        HighScore,
        TimeGame
    }

}
