using UnityEngine;

public class PlayerVisuals : PlayerComponents
{
    [SerializeField] private SpriteRenderer _sprite;
    private bool _isFacingLeft;
    private Camera _camera;

    private void Update()
    {
        UpdateDir();
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    public void UpdateDir()
    {
        Vector2 cursorPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        if (cursorPos.x > transform.position.x && _isFacingLeft || cursorPos.x < transform.position.x && !_isFacingLeft)
        {
            _sprite.transform.Rotate(0, 180, 0);
            _isFacingLeft = !_isFacingLeft;
        }
    }
}