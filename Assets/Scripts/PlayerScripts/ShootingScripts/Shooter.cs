using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _bulletSpawnPosition;
    [SerializeField] private float _reloadTime;
    private Button[] _buttons;
    private Button _shootButton;
    private PhotonView _view;
    private float _deltaTime;

    private void Start()
    {
        _deltaTime = 0;
        _view = GetComponent<PhotonView>();
        _buttons = FindObjectsOfType<Button>();
        foreach (Button b in _buttons)
            if(b.tag == "ShootButton")
                _shootButton = b;
        _shootButton.onClick.AddListener(Shoot);
    }

    private void Update()
    {
        _deltaTime += Time.deltaTime;
    }

    public void Shoot()
    {
        if (_deltaTime > _reloadTime && _view.IsMine)
        {
            _deltaTime = 0;
            _view.RPC("SpawnBullet", RpcTarget.MasterClient, _bulletSpawnPosition.transform.position, _bulletSpawnPosition.transform.rotation);
        }
    }

    [PunRPC]
    public void SpawnBullet(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        PhotonNetwork.Instantiate(_bullet.name, spawnPosition, spawnRotation);
    }
}
