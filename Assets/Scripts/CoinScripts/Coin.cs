using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameObject _coinsParent;

    private void Awake()
    {
        _coinsParent = GameObject.Find("Coins");
        transform.SetParent(_coinsParent.transform);
    }
}
