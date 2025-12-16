using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.WSA;
public class AttackManager : MonoBehaviour
{

    [SerializeField] private GameObject beamShotBeam;

    // each function name is the attack names

    // Attacks
    public void Punch(CharacterMain character)
    {

        Animator animator = character.GetAnimator();

        animator.SetTrigger("PunchTrigger");

       
    }

    public void Punch2(CharacterMain character)
    {

        Animator animator = character.GetAnimator();

        animator.SetTrigger("PunchTrigger");

        
    }

    public void Punch3(CharacterMain character)
    {

        Animator animator = character.GetAnimator();

        animator.SetTrigger("PunchTrigger");

       
    }

    public void BeamShot(CharacterMain character)
    {
         
        print("Beam Shot");

        Animator animator = character.GetAnimator();
        GameObject visualBody = character.GetPlayerVisual();

        character.changeMovementSpeed(0, false);

        animator.SetTrigger("BeamShotTrigger");

        StartCoroutine(BeamShotWait(visualBody, character));

    }

    private IEnumerator BeamShotWait(GameObject visualBody, CharacterMain character)
    {
        yield return new WaitForSeconds(.5f);

        GameObject beamShotPart = Instantiate(beamShotBeam, visualBody.transform);

       // Vector3 beamPosition = new Vector3(rightHand.transform.position.x, rightHand.transform.position.y + 0.8379999993f, rightHand.transform.position.z + 3.51999998f);

        Vector3 beamPosition = new Vector3(visualBody.transform.position.x, 0.920000017f, 4.0999999f);

        print(beamPosition);

        beamShotPart.transform.position = beamPosition;

        yield return new WaitForSeconds(0.2f);

        beamShotPart.tag = "Untagged";

        //beamShotPart.transform.SetParent(null, true);

        yield return new WaitForSeconds(2f);

        Destroy(beamShotPart);

        character.changeMovementSpeed(0, true);

        character.specialAttackCooldown = false;
    }
}
