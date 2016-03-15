using UnityEngine;
using System;

public class GameTimer : MonoBehaviour {

    DateTime startGameTime;
    DateTime finishTime;
    public bool isStart;
    TimeSpan elapsedSpan;
    // Use this for initialization
    void Start() {
        startGameTime = DateTime.Now;
    }

    // Update is called once per frame
    void Update() {

    }

    public void startTimer(int timeInSec) { 
        if (!isStart) { 
        startGameTime = DateTime.Now;
        finishTime = startGameTime.AddSeconds(timeInSec);
        isStart = true;
    }
    }

    public void stopTimer() {

    }

    public TimeSpan getCurrTime() {
        
        if (isStart) elapsedSpan = new TimeSpan(finishTime.Ticks - DateTime.Now.Ticks);
        else elapsedSpan = new TimeSpan(0);
        return elapsedSpan;
    }
}
