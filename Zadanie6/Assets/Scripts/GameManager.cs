using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;

    public Fruit[] fruits;

    public Player player;

    public Text gameOverText;
    public Text livesText;
    public Text potionsText;

    public int lives { get; private set; }

    // potionki / owocki 
    public int potions { get; private set; }

    private void Start()
    {
        NewGame();
        // press space to start 

    }

    private void Update()
    {
        // do zmiany na przycisk reset
        if (this.lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }

        // gdy wejdzie w owocek to podnosi, moze uzyc przycisku E aby zjeśc i przywrócic sobie 1 hp

        if (potions > 0 && Input.GetKeyDown(KeyCode.E))
        {
            PlayerEat();
        }

    }

    private void NewGame()
    {

        SetLives(3);
        SetPotions(0);
        ResetState();
    }

    private void ResetState()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }

        this.player.ResetState();

        Time.timeScale = 0f; // aby po otrzymaniu DMG była pauza
    }

    private void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.player.gameObject.SetActive(false);

        Time.timeScale = 0f;
    }

    private void SetLives(int live)
    {
        this.lives = live;
        this.livesText.text = "HP = " + lives.ToString();
    }

    // gdy bierzemy owocka to on znika i wchodzi nam do EQ
    public void GetPotion(Fruit fr) {
        var rightFruit = GameObject.Find(fr.name);
       
        rightFruit.SetActive(false);

        SetPotions(this.potions + 1);
    }

    private void SetPotions(int potion)
    {
        this.potions = potion;
        this.potionsText.text = "Potions = " + potions.ToString();
    }


    public void PlayerHit()
    {
        this.player.DeathSequence();
        SetLives(this.lives - 1);


        if(this.lives > 0)
        {
            Invoke(nameof(ResetState), 2.0f);
        }
        else
        {
            GameOver();
        }

    }

    // uzycie mikstury / jedzonka
    public void PlayerEat()
    {
            SetPotions(this.potions - 1);
            SetLives(this.lives + 1);
    }

    // load: HP, POTIONÓW, Pozycja gracza, pozycja duchów, pozycja owocków

    public void LoadData(GameDataToSave data)
    {
        // zawartosc: int hp, int pots, Vector3 playerPos, Vector3[] ghostsPos, bool[] fruitsActiv
        // player data
        SetLives(data.playerHealth);
        SetPotions(data.playerPotions);
        this.player.transform.position = data.playerPosition;

        // ghost data:
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].transform.position = data.ghostsPosition[i];
        }

        // aktywne owocki/potionki:
        for (int i = 0; i < fruits.Length; i++)
        {
            if (data.activeFruits[i])
            {
                Debug.Log("jest owoc " + fruits[i].name);
                FindInActiveObjectByName(fruits[i].name).SetActive(true);
            }
            else
            {
                Debug.Log("nie ma owoc " + fruits[i].name);
                FindInActiveObjectByName(fruits[i].name).SetActive(false);
            }

        }

        Time.timeScale = 0f; // aby po wczytaniu była pauza

    }

    // superfajna metoda!!!!!!!!! 
    GameObject FindInActiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }

}




