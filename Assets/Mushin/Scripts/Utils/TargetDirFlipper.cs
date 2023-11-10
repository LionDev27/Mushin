using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDirFlipper : MonoBehaviour
{
    public Transform target;

    private float _startingScaleX;

    private void Start()
    {
        _startingScaleX = transform.localScale.x;
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
