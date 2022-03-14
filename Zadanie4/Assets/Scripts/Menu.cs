using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Menu : MonoBehaviour
{
    private GameObject widok1;
    private GameObject widok2;

    [SerializeField] private InputField playerName;
    private bool isNameOK = false;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Button buttonStart;


    public void Start()
    {
        widok1 = GameObject.Find("Widok 1");
        widok2 = GameObject.Find("Widok 2");

        // aby na pewno sie nie pokazały na odwrót
        widok2.gameObject.SetActive(false);
        widok1.gameObject.SetActive(true);
    }
    public void goToMenu2()
    {
        widok1.gameObject.SetActive(false);
        widok2.gameObject.SetActive(true);
    }

    public void goBackToMenu1()
    {
        widok2.gameObject.SetActive(false);
        widok1.gameObject.SetActive(true);
    }

    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void checkName(){
        if(playerName.text.Length == 0){
            playerName.image.color = new Color(1, 0.51f, 0.51f);
            isNameOK = false;
        }
        else if (!Regex.IsMatch(playerName.text, @"^[a-zA-Z]+$"))
        {
            playerName.image.color = new Color(1, 0.51f, 0.51f);
            isNameOK = false;
        }
        else
        {
            playerName.image.color = new Color(0.59f, 1, 0.6f);
            isNameOK = true;
        }


    }

    public void ToggleLogic()
    {
        if (toggle.isOn && isNameOK)
        {
            buttonStart.interactable = true;
        }
        else
        {
            buttonStart.interactable = false;
        }

    }



}
