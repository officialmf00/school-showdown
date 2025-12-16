using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenMenu : MonoBehaviour
{
    private Canvas hideMenu;

    private GameObject hideMenuSource;

    private GameObject showMenuSource;

    private Canvas showMenu;

    public void changeHideMenu(string menuName)
    {
        hideMenuSource = GameObject.Find(menuName);

        hideMenu = hideMenuSource.GetComponentInParent<Canvas>();

    }
    public void openMenu(string openMenuName)
    {
     

        foreach (Transform child in hideMenu.transform)
        {
            child.gameObject.SetActive(false);
        }

        showMenuSource = GameObject.Find(openMenuName);

        showMenu = showMenuSource.GetComponentInParent<Canvas>();

        foreach (Transform child in showMenu.transform)
        {
            child.gameObject.SetActive(true);
        }

    }    
}
