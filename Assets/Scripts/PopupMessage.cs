using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupMessage : MonoBehaviour
{

    private float fadeTime = 1f;
    private float messageVisibilityTime = 2f;

    private bool messageIsVisible = false;
   
    [SerializeField] private TextMeshProUGUI messageTextObject;
    [SerializeField] private AudioSource uiErrorSound;

    public void showMessage(string message)
    {
        if (!messageIsVisible)
        {
            messageIsVisible = true;
            messageTextObject.text = message;
            StartCoroutine(fadeCoroutine());
        }

    }

    private IEnumerator fadeCoroutine()
    {
        float elapsed = 0f;

        uiErrorSound.Play();

        while (elapsed < fadeTime)
        {

            elapsed += Time.deltaTime;
            float alpha = Mathf.SmoothStep(0, 1, elapsed / fadeTime); // Use SmoothStep for easing
            messageTextObject.alpha = alpha;


            yield return null;
        }

        // Ensure it's fully visible

        messageTextObject.alpha = 1;

        yield return new WaitForSeconds(messageVisibilityTime);

        elapsed = 0f;

        while (elapsed < fadeTime)
        {

            elapsed += Time.deltaTime;
            float alpha = Mathf.SmoothStep(1, 0, elapsed / fadeTime); // Use SmoothStep for easing
            messageTextObject.alpha = alpha;


            yield return null;
        }

        // Ensure it's fully hidden

        messageTextObject.alpha = 0;

        // Allow messages to show up again

        messageIsVisible = false;



    }

}
