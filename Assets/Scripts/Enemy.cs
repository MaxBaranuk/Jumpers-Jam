using UnityEngine;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if (otherCollider.gameObject.tag == "Jumper")
        {
            Settings.isInGame = false;
            otherCollider.gameObject.GetComponent<Rigidbody2D>().angularDrag = 0;
            otherCollider.gameObject.GetComponentInChildren<BoxCollider2D>().isTrigger = true;
            otherCollider.gameObject.GetComponentInChildren<CircleCollider2D>().isTrigger = true;
            otherCollider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5, 5), 10.8f), ForceMode2D.Impulse);
        }
    }
}
