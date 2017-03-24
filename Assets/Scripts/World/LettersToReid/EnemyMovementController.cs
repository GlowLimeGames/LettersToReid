using UnityEngine;
using System.Collections;

public class EnemyMovementController : MonoBehaviour {
    bool isGoingRight = true;
    float speed = 0.05f;
    float direction = 1;
    bool canClimb;
    public GameObject character;
    
    float timePassed = 5;
    float timeToAct = 5;

    Vector3[] memoryLocations = { new Vector3(5.76f, 0.26f, 0), new Vector3(16.21f, 2.37f, 0),
    new Vector3(7.1f, 2.43f, 0), new Vector3(-6.06f, 2.43f, 0), new Vector3(-10.66f, 4.55f, 0),
    new Vector3(-16.82f, 6.74f, 0), new Vector3(3.77f, 6.69f, 0), new Vector3(11.13f, 4.56f, 0),
    new Vector3(13.46f, 8.91f, 0), new Vector3(15.87f, 11.08f, 0)};

	// Use this for initialization
	void Start () {

	}
	
    void OnCollisionEnter2D (Collision2D coll)
    {
		if (coll.gameObject.tag == "wall") {
			if (isGoingRight) {
				direction = -1f;
				isGoingRight = false;
			} else {
				direction = 1;
				isGoingRight = true;
			}
		}
        //moves enemy and character to new locations after collision
        else if (coll.gameObject.tag == "Player") {
			int i = Random.Range (0, 10);
			transform.position = memoryLocations [i];

		} 
		else if (coll.gameObject.tag == "ladder") {
			canClimb = true;
			transform.Translate (0, speed, 0);

			if (coll.gameObject.tag == "wall") {
				transform.Translate (0, -speed, 0);
			}

		}

        else if( coll.gameObject.tag == "letter")
        {
            Physics2D.IgnoreCollision(coll.collider, GetComponent<Collider2D>(), true);
        }
    }

	void OnCollisionExit2D(Collision2D col2)
	{
		if (col2.gameObject.tag == "ladder")
		{
			canClimb = false;
			Debug.Log("Exit");
		}
	}

	void Update () {

		if (Time.time >= timeToAct)
		{
			transform.position = new Vector3(character.transform.position.x - 3f, character.transform.position.y, 0);
			timeToAct += timePassed + 2;
		}
		else
		{
			transform.Translate(direction * speed, 0, 0);
			if (isGoingRight)
			{
				GetComponent<SpriteRenderer>().flipX = true;
			}
			else
			{
				GetComponent<SpriteRenderer>().flipX = false;
			}
		}
		

}

