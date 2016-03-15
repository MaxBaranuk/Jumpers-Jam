
[System.Serializable]
 public class Game {

    public int totalScore;
    public int totalStars;
    public int lives;
    public int lastScore;
    public System.DateTime timerTime;
    public bool startTimer;
    public Level[] levels = {new Level(0,0, true),
                             new Level(0,0, true),
                             new Level(0,0, true),
                             new Level(0,0, true),
                             new Level(0,0, true),
                             new Level(0,0, true)};

    public Game() {
    totalScore = 0;
    totalStars = 0;
    lastScore = 0;
    lives = 10;
    }	
}
