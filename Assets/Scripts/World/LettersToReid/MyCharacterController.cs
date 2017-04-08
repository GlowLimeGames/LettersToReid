using UnityEngine;
using System.Collections;

public class MyCharacterController : MonoBehaviour {

	//script for controlling character movement

	public float moveSpeed = 10;
	public float oldY;
	public float climbSpeed = .1f;
	public bool isGrounded;
	public bool canClimb = false;
	public int  enemyHits = 0;

	Vector3[] memoryLocations = { new Vector3(5.76f, 0.26f, 0), new Vector3(16.21f, 2.37f, 0),
		new Vector3(7.1f, 2.43f, 0), new Vector3(-6.06f, 2.43f, 0), new Vector3(-10.66f, 4.55f, 0),
		new Vector3(-16.82f, 6.74f, 0), new Vector3(3.77f, 6.69f, 0), new Vector3(11.13f, 4.56f, 0),
		new Vector3(13.46f, 8.91f, 0), new Vector3(15.87f, 11.08f, 0)};

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().enabled = true;

	}

	// Update is called once per frame
	void Update () {
		//move left
		if (transform.position.y <= oldY + 0.1f && !isGrounded)
		{
			isGrounded = true;
		}

		if (Input.GetKey(KeyCode.A))
		{
			GetComponent<SpriteRenderer>().flipX = true;

			transform.Translate(-moveSpeed, 0, 0);

		}

		//move right
		if (Input.GetKey(KeyCode.D))
		{
			GetComponent<SpriteRenderer>().flipX = false;

			transform.Translate(moveSpeed, 0, 0);

		}
		// for climbing ladders
		if (canClimb)
		{
			if (Input.GetKey(KeyCode.W))
			{
				GetComponent<Rigidbody2D>().gravityScale = 0.0f;
				transform.Translate(0, climbSpeed, 0);

			}

			if (Input.GetKey(KeyCode.S))
			{
				GetComponent<Rigidbody2D>().gravityScale = 0.0f;
				transform.Translate(0, -climbSpeed, 0);
			}

		}
		else
		{
			GetComponent<Rigidbody2D>().gravityScale = 1f;
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.collider.tag == "ground")
		{
			isGrounded = true;
		}

		else if (col.gameObject.tag == "ladder")
		{
			canClimb = true;
			Debug.Log("Enter");
		}
		else if (col.gameObject.tag == "enemy")
		{
			transform.position = memoryLocations[enemyHits];
			if(enemyHits < 9 )
			{
				enemyHits++;
			}
			else if(enemyHits >= 9)
			{
				enemyHits = 0;
			}
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
}