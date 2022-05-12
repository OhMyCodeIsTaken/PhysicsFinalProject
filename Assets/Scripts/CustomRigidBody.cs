using UnityEngine;

public class CustomRigidBody : MonoBehaviour
{
    public Vector3 Velocity;

    public float Mass;

    public void FixedUpdate()
    {
        Move();
        ApplyDrag(CustomPhysics.AirFriction);
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
        }
    }

}
