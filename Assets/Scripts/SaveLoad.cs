using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{

    public static void save(int level, int points, int stars)
    {
        if (stars >= Settings.game.levels[level].stars) {
            Settings.game.levels[level].stars = stars;
            if (points > Settings.game.levels[level].points) {
                Settings.game.levels[level].points = points;
            }
        }
        Settings.game.totalScore = 0;
        Settings.game.totalStars = 0;
        for (int i = 0; i < 6; i++) {
            

            Settings.game.totalScore += Settings.game.levels[i].points;
            Settings.game.totalStars += Settings.game.levels[i].stars;

        }

        Social.ReportScore(100, "CgkI-vWtg-YEEAIQBg", (bool success) =>{});
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd"); //you can call it anything you want
        bf.Serialize(file, Settings.game);
        file.Close();
    }

    public static void save() {
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd"); //you can call it anything you want
        bf.Serialize(file, Settings.game);
        file.Close();
    }
    
    public static void load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            Settings.game = (Game)bf.Deserialize(file);
            file.Close();
        }
    }
}
