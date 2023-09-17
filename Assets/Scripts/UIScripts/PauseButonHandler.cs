using UnityEngine;

public class PauseButonHandler : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    
    public void OnPauseButtonClick()
    {
        _pauseMenu.SetActive(true);
    }
}
