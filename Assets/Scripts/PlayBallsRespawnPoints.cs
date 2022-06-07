using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBallsRespawnPoints : MonoBehaviour
{
    [SerializeField] private Transform _whiteBallRespawnPoint;
    public List<Transform> RespawnTransforms = new List<Transform>();

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            RespawnTransforms.Add(transform.GetChild(i).transform);
        }
    }

    public void RespawnBall(CustomCollider collider)
    {
        if (collider.gameObject.tag == "Ball")
        {
            CustomRigidBody colliderRigidBody = collider.GetComponent<CustomRigidBody>();
            colliderRigidBody.Velocity = Vector3.zero;

            if (collider.gameObject.layer == 3) // Respawn White Ball at it's respawn point
            {
                collider.transform.position = _whiteBallRespawnPoint.position;
            }
            else // Respawn a PlayBall at a random location from the list, then adding force to prevent "stacking" balls on top of each other
            {
                collider.transform.position = RespawnTransforms[new System.Random().Next(0, RespawnTransforms.Count)].position;
                colliderRigidBody.AddForce(new Vector3(3, 0, 0));
            }
        }

        

    }

}
