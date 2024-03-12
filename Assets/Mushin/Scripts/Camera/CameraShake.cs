using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class CameraShake : MonoBehaviour
{
    private CinemachineImpulseSource _impulseSource;

    private void Awake()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shake(float intensity = 1f)
    {
        _impulseSource.GenerateImpulse(intensity);
    }
}
