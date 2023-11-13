using UnityEngine;

public class TargetDirFlipper : MonoBehaviour
{
    public Transform target;

    [SerializeField] private SpriteRenderer _otherRenderer;
    private SpriteRenderer _renderer;
    private float _startingScaleX;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _startingScaleX = transform.localScale.x;
    }

    private void Update()
    {
        if (_otherRenderer.sprite != _renderer.sprite)
            _otherRenderer.sprite = _renderer.sprite;
    }

    public void Flip()
    {
        if (target == null) return;
        var currentScale = transform.localScale;
        currentScale.x = TargetDir().x switch
        {
            > 0f => _startingScaleX,
            < 0f => -_startingScaleX,
            _ => currentScale.x
        };
        transform.localScale = currentScale;
        _otherRenderer.transform.localScale = currentScale;
    }

    public bool CanFlip()
    {
        var currentScale = transform.localScale;
        return (TargetDir().x > 0 && currentScale.x < _startingScaleX) ||
               (TargetDir().x < 0 && currentScale.x > -_startingScaleX);
    }

    private Vector3 TargetDir()
    {
        return target.position - transform.position;
    }
}
