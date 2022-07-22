using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    GameManager _gameManager;

    void OnEnable()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Crossed"))
        {
            _gameManager.IncrementScore();
            Debug.Log("Triggerred");
        }
    }
}
