using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsChanger : MonoBehaviour
{
    // do jednego bloku aktywacyjnego może być przyczepione kilka światełek
    [SerializeField] private GameObject[] torch;

    void Start()
    {
        for (int i = 0; i < torch.Length; i++)
        {
            torch[i].SetActive(false);
        }
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < torch.Length; i++)
            {
                torch[i].SetActive(true);
            }
        }
    }

    // swiatła gasną po 3 sekundach od zejścia z klocka

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < torch.Length; i++)
            {
                StartCoroutine(Waiter(i));
            }
        }
    }


    IEnumerator Waiter(int i)
    {
        yield return new WaitForSeconds(3f);
        torch[i].SetActive(false);
    }

}
