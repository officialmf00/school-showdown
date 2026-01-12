using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class OpenMenu : MonoBehaviour
{

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject playMenu;

    [SerializeField] private AudioSource uiClickSound;

    // For Reference: 1 = Main Menu, 2 = Play Menu, add onto as UI in title screen menu expands

    public void toggleMenu(int menuNum)
    {
        GameObject theMenu = null;

        // figure out which menu to toggle

        if (menuNum == 1)
        {
            theMenu = mainMenu;
        }
        else if(menuNum == 2)
        {
            theMenu = playMenu;
        }

        // whether the menu will be shown or hidden based on what its current state is

        uiClickSound.Play();

        if (theMenu.activeSelf)
        {
            theMenu.SetActive(false);
        }
        else if (!theMenu.activeSelf)
        {
            theMenu.SetActive(true);
        }

    }
   
}    



