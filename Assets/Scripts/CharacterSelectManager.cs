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
    [SerializeField] private string currentySelectingMusic;

    [SerializeField] private GameObject[] fightStages;
    [SerializeField] private GameObject[] characterModels;
    [SerializeField] private GameObject[] fightSongs;

    [SerializeField] private AttackManager attackManager;

    [SerializeField] private GameObject battleMusicHolder;

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

        GameObject fightSong = null;

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

        // spawn in selected track
        
        while (i < fightSongs.Length)
        {
            if (fightSongs[i].name == currentySelectingMusic)
            {
                fightSong = Instantiate(fightSongs[i], battleMusicHolder.gameObject.transform);
                break;
            }
            i++;
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

        // set player names in battle UI

        battleUI.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().text = player1NameText.text;
        battleUI.transform.GetChild(5).gameObject.GetComponent<TextMeshProUGUI>().text = player2NameText.text;

        // set P1Health character & Changeimgbar

        battleUI.transform.GetChild(2).gameObject.GetComponent<ChangeImageSize>().SetHealthCharacter(player1Character.GetComponent<Character>());

        player1Character.GetComponent<Character>().ChangeImageBar(battleUI.transform.GetChild(2).gameObject.GetComponent<ChangeImageSize>());

        // set P2Health character & Changeimgbar

        battleUI.transform.GetChild(3).gameObject.GetComponent<ChangeImageSize2>().SetHealthCharacter(player2Character.GetComponent<Character2>());

        player2Character.GetComponent<Character2>().ChangeImageBar(battleUI.transform.GetChild(3).gameObject.GetComponent<ChangeImageSize2>());

        // set P1 LifeImages

        player1Character.GetComponent<Character>().ChangeLifeImage(0, battleUI.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.GetComponent<Image>());
        player1Character.GetComponent<Character>().ChangeLifeImage(1, battleUI.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject.GetComponent<Image>());
        player1Character.GetComponent<Character>().ChangeLifeImage(2, battleUI.transform.GetChild(2).gameObject.transform.GetChild(3).gameObject.GetComponent<Image>());

        // set P2 LifeImages

        player2Character.GetComponent<Character2>().ChangeLifeImage(0, battleUI.transform.GetChild(3).gameObject.transform.GetChild(3).gameObject.GetComponent<Image>());
        player2Character.GetComponent<Character2>().ChangeLifeImage(1, battleUI.transform.GetChild(3).gameObject.transform.GetChild(2).gameObject.GetComponent<Image>());
        player2Character.GetComponent<Character2>().ChangeLifeImage(2, battleUI.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<Image>());

        // set P1 AttackManager

        player1Character.GetComponent<Character>().ChangeAttackManager(attackManager);

        // set P2 AttackManager

        player2Character.GetComponent<Character2>().ChangeAttackManager(attackManager);

        // show battle UI

        this.gameObject.SetActive(false);

        battleUI.SetActive(true);

        // Play battle track

        fightSong.GetComponent<AudioSource>().Play();
    }
}
