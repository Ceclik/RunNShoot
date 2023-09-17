using Photon.Pun;
using UnityEngine;

public class HealthDecreaser : Player
{
    [SerializeField] private GameObject HealthBar;
    [SerializeField] private Color _targetHealthBarColor;
    [SerializeField] private float _deltaHealth;
    private GameObject _deathMenu;
    private Color _originalHealthbarColor;
    private float _lastHealthValue;
    private GameObject _playersParent;

    private void Start()
    {
        _deathMenu = GameObjectExtension.Find("DeathMenu");
        _playersParent = GameObject.Find("Players");
        _lastHealthValue = Health;
        _originalHealthbarColor = HealthBar.GetComponent<SpriteRenderer>().color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Bullet>(out Bullet bullet))
            DecreaseHealth();
    }

    private void Update()
    {
        UpdateHealthBar();
        if (Health < 0 && _view.IsMine)
        {
            _deathMenu.SetActive(true);
            PhotonNetwork.Destroy(gameObject);
        }
    }

    public void DecreaseHealth()
    {
        float newHealthValue = Health - _deltaHealth;
        _view.RPC("SyncronizeHealth", RpcTarget.AllBuffered, _view.ViewID, newHealthValue);
    }

    public void UpdateHealthBar()
    {
        if (Health != _lastHealthValue && HealthBar.activeSelf)
        {
            if (Health <= 0)
                HealthBar.SetActive(false);
           
            HealthBar.transform.localPosition = new Vector3(HealthBar.transform.localPosition.x - (_lastHealthValue - Health) * 0.005f, HealthBar.transform.localPosition.y, 0f);
            HealthBar.transform.localScale = new Vector3(HealthBar.transform.localScale.x - 0.01f * (_lastHealthValue - Health), HealthBar.transform.localScale.y, 0f);
            HealthBar.GetComponent<SpriteRenderer>().color = Color.Lerp(_originalHealthbarColor, _targetHealthBarColor, (100f - Health) / 100f);
            _lastHealthValue = Health;
        } 
    }

    [PunRPC]
    public void SyncronizeHealth(int ID, float healthValue)
    {
        for (int i = 0; i < _playersParent.transform.childCount; i++)
            if (_playersParent.transform.GetChild(i).GetComponent<Player>()._view.ViewID == ID)
                _playersParent.transform.GetChild(i).GetComponent<HealthDecreaser>().Health = healthValue;
    }
}

public static class GameObjectExtension
{
    public static Object Find(string name, System.Type type)
    {
        Object[] objects = Resources.FindObjectsOfTypeAll(type);
        foreach (var obj in objects)
        {
            if (obj.name == name)
                return obj;
        }
        return null;
    }

    public static GameObject Find(string name)
    {
        return Find(name, typeof(GameObject)) as GameObject;
    }
}
