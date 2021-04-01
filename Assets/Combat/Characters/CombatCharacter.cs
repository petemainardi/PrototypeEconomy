using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
/**
 * Behavior of a player-controlled character that participates in combat.
 */
// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
public class CombatCharacter : MonoBehaviour, ICombattant
{
    // Fields & Properties ========================================================================
    [Header("Resource Pools")]
    [SerializeField] private ResourcePool _health;
    [SerializeField] private ResourcePool _xp;
    [SerializeField] private ResourcePool _gold;
    public ResourcePool HealthPool => _health;
    public ResourcePool XpPool => _xp;
    public ResourcePool GoldPool => _gold;

    [Header("Level")]
    [SerializeField] private AnimationCurve _xpPerLevelUp;
    [SerializeField] private int _currentLevel = 1;
    //[SerializeField] private UnityEvent _onLevelUp;

    private CharacterAbility[] _abilities;

    [Header("Events")]
    [SerializeField] private UnityEvent _onTakeDamage;
    [SerializeField] private UnityEvent _onHeal;
    [SerializeField] private UnityEvent _onDeath;
    public UnityEvent OnTakeDamage => _onTakeDamage;
    public UnityEvent OnHeal => _onHeal;
    public UnityEvent OnDeath => _onDeath;

    // ============================================================================================

    // Mono =======================================================================================
    private void Start()
    {
        _abilities = this.GetComponentsInChildren<CharacterAbility>();

        // For prototype purposes, just assume there is one enemy and that is our target
        // TODO: actually implement a targeting system...
        this.Targets = new List<ICombattant>() { GameObject.FindObjectOfType<CombatEnemy>() as ICombattant };
    }

    // ============================================================================================

    // Methods ====================================================================================
    public virtual void GainXP(int numXP) => _xp.Receive(numXP);
    public virtual void LevelUp()
    {
        if (_xp.PercentFull == 1)
        {
            _xp.Expend(_xp.Value);
            _xp.Capacity = (int)_xpPerLevelUp.Evaluate(++_currentLevel);
        }
    }

    // Assume ability components are ordered on the gameobject by unlock order
    public virtual CharacterAbility NextAbilityToUnlock => _abilities.FirstOrDefault(a => !a.IsUnlocked);
    public virtual void UnlockNextAbility() => this.NextAbilityToUnlock.IsUnlocked = true;
    // ============================================================================================

    // ICombattant ================================================================================
    public List<ICombattant> Targets { get; set; }
    
    public virtual void TakeDamage(int dmg)
    {
        _health.Expend(Mathf.Max(0, dmg));
        _onTakeDamage.Invoke();
    }
    public virtual void Heal(int healing)
    {
        _health.Receive(Mathf.Max(0, healing));
        _onHeal.Invoke();
    }
    public virtual void Die()
    {
        Debug.LogWarning("Not yet implemented...");
        // TODO
        _onDeath.Invoke();
    }
    // ============================================================================================
}
// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
