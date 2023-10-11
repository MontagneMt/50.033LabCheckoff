using UnityEngine;

public class CoinPowerup : BasePowerup
{
    protected override void Start()
    {
        base.Start();
        this.type = PowerupType.Coin;
    }

    public override void SpawnPowerup()
    {
        spawned = true;

        DestroyPowerup();

        PowerupManager.instance.powerupCollected.Invoke(this);
    }

    public override void ApplyPowerup(MonoBehaviour i)
    {
        // Add score
        GameManager manager;
        bool result = i.TryGetComponent<GameManager>(out manager);
        if (result) manager.IncreaseScore(1);
    }
}