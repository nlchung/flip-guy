using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : MonoBehaviour
{
    [SerializeField] int _playerNumber;
    [SerializeField] GameObject _enemy;
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject == _enemy)
        {
            Score.instance.UpdateScore(_playerNumber);
            GameManager.instance.GameOver();
        };
    }
}
