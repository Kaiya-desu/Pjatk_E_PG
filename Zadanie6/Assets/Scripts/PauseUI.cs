using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Text info;

    private void Start()
    {
        Time.timeScale = 0f;
    }

    void Update()
    {
        // gdy jest zapauzowana gra w innym miejscu (np po load)
        if(Time.timeScale == 0f)
        {
            info.text = "PRESS SPACE TO PLAY";
        }

        // pauzowanie i odpauzowanie gry za pomocą spacji (domyślnie gra jest zapauzowana)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.timeScale == 0f)
            {
                info.text = "";
                Time.timeScale = 1f;
            }
            else
            {
                info.text = "PRESS SPACE TO PLAY";
                Time.timeScale = 0f;
            }
        }

    }
}
