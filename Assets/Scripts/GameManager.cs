using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Text _scoreUI;

    [SerializeField] private int _score;

    public int Score 
    { 
        get => _score;
        set
        {
            _score = value;
            _scoreUI.text = _score.ToString();
        }
    }
}
