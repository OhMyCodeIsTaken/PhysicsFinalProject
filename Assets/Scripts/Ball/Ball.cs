using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private int _score;

    public int Score { get => _score; }

    public virtual void PocketBall() { }


}
