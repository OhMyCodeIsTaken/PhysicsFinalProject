using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCollider : MonoBehaviour
{
    [SerializeField] private bool _isTrigger;

    public Action<CustomCollider> OnCollisionWith;

    public bool IsTrigger { get => _isTrigger; }
    public bool Touched { get => _touched; set => _touched = value; }

    private bool _touched = false;

    private void Awake()
    {
        CustomPhysics.Instance.Colliders.Add(this);
    }
}
