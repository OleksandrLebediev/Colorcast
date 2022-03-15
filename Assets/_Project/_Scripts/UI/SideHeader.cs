using UnityEngine;

public class SideHeader : MonoBehaviour, IUICmponent
{
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
