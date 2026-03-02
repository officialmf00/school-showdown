using TMPro;
using UnityEngine;

public class FightMusicToggle : MonoBehaviour
{
    private TextMeshProUGUI buttonText;

    private bool m_IsPlaying = true;

    private void Start()
    {
        buttonText = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        buttonText.text = "Toggle Music: " + m_IsPlaying.ToString();
    }

    public void ToggleMusic()
    {
        m_IsPlaying = !m_IsPlaying;
        buttonText.text = "Toggle Music: " + m_IsPlaying.ToString();
    }

    public bool getMusicState()
    {
        return m_IsPlaying;
    }
}
