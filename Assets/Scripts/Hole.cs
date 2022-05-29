using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CustomCollider>().OnCollisionWith += GainScore;
    }

    private void GainScore(CustomCollider collider)
    {
        PlayBall ball = collider.gameObject.GetComponent<PlayBall>();
        if (ball)
        {
            ball.GainScore();
        }
        
    }
}
