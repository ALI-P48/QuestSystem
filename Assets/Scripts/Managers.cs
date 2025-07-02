using System;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private Manager[] _managers;

    private void Awake()
    {
        _managers = GetComponentsInChildren<Manager>();
        DontDestroyOnLoad(gameObject);
        foreach (Manager manager in _managers)
        {
            manager.CreateInstance();
        }
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        foreach (Manager manager in _managers)
        {
            manager.Initialize();
        }
    }
}
