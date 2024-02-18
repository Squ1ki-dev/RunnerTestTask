using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        IRunner runner = collider.GetComponent<IRunner>();
        if (runner != null)
            runner.IsDead = true;
    }
}
