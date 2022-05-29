using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCollider : MonoBehaviour
{
    public Action<CustomCollider> OnCollisionWith;


    internal virtual void CollidesWith(CustomCollider otherCollider)
    {
        throw new NotImplementedException();
    }
}
