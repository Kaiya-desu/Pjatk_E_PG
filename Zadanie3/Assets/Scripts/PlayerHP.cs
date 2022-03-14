using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{
    public int health = 3;
    public int numOfHearts = 3;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public Text endInfo;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // gdy dajemy wiecej serduszek
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(health > numOfHearts)
        {
            health = numOfHearts;
        }

        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }


            if(i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if (health < 1)
        {
            resetGame = true;
            // Debug.Log("OOPS!");
            animator.SetTrigger("Death");
            endInfo.text = "GAME OVER";
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; // aby sie nie ruszac
            StartCoroutine(WaitForSeconds(3));
        }
    }

    bool resetGame = false;
    bool canTakeDamage = true;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (canTakeDamage && !resetGame)
            {
                StartCoroutine(WaitForSeconds(1));
                health -= 1;
                //Debug.Log("HIT!");
            }
        }
    }

    IEnumerator WaitForSeconds(int x)
    {
        canTakeDamage = false;
        yield return new WaitForSecondsRealtime(x);
        canTakeDamage = true;
        if (resetGame)
        {
          SceneManager.LoadScene("Game"); 
        }
    }


}
