using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour
{
    private string player1CharacterName;
    private string player2CharacterName;

    [SerializeField] private GameObject battleUI;

    [SerializeField] private TextMeshProUGUI player1NameText;
    [SerializeField] private TextMeshProUGUI player2NameText;

    [SerializeField] private int currentSelectingPlayer;

    [SerializeField] private string currentlySelectingStage;

    [SerializeField] private GameObject[] fightStages;

    [SerializeField] private GameObject[] characterModels;

    private Vector3 player1SpawnPos = new(0, 1, 1.22000003f);
    private Vector3 player2SpawnPos = new(0, 1, 6.81379747f);

    private Quaternion player1SpawnRotation = new(0, 0, 0, 1);
    private Quaternion player2SpawnRotation = new(0, 180.235321f, 0, 1);

    public void SelectCharacter(string characterName)
    {

        switch (currentSelectingPlayer)
        {
            case 1:
                player1CharacterName = characterName;
                player1NameText.text = characterName;
                break;
            case 2:
                player2CharacterName = characterName;
                player2NameText.text = characterName;
                break;
        }

    }

    public void SwitchSelectingPlayer()
    {
        if (currentSelectingPlayer == 1)
        {
            currentSelectingPlayer = 2;

        }
        else
        {
            currentSelectingPlayer = 1;
        }
    }

    public void StartFreeplayMatch()
    {

        int i = 0; // used for instanstiating stage and player models

        GameObject player1Character = null;

        GameObject player2Character = null;

        // spawn in selected stage

        while (i < fightStages.Length)
        {
            if (fightStages[i].name == currentlySelectingStage)
            {
                Instantiate(fightStages[i]);
                break;
            }
            i ++;
        }

        i = 0;

        // spawn in player 1

        while (i < characterModels.Length)
        {
            if (characterModels[i].name == player1NameText.text)
            {
                player1Character = Instantiate(characterModels[i]);
                player1Character.transform.position = player1SpawnPos;
                player1Character.transform.rotation = player1SpawnRotation;
                break;
            }
            i ++;
        }

        i = 0;

        // spawn in player 2

        while (i < characterModels.Length)
        {
            if (characterModels[i].name == player2NameText.text)
            {
                player2Character = Instantiate(characterModels[i]);
                player2Character.transform.position = player2SpawnPos;
                break;
            }
            i++;
        }

        i = 0;

        // load proper values

        // for this, get function to return character script, and attach jump/movement script also acquired through different functions, then change the values through theres

        // show battle UI

        this.gameObject.SetActive(false);

        battleUI.SetActive(true);
    }
}
