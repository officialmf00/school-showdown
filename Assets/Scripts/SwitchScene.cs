using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
   public void switchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }    
}
