using System.Collections.Generic;
using UnityEngine;

public class SceneScaler : MonoBehaviour
{
    private float _screenWidth;
    private float _screenAspect;

    [SerializeField] private GameObject _walls;
    [SerializeField] private GameObject _coinsParent;
    [SerializeField] private GameObject _bulletsParent;
    private void Awake()
    {
        _screenAspect = (float)Screen.width / Screen.height;
        float cameraHeight = Camera.main.orthographicSize * 2;
        _screenWidth = _screenAspect * cameraHeight;
        if (_walls != null) Scale(0.2f, _walls);
        if (_coinsParent != null) Scale(1.0f, _coinsParent);
        //if (_bulletsParent != null) Scale(1.0f, _bulletsParent);
    }

    public void Scale(float scaleCooficeent, List<GameObject> scaleObjects)
    {
        float scaleValue = _screenWidth * scaleCooficeent;
        foreach (GameObject scaleObject in scaleObjects)
            scaleObject.transform.localScale = new Vector3(scaleValue, scaleObject.transform.localScale.y, 0);
    }
    public void Scale(float scaleCooficeent, GameObject parent)
    {
        float scaleValue = _screenWidth * scaleCooficeent;
        parent.transform.localScale = new Vector3(scaleValue, parent.transform.localScale.y, 0);
    }
}