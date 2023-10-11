using UnityEngine;

public class ButtonManager : MonoBehaviour, IInteractiveButton
{
    public void ButtonClick()
    {
        GameManager.instance.GameRestart();
    }
}
