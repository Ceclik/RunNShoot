using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))] 
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigitbody2d;
    [SerializeField] private float _movingSpeed;
    private FloatingJoystick _joystick;

    private GameObject _playersParent;
    private PhotonView _view;

    private float _xMovingDirection, _yMovingDirection;
    private float _xJoystikPosition, _yJoystikPosition;

    private void Start()
    {
        _playersParent = GameObject.Find("Players");
        transform.SetParent(_playersParent.transform);
        _joystick = FindObjectOfType<FloatingJoystick>();
        _view = GetComponent<PhotonView>();
    }
    private void FixedUpdate()
    {
        _xMovingDirection = _joystick.Horizontal * _movingSpeed;
        _yMovingDirection = _joystick.Vertical * _movingSpeed;
        _xJoystikPosition = _joystick.Horizontal;
        _yJoystikPosition = _joystick.Vertical;

        if (_view.IsMine)
        {
            _rigitbody2d.velocity = new Vector3(_xMovingDirection, _yMovingDirection, 0);
            if (_xJoystikPosition != 0 || _yJoystikPosition != 0)
            {
                Vector3 direction = new Vector3(_xJoystikPosition, _yJoystikPosition);
                direction.Normalize();
                float rotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, -rotation);
            }
        }
    }
}