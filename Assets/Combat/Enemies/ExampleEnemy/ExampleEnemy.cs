using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
/**
 * Example script of a specific implementation of a CombatEnemy.
 * 
 * The purpose of a CombatEnemy subclass like this would be to house any relevent implementation of
 * an ability effect, or data pertininent to this character, that differs from the default
 * capabilities.
 */
// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
public class ExampleEnemy : CombatEnemy
{

    // Abilities ==================================================================================
    [Header("Ability Attributes")]
    [SerializeField] private int _stage1AttackDamage = 5;

    public void Stage1Attack()
    {
        if (this.Targets.Count > 0)
        {
            this.Targets.ForEach(t => t.TakeDamage(_stage1AttackDamage));
        }
    }


    [SerializeField] private int _stage2AttackDamage = 7;
    public void Stage2Attack()
    {
        if (this.Targets.Count > 0)
        {
            this.Targets.ForEach(t => t.TakeDamage(_stage2AttackDamage));
        }
    }


    [SerializeField] private int _stage3AttackDamage = 10;
    public void Stage3Attack()
    {
        if (this.Targets.Count > 0)
        {
            this.Targets.ForEach(t => t.TakeDamage(_stage3AttackDamage));
        }
    }
    // ============================================================================================
}
// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
