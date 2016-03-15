using UnityEngine;
using System.Collections;
using GooglePlayGames;

public class Jumper : MonoBehaviour {

    Rigidbody2D rig;
    GameObject cam;
    
	// Use this for initialization
	void Start () {
        rig = gameObject.transform.parent.GetComponent<Rigidbody2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        Settings.isInGame = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (Settings.isInGame)
        {
            if (Settings.isStart & Time.timeScale > 0)
            {
                   if ((Input.GetTouch(0).position.x - Screen.width / 2) > 0) 
                    transform.parent.position = new Vector3(transform.parent.position.x + 0.1f, transform.parent.position.y, -1f);
   
                if ((Input.GetTouch(0).position.x - Screen.width / 2) < 0)
                    transform.parent.position = new Vector3(transform.parent.position.x - 0.1f, transform.parent.position.y, -1f);
                               
            }
                if (transform.parent.position.x < -6.65f)
                    transform.parent.position = new Vector3(6.65f, transform.position.y, -1f);
                
                if (transform.parent.position.x > 6.65f)              
                    transform.parent.position = new Vector3(-6.65f, transform.position.y, -1f);
                
        }
        else {
            transform.parent.Rotate(0, 0, 200f * Time.deltaTime);
        }              
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Platform" & Settings.isStart)     
            rig.AddForce(new Vector2(0, 20f), ForceMode2D.Impulse);
                  
        if (coll.gameObject.tag == "WinPlatform" & Settings.isStart)
        {
            float perc = Settings.collectedCoins / Settings.totalCoins;
            int stars;
            if (perc < 0.4f) stars = 1;        
            else if (perc > 0.5f) stars = 3;                
                else stars = 2;

            if (Settings.currentLevel < 5) {
                Settings.game.levels[Settings.currentLevel + 1].isOpen = true;
                PlayGamesPlatform.Instance.IncrementAchievement(
                Settings.achievementCodes[Settings.currentLevel], 5, (bool success) =>
        {
            // handle success or failure
        });
            } 
            
            Settings.game.lastScore = Settings.points;
            SaveLoad.save(Settings.currentLevel, Settings.points, stars);
            cam.GetComponent<GUIGame>().openWinDialog();
        }
        
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "Coin") {

            pointsEnter(other.gameObject);
            Settings.collectedCoins++;
            
        }

        if (other.gameObject.tag == "Heart")
        {
            Destroy(other.gameObject);
            Settings.game.lives++;

        }
    }

    void pointsEnter(GameObject obj) {
        StartCoroutine(pointsEnterEnum(obj));
    }

    IEnumerator pointsEnterEnum(GameObject obj)
    {
        Settings.points += 20;
        Nextpeer.ReportScoreForCurrentTournament((uint)Settings.points);
        obj.GetComponent<Animator>().SetTrigger("enter");
        yield return new WaitForSeconds(0.7f);
        Destroy(obj);
    }
}
