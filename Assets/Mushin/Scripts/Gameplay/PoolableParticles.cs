using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class PoolableParticles : MonoBehaviour, IPoolable
{
    private ParticleSystem _particles;
    private string _poolTag;

    public void SetTag(string poolTag)
    {
        _poolTag = poolTag;
    }

    private void Awake()
    {
        _particles = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        StartCoroutine(RecycleAfterDuration());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator RecycleAfterDuration()
    {
        yield return new WaitForSeconds(_particles.main.duration);
        Recycle();
    }

    public void Recycle()
    {
        ObjectPooler.Instance.ReturnToPool(_poolTag, gameObject);
    }
}
