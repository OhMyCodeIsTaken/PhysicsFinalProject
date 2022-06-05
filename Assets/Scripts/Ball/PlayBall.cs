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
        GameManager.Instance.Score += Score;
        gameObject.SetActive(false);
    }
}
