using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()] 
public class AttackSO : ScriptableObject
{
    // SO = ScriptableObject

    public float attackDamage;

    public float attackCooldown;

    public GameObject mainAttackPart;

}
