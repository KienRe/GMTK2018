using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageLerp : MonoBehaviour
{
    public float duration = 5;
    public float fadeTime = 0.5f;

    public Color c1, c2;

    public IEnumerator PanelFlash()
    {
        Image image = GetComponent<Image>();

        float timer = 0;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            if (timer % 1f < 0.5f)
            {
                image.color = c1;
            }
            else
            {
                image.color = c2;
            }
            yield return null;
        }
    }


}
