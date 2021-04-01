using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
/**
 * Example script of a specific implementation of a CombatCharacter.
 * 
 * The purpose of a CombatCharacter subclass like this would be to house any relevent
 * implementation of an ability effect, or data pertininent to this character, that differs from
 * the default capabilities.
 */
// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
public class ExampleCharacter : CombatCharacter
{

    // Abilities ==================================================================================
    [Header("Ability Attributes")]

    [SerializeField] private int _basicAttackDamage = 2;
    public void BasicAttack()
    {
        if (this.Targets.Count > 0)
        {
            this.Targets.ForEach(t => t.TakeDamage(_basicAttackDamage));
        }
    }

    [SerializeField] private int _heavyAttackDamage = 10;
    public void HeavyAttack()
    {
        if (this.Targets.Count > 0)
        {
            this.Targets.ForEach(t => t.TakeDamage(_heavyAttackDamage));
        }
    }

    [SerializeField] private int _autoXP = 1;
    public void AutoXP()
    {
        this.XpPool.Receive(this._autoXP);
    }
    // ============================================================================================
}
// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
