using UnityEngine;

public class Bird : MonoBehaviour {

    int direction;
    float gravity;
    float speed = 1f;


	void Start () {

        if (Random.value > 0.5) direction = 1;
        else direction = -1;
        
        if (Random.value > 0.5) gravity = 0.02f;
        else gravity = -0.02f;
        
        transform.localScale = new Vector3(-direction, 1f, 1f);
        transform.position = new Vector3(8f * direction, transform.position.y, transform.position.z);
        gameObject.GetComponent<Rigidbody2D>().gravityScale = gravity;
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(transform.position.x - speed * direction * Time.deltaTime, transform.position.y, transform.position.z);
        
        if (transform.position.x < -9 | transform.position.x > 9) {
            direction = -direction;
            transform.localScale = new Vector3(-direction, 1f, 1f);
        }
        
	}
}
