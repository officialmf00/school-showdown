using TMPro;
using UnityEngine;

public class ChangeVersionText : MonoBehaviour
{
    private TextMeshProUGUI versionText;
    void Awake()
    {
        versionText = GetComponentInParent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        versionText.text = "Version " + Application.version;
    }
}
