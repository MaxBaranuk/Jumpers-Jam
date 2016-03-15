using UnityEngine;
using System;

public class TimeController : MonoBehaviour {


	void Start () {
        DontDestroyOnLoad(gameObject);

	}
	
	// Update is called once per frame
	void Update () {

        if (!Settings.game.startTimer & Settings.game.lives < 10)
        {
            Settings.game.startTimer = true;
            Settings.game.timerTime = DateTime.Now;
        }

        TimeSpan elapsedSpan = new TimeSpan(DateTime.Now.Ticks - Settings.game.timerTime.Ticks);

        if (Settings.game.startTimer & (elapsedSpan.TotalSeconds > 540))
        {
                Settings.game.lives++;
                Settings.game.timerTime = Settings.game.timerTime.AddMinutes(9.0);
                if (Settings.game.lives == 10) {
                    Settings.game.startTimer = false;
                }
        }

	}
}
