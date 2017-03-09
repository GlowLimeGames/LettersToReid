/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using UnityEngine;

public abstract class LTRUITemplate : MonoBehaviourExtended 
{	
    protected UIInterchange interchange;
    protected bool isVisible
    {
        get
        {
            return gameObject.activeInHierarchy;
        }
    }

    public virtual void Show()
    {
        ToggleVisible(true);
    }

    public virtual void Hide()
    {
        ToggleVisible(false);
    }

    public void ToggleVisible(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }

	protected override void fetchReferences()
    {
        base.fetchReferences();
        this.interchange = UIInterchange.Instance;
        interchange.RegisterUI(GetType(), this);
    }

    protected void toggleCanvasGroup(CanvasGroup canvas, bool isEnabled)
    {
        canvas.alpha = isEnabled ? 1 : 0;
        canvas.interactable = isEnabled;
        canvas.blocksRaycasts = isEnabled;
    }
        
}
