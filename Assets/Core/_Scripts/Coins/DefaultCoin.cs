using UnityEngine;

public class DefaultCoin : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        IRunner runner = collider.GetComponent<IRunner>();
        if (runner != null)
        {
            runner.IncreaseScore(1);
            Destroy(gameObject);
        }
    }
}
