using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score instance;

    [SerializeField] private TextMeshProUGUI _player1ScoreText;
    [SerializeField] private TextMeshProUGUI _player2ScoreText;

    // Start is called before the first frame update
    private void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        _player1ScoreText.text = PlayerPrefs.GetInt("p1").ToString();
        _player2ScoreText.text = PlayerPrefs.GetInt("p2").ToString();
    }

    public void UpdateScore(int player) {
        if (player == 1){
            int _player1Score = PlayerPrefs.GetInt("p1");
            _player1Score++;
            _player1ScoreText.text = _player1Score.ToString();
            PlayerPrefs.SetInt("p1", _player1Score);
        }
        if (player == 2){
            int _player2Score = PlayerPrefs.GetInt("p2");
            _player2Score++;
            _player2ScoreText.text = _player2Score.ToString();
            PlayerPrefs.SetInt("p2", _player2Score);
        }
    }
}
