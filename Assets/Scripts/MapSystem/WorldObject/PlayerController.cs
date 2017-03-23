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
    const string HOR = "Horizontal";
    const string VERT = "Vertical";

    float speed
    {
        get
        {
            return tuning.CharacterMoveSpeed;
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

    public void Setup(MapController map)
    {
        this.map = map;
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

    void Start()
    {
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
        // Clamps velocity to max player speed:
        rigibody.velocity = new Vector2(getClampedPlayerSpeed(), rigibody.velocity.y);
    }
        
    void OnTriggerEnter2D(Collider2D collider)
    {
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
        MapObjectBehaviour obj;
        if(checkForMapObjCollideEvent(collider, out obj))
        {
            if(obj is MapTileBehaviour)
            {
                handleExitCollideWithTile(obj as MapTileBehaviour);
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
        map.HandlePortalEnter(player, obj);
    }

    Vector2 getMoveVector()
    {
		float horMove = Input.GetAxis(HOR) * speed;
        float vertMove = Input.GetAxis(VERT);
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
        return vertMove * speed;
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
