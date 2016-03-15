using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectJumper : MonoBehaviour {

    bool moveLeft;
    bool moveRight;
    int currentJumper;
    float currentJumpPos = 0;
    public GameObject[] jumpers;
    AudioSource sourse;
    // Use this for initialization
    void Start () {
        sourse = GetComponent<AudioSource>();
        currentJumper = Settings.currentJumper;
        for (int i = 0; i < jumpers.Length; i++)
        {
            jumpers[i].transform.position = new Vector3(12.42f * i - 12.42f * currentJumper, 0, 0);
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Settings.gametype == Settings.GameType.Tournament) Nextpeer.ReportForfeitForCurrentTournament();
            SceneManager.LoadScene(1);
        }

        if (moveLeft) {
            foreach (GameObject jum in jumpers) {
                jum.transform.position = new Vector3(jum.transform.position.x - 100f*Time.deltaTime, 0, 0);
            }
            currentJumpPos += 100f*Time.deltaTime;
            if (currentJumpPos >= 12.42) {
                moveLeft = false;
                currentJumpPos = 0;
                currentJumper++;
                for (int i = 0; i<jumpers.Length; i++)
                {
                    jumpers[i].transform.position = new Vector3(12.42f * i - 12.42f * currentJumper, 0, 0);
                }
            }
        }
        if (moveRight) {
            foreach (GameObject jum in jumpers)
            {
                jum.transform.position = new Vector3(jum.transform.position.x + 100f * Time.deltaTime, 0, 0);
            }
            currentJumpPos += 100f * Time.deltaTime;
            if (currentJumpPos >= 12.42)
            {
                moveRight = false;
                currentJumpPos = 0;
                currentJumper--;
                for (int i = 0; i < jumpers.Length; i++)
                {
                    jumpers[i].transform.position = new Vector3(12.42f * i - 12.42f * currentJumper, 0, 0);
                }
            }
        }
	}

    public void previous() {
        sourse.Play();
        if (currentJumper > 0) moveRight = true;
    }

    public void next() {
        sourse.Play();
        if (currentJumper < 8) moveLeft = true;
    }

    public void selectOk() {
        sourse.Play();
        Settings.currentJumper = currentJumper;
        if (Settings.gametype == Settings.GameType.Adventure)
        {
            SceneManager.LoadScene(3);
        }
        else {
            SceneManager.LoadScene(10);
        }


    }

    public void selectCancel() {
        sourse.Play();
        SceneManager.LoadScene(1);
    }
}
