using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject
{
    public GameObject gameObject { get; }
    public Transform transform { get; }
    public ObjectPool Pool { get; set; }

    public void SetPool(ObjectPool pool)
    {
        Pool = pool;
    }
}