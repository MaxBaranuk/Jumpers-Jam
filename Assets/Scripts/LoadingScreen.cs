using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SaveLoad.load();
        playGamesInit();
        StartCoroutine(loadMenuScreen());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator loadMenuScreen()
    {
        SceneManager.LoadSceneAsync(1);
        yield return 1f;
    }

    void playGamesInit()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
    .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();

    }
}
