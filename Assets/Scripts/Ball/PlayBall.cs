using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayBall : Ball
{
    [SerializeField] private Text _scoreUI;

    private void Start()
    {
        _scoreUI.text = Score.ToString();
    }
    public override void PocketBall()
    {
        // When a PlayBall is "deposited", add it's score to the player's score and turn off the PlayBall
        GameManager.Instance.Score += Score;
        gameObject.SetActive(false);
    }
}
