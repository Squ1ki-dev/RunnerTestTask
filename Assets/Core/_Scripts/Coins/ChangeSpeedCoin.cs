using UnityEngine;

public class ChangeSpeedCoin : MonoBehaviour
{
    [SerializeField] private float _duration = 10f;
    [SerializeField] private float _speedChange;

    private void OnTriggerEnter(Collider collider)
    {
        // IRunner is also an IEffectTarget
        IRunner runner = collider.GetComponent<IRunner>();
        if (runner != null)
        {
            runner.AddEffect(new ChangeSpeedCoinEffect(runner, _speedChange, _duration));
            Destroy(gameObject);
        }
    }
}
