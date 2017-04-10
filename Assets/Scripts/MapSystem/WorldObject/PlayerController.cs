/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using k = MapGlobal;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MController 
{
	

    [SerializeField]
    KeyCode enterDoorway = KeyCode.W;
    KeyCode enterDoorwayAlt = KeyCode.UpArrow;

    int collisionCount;

	bool walking;

    const string HOR = "Horizontal";
    const string VERT = "Vertical";
	int climbDelay = 5;
	public float climbTimer = 0;
	int walkDelay = 5;
	public float walkTimer = 0;

    float speed
    {
        get
        {
            return tuning.CharacterMoveSpeed;
        }
    }

    float climbSpeed
    {
        get
        {
            return tuning.PlayerClimbSpeed;
        }
    }

    float jump
    {
        get
        {
            return tuning.CharacterJumpSpeed;
        }
    }

    bool canJump
    {
        get
        {
            return rigibody.IsTouchingLayers() && rigibody.velocity.y <= 0;
        }
    }
        
    bool isClimbing
    {
        get
        {
            return currentClimbingTarget;
        }
    }

    [SerializeField]
    float gravityScale = 2;

    [SerializeField]
    PlayerState defaultState = PlayerState.WalkRight;

    Rigidbody2D rigibody;
    MapController map;
    CameraController cam;
	Animator anim;

    MapTileBehaviour currentClimbingTarget;
    MapUnitBehaviour player;
	PlayerState currentState = PlayerState.Idle;
    LTRTuning gameTuning;

    MapObjectBehaviour collidingPortal;

    public void Setup(MapController map)
    {
        this.map = map;
        map.SetActivePlayer(this);
    }

	// Use this for initialization
	protected override void Awake()
    {
        base.Awake ();
        rigibody = GetComponent<Rigidbody2D>();	
        rigibody.freezeRotation = true;
        player = GetComponent<MapUnitBehaviour>();
		anim = GetComponent<Animator>();
        this.currentState = defaultState;
        gameTuning = LTRTuning.Get;
	}

    protected override void Start()
    {
        base.Start();
        tuning = MapTuning.Get;
        rigibody.gravityScale = tuning.PlayerGravityScale;
        rigibody.drag = tuning.PlayerGravityScale;
        cam = CameraController.Get;
        if(cam.RequestFocus(transform))
        {
            cam.LockFocus(transform);
        }
    }

    void Update()
    {
        rigibody.gravityScale = isClimbing ? 0 : gravityScale;
        rigibody.AddForce(getMoveVector());
        if(movementKeyPressed())
        {
            // Clamps velocity to max player speed:
            rigibody.velocity = new Vector2(getClampedPlayerSpeed(), rigibody.velocity.y);
        }
        else if(collisionCount > 0)
        {
            rigibody.velocity = Vector2.zero;
        }

        if((Input.GetKeyDown(enterDoorway) || Input.GetKeyDown(enterDoorwayAlt)) && collidingPortal)
        {
            travel.CompleteTravel();
            map.HandlePortalEnter(player, collidingPortal);
        }

        if(currentState == PlayerState.Climb)
        {
			
            if (climbTimer >= climbDelay) 
            {
				

                climbTimer = 0;

            }
        }
        else if (currentState == PlayerState.WalkLeft || currentState == PlayerState.WalkRight)
        {
			

			if (walkTimer >= walkDelay) 
            {
				
                walkTimer = 0;
            }
        }

		onWalkKeyPressed ();
		onWalkKeyReleased ();
		StartCoroutine( stepBreaks ());

        // Clamps velocity to max player speed:
    }
        
    bool movementKeyPressed()
    {
        return Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.D) ||
                Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.UpArrow) ||
                Input.GetKey(KeyCode.LeftArrow) ||
                Input.GetKey(KeyCode.RightArrow) ||
                Input.GetKey(KeyCode.DownArrow);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        collisionCount++;
        MapObjectBehaviour obj;
        if(checkForMapObjCollideEvent(collider, out obj))
        {
            if(checkForPortalCollideEvent(obj))
            {
                handlePortalCollider(obj);
            }
            if(obj is MapTileBehaviour)
            {
                handleEnterCollideWithTile(obj as MapTileBehaviour);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        collisionCount--;
        MapObjectBehaviour obj;
        if(checkForMapObjCollideEvent(collider, out obj))
        {
            if(obj is MapTileBehaviour)
            {
                handleExitCollideWithTile(obj as MapTileBehaviour);
            }
            if(obj.Descriptor.IsPortal)
            {
                travel.CompleteTravel();
                handlePortalExit(obj);
            }
        }
    }

    float getClampedPlayerSpeed() 
    {
        return Mathf.Clamp(rigibody.velocity.x, -gameTuning.MaxPlayerSpeed, gameTuning.MaxPlayerSpeed);
    }

    bool checkForMapObjCollideEvent(Collider2D collider, out MapObjectBehaviour obj)
    {
        if(obj = collider.GetComponent<MapObjectBehaviour>())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool checkForPortalCollideEvent(MapObjectBehaviour obj)
    {
        return obj.Descriptor.IsPortal;
    }

    void handleEnterCollideWithTile(MapTileBehaviour tile)
    {
        if(tile.GetTile.IsClimbable)
        {
            handleEnterClimbableTile(tile);
        }
    }

    void handleExitCollideWithTile(MapTileBehaviour tile)
    {
        if(tile.GetTile.IsClimbable)
        {
            handleExitClimbableTile(tile);
        }
    }

    void handleEnterClimbableTile(MapTileBehaviour tile)
    {
        currentClimbingTarget = tile;
    }

    void handleExitClimbableTile(MapTileBehaviour tile)
    {
        if(currentClimbingTarget == tile)
        {
            currentClimbingTarget = null;
        }
    }
        
    void handlePortalCollider(MapObjectBehaviour obj)
    {

        if(obj.name.Contains(MapGlobal.MAP_KEY))
        {
            map.HandlePortalEnter(player, obj);
        }
        else
        {
            collidingPortal = obj;
        }
    }

    void handlePortalExit(MapObjectBehaviour obj)
    {
        if(collidingPortal == obj)
        {
            collidingPortal = null;
        }

        map.HandlePortalEnter(player, obj);
		EventController.Event ("sx_wooden_door_open_01");


    }

    Vector2 getMoveVector()
    {
        float horMove;
        float vertMove = Input.GetAxis(VERT);
        horMove = Input.GetAxis(HOR) * speed;
        if(isClimbing)
        {
            vertMove = getClimbingVertVelocity(vertMove);
			updatePlayerState(PlayerState.Climb);
        }
        else 
        {
            if(gameTuning.JumpEnabled)
            {
                vertMove = getJumpingVertVelocity(vertMove);
            }
            else
            {
                vertMove = 0;
            }
			updatePlayerWalkingState(horMove);
        }
		return new Vector2(horMove, vertMove);
    }
        
	void updatePlayerWalkingState(float horMove)
	{

		if(horMove > 0)
		{
			
			updatePlayerState(PlayerState.WalkRight);
		}
		else if (horMove < 0)
		{
			 
			updatePlayerState(PlayerState.WalkLeft);
		}
	}

	void onWalkKeyPressed() {
		if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.D)) {
			walking = true;
		}
			
	}

	void onWalkKeyReleased () {
		if (Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.D)) {
			walking = false;
		}
	}

	IEnumerator stepBreaks() {

		while (walking == true) {
			EventController.Event ("play_footsteps"); 
			yield return new WaitForSeconds (2);
		}

	}

    float getClimbingVertVelocity(float vertMove)
    {
        return vertMove * climbSpeed;
    }

    float getJumpingVertVelocity(float vertMove)
    {
        if(canJump && vertMove > 0)
        {
            vertMove = jump;
        }
        else
        {
            vertMove = 0;
        }
        return vertMove;
    }
		
	void updatePlayerState(PlayerState state)
	{
		this.currentState = state;
		if(anim)
		{
			updatePlayerAnim(this.currentState);
		}
	}

	void updatePlayerAnim(PlayerState state)
	{
		anim.SetBool(k.BACK_KEY, isClimbing);
		anim.SetBool(k.LEFT_KEY, state == PlayerState.WalkLeft);
		anim.SetBool(k.RIGHT_KEY, state == PlayerState.WalkRight);
	}

}

enum PlayerState
{
	Idle,
	WalkLeft,
	WalkRight,
	WalkBack,
	Climb,
}
