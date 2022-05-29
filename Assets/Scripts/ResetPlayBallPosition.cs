using System.Collections.Generic;
using UnityEngine;

public class ResetPlayBallPosition : MonoBehaviour
{
    [SerializeField] private Transform _whiteBallRespawnPoint;
    [SerializeField] private PlayBallsRespawnPoints _playBallsRespawnPoints;

    [SerializeField] private List<CustomCircleCollider2D> _children = new List<CustomCircleCollider2D>();

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _children.Add(transform.GetChild(i).GetComponent<CustomCircleCollider2D>());
        }

        foreach (var child in _children)
        {
            child.OnCollisionWith += CheckAndResetPlayBallPosition;
        }
    }

    public void CheckAndResetPlayBallPosition(CustomCollider collider)
    {
        if (collider.gameObject.tag == "Play Ball")
        {
            collider.GetComponent<CustomRigidBody>().Velocity = Vector3.zero;

            if (collider.gameObject.layer == 3)
            {
                collider.transform.position = _whiteBallRespawnPoint.position;
            }
            else
            {
                collider.transform.position = _playBallsRespawnPoints.RespawnTransforms[new System.Random().Next(0, _playBallsRespawnPoints.RespawnTransforms.Count)].position;
            }
        }

    }
}
