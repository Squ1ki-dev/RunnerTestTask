using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        Character runner = collider.GetComponent<Character>();
        if (runner != null)
            runner.IsDead = true;
    }
}
