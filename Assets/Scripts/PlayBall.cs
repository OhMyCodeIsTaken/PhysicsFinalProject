using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBall : MonoBehaviour
{
    [SerializeField] private int _score;

    public int Score { get => _score; }

    internal void GainScore()
    {
        GameManager.Instance.Score += _score;
        gameObject.SetActive(false);
    }
}
