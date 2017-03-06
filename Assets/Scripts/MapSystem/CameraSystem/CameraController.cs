/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MController 
{
    #region Static Accessors

    public static CameraController Get
    {
        get 
        {
            return _mostRecentInstance;
        }
    }

    #endregion

    static CameraController _mostRecentInstance;

    #region Instance Accessors

    public bool HasFocus
    {
        get 
        {
            return currentFocus != null;
        }
    }

    public bool FocusLocked
    {
        get;
        private set;
    }

    public bool HasBackground
    {
        get
        {
            return backgroundImage != null;
        }
    }

    #endregion

    Vector3 cameraWindow
    {
        get
        {
            float size = activeCamera.orthographicSize * 2f;
            return new Vector3(size, size);
        }
    }

    [SerializeField]
    Transform currentFocus;
    SpriteRenderer backgroundImage;
    Camera activeCamera;

	// Use this for initialization
	protected override void Awake ()
    {
        base.Awake ();
        _mostRecentInstance = this;
        activeCamera = GetComponent<Camera>();
	}

    void Update()
    {
        if(HasFocus)
        {
            trackFocus();
        }
    }

    void trackFocus()
    {
        if(HasFocus)
        {
            Vector3 newPos = currentFocus.position;
            // Don't adjust the z-depth (because it's a 2D system):
            newPos.z = transform.position.z;
            transform.position = newPos;
        }
    }

    public bool RequestFocus(Transform obj)
    {
        if(FocusLocked)   
        {
            return false;
        }
        else 
        {
            currentFocus = obj;
            return true;
        }
    }

    public void LockFocus(Transform focus)
    {
        if(focus == this.currentFocus)
        {
            FocusLocked = true;
        }
    }

    public void ReleaseFocus(Transform focus)
    {
        if(focus == this.currentFocus)
        {
            FocusLocked = false;
        }
    }

    public void SetBackground(Sprite sprite)
    {
        if(!backgroundImage)
        {
            backgroundImage = createBackground();
        }
        backgroundImage.sprite = sprite;
    }

    SpriteRenderer createBackground()
    {
        GameObject backgroundObj = new GameObject();
        Transform backgroundTrans = backgroundObj.transform;
        backgroundTrans.SetParent(transform);
        backgroundTrans.localScale = cameraWindow;
        return backgroundObj.AddComponent<SpriteRenderer>();
    }

}
