using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class TitleScreenFadeIn : MonoBehaviour
{

    [SerializeField] private GameObject fadeInObject; // for disabling when fade in is complete
    [SerializeField] private Image fadeInImage;
    [SerializeField] private AudioSource titleScreenAmbience;
    [SerializeField] private AudioSource menuMusic;

    private float fadeTime = 3;

    private float finalAmbienceVolume = 0.8f;

    private Color fadeColor;


    private void Awake()
    {

        // Keep menu music playing

        DontDestroyOnLoad(menuMusic);

        // Value adjustments to make sure nothing is off

        // Adjusting fadeInImage

        fadeColor = fadeInImage.color;

        fadeColor.a = 1;

        fadeInImage.color = fadeColor;

        // Adjusting ambience

        titleScreenAmbience.volume = 0;

        // Start fade

        StartCoroutine(fadeInCoroutine());

    }

    private IEnumerator fadeInCoroutine()
    {
        float elapsed = 0f;
        while (elapsed < fadeTime)
        {
       
            elapsed += Time.deltaTime;
            float alpha = Mathf.SmoothStep(1, 0, elapsed / fadeTime); // Use SmoothStep for easing
            float newVolume = Mathf.SmoothStep(0, finalAmbienceVolume, elapsed / fadeTime); // Use SmoothStep for easing
            fadeColor.a = alpha;
            fadeInImage.color = fadeColor;
            titleScreenAmbience.volume = newVolume;

            
            yield return null;
        }

        // Ensure it's fully visible at the end

        fadeColor.a = 0;
        titleScreenAmbience.volume = finalAmbienceVolume;

        // Disable fade UI to allow menu interaction & start playing menu music

        menuMusic.Play();

        fadeInObject.SetActive(false);

    }

   
}
