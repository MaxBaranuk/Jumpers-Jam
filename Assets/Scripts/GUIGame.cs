using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GUIGame : MonoBehaviour {

    public Sprite[] numbers;
    public Sprite empty;
    public GameObject numOne;
    public GameObject numTwo;
    public GameObject numThree;
    public GameObject numFour;
    public GameObject exitPanel;
    public GameObject shade;
    public GameObject gamePanel;
    public GameObject numOneWin;
    public GameObject numTwoWin;
    public GameObject numThreeWin;
    public GameObject numFourWin;
    public GameObject WinPanel;
    public GameObject livesNumOneWin;
    public GameObject livesNumTwoWin;
    public GameObject buyPanel;
    public GameObject losePanelV;
    public GameObject losePanelI;

    public GameObject livesNumOne;
    public GameObject livesNumTwo;

    public Sprite[] liveNumbers;
    bool isPaused;
    AudioSource sourse;
    public AudioClip click;

    void Start () {
        sourse = GetComponent<AudioSource>();
        MainMusic.source.Pause();
        exitPanel.SetActive(false);
        WinPanel.SetActive(false);
        shade.SetActive(false);
        buyPanel.SetActive(false);
    }
	

	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape)& !losePanelV.activeInHierarchy& !losePanelI.activeInHierarchy) pause();        
        showNumbers(Settings.points, numOne, numTwo, numThree, numFour);
        showLives(Settings.game.lives, livesNumOne, livesNumTwo);
	}

    void showNumbers(int number, GameObject numOne, GameObject numTwo, GameObject numThree, GameObject numFour)
    {
        int one = 0;
        int two = 0;
        int three = 0;
        int four = 0;

        four = number % 10;
        numFour.GetComponent<Image>().sprite = numbers[four];

        if (number > 9)
        {
            three = (number % 100 - four) / 10;
            numThree.GetComponent<Image>().sprite = numbers[three];
        }
        if (number > 99)
        {
            two = (number % 1000 - three - four) / 100;
            numTwo.GetComponent<Image>().sprite = numbers[two];
        }
        if (number > 999)
        {
            one = (number % 10000 - two - three - four) / 1000;
            numOne.GetComponent<Image>().sprite = numbers[one];
        }

    }

    void showLives(int number, GameObject livesNumOne, GameObject livesNumTwo)
    {
        int one = 0;
        int two = 0;

        two = number % 10;
        livesNumTwo.GetComponent<Image>().sprite = liveNumbers[two];

        if (number > 9)
        {
            one = number / 10;
            livesNumOne.GetComponent<Image>().sprite = liveNumbers[one];
        }
        else {
            livesNumOne.GetComponent<Image>().sprite = empty;
        }
    }

    void pause() {
        StartCoroutine(clickSound());
        Time.timeScale = 0;
        exitPanel.SetActive(true);
        shade.SetActive(true);
    }

    public void openBuyPanel()
    {
        StartCoroutine(clickSound());
        Time.timeScale = 0;
        buyPanel.SetActive(true);
        shade.SetActive(true);
    }

    public void closeBuyPanel()
    {
        StartCoroutine(clickSound());
        Time.timeScale = 1;
        buyPanel.SetActive(false);
        shade.SetActive(false);
    }

    public void openWinDialog() {
        Time.timeScale = 0;
        WinPanel.SetActive(true);
        gamePanel.SetActive(false);
        showNumbers(Settings.points, numOneWin, numTwoWin, numThreeWin, numFourWin);
        showLives(Settings.game.lives, livesNumOneWin, livesNumTwoWin);
        shade.SetActive(true);
    }

    public void restart() {
        StartCoroutine(clickSound());
        switch (Settings.currentLevel) { 
            case 0:
                SceneManager.LoadScene(4);
                break;
            case 1:
                SceneManager.LoadScene(5);
                break;
            case 2:
                SceneManager.LoadScene(6);
                break;
            case 3:
                SceneManager.LoadScene(7);
                break;
            case 4:
                SceneManager.LoadScene(8);
                break;
            case 5:
                SceneManager.LoadScene(9);
                break;
        }
        
    }

    public void toLevels() {
        StartCoroutine(clickSound());
        Nextpeer.ReportForfeitForCurrentTournament();
       SceneManager.LoadScene(3);
        MainMusic.source.Play();
    }
    
    public void exitOk() {
        StartCoroutine(clickSound());
        Nextpeer.ReportForfeitForCurrentTournament();
        SceneManager.LoadScene(1);
        MainMusic.source.Play();
    }

    public void exitCancel()
    {
        StartCoroutine(clickSound());
        Time.timeScale = 1;
        exitPanel.SetActive(false);
        shade.SetActive(false);
    }

    IEnumerator clickSound() {
        sourse.PlayOneShot(click);
        yield return null;
    }
}
