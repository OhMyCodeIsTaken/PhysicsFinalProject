using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] bool _lockPlayerInput;

    [SerializeField] private GameObject _nextLevelButton;

    [SerializeField] private int _score;
    [SerializeField] private int _scoreToWin;
    [SerializeField] private Text _scoreUI;

    [SerializeField] private int _numberOfShotsLeft;
    [SerializeField] private Text _numberOfShotsLeftUI;

    public int Score 
    { 
        get => _score;
        set
        {
            _score = value;
            _scoreUI.text = _score.ToString() + "/" + _scoreToWin;
            if(_score >= _scoreToWin)
            {
                Win();
            }
        }
    }

    public int NumberOfShotsLeft
    {
        get => _numberOfShotsLeft;
        set
        {
            _numberOfShotsLeft = value;
            _numberOfShotsLeftUI.text = _numberOfShotsLeft.ToString();
        }
    }

    public bool LockPlayerInput { get => _lockPlayerInput; }

    private void Start()
    {
        Score = 0;
        NumberOfShotsLeft = NumberOfShotsLeft;
    }

    internal void ExpendShot()
    {
        NumberOfShotsLeft--;
        if(NumberOfShotsLeft <= 0)
        {
            Lose();
        }
    }

    private void Lose()
    {
        _lockPlayerInput = true;
    }

    private void Win()
    {
        _lockPlayerInput = true;
        _nextLevelButton.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex + 1  != SceneManager.sceneCountInBuildSettings)
        {
            int sceneToLoadIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(sceneToLoadIndex);
        }       
    }
}
