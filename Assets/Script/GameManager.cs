using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }
    void Awake()
    {
        if (gameManager != this && gameManager != null)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }
    }

    public void GameOver()
    {
        Destroy(GameObject.FindWithTag("Player"));
        Debug.Log("Game Over!");
    }
}
