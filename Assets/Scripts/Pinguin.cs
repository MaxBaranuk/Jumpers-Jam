using UnityEngine;
using System.Collections;

public class Pinguin : MonoBehaviour
{

    public GameObject ball;
    public Animator throwBallAnim;
    public int direction;
	// Use this for initialization
	void Start () {
        InvokeRepeating("throwBall", 2, 4);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void throwBall() {
        StartCoroutine(throwEnum());

    }

    IEnumerator throwEnum() {
        throwBallAnim.SetTrigger("Throw");
        yield return new WaitForSeconds(0.5f);
        throwBallAnim.SetTrigger("stay");
        GameObject b;
        if (direction == 1)
        {
            b = (GameObject)Instantiate(ball, new Vector3(transform.parent.position.x + 2.575f, transform.parent.position.y + 0.847f, transform.parent.position.z), Quaternion.identity);
        }
        else {
            b = (GameObject)Instantiate(ball, new Vector3(transform.parent.position.x - 1.13f, transform.parent.position.y + 0.847f, transform.parent.position.z), Quaternion.identity);
        }
        
        b.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction, 0), ForceMode2D.Impulse);
        
    }
}
