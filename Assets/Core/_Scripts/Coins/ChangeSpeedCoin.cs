using UnityEngine;

public class ChangeSpeedCoin : Coin<ChangeSpeedCoinEffect>
{
    [SerializeField] private SpeedCoinSO _speedCoinSo;

    private void OnTriggerEnter(Collider collider)
    {
        // IRunner is also an IEffectTarget
        Character runner = collider.GetComponent<Character>();
        if (runner != null)
        {
            runner.AddEffect(new ChangeSpeedCoinEffect(runner, _speedCoinSo.SpeedChange, _speedCoinSo.Duration));
            Destroy(gameObject);
        }
    }

    protected override void SelectCoin(Collider collider)
    {
        Character runner = collider.GetComponent<Character>();
        if (runner != null)
        {
            runner.AddEffect(new ChangeSpeedCoinEffect(runner, _speedCoinSo.SpeedChange, _speedCoinSo.Duration));
            Destroy(gameObject);
        }
    }
}
