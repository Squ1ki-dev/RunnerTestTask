using UnityEngine;

public class FlyCoin : MonoBehaviour
{
    [SerializeField] private float _duration = 10f;

    private void OnTriggerEnter(Collider collider)
    {
        IRunner runner = collider.GetComponent<IRunner>();
        if (runner != null)
        {
            runner.AddEffect(new FlyCoinEffect(runner, _duration));
            Destroy(gameObject);
        }
    }
}
