using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    void Start()
    {
        GetComponent<CustomCollider>().OnCollisionWith += GainScore;
    }

    private void GainScore(CustomCollider collider)
    {
        Ball ball = collider.gameObject.GetComponent<Ball>();
        if (ball)
        {
            ball.PocketBall();
        }
        
    }
}
