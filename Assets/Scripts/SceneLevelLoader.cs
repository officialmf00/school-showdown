using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLevelLoader : MonoBehaviour
{

    [SerializeField] private Animator transition;

    [SerializeField] private float transitionTime = 0.5f;
    public void LoadScene(string sceneName) // Refer to in any script that loads new scene, make sure SceneLevelLoader is in all scenes for transitions
    {
        StartCoroutine(SceneTransition(sceneName));
    }

    IEnumerator SceneTransition(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}
