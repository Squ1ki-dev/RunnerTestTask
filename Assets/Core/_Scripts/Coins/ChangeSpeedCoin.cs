using UnityEngine;

public class ChangeSpeedCoin : MonoBehaviour
{
    [SerializeField] private SpeedCoinSO _speedCoinSo;

    private void OnTriggerEnter(Collider collider)
    {
        IRunner runner = collider.GetComponent<IRunner>();
        if (runner != null)
        {
            runner.AddEffect(new ChangeSpeedCoinEffect(runner, _speedCoinSo.SpeedChange, _speedCoinSo.Duration));
            Destroy(gameObject);
        }
    }
}
