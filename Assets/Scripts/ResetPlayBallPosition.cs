using System.Collections.Generic;
using UnityEngine;

public class ResetPlayBallPosition : MonoBehaviour
{
    
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
            child.OnCollisionWith += _playBallsRespawnPoints.RespawnBall;
        }
    }
}
