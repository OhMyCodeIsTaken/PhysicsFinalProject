using System;
using UnityEngine;

public class CustomRigidBody : MonoBehaviour
{
    public Vector3 OldVelocity;

    public Vector3 OldPosition;

    public Vector3 Velocity;

    public float Mass;

    private CustomCollider _collider;

    public CustomCollider Collider { get => _collider; set => _collider = value; }

    private void Start()
    {       
        _collider = GetComponent<CustomCollider>();
        /* When this rigid body collides with another object with a collider, it will transfer some momentum to the other object
          (if that object also has a rigidbody) */
        _collider.OnCollisionWith += TransferArbitraryMomentum;
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
            // Lose a portion of the Velocity, with direction equal to to the original Velocity* and the magnitude being dependant on the friction "size"
            /* *Note: The direction of the Velocity is the same, but when you subtract the new Vector it effectively means velocity loses "speed" in the direction
             OPPOSITE to itself. */
            Velocity -= Velocity.normalized * (friction * Time.deltaTime) / Mass;  

            if (Velocity.magnitude <= 0.01f) // Once the velocity is negligible and is close to zero, reset it to zero
            {
                Velocity = Vector3.zero;
            }
        }
    }

    #region Momentum
    private void TransferArbitraryMomentum(CustomCollider otherCollider)
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

    private void TransferMomentum(CustomCollider otherCollider)
    {
        Vector3 otherDirection = (otherCollider.transform.position - transform.position).normalized;
        CustomRigidBody otherRigidBody = otherCollider.GetComponent<CustomRigidBody>();

        if (otherRigidBody != null)
        {
            Debug.Log("2");
            InitOldVelocitiesOnMomentumTransfer(otherRigidBody);

            Velocity = CalculateNewVelocities2D(OldVelocity, otherRigidBody.OldVelocity, OldPosition, otherRigidBody.OldPosition, Mass, otherRigidBody.Mass);
        }
        else
        {
            Velocity = -Velocity.magnitude * otherDirection;
        }
    }

    private Vector3 CalculateNewVelocities2D(Vector3 oldVelocity1, Vector3 oldVelocity2, Vector3 position1, Vector3 position2, float mass1, float mass2)
    {
        Vector3 velocityDiff = oldVelocity1 - oldVelocity2;
        Vector3 positionDiff = position1 - position2;

        Vector3 newVelocity = OldVelocity - (2 * mass2 / (mass1 + mass2)) * ((Vector3.Dot(velocityDiff, positionDiff)) / (float)Math.Pow((positionDiff.magnitude), 2)) * positionDiff;
        return newVelocity;
    }

    private void InitOldVelocitiesOnMomentumTransfer(CustomRigidBody otherRigidBody)
    {
        if (Collider.CollisionOnlyRegisteredOnce(otherRigidBody.Collider))
        {
            // Only runs on the first CollisionCheck of the two colliders
            Debug.Log("1");
            OldVelocity = Velocity;
            otherRigidBody.OldVelocity = otherRigidBody.Velocity;

            OldPosition = transform.position;
            otherRigidBody.OldPosition = otherRigidBody.transform.position;

        }
    }
    #endregion

}
