using Photon.Pun;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    private float _runningTime;
    private PhotonView _view;
    private GameObject _bulletsParent;

    private void Awake()
    {
        _bulletsParent = GameObject.Find("Bullets");
        transform.SetParent(_bulletsParent.transform);
        _view = GetComponent<PhotonView>();
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        _runningTime = 0;
    }

    private void Update()
    {
        _runningTime += Time.deltaTime;
        if (_runningTime > 0.05f) 
            gameObject.GetComponent<BoxCollider2D>().enabled = true;

        Vector3 currentPosition = transform.position;
        Vector3 newPosition = currentPosition + transform.up * _bulletSpeed * Time.deltaTime;
        float distance = _bulletSpeed * Time.deltaTime;
        Vector3 rayStartPosition = currentPosition + transform.up * 0.08f;
        RaycastHit2D hit = Physics2D.Raycast(rayStartPosition, transform.up, distance);

        if (hit.collider != null)
            _view.RPC("DestroyBullet", RpcTarget.MasterClient);
        else
            transform.position = newPosition;
    }
    [PunRPC]
    public void DestroyBullet()
    {
        if (_view.IsMine)
            PhotonNetwork.Destroy(gameObject);
    }
}
