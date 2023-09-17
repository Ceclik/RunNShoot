using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _player;
   

    private void Start()
    {
        GameObject spawnedPlayer = PhotonNetwork.Instantiate(_player.name, Vector3.zero, Quaternion.identity);
        spawnedPlayer.transform.SetParent(transform);
    }
}
