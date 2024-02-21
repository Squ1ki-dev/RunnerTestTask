using UnityEngine;

public class FlyCoin : MonoBehaviour
{
    [SerializeField] private FlyCoinSO _coinSo;

    private void OnTriggerEnter(Collider collider)
    {
        IRunner runner = collider.GetComponent<IRunner>();
        if (runner != null)
        {
            runner.AddEffect(new FlyCoinEffect(runner, _coinSo.Duration));
            Destroy(gameObject);
        }
    }
}
