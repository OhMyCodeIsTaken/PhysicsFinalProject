using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBallsRespawnPoints : MonoBehaviour
{
    public List<Transform> RespawnTransforms = new List<Transform>();

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            RespawnTransforms.Add(transform.GetChild(i).transform);
        }
    }
}
