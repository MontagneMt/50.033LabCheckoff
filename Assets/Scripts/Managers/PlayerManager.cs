using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public UnityEvent killed;

    public void MarioKilled() {
        killed.Invoke();
    }
}
