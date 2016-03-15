using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour {

    public GameObject[] oneStarsIm;
    public GameObject[] twoStarsIm;
    public GameObject[] threeStarsIm;
    public GameObject[] points;
    public GameObject[] playButtons;
    public GameObject[] levelImages;

    public GameObject extraLifePanel;
    public GameObject extraLivesNumOne;
    public GameObject extraLivesNumTwo;
    public GameObject extraClockMin;
    public GameObject extraClockSecOne;
    public GameObject extraClockSecTwo;
    public GameObject buyPanel;
    public GameObject shadePanel;
    public GameObject clock;
    public GameObject clockMin;
    public GameObject clockSecOne;
    public GameObject clockSecTwo;

    public GameObject livesNumOne;
    public GameObject livesNumTwo;

    public Sprite empty;

    public Sprite[] liveNumbers;
    public Sprite[] clockNumbers;

    public Sprite lockImage;
    public Sprite disableButton;
    public Sprite oneStar;
    public Sprite twoStar;
    public Sprite threeStar;
    int[] pointsToFinish = { 2000, 2500, 3000, 3500, 4000, 5000 };
    AudioSource sourse;

    void Start () {
        sourse = GetComponent<AudioSource>();
        buyPanel.SetActive(false);
        extraLifePanel.SetActive(false);
        shadePanel.SetActive(false);
        for (int i = 0; i < 6; i++)
        {

            if (Settings.gametype == Settings.GameType.Adventure)
            {
                points[i].GetComponent<Text>().text = "" + Settings.game.levels[i].points+" / "+pointsToFinish[i];
                switch (Settings.game.levels[i].stars)
                {
                    case 1:
                        oneStarsIm[i].GetComponent<Image>().sprite = oneStar;
                        break;
                    case 2:
                        oneStarsIm[i].GetComponent<Image>().sprite = oneStar;
                        twoStarsIm[i].GetComponent<Image>().sprite = twoStar;
                        break;
                    case 3:
                        oneStarsIm[i].GetComponent<Image>().sprite = oneStar;
                        twoStarsIm[i].GetComponent<Image>().sprite = twoStar;
                        threeStarsIm[i].GetComponent<Image>().sprite = threeStar;
                        break;
                    default:
                        break;
                }
            }
            if (!Settings.game.levels[i].isOpen)
            {
                playButtons[i].GetComponent<Button>().interactable = false;
                levelImages[i].GetComponent<Image>().sprite = lockImage;
            }
        }      
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Settings.gametype == Settings.GameType.Tournament) Nextpeer.ReportForfeitForCurrentTournament();
            SceneManager.LoadScene(1);

        }

        showLives(Settings.game.lives);

        if (Settings.game.startTimer)
        {
            clock.SetActive(true);
            showTime();            
        }
        else
        {
            clock.SetActive(false);
        }

        if (extraLifePanel.activeSelf)
        {
            showExtraTime();
            showExtraLives(Settings.game.lives);
        }
	}

    void showTime()
    {
        int one = 0;
        int two = 0;

        DateTime timeToShow = Settings.game.timerTime.AddMinutes(9);
        TimeSpan elapsedSpan = new TimeSpan(timeToShow.Ticks - DateTime.Now.Ticks);
        clockMin.GetComponent<Image>().sprite = clockNumbers[elapsedSpan.Minutes];

        one = elapsedSpan.Seconds / 10;
        two = elapsedSpan.Seconds % 10;
        clockSecOne.GetComponent<Image>().sprite = clockNumbers[one];
        clockSecTwo.GetComponent<Image>().sprite = clockNumbers[two];
    }

    public void openLevelOne() {
        if (playButtons[0].GetComponent<Button>().interactable)
        {
            sourse.Play();
            if (Settings.game.lives > 0)
            {
                Settings.currentLevel = 0;
                SceneManager.LoadScene(4);
            }
            else
            {
                extraLifePanel.SetActive(true);
            }
        }
    }
    public void openLevelTwo()
    {
        if (playButtons[1].GetComponent<Button>().interactable)
        {
            sourse.Play();
            if (Settings.game.lives > 0)
            {
                Settings.currentLevel = 1;
                SceneManager.LoadScene(5);
            }
            else
            {
                extraLifePanel.SetActive(true);
            }
        }
    }
    public void openLevelThree()
    {
        if (playButtons[2].GetComponent<Button>().interactable)
        {
            sourse.Play();
            if (Settings.game.lives > 0)
            {
                Settings.currentLevel = 2;
                SceneManager.LoadScene(6);
            }
            else
            {
                extraLifePanel.SetActive(true);
            }
        }
    }
    public void openLevelFour()
    {
        if (playButtons[3].GetComponent<Button>().interactable)
        {
            sourse.Play();
            if (Settings.game.lives > 0)
            {
                Settings.currentLevel = 3;
                SceneManager.LoadScene(7);
            }
            else
            {
                extraLifePanel.SetActive(true);
            }
        }
    }
    public void openLevelFive()
    {
        if (playButtons[4].GetComponent<Button>().interactable)
        {
            sourse.Play();
            if (Settings.game.lives > 0)
            {
                Settings.currentLevel = 4;
                SceneManager.LoadScene(8);
            }
            else
            {
                extraLifePanel.SetActive(true);
            }
        }
    }
    public void openLevelSix()
    {
        if (playButtons[5].GetComponent<Button>().interactable)
        {
            sourse.Play();
            Settings.currentLevel = 5;
            SceneManager.LoadScene(9);
        }
    }

    void showLives(int number)
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
    }

    public void extraLifesCancel()
    {
        sourse.Play();
        extraLifePanel.SetActive(false);
    }

    public void openBuyPanel()
    {
        sourse.Play();
        shadePanel.SetActive(true);
        buyPanel.SetActive(true);
    }

    public void closeBuyPanel()
    {
        sourse.Play();
        shadePanel.SetActive(false);
        buyPanel.SetActive(false);
    }

    public void openHowToPlayScreen()
    {
        sourse.Play();
        SceneManager.LoadScene(11);
    }

    void showExtraTime()
    {
        int one = 0;
        int two = 0;

        DateTime timeToShow = Settings.game.timerTime.AddMinutes(5);
        TimeSpan elapsedSpan = new TimeSpan(timeToShow.Ticks - DateTime.Now.Ticks);
        extraClockMin.GetComponent<Image>().sprite = clockNumbers[elapsedSpan.Minutes];

        one = elapsedSpan.Seconds / 10;
        two = elapsedSpan.Seconds % 10;
        extraClockSecOne.GetComponent<Image>().sprite = clockNumbers[one];
        extraClockSecTwo.GetComponent<Image>().sprite = clockNumbers[two];
    }

    void showExtraLives(int number)
    {
        int one = 0;
        int two = 0;

        two = number % 10;
        extraLivesNumOne.GetComponent<Image>().sprite = empty;
        extraLivesNumTwo.GetComponent<Image>().sprite = liveNumbers[two];

        if (number > 9)
        {
            one = number / 10;
            extraLivesNumOne.GetComponent<Image>().sprite = liveNumbers[one];
        }
    }

}
