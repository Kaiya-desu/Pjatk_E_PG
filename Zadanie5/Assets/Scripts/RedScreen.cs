using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedScreen : MonoBehaviour {

    private Image image;
    private Color color;
    private Color StartColor;
    private Color EndColor;

    void Start() {
        image = GetComponent<Image>();
        color = image.color; // oryginalny kolor

        StartColor = new Color(color.r, color.g, color.b, 0);
        EndColor = new Color(color.r, color.g, color.b, 0.9f);

    GameEventSystem.Instance.OnPlayerInDangerZone += SetRedScreen;
    }

    private bool set; //do wyjścia z korutyny.
    void SetRedScreen(bool setScreen) {

        set = setScreen;
        if (setScreen) {
            StartCoroutine(lerpInAlpha(3f));
        } else {
            image.color = StartColor;
        }
    }


    public IEnumerator lerpInAlpha(float duration)
    {
        for (float t=0.01f; t < duration; t += 0.1f)
        {
            if (!set)
            {
                image.color = StartColor;
                break;
            }
            image.color = Color.Lerp(StartColor, EndColor, t/duration);
            yield return null;
        }
        if (set)
        {
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(lerpOutAlpha(3f));
        }

    }

    public IEnumerator lerpOutAlpha(float duration)
    {
        for (float t = 0.01f; t < duration; t += 0.1f)
        {
            if (!set)
            {
                image.color = StartColor;
                break;
            }
            image.color = Color.Lerp(EndColor, StartColor, t/duration);
            yield return null;
            
        }
            StartCoroutine(lerpInAlpha(3f));
    }


}