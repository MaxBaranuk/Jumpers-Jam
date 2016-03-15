using UnityEngine;
using System.Collections;

public class MainMusic : MonoBehaviour {

    public static AudioSource source;
    public static bool AudioBegin;
	// Use this for initialization
    void Awake()
    {
        Debug.Log("awake" + AudioBegin);
        if (!AudioBegin)
        {
            source = GetComponent<AudioSource>();
//            source.Play();
            DontDestroyOnLoad(gameObject);
            AudioBegin = true;
        }
    }
}
