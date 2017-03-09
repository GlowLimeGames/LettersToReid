/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

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

}
