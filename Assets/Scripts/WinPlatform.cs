using UnityEngine;

public class WinPlatform : MonoBehaviour {

    GameObject jumper;
	// Use this for initialization
	void Start () {
        jumper = GameObject.Find("jumper");
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < jumper.transform.position.y+0.5f) gameObject.GetComponent<BoxCollider2D>().isTrigger = false;       
        if (transform.position.y > jumper.transform.position.y+1.5f) gameObject.GetComponent<BoxCollider2D>().isTrigger = true;       
	}
}
