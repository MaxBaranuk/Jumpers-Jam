using UnityEngine;

public class Platform : MonoBehaviour {

    public int direction;
    float platformSpeed;
    GameObject jumper;
    GameObject cam;
	// Use this for initialization
	void Start () {
        platformSpeed = Random.Range(0.005f, 0.03f);
        cam = GameObject.FindGameObjectWithTag("MainCamera");
       
	}
	
	// Update is called once per frame
	void Update () {
        jumper = cam.GetComponent<GameManager>().jumper;
        if (Time.timeScale > 0)
        {
            if (transform.position.y < jumper.transform.position.y - 1.1f) gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            
            if (transform.position.y > jumper.transform.position.y) gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            
            transform.position = new Vector3(transform.position.x + direction * platformSpeed, transform.position.y, -0.1f);

            if (transform.position.x > 4.3f) direction = -1;

            if (transform.position.x < -4.3f) direction = 1;
           
            if ((jumper.transform.position.y - transform.position.y) > 17)
            {
                cam.GetComponent<GameManager>().setLastTransformPosition(transform.position.y - 5f);
                if (Settings.isStart && Settings.isInGame) Settings.points += 2;
                Destroy(gameObject);
            }
        }
	}

}
