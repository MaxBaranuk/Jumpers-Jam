using UnityEngine;

public class Block : MonoBehaviour {

    public int numOfPlatforms;
    public GameObject emptyPlatform;
    public GameObject platformCoin;
    public GameObject enemyPlatform;
    public GameObject enemyPlatform2;
    public GameObject winPlatform;
    public GameObject heart;
    public int blockType;

	// Use this for initialization
	void Start () {
        switch (blockType) {      
            case 0:
                break;
            case 1:
                Instantiate(winPlatform, new Vector3(0, transform.position.y - 4f, 0), Quaternion.identity);
                break;
            case 2:
                createFullBlock();
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
	}

    void createFullBlock() {
        GameObject[] platforms = new GameObject[numOfPlatforms + 1];
        if (Random.value>0.5 & transform.position.y > 22f)
        {
            GameObject one = (GameObject)Instantiate(enemyPlatform, new Vector3(Random.Range(-4.5f, 4.5f),
                                  Random.Range(transform.position.y - gameObject.GetComponent<Renderer>().bounds.size.y / 2,
                                               transform.position.y + gameObject.GetComponent<Renderer>().bounds.size.y / 2),
                                               -1.2f), Quaternion.identity);
            platforms[0] = one;
        }
        else
        {
            if (transform.position.y > 22f)
            {
                GameObject two = (GameObject)Instantiate(enemyPlatform2, new Vector3(Random.Range(-4.5f, 4.5f),
                                      Random.Range(transform.position.y - gameObject.GetComponent<Renderer>().bounds.size.y / 2,
                                                   transform.position.y + gameObject.GetComponent<Renderer>().bounds.size.y / 2),
                                                   -1.2f), Quaternion.identity);
                platforms[0] = two;
            }
            else
            {
                platforms[0] = (GameObject)Instantiate(emptyPlatform, new Vector3(Random.Range(-4.5f, 4.5f),
                                      Random.Range(transform.position.y - gameObject.GetComponent<Renderer>().bounds.size.y / 2,
                                                   transform.position.y + gameObject.GetComponent<Renderer>().bounds.size.y / 2),
                                                   -1f), Quaternion.identity);
            }
        }

        if (Random.value < 0.12f) {
            Instantiate(heart, new Vector3(Random.Range(-4.5f, 4.5f),
                                      Random.Range(transform.position.y - gameObject.GetComponent<Renderer>().bounds.size.y / 2,
                                                   transform.position.y + gameObject.GetComponent<Renderer>().bounds.size.y / 2),
                                                   -1.2f), Quaternion.identity);
        }

        for (int i = 1; i < numOfPlatforms + 1; i++)
        {
            float height = gameObject.GetComponent<Renderer>().bounds.size.y;
            float platformType = Random.Range(0f, 1f);
            GameObject obj;
            if (platformType < 0.6f)
            {
                obj = (GameObject)Instantiate(emptyPlatform, new Vector3(Random.Range(-4.5f, 4.5f),
                                  transform.position.y - height / 2 + i * height / numOfPlatforms,
                                                   -1), Quaternion.identity);
            }
            else
            {
                obj = (GameObject)Instantiate(platformCoin, new Vector3(Random.Range(-4.5f, 4.5f),
                                  transform.position.y - height / 2 + i * height / numOfPlatforms, -1), Quaternion.identity);
                Settings.totalCoins++;
            }
            platforms[i] = obj;
        }
        foreach (GameObject obj in platforms)
        {
            obj.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}
