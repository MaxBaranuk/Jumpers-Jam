using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using ChartboostSDK;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour {

    public GameObject[] jumpers;
    public Sprite[] clockNumbers;
    public GameObject jumper;
    public GameObject block;
    public GameObject finishBlock;
    public GameObject emptyBlock;
    public GameObject startPlatform;
    public GameObject startBack;
    public GameObject startBack2;
    public GameObject startPanel;
    public GameObject finger;

    public GameObject countdownTimer;
    public GameObject countdownMin;
    public GameObject countdownSecOne;
    public GameObject countdownSecTwo;

    public GameObject timeToPlayVMin;
    public GameObject timeToPlayVSecOne;
    public GameObject timeToPlayVSecTwo;

    public GameObject timeToPlayIMin;
    public GameObject timeToPlayISecOne;
    public GameObject timeToPlayISecTwo;

//    public GameObject testInfo;

    public GameObject losePanelV;
    public GameObject losePanelI;
    GameTimer timer;
    InterstitialAd interstitial;
    public Sprite fingerTap;
    public int pointsToFinish;
    int screenAfterClose;
    float lastPlatformPosition = -12f;
    bool finish;
    bool isGameLose;
    bool emptyBlockCreate;
    AudioSource sourse;
    public AudioClip click;

    void OnEnable()
    {
        SetupDelegates();
    }

    void Start () {
        sourse = GetComponent<AudioSource>();
        jumper = (GameObject) Instantiate(jumpers[Settings.currentJumper], new Vector3(0f, -3.7f, 0f), Quaternion.identity);
        Settings.points = 0;
        Settings.totalCoins = 0;
        Settings.collectedCoins = 0;
        Settings.isStart = false;
        timer = GameObject.Find("GameTimer").GetComponent<GameTimer>();
        losePanelV.SetActive(false);
        losePanelI.SetActive(false);
        RequestInterstitial();
        Chartboost.setShouldPauseClickForConfirmation(false);
        Chartboost.setAutoCacheAds(true);
        Chartboost.cacheRewardedVideo(CBLocation.Default);
        Chartboost.cacheMoreApps(CBLocation.Default);
        Chartboost.cacheInterstitial(CBLocation.Default);
    }

    void Update () {
//        testInfo.GetComponent<Text>().text = ""+timer.getCurrTime().Ticks + timer.isStart;
        if (timer.isStart && timer.getCurrTime().Ticks < 0) SceneManager.LoadScene(1);
        if (losePanelV.activeInHierarchy) showTime(timeToPlayVMin, timeToPlayVSecOne, timeToPlayVSecTwo);
        if (losePanelI.activeInHierarchy) showTime(timeToPlayIMin, timeToPlayISecOne, timeToPlayISecTwo);

        if (Settings.isInGame) transform.position = new Vector3(0, jumper.transform.position.y + 6.88f, transform.position.z);   
        if (jumper.transform.position.y <= lastPlatformPosition& !isGameLose)
        {
            StartCoroutine(fallSound());
            if (Settings.gametype == Settings.GameType.Tournament && Settings.tournamentType == Settings.TournamentType.TimeGame)
            {
                Destroy(jumper);
                jumper = (GameObject)Instantiate(jumpers[Settings.currentJumper], new Vector3(0f, lastPlatformPosition+12f, 0f), Quaternion.identity);

            }
            else {
                isGameLose = true;
                Settings.game.lives--;
                jumper.GetComponent<Rigidbody2D>().isKinematic = true;
                if (Settings.gametype == Settings.GameType.Tournament)
                {
                    screenAfterClose = 1;                   
                    Nextpeer.ReportControlledTournamentOverWithScore((uint)Settings.points);
                }
                else
                {
                    Settings.game.lastScore = Settings.points;
                    SaveLoad.save(Settings.currentLevel, Settings.points, 0);                    
                    screenAfterClose = 3;
                }
                loseGame(screenAfterClose);
            }           
        }

        if (Settings.points > pointsToFinish && !finish && Settings.gametype == Settings.GameType.Adventure) finishLevel();          
        
        blockChanging();

        if (!Settings.isStart && Input.GetTouch(0).phase == TouchPhase.Began) finger.GetComponent<Image>().sprite = fingerTap;
        if (!Settings.isStart && Input.GetTouch(0).phase == TouchPhase.Ended) startLevel();

        if (Settings.gametype == Settings.GameType.Tournament && Settings.tournamentType == Settings.TournamentType.TimeGame)
        {
            showTime(countdownMin, countdownSecOne, countdownSecTwo);
            if (timer.getCurrTime().Ticks < 0)
            {
                Settings.game.lives--;
                SceneManager.LoadSceneAsync(1);
                Nextpeer.ReportControlledTournamentOverWithScore((uint)Settings.points);
            }
        }
    }

    void SetupDelegates()
    {
        Chartboost.didCompleteRewardedVideo += didCompleteRewardedVideo;
        Chartboost.didClickMoreApps += didClickMoreApps;
    }

    void OnDisable()
    {
        Chartboost.didCompleteRewardedVideo -= didCompleteRewardedVideo;
        Chartboost.didClickMoreApps -= didClickMoreApps;
    }
    public void setLastTransformPosition(float position) {
        lastPlatformPosition = position;
    }

    void didClickMoreApps(CBLocation location)
    {
        Settings.game.lives++;
    }

    void didCompleteRewardedVideo(CBLocation location, int reward)
    {
        Settings.game.lives++;
    }
    void blockChanging()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        foreach (GameObject block in blocks)
        {
            if ((gameObject.transform.position.y - block.transform.position.y) >= 45)
            {
                if (!finish) Instantiate(block, new Vector3(0, block.transform.position.y + 22.05f * 4, 0), Quaternion.identity);
                
                else if (emptyBlockCreate)
                        Instantiate(emptyBlock, new Vector3(0, block.transform.position.y + 22.05f * 4, 0), Quaternion.identity);                   
                    else {
                        Instantiate(finishBlock, new Vector3(0, block.transform.position.y + 22.05f * 4, 0), Quaternion.identity);
                        emptyBlockCreate = true;
                    }
                                    
                Destroy(block);
            }
        }
    }

    void finishLevel() {
        finish = true;
    }

    void loseGame(int screen) {

        if (Settings.gametype == Settings.GameType.Tournament) SceneManager.LoadScene(screen);
        else showAd();
    }

    public void losePanelClose() {
        SceneManager.LoadScene(screenAfterClose);
    }

    public void showVideo() {
        Chartboost.showRewardedVideo(CBLocation.Default);
    }

    public void showMoreApps() {
        Chartboost.showMoreApps(CBLocation.Default);
    }

    void startLevel() {
        startPanel.SetActive(false);
        Settings.isStart = true;
        if (Settings.gametype == Settings.GameType.Tournament && Settings.tournamentType == Settings.TournamentType.TimeGame) {
            countdownTimer.SetActive(true);
            timer.startTimer(150);
        } 
        jumper.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 20f), ForceMode2D.Impulse);
        Instantiate(block, new Vector3(0, -2 * 22.05f, 0), Quaternion.identity);
        Instantiate(block, new Vector3(0, -22.05f, 0), Quaternion.identity);
        Instantiate(block, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(block, new Vector3(0, 22.05f, 0), Quaternion.identity);
        Destroy(startPlatform);
        Destroy(startBack);
        Destroy(startBack2);
    }

    void showTime(GameObject min, GameObject sec1, GameObject sec2)
    {
        int one = 0;
        int two = 0;
        min.GetComponent<Image>().sprite = clockNumbers[timer.getCurrTime().Minutes];
        one = timer.getCurrTime().Seconds / 10;
        two = timer.getCurrTime().Seconds % 10;
        sec1.GetComponent<Image>().sprite = clockNumbers[one];
        sec2.GetComponent<Image>().sprite = clockNumbers[two];
    }

    void showAd() {
        Destroy(jumper);
        if (Random.value > 0.5f) openLosePanel();
        else {
            SceneManager.LoadSceneAsync(1);
           
            int adType = Random.Range(0,3);
            switch (adType) {
                case 0:
                    if (interstitial.IsLoaded()) interstitial.Show();
  //                  else Chartboost.showInterstitial(CBLocation.Default);
                    break;
                case 1:
                    if(Chartboost.hasInterstitial(CBLocation.Default)) Chartboost.showInterstitial(CBLocation.Default);
                    break;
                case 2:
                    if (Advertisement.IsReady()) Advertisement.Show();
  //                  else Chartboost.showInterstitial(CBLocation.Default);
                    break;
            }
            
        }  
    }

    void openLosePanel() {
        timer.startTimer(30);
        if (Random.value > 0.5f) losePanelV.SetActive(true);
        else losePanelI.SetActive(true);
    }

    public void RequestInterstitial()
    {
        #if UNITY_ANDROID
        string adUnitId = "ca-app-pub-5189333161534729/2220015690";
        #elif UNITY_iOS
        //                  #if UNITY_iOS
        string adUnitId = "ca-app-pub-5778004916491997/4286337068";
        #endif
        interstitial = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }

    IEnumerator fallSound()
    {
        sourse.PlayOneShot(click);
        yield return null;
    }
}
