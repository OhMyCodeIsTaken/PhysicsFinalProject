using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    void Start()
    {
        GetComponent<CustomCollider>().OnCollisionWith += DepositBallInHole;
    }

    // This function will be called when a ball collides with a hole, which will call the ball's PocketBall function. 
    private void DepositBallInHole(CustomCollider collider)
    {
        Ball ball = collider.gameObject.GetComponent<Ball>();
        if (ball)
        {
            ball.PocketBall();
        }
        
    }
}
