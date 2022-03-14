using System;
using UnityEngine;

public class GameData : MonoBehaviour
{
    // zapis: HP, POTIONÓW, Pozycja gracza, pozycja duchów, pozycja owocków
    public GameManager gameManager { get; private set; }

    //player:
    public int playerHealth;
    public int playerPotions;
    public Vector3 playerPosition;
    public GameDataToSave saveData { get; private set; }

    //ghosts
    public Vector3[] ghostsPosition;

    // fruits/potions

    public bool[] activeFruits;

    private void Awake()
    {
        saveData = new GameDataToSave();
        this.gameManager = GetComponent<GameManager>();
        // długosc tablic:
        saveData.ghostsPosition = new Vector3[this.gameManager.ghosts.Length];
        saveData.activeFruits = new bool[this.gameManager.fruits.Length];
    }

    private void Update()
    {
        // player data:
        saveData.playerHealth = this.gameManager.lives;
        saveData.playerPotions = this.gameManager.potions;
        saveData.playerPosition = this.gameManager.player.transform.position;

        // ghost data:
        for (int i = 0; i < this.gameManager.ghosts.Length; i++)
        {
            saveData.ghostsPosition[i] = this.gameManager.ghosts[i].transform.position;
        }

        // aktywne owocki/potionki:
        for (int i = 0; i < this.gameManager.fruits.Length; i++)
        {
            var current = GameObject.Find(this.gameManager.fruits[i].name);
            if (current)
            {
                saveData.activeFruits[i] = true;  
            }
            else
            {
                saveData.activeFruits[i] = false;
            }

        }

        // testowanie zapisu
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("HP " + playerHealth + ", POT " + playerPotions + ", VEC " + playerPosition);

            for (int i = 0; i < ghostsPosition.Length; i++)
            {
                Debug.Log("Ghost " + i + " " + ghostsPosition[i]);
            }

            for (int i = 0; i < activeFruits.Length; i++)
            {
                Debug.Log("Fruit " + i + " " + activeFruits[i]);
            }

        }
    }


}

[Serializable]
public class GameDataToSave
{
    // player
    public int playerHealth;
    public int playerPotions;
    public Vector3 playerPosition;
    //ghosts
    public Vector3[] ghostsPosition;

    // fruits/potions
    public bool[] activeFruits;
}
