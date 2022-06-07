using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCircleCollider2D : CustomCollider
{
    [SerializeField] private float _raidus;

    public float Raidus { get => _raidus; set => _raidus = value; }

    private void OnDrawGizmos()
    {
        // Shows the Collider bounds and radius in editor mode
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Raidus);
    }
}
