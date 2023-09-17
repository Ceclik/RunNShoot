using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : Player
{
    private Text _scoreText;

    private void Awake()
    {
        _view = GetComponent<PhotonView>();
        if (_view.IsMine)
            _scoreText = FindObjectOfType<Text>();
        _view = GetComponent<PhotonView>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Coin>(out Coin coin))
            ScoreIncreaser();
    }

    public void ScoreIncreaser()
    {
        if (_view.IsMine)
            _scoreText.text = $"Score: {++_score}";
    }
}
