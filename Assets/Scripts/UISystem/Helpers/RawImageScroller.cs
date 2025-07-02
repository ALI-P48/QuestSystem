using UnityEngine;
using UnityEngine.UI;

public class RawImageScroller : MonoBehaviour
{
    [SerializeField] private Vector2 _scrollSpeed;
    [SerializeField] private float _multiplayer;
    
    private RawImage _rawImage;
    private Rect _uvRect;
    private Vector2 _speed;

    private void Awake()
    {
        _rawImage = GetComponent<RawImage>();
        _uvRect = _rawImage.uvRect;
        _speed = _scrollSpeed * _multiplayer;
    }

    private void Update()
    {
        //_speed = _scrollSpeed * _multiplayer;
        _uvRect.x += _speed.x * Time.deltaTime;
        _uvRect.y += _speed.y * Time.deltaTime;
        _rawImage.uvRect = _uvRect;
    }
}
