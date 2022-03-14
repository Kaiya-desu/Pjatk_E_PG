using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private bool pauseGame = true;

    private void Start()
    {
        Time.timeScale = 0f;
    }

    void Update()
    {
        // pauzowanie i odpauzowanie gry za pomocą spacji (domyślnie gra jest zapauzowana)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pauseGame)
            {
                canvas.enabled = false;
                Time.timeScale = 1f;
                pauseGame = false;
            }
            else if (!pauseGame)
            {
                canvas.enabled = true;
                Time.timeScale = 0f;
                pauseGame = true;
            }
        }

    }
}
