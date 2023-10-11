using UnityEngine;
using UnityEngine.Events;

public class AnimationEventTool : MonoBehaviour
{

    public UnityEvent powerupSpawn;

    public void PowerupTriggerOnSpawnCompletion()
    {
        powerupSpawn.Invoke();
    }
}