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

    public List<CustomCollider> CachedColliders = new List<CustomCollider>();

    private bool _touched = false;

    private void Awake()
    {
        CustomPhysics.Instance.Colliders.Add(this);
    }

    /// <summary>
    /// Checks if a collision between two CustomColliders occured and that only the first one's OnColissionWith event was triggered. 
    /// This can be used to run a function only once when two colliders collide
    /// </summary>
    /// <param name="customCollider"></param>
    /// <returns></returns>
    public bool CollisionOnlyRegisteredOnce(CustomCollider otherCollider)
    {
        if(CachedColliders.Contains(otherCollider) && !otherCollider.CachedColliders.Contains(this))
        {
            return true;
        }

        return false;
    }
}
