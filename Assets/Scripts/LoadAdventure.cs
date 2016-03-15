using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadAdventure : MonoBehaviour {

	// Use this for initialization
	void Start () {

        StartCoroutine(loadAdventureScreen());
    }

    IEnumerator loadAdventureScreen()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(2);
        
    }   // Update is called once per frame
    void Update () {
//	if(Settings.gametype == Settings.GameType.NoType) SceneManager.LoadSceneAsync(1);
    }
}
