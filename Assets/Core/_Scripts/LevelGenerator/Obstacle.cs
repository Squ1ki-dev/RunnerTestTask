using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        Player runner = collider.GetComponent<Player>();
        if (runner != null)
            runner.IsDead = true;
    }
}
