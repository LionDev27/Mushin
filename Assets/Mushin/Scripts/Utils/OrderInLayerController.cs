using System;
using UnityEngine;

public class OrderInLayerController : MonoBehaviour
{
    [SerializeField] private bool _canChange;
    [SerializeField] private int _offset;

    private SpriteRenderer _spriteRenderer;
    private int _maxY;
    private int _lastOrder;
    private float _timer;
    private float _timerMax = .1f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        //TODO: Añadir altura del mapa.
        _maxY = 500;
        ChangeSortingOrder();
    }

    void LateUpdate()
    {
        if (_timer <= 0f)
        {
            if (CurrentOrder() != _lastOrder && _canChange)
                ChangeSortingOrder();
        }
        else
            _timer -= Time.deltaTime;
    }

    private void ChangeSortingOrder()
    {
        _spriteRenderer.sortingOrder = CurrentOrder();
        _lastOrder = _spriteRenderer.sortingOrder;
        _timer = _timerMax;
    }

    private int CurrentOrder()
    {
        return _maxY - (int)transform.position.y - _offset;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + _offset));
    }
}
