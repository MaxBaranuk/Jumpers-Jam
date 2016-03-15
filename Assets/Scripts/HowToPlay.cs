using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour {

    public GameObject firstPanel;
    public GameObject secondPanel;
    int clicks = 0;
	// Use this for initialization
	void Start () {
        firstPanel.SetActive(true);
        secondPanel.SetActive(false);
    }
	
    public void click() {
        switch (clicks) {
            case 0:
                firstPanel.SetActive(false);
                secondPanel.SetActive(true);
                clicks++;
                break;
            case 1:
                if(Settings.gametype == Settings.GameType.Adventure) SceneManager.LoadScene(3);
                else SceneManager.LoadScene(10);
                break;  
        }
    }
}
