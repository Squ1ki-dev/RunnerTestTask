using UnityEngine;

public class FlyCoin : Coin<FlyCoinEffect>
{
    [SerializeField] private FlyCoinSO _coinSo;

    protected override void SelectCoin(Collider collider)
    {
        Player runner = collider.GetComponent<Player>();
        if (runner != null)
        {
            runner.AddEffect(new FlyCoinEffect(runner, _coinSo.Duration));
            Destroy(gameObject);
        }
    }
}
