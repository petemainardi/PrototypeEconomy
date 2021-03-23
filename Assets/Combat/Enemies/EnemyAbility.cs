using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
/**
 * Allow the enemy to do something in combat, flagged by the behavior stage of the enemy in which
 * this ability becomes available.
 */
// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
public class EnemyAbility : CharacterAbility
{
    [Header("Enemy Stage For Unlock")]
    [SerializeField, Tooltip("Behavior stage at which this ability unlocks. Stage 1 is unlocked to begin with.")]
    private int _stage;
    public int Stage => _stage;

    [SerializeField, Tooltip("Whether to lock this ability once the stage past this ability's stage is reached.")]
    private bool _lockOnNextStage;
    public bool LockOnNextStage => _lockOnNextStage;
}
// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
