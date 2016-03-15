using UnityEngine;

public class MainCamera : MonoBehaviour {

    GameObject jumper;
	// Use this for initialization
	void Start () {

        jumper = GetComponent<GameManager>().jumper;
	}
	
	// Update is called once per frame
	void Update () {

        if (Settings.isInGame)
        {
            transform.position = new Vector3(0, jumper.transform.position.y + 6.88f, transform.position.z);
        }
	}
}
