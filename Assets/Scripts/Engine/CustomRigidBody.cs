using System;
using UnityEngine;

public class CustomRigidBody : MonoBehaviour
{
    public Vector3 Velocity;

    public float Mass;

    private CustomCollider _collider;

    public CustomCollider Collider { get => _collider; set => _collider = value; }

    private void Start()
    {
        _collider = GetComponent<CustomCollider>();
        _collider.OnCollisionWith += TransferVelocity;
    }

    private void TransferVelocity(CustomCollider otherCollider)
    {
        Vector3 otherDirection = (otherCollider.transform.position - transform.position).normalized;
        Vector3 newVelocityToApply = 0.3f * Velocity.magnitude * otherDirection;

        CustomRigidBody otherRigidBody = otherCollider.GetComponent<CustomRigidBody>();

        if (otherRigidBody != null)
        {
            
            otherRigidBody.Velocity += newVelocityToApply;
            Velocity -= newVelocityToApply;
        }
        else
        {
            Velocity = -newVelocityToApply;
        }
        
    }

    public void FixedUpdate()
    {
        Move();
        ApplyDrag(CustomPhysics.Instance.AirFriction);
    }

    public void AddForce(Vector3 force)
    {
        Velocity += (force * Time.deltaTime) / Mass;
    }

    public void Move()
    {
        gameObject.transform.position += Velocity;
    }

    private void ApplyDrag(float friction)
    {
        if (Velocity != Vector3.zero)
        {
            Velocity -= Velocity.normalized * (friction * Time.deltaTime) / Mass;

            if (Velocity.magnitude <= 0.01f) // Once the velocity is negligible and is close to zero, reset it to zero
            {
                Velocity = Vector3.zero;
            }
        }
    }

}
