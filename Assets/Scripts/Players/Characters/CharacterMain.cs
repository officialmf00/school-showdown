using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public abstract class CharacterMain : MonoBehaviour
{ 
    public enum Player
    {
        P1, P2
    }

    protected enum AttackCooldownTypes
    {
        BasicAttackCooldown,

    }

    public enum BodyParts
    {
        Head, LeftHand, RightHand, LeftLeg, RightLeg, Torso
    }


    [SerializeField] protected CharacterSO characterSO;

    [SerializeField] protected Player player;

    [SerializeField] protected GameObject leftHand;
    [SerializeField] protected GameObject rightHand;
    private SphereCollider LeftHandCollider;
    private SphereCollider RightHandCollider;

    [SerializeField] protected GameObject charVisual;

    [SerializeField] protected bool BasicAttackCooldown;
    public bool specialAttackCooldown;

    protected float charHealth;

    private Animator charAnimator;

    private Controls controls;

    [SerializeField] protected bool isAttacking;

    [SerializeField] protected ChangeImageSizeMain changeimgbar;

    protected float currentDealDamage;
    protected float currentGetHealth;

    private int lifeCount = 3;

    [SerializeField] protected Image[] lifeImages;

    private int lastAttackNum = 0;

    private bool canGetHit = true;

    [SerializeField] protected AttackManager attackManager;

    [SerializeField] protected MovementMain movementScript;
    private void Awake()
    {
        controls = new Controls();
        charAnimator = GetComponent<Animator>();
        charHealth = characterSO.Health;
        LeftHandCollider = leftHand.GetComponent<SphereCollider>();
        RightHandCollider = rightHand.GetComponent<SphereCollider>();
        isAttacking = false;
    }

    private void OnEnable()
    {

        if (player == Player.P1)
        {
            controls.Fighting.SpecialAttack1P1.performed += PerformAttack1;
            controls.Fighting.SpecialAttack2P1.performed += PerformAttack2;
            controls.Fighting.SpecialAttack3P1.performed += PerformAttack3;
            controls.Fighting.SpecialAttack4P1.performed += PerformAttack4;

            controls.Fighting.BasicAttack1P1.performed += PerformBasicAttack;

            controls.Fighting.SpecialAttack1P1.Enable();
            controls.Fighting.SpecialAttack2P1.Enable();
            controls.Fighting.SpecialAttack3P1.Enable();
            controls.Fighting.SpecialAttack4P1.Enable();

            controls.Fighting.BasicAttack1P1.Enable();
        }

        else if (player == Player.P2)
        {
            controls.Fighting.SpecialAttack1P2.performed += PerformAttack1;
            controls.Fighting.SpecialAttack2P2.performed += PerformAttack2;
            controls.Fighting.SpecialAttack3P2.performed += PerformAttack3;
            controls.Fighting.SpecialAttack4P2.performed += PerformAttack4;

            controls.Fighting.BasicAttack1P2.performed += PerformBasicAttack;

            controls.Fighting.SpecialAttack1P2.Enable();
            controls.Fighting.SpecialAttack2P2.Enable();
            controls.Fighting.SpecialAttack3P2.Enable();
            controls.Fighting.SpecialAttack4P2.Enable();

            controls.Fighting.BasicAttack1P2.Enable();
        }
    }

    private void OnDisable()
    {
        controls.Fighting.SpecialAttack1P1.Disable();
        controls.Fighting.SpecialAttack2P1.Disable();
        controls.Fighting.SpecialAttack3P1.Disable();
        controls.Fighting.SpecialAttack4P1.Disable();
        controls.Fighting.SpecialAttack1P2.Disable();
        controls.Fighting.SpecialAttack2P2.Disable();
        controls.Fighting.SpecialAttack3P2.Disable();
        controls.Fighting.SpecialAttack4P2.Disable();

        controls.Fighting.BasicAttack1P1.Disable();
        controls.Fighting.BasicAttack1P2.Enable();
    }

    protected void PerformBasicAttack(InputAction.CallbackContext ctx)
    {
        switch (lastAttackNum)
        {
            case 0:
                ChooseAttack("Punch1");
                lastAttackNum = 1;
                break;
            case 1:
                ChooseAttack("Punch2");
                lastAttackNum = 2;
                break;
            case 2:
                ChooseAttack("Punch3");
                lastAttackNum = 0;
                break;

        }
    }
    protected void PerformAttack1(InputAction.CallbackContext ctx)
    {
        ChooseAttack(characterSO.specialMove1.name);
    }

    protected void PerformAttack2(InputAction.CallbackContext ctx)
    {
        ChooseAttack(characterSO.specialMove1.name);
    }

    protected void PerformAttack3(InputAction.CallbackContext ctx)
    {
        ChooseAttack(characterSO.specialMove1.name);
    }

    protected void PerformAttack4(InputAction.CallbackContext ctx)
    {
        ChooseAttack(characterSO.specialMove1.name);
    }

    public GameObject GetBodyPart(BodyParts bodyParts)
    {
        switch (bodyParts)
        {
            case BodyParts.LeftHand:
                return leftHand;

            case BodyParts.RightHand:
                return rightHand;

        }
        return null;
    }

    public Animator GetAnimator()
    {
        return charAnimator;
    }

    public Player GetPlayerEnum()
    {
        return player;
    }

    public GameObject GetPlayerVisual()
    {
        return charVisual;
    }

    // for character selection

    public void ChangeImageBar(ChangeImageSizeMain imageValue)
    {
        changeimgbar = imageValue;
    }

    public void ChangeLifeImage(int lifeImageNumber, Image imageValue)
    {
        lifeImages[lifeImageNumber] = imageValue;
    }

    public void ChangeAttackManager(AttackManager attackManagerValue)
    {
        attackManager = attackManagerValue;
    }

    //

    public void setCurrentDealDamage(float damageNumber)
    {
        currentDealDamage = damageNumber;
    }

    public void DealDamage(float damageValue)
    {
        charHealth -= damageValue;
    }
    public float getHealth()
    { return charHealth; }
    public float getSOHealth()
    { return characterSO.Health; }

    public void changeMovementSpeed(float speed, bool revertSpeed)
    {
        movementScript.changeMovementSpeed(speed, revertSpeed);
    }
    private void ChooseAttack(string attackName)
    {
        switch (attackName)
        {
            case "Punch1":
                if (BasicAttackCooldown == false)
                {
                    BasicAttackCooldown = true;
                    isAttacking = true;
                    setCurrentDealDamage(characterSO.basicAttackDamage);
                    attackManager.Punch(this);
                    StartCoroutine(StartAttackCooldown(AttackCooldownTypes.BasicAttackCooldown));

                }
                break;

            case "Punch2":
                if (BasicAttackCooldown == false)
                {
                    BasicAttackCooldown = true;
                    isAttacking = true;
                    setCurrentDealDamage(characterSO.basicAttackDamage);
                    attackManager.Punch2(this);
                    StartCoroutine(StartAttackCooldown(AttackCooldownTypes.BasicAttackCooldown));

                }
                break;

            case "Punch3":
                if (BasicAttackCooldown == false)
                {
                    BasicAttackCooldown = true;
                    isAttacking = true;
                    setCurrentDealDamage(characterSO.basicAttackDamage);
                    attackManager.Punch3(this);
                    StartCoroutine(StartAttackCooldown(AttackCooldownTypes.BasicAttackCooldown));

                }
                break;

            default:
                if (specialAttackCooldown == false)
                {
                    specialAttackCooldown = true;
                    ScriptableObject attackObject = characterSO.specialMove1;
                    string useAttackName = attackObject.name;
                    useAttackName = useAttackName.Replace(" ", "");
                    // print(useAttackName);
                    setCurrentDealDamage(characterSO.specialMove1.attackDamage);
                    attackManager.SendMessage(useAttackName, this);
                }
                break;



        }
    }

    private IEnumerator StartAttackCooldown(AttackCooldownTypes cdType)
    {
        switch (cdType)
        {
            case AttackCooldownTypes.BasicAttackCooldown:
                rightHand.gameObject.tag = "AttackingTag";
                yield return new WaitForSeconds(characterSO.basicAttackCooldown);
                rightHand.gameObject.tag = "Player";
                BasicAttackCooldown = false;
                isAttacking = false;
                break;
        }
    }

    private IEnumerator CanGetHitCooldown()
    {
        yield return new WaitForSeconds(0.15f);
        canGetHit = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        CharacterMain otherCharMain = other.GetComponentInParent<CharacterMain>();

        Player theOtherCharMainPlayer;

        Player theThisCharMainPlayer;

        if (otherCharMain != null)
        {
            //print(otherCharMain.gameObject.name + " " + player);
            theOtherCharMainPlayer = otherCharMain.player;

            CharacterMain thisCharMain = GetComponent<CharacterMain>();
            theThisCharMainPlayer = thisCharMain.player;

            if (theOtherCharMainPlayer != theThisCharMainPlayer && other.gameObject.tag == "AttackingTag" && canGetHit == true)
            {
                //print(otherCharMain.gameObject.name + " " + player);
                theOtherCharMainPlayer = otherCharMain.player;

                thisCharMain = GetComponent<CharacterMain>();
                theThisCharMainPlayer = thisCharMain.player;
                canGetHit = false;
                print("hit successful on " + theThisCharMainPlayer + ", now dealing " + otherCharMain.currentDealDamage + " damage");
                // Debug.Log("Other Char: " + character.name + " This Char: " + this.name);
                currentGetHealth = getHealth();
                DealDamage(otherCharMain.currentDealDamage);
                //print("Dealt damage to character");
                isAttacking = false;
                changeimgbar.changeHealth(currentGetHealth, -otherCharMain.currentDealDamage);
                StartCoroutine(CanGetHitCooldown());
            }
            else
            {
                print(player + "getCharacterMain = " + other.GetComponent<CharacterMain>() != null + " isAttacking = " + isAttacking);

            }
        }
    }

    private void Update()
    {
        if (charHealth < 1)
        {
            charHealth = characterSO.Health;
            changeimgbar.changeHealth(getSOHealth(), getSOHealth());
            lifeImages[lifeCount - 1].enabled = false;
            lifeCount -= 1;

            if (lifeCount <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
