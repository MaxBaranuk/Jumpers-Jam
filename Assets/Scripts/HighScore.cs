using UnityEngine;

public class HighScore : MonoBehaviour {

    public GameObject info;
	// Use this for initialization
	void Start () {
        SaveLoad.load();

	}


	void Update () {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel(1);

        }
	}
}
