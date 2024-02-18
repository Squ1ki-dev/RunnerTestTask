using UnityEngine;

public class ChangeSpeedCoin : MonoBehaviour
{
    [SerializeField] private CoinSO _coinSo;

    private void OnTriggerEnter(Collider collider)
    {
        // IRunner is also an IEffectTarget
        IRunner runner = collider.GetComponent<IRunner>();
        if (runner != null)
        {
            runner.AddEffect(new ChangeSpeedCoinEffect(runner, _coinSo.SpeedChange, _coinSo.Duration));
            Destroy(gameObject);
        }
    }
}
