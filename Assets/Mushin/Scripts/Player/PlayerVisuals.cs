using Mushin.Scripts.Player;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    private Player _player;
    [SerializeField] private DashShadowSpawner _dashShadowSpawner;
    [SerializeField] private SpriteRenderer _sprite;
    private bool _isFacingLeft;
    private Camera _camera;
    public void Configure(Player player)
    {
        _player = player;
        _dashShadowSpawner.Configure(_sprite);
    }
    private void Update()
    {
        UpdateSpriteDirection();
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    private void UpdateSpriteDirection()
    {
        Vector2 cursorPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        if (cursorPos.x > transform.position.x+.5f && _isFacingLeft
            || cursorPos.x < transform.position.x-.5f && !_isFacingLeft)
        {
            _sprite.transform.Rotate(0, 180, 0);
            _isFacingLeft = !_isFacingLeft;
        }
    }

    public void SpawnDashShadows() => _dashShadowSpawner.SpawnShadows();
    
    //Manejar animaciones
    public void Reset()
    {
        
    }
}