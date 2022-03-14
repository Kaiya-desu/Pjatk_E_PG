using UnityEngine;
using System.Collections;

public class PlatformDisappear : MonoBehaviour
{
    private float DisappearTime = 1;
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("PlatformOff", DisappearTime);
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("PlatformOn", DisappearTime * 4);
        }
    }

    void PlatformOff()
    {
        gameObject.SetActive(false);
    }

    void PlatformOn()
    { 
        gameObject.SetActive(true);
    }

}
