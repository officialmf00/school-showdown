using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()] 
public class CharacterSO : ScriptableObject
{
    // SO = ScriptableObject

    public string characterName;

    public GameObject characterPrefab; // Add attack scripts to prefab

    public AttackSO specialMove1;

    public AttackSO specialMove2;

    public AttackSO specialMove3;
    
    public AttackSO specialMove4;

    public int Health;

    public float basicAttackDamage;

    public float basicAttackCooldown;

}
