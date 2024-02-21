using UnityEngine;

public class FlyCoin : Coin<FlyCoinEffect>
{
    [SerializeField] private FlyCoinSO _coinSo;

    protected override void SelectCoin(Collider collider)
    {
        Character runner = collider.GetComponent<Character>();
        if (runner != null)
        {
            runner.AddEffect(new FlyCoinEffect(runner, _coinSo.Duration));
            Destroy(gameObject);
        }
    }
}
