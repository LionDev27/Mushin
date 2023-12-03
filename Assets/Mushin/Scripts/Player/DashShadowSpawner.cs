using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashShadowSpawner : MonoBehaviour
{
    private PlayerDash _playerDash;

    [SerializeField] private GameObject _shadowPrefab;
    [SerializeField] private int numberOfShadows;
    [SerializeField] private float _secondsBetwwenShadows;

    // [SerializeField] private List<GameObject> pool = new List<GameObject>();
    private SpriteRenderer _playerSpriteRenderer;
    private Transform _playerTransform;
    private string poolTag = "dashShadow";

    public void Configure(SpriteRenderer spriteRenderer)
    {
        _playerSpriteRenderer = spriteRenderer;
        _playerTransform = _playerSpriteRenderer.transform;
    }

    public void SpawnShadows()
    {
        StartCoroutine(SpawnShadowsCoroutine());
    }

    private IEnumerator SpawnShadowsCoroutine()
    {
        for (int i = 0; i < numberOfShadows; i++)
        {
            if (_shadowPrefab.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.sprite = _playerSpriteRenderer.sprite;
                
                ObjectPooler.Instance.SpawnFromPool(poolTag,_playerTransform.position,_playerTransform.rotation);
                yield return new WaitForSeconds(_secondsBetwwenShadows);
            }
        }
    }
}