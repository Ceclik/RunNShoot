using Photon.Pun;
using System.Collections;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _coin;
    [SerializeField] private float _deltaSpawnTime;
    private GameObject _playersParent;
    private float _xCoinPosition, _yCoinPosition, xMin, xMax, yMin, yMAx;

    private void Awake()
    {
        _playersParent = GameObject.Find("Players");
        Bounds bounds = GetComponent<Renderer>().bounds;
        xMin = bounds.min.x;
        xMax = bounds.max.x;
        yMin = bounds.min.y;
        yMAx = bounds.max.y;
        if (PhotonNetwork.IsMasterClient)
            StartCoroutine(CoinSpawner());
    }

    private IEnumerator CoinSpawner()
    {
        yield return new WaitForSeconds(_deltaSpawnTime);
        if (_playersParent.transform.childCount > 1)
        {
            _xCoinPosition = Random.Range(xMin + 0.2f, xMax - 0.2f);
            _yCoinPosition = Random.Range(yMin + 0.2f, yMAx - 0.2f);
            PhotonNetwork.Instantiate(_coin.name, new Vector3(_xCoinPosition, _yCoinPosition), Quaternion.identity);
        }
        StartCoroutine(CoinSpawner());
    }
}
