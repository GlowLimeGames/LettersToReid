using UnityEngine;
using System.Collections;

public class CharacterCollisonHandler : MonoBehaviour{
	public MyCharacterController charactor;
	public MemoryBehvior memory;
	public bool memoryRead = false;
	public int  enemyHits = 0;
	public const int maxHit = 9;



	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.collider.tag == "ground") {
			charactor.isGrounded = true;
		} 
		else if (col.gameObject.tag == "ladder") {
			charactor.canClimb = true;
			Debug.Log ("Enter");
		} 
		else if (col.gameObject.tag == "enemy") {
			
			transform.position = charactor.memoryLocations [enemyHits];

			if (enemyHits < maxHit) {
				enemyHits++;
			} else if (enemyHits >= maxHit) {
				enemyHits = 0;
			}
		} 
		else if (col.gameObject.tag == "memory") {
			if (!memoryRead) {
				memory.showMemory ();
				memoryRead = true;
				memory.closeMemory ();
			} 
			else {
				memory.showMemory ();
			}
		}
	}

	void OnCollisionExit2D(Collision2D col2)
	{
		if (col2.gameObject.tag == "ladder")
		{
			charactor.canClimb = false;
			Debug.Log("Exit");
		}
	}
		
}