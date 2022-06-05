using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPhysics : Singleton<CustomPhysics>
{
    private float _airFriction = 0.1f;

    public float AirFriction { get => _airFriction; set => _airFriction = value; }

    public List<CustomCollider> Colliders = new List<CustomCollider>();

    void FixedUpdate()
    {
        foreach (CustomCollider collider in Colliders)
        {
            if (collider.gameObject.activeSelf)
            {
                foreach (CustomCollider otherCollider in Colliders)
                {
                    if (collider == otherCollider || !otherCollider.gameObject.activeSelf)
                    {
                        continue;
                    }

                    if (CollisionCheck(collider, otherCollider))
                    {
                        collider.OnCollisionWith?.Invoke(otherCollider);
                    }
                }
            }
        }
    }

    private static bool CollisionCheck(CustomCollider collider, CustomCollider otherCollider)
    {
        if(collider is CustomCircleCollider2D && otherCollider is CustomCircleCollider2D)
        {
            return CirclesCollisionCheck((CustomCircleCollider2D)collider, (CustomCircleCollider2D)otherCollider);
        }

        return false;
    }

    private static bool CirclesCollisionCheck(CustomCircleCollider2D collider, CustomCircleCollider2D otherCollider)
    {
        Vector2 distance = collider.transform.position - otherCollider.transform.position;

        if (distance.magnitude <= collider.Raidus + otherCollider.Raidus)
        {
            return true;
        }

        return false;
    }
}
