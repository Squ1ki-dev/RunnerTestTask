using UnityEngine;

public class DefaultCoin : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        Player runner = collider.GetComponent<Player>();
        if (runner != null)
        {
            runner.IncreaseScore(1);
            Destroy(gameObject);
        }
    }
}
