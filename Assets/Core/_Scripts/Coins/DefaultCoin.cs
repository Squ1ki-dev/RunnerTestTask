using UnityEngine;

public class DefaultCoin : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        Character runner = collider.GetComponent<Character>();
        if (runner != null)
        {
            runner.IncreaseScore(1);
            Destroy(gameObject);
        }
    }
}
