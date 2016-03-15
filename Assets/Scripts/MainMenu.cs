using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using ChartboostSDK;

public class MainMenu : MonoBehaviour {

    public Sprite[] bigNumbers;
    public Sprite[] liveNumbers;
    public Sprite[] clockNumbers;
    public GameObject timer;
    public GameObject clockImage;
    public GameObject clockPoints;
    public GameObject clockMin;
    public GameObject clockSecOne;
    public GameObject clockSecTwo;
    public GameObject numOne;
    public GameObject numTwo;
    public GameObject numThree;
    public GameObject numFour;
    public GameObject exitPanel;
    public GameObject buyPanel;
    public Sprite empty;
    public Sprite clockIm;
    public Sprite clockPointsIm;
    public GameObject livesNumOne;
    public GameObject livesNumTwo;
    public GameObject shadePanel;

    AudioSource sourse;
    public AudioClip click;
//    public AudioClip menuSound;
//    public GameObject testInfo;

    private bool showInterstitial = true;
    private bool showMoreApps = true;
    private bool showRewardedVideo = true;
    private bool activeAgeGate = false;

    void playGamesInit()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
    .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();

    }

    void initNextPeer()
    {
        NPGameSettings npSettings = new NPGameSettings();
#if UNITY_IPHONE
         // Set special settings for iOS here:
               
         String gameKey = "b154516a1005e49e7cac53ef218642a0";
#elif UNITY_ANDROID
        // Set special settings for Android here:
        // UNITY_ANDROID

        String gameKey = "657c52af3490d42b5eeb2c03ccfe39bc";

#endif

        Nextpeer.Init(gameKey, npSettings);
    }

    //void OnEnable()
    //{
    //    SetupDelegates();
    //}

    void Start () {
        MainMusic.source.Play();
        sourse = GetComponent<AudioSource>();
        Settings.gametype = Settings.GameType.Adventure;
        exitPanel.SetActive(false);
        shadePanel.SetActive(false);
        buyPanel.SetActive(false);
        initNextPeer();
        if(!PlayGamesPlatform.Instance.IsAuthenticated()) playGamesAuth();
        Chartboost.setShouldPauseClickForConfirmation(false);

       
        //        Chartboost.setAutoCacheAds(true);
        //        Chartboost.cacheInterstitial(CBLocation.Default);
        Chartboost.cacheMoreApps(CBLocation.Default);
//         testInfo.GetComponent<Text>().text = "Has apps: " + Chartboost.hasMoreApps(CBLocation.Default);

    }

   // Update is called once per frame
    void Update () {

        showBestScore(Settings.game.lastScore);
        showLives(Settings.game.lives);
        if (Settings.game.startTimer)
        {
            timer.SetActive(true);
            showTime();
        }
        else timer.SetActive(false);
        
        if (Input.GetKeyDown(KeyCode.Escape)) exit();
	}
   
    void showBestScore(int number)
    {
        int one = 0;
        int two = 0;
        int three = 0;
        int four = 0;

        four = number % 10;
        numFour.GetComponent<Image>().sprite = bigNumbers[four];

        if (number > 9)
        {
            three = (number % 100 - four) / 10;
            numThree.GetComponent<Image>().sprite = bigNumbers[three];
        }
        if (number > 99)
        {
            two = (number % 1000 - three - four) / 100;
            numTwo.GetComponent<Image>().sprite = bigNumbers[two];
        }
        if (number > 999)
        {
            one = (number % 10000 - two - three - four) / 1000;
            numOne.GetComponent<Image>().sprite = bigNumbers[one];
        }
  }

    void showLives(int number)
    {
        int one = 0;
        int two = 0;
        
        two = number % 10;
        livesNumOne.GetComponent<Image>().sprite = empty;
        livesNumTwo.GetComponent<Image>().sprite = liveNumbers[two];

        if (number > 9)
        {
            one = number / 10;
            livesNumOne.GetComponent<Image>().sprite = liveNumbers[one];
        }
        
    }

    void showTime()
    {
        int one = 0;
        int two = 0;

        DateTime timeToShow = Settings.game.timerTime.AddMinutes(9);
        TimeSpan elapsedSpan = new TimeSpan(timeToShow.Ticks - DateTime.Now.Ticks);
        clockImage.GetComponent<Image>().sprite = clockIm;
        clockPoints.GetComponent<Image>().sprite = clockPointsIm;
        clockMin.GetComponent<Image>().sprite = clockNumbers[elapsedSpan.Minutes];

        one = elapsedSpan.Seconds / 10;
        two = elapsedSpan.Seconds % 10;
        clockSecOne.GetComponent<Image>().sprite = clockNumbers[one];
        clockSecTwo.GetComponent<Image>().sprite = clockNumbers[two];
    }

    public void startGame() {
        sourse.Play();
//        sourse.PlayOneShot(click);
        Settings.gametype = Settings.GameType.Adventure;
        SceneManager.LoadScene(2);        
    }

    void exit()
    {
        sourse.Play();
//        sourse.PlayOneShot(click);
        exitPanel.SetActive(true);
    }

    public void exitOk()
    {
        sourse.Play();
//        sourse.PlayOneShot(click);
        SaveLoad.save();
        Application.Quit();
    }

    public void exitCancel()
    {
        sourse.Play();
 //       sourse.PlayOneShot(click);
        exitPanel.SetActive(false);
    }

    public void openChallenge() {
        sourse.Play();
        //        sourse.PlayOneShot(click);
        Nextpeer.LaunchDashboard();
        SceneManager.LoadScene(12);
        
            }

    public void openAchievements() {

        sourse.Play();
  //      sourse.PlayOneShot(click);
        Social.ShowAchievementsUI();           
    }

    public void openBuyPanel() {
        sourse.Play();
        //        sourse.PlayOneShot(click);
        shadePanel.SetActive(true);
        buyPanel.SetActive(true);
    }

    public void closeBuyPanel() {
        sourse.Play();
//        sourse.PlayOneShot(click);
        shadePanel.SetActive(false);
        buyPanel.SetActive(false);
    }

    public void showMoreGames() {
        sourse.Play();
 //       sourse.PlayOneShot(click);
        Chartboost.showMoreApps(CBLocation.Default);
    }
   
    void playGamesAuth()
    {

        PlayGamesPlatform.Instance.Authenticate((bool sucsess) =>
        {
        });
    }

    public void openLeaderboard() {
        sourse.Play();
        //       sourse.PlayOneShot(click);
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI("CgkI-vWtg-YEEAIQBg");
    }

    public void postScore() {
        Social.ReportScore(100, "CgkI-vWtg-YEEAIQBg", (bool success) =>
        {
        });
    }

}
