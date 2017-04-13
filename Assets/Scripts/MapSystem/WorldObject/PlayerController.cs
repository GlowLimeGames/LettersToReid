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
    UIInterchange ui;

    [SerializeField]
    KeyCode enterDoorway = KeyCode.W;
    [SerializeField]
    KeyCode enterDoorwayAlt = KeyCode.UpArrow;

    [SerializeField]
    KeyCode openMemoryKey = KeyCode.Space;

    int collisionCount;

    const string HOR = "Horizontal";
    const string VERT = "Vertical";
	int climbDelay = 5;
	public float climbTimer = 0;
	int walkDelay = 5;
	public float walkTimer = 0;

    bool memoryOpen;
    bool inMemoryCollider;

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
    MemoryBehvior targetMemory;

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
        this.ui = UIInterchange.Instance;
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
            CompleteTravel();
            map.HandlePortalEnter(player, collidingPortal);
        }
        if(Input.GetKeyDown(openMemoryKey))
        {
            tryInteractWithMemory(canClose:true);
        }
        if(currentState == PlayerState.Climb)
        {
            if (climbTimer >= climbDelay) 
            {

                EventController.Event ("play_ladder_climb");

                climbTimer = 0;

            }
        }
        else if (currentState == PlayerState.WalkLeft || currentState == PlayerState.WalkRight)
        {
            if (walkTimer >= walkDelay) 
            {

                EventController.Event ("play_footsteps"); 

                walkTimer = 0;
            }
        }
    }
        
    void OnMouseUp()
    {
        tryInteractWithMemory(canClose:false);
    }

    bool tryInteractWithMemory(bool canClose)
    {
        if(targetMemory)
        {
            if(canClose)
            {
                memoryOpen = ui.ToggleMemoryDisplay(targetMemory.Get);
                // If memory is closed and we're outside it's collider, set it to null:
                if(!(memoryOpen || inMemoryCollider))
                {
                    targetMemory = null;
                }
            }
            else
            {
                ui.DisplayMemory(targetMemory.Get);
                memoryOpen = true;
            }
            if(memoryOpen)
            {
                targetMemory.Collect();
            }
            return true;
        }
        else
        {
            return false;
        }
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

    public void CompleteTravel()
    {
        travel.CompleteTravel();
    }

    public void OnTriggerEnter2D(Collider2D collider)
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
        MemoryBehvior mem = collider.GetComponent<MemoryBehvior>();
        if(mem)
        {
            handleEnterCollidedWithMemory(mem);
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
                CompleteTravel();
                handlePortalExit(obj);
            }
        }
        MemoryBehvior mem = collider.GetComponent<MemoryBehvior>();
        if(mem)
        {
            handleExitCollideWithMemory(mem);
        }
    }

    void handleEnterCollidedWithMemory(MemoryBehvior mem)
    {
        this.targetMemory = mem;
        inMemoryCollider = true;
    }

    void handleExitCollideWithMemory(MemoryBehvior mem)
    {
        if(this.targetMemory == mem && !memoryOpen)
        {
            this.targetMemory = null;
        }
        inMemoryCollider = false;
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
