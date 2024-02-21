using UnityEngine;

public class ChangeSpeedCoin : Coin<ChangeSpeedCoinEffect>
{
    [SerializeField] private SpeedCoinSO _speedCoinSo;

    protected override void SelectCoin(Collider collider)
    {
        Player runner = collider.GetComponent<Player>();
        if (runner != null)
        {
            runner.AddEffect(new ChangeSpeedCoinEffect(runner, _speedCoinSo.SpeedChange, _speedCoinSo.Duration));
            Destroy(gameObject);
        }
    }
}
