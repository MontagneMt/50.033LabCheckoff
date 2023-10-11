using UnityEngine.Events;


public class PowerupManager : Singleton<PowerupManager> 
{
    public UnityEvent<BasePowerup> powerupCollected;

    void Start() {
        powerupCollected.AddListener(FilterAndSelectPowerup);
    }

    private void FilterAndSelectPowerup(IPowerup i) {
        switch (i.powerupType) {
            case PowerupType.Coin:
            {
                GameManager.instance.powerupAffectsManager.Invoke(i);
                break;
            }
            case PowerupType.MagicMushroom:
            {
                GameManager.instance.powerupAffectsPlayer.Invoke(i);
                break;
            }
            case PowerupType.OneUpMushroom:
            {
                GameManager.instance.powerupAffectsPlayer.Invoke(i);
                break;
            }
            case PowerupType.SuperStar:
            {
                GameManager.instance.powerupAffectsPlayer.Invoke(i);
                break;
            }
            default:
                break;
        }
    }

}