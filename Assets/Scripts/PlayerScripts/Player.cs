using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviourPunCallbacks
{
    public PhotonView _view { get; protected set; }
    protected int _score;
    [HideInInspector] public float Health;

    private void Awake()
    {
        _view = GetComponent<PhotonView>();
        Health = 100f;
        if (_view.IsMine)
            GetComponent<SpriteRenderer>().color = Color.green;
        else
            GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player"))
            GetComponent<Rigidbody2D>().angularVelocity = 0f;
    }
}
