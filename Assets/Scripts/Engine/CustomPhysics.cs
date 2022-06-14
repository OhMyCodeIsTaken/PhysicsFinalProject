using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPhysics : Singleton<CustomPhysics>
{
    private float _airFriction = 0.1f;

    public float AirFriction { get => _airFriction; set => _airFriction = value; }

    // This is the list of all colliders in the scene. The engine will use this list to check and resolve collisions that occur in the scene.
    public List<CustomCollider> Colliders = new List<CustomCollider>();

    void FixedUpdate()
    {
        foreach (CustomCollider collider in Colliders)  
        {
            // Clean all recorded cached colliders from the previous frame
            collider.CachedColliders.Clear();
        }        

        foreach (CustomCollider collider in Colliders)
        {
            if (collider.gameObject.activeSelf) // Skip collision check if the gameObject is turned off
            {
                foreach (CustomCollider otherCollider in Colliders)
                {
                    if (collider == otherCollider || !otherCollider.gameObject.activeSelf)
                    {
                        // Skip this iteration if the collider is "colliding with itself" OR if the other gameObject is turned off
                        continue;
                    }

                    if (CollisionCheck(collider, otherCollider)) // Check if collider and otherCollider are colliding
                    {
                        collider.CachedColliders.Add(otherCollider);    // Record which colliders collider collided with this frame.
                        collider.OnCollisionWith?.Invoke(otherCollider);    // Resolve collision according to the collision behavior that's added to collider
                    }
                }
            }
        }
    }

    private static bool CollisionCheck(CustomCollider collider, CustomCollider otherCollider)
    {
        /* As of now, this project only supports CustomCircleCollider2D collision,
           so this "generic" collision check will only check CustomCircleCollider2D collisions.
           If the colliders are not both CustomCircleCollider2D, this collision check will fail.
        */
        if (collider is CustomCircleCollider2D && otherCollider is CustomCircleCollider2D)
        {
            return CirclesCollisionCheck((CustomCircleCollider2D)collider, (CustomCircleCollider2D)otherCollider);
        }

        return false;
    }

    private static bool CirclesCollisionCheck(CustomCircleCollider2D collider, CustomCircleCollider2D otherCollider)
    {
        // Calculate the distance between the objects
        Vector2 distance = collider.transform.position - otherCollider.transform.position;

        // Collision occurs if the distance is smaller or equal to the sum of both cricle's radius.
        if (distance.magnitude <= collider.Radius + otherCollider.Radius)
        {
            return true;
        }

        return false;
    }
}
