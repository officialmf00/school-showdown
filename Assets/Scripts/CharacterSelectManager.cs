
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour
{
    [SerializeField] private string player1CharacterName;
    [SerializeField] private string player2CharacterName;

    [SerializeField] private GameObject battleUI;

    [SerializeField] private GameObject freeplayStartButton;

    [SerializeField] private TextMeshProUGUI player1NameText;
    [SerializeField] private TextMeshProUGUI player2NameText;

    [SerializeField] private TextMeshProUGUI stageNameText;
    [SerializeField] private TextMeshProUGUI trackNameText;

    [SerializeField] private int currentSelectingPlayer;
    [SerializeField] private string currentlySelectingStage;
    [SerializeField] private string currentySelectingMusic;

    [SerializeField] private GameObject[] fightStages;
    [SerializeField] private GameObject[] characterModels;
    [SerializeField] private GameObject[] fightSongs;

    [SerializeField] private AttackManager attackManager;

    [SerializeField] private GameObject battleMusicHolder;

    [SerializeField] private FightMusicToggle battleMusicToggleButton;

    private Vector3 player1SpawnPos = new(0, 1, 1.22000003f);
    private Vector3 player2SpawnPos = new(0, 1, 6.81379747f);

    private Quaternion player1SpawnRotation = new(0, 0, 0, 1);
    private Quaternion player2SpawnRotation = new(0, 180.235321f, 0, 1);

    [SerializeField] private string[] Player1Characters;
    [SerializeField] private string[] Player2Characters;

    private void Awake()
    {
        freeplayStartButton.SetActive(false);
    }

    public void SelectCharacter(string characterName)
    {

        int i = 0; // for what player is selected (1)

        while (i < Player1Characters.Length)
        {
            if (Player1Characters[i] == characterName)
            {
                currentSelectingPlayer = 1;
                break;
            }

            i++;
        }

        i = 0; // for what player is selected (2)

        while (i < Player2Characters.Length)
        {
            if (Player2Characters[i] == characterName)
            {
                currentSelectingPlayer = 2;
                break;
            }

            i++;
        }

        //print(currentSelectingPlayer);

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

        // auto-set music

        if (currentySelectingMusic == "None") // auto set music if no song was already chosen
        {
            switch (characterName)
            {
                case "Mo":
                    currentySelectingMusic = "Storyteller";
                    trackNameText.text = "Storyteller";
                    break;
            }
        }

        StartCheck();

    }

    public void SelectStage(string stageName)
    {
        currentlySelectingStage = stageName;
        stageNameText.text = stageName;

        StartCheck();
    }

    public void SelectMusic(string trackName)
    {
        currentySelectingMusic = trackName;
        trackNameText.text = trackName;

        StartCheck();
    }

    private void StartCheck()
    {
        if (player1CharacterName != "None" && player2CharacterName != "None" && currentySelectingMusic != "None" && currentlySelectingStage != "None")
        {
            freeplayStartButton.SetActive(true);
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

        if (battleMusicToggleButton.getMusicState())
        {
            fightSong.GetComponent<AudioSource>().Play();
        }
    }
}
