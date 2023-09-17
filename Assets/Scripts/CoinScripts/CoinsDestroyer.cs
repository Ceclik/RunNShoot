using Photon.Pun;
using UnityEngine;

public class CoinsDestroyer : MonoBehaviour
{
    protected PhotonView _view;

    private void Awake()
    {
        _view = GetComponent<PhotonView>();
    }

    [PunRPC]
    public void DestroyCoin()
    {
        if(_view.IsMine)
            PhotonNetwork.Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
            _view.RPC("DestroyCoin", RpcTarget.MasterClient); 
    }
}
