using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCircleCollider2D : CustomCollider
{
    [SerializeField] private float _radius;

    public float Radius { get => _radius; set => _radius = value; }

    private void OnDrawGizmos()
    {
        // Shows the Collider bounds and radius in editor mode
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
