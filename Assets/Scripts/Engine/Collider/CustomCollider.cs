using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCollider : MonoBehaviour
{
    [SerializeField] private bool _isTrigger;

    public Action<CustomCollider> OnCollisionWith;

    public bool IsTrigger { get => _isTrigger; }

    internal virtual void CollidesWith(CustomCollider otherCollider)
    {
        throw new NotImplementedException();
    }
}
