using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public abstract class ChangeImageSizeMain : MonoBehaviour
{
    private float theValue;
    private int theValueRounded;
    [SerializeField] protected Image healthBar;
    [SerializeField] protected TextMeshProUGUI healthText;
    [SerializeField] protected CharacterMain character;
    private float displayHP;
    private float currentDisplayHP;
    private void Awake()
    {
        displayHP = character.getSOHealth();
        displayHP = Mathf.Round(displayHP);
        currentDisplayHP = displayHP;
        theValue = displayHP;
        healthText.text = displayHP.ToString() + "/" + displayHP.ToString();
    }
    public void changeHealth(float currentHealth, float changeValue)
    {
        theValue = currentHealth;

        currentDisplayHP = Mathf.Round(currentHealth);

        if (changeValue < 0)
        {
            theValue += changeValue;
            healthBar.fillAmount = theValue / 100f;
        }
        else
        {
            theValue += changeValue;
            theValue = Mathf.Clamp(theValue, 0, 100);

            healthBar.fillAmount = theValue / 100f;
        }
    }
    private void Update()
    {
        theValueRounded = Mathf.RoundToInt(theValue);
        healthText.text = theValueRounded.ToString() + "/" + displayHP.ToString();
    }

    public void SetHealthCharacter(CharacterMain characterValue)
    {
        character = characterValue;
    }
}
