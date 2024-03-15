using System;
using System.Collections.Generic;
using UnityEngine;


public class ServiceLocator
{
    public static ServiceLocator Instance => _instance ??= new ServiceLocator();
    private static ServiceLocator _instance;

    private readonly Dictionary<Type, object> _services; //solo se puede registrar 1 objeto de cada tipo

    private ServiceLocator()
    {
        _services = new Dictionary<Type, object>();
    }

    public void RegisterService<T>(T service)
    {
        var type = typeof(T);
        if (_services.ContainsKey(type))
        {
            Debug.LogWarning($"Service {type} already registered");
        }
        else
        {
            _services.Add(type, service);
        }
    }

    public T GetService<T>()
    {
        var type = typeof(T);
        if (_services.TryGetValue(type, out var service))
        {
            return (T)service;
        }

        throw new Exception($"Service {type} not found");
    }

    public bool Contains<T>()
    {
        var type = typeof(T);
        return _services.ContainsKey(type);
    }

    public void UnregisterService<T>()
    {
        var type = typeof(T);
        if (_services.ContainsKey(type))
        {
            _services.Remove(type);
            return;
        }

        Debug.LogWarning($"Service {type} is not registered");
    }
}