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
public class CombatEnemy : MonoBehaviour, ICombattant
{
    // Fields & Properties ========================================================================
    [Header("Resource Pools")]
    [SerializeField] private ResourcePool _health;
    public ResourcePool HealthPool => _health;

    [SerializeField, Tooltip("When health percentage is reduced below each of these values, unlock abilities for that stage")]
    List<float> _stageHealthThresholds = new List<float>();

    private EnemyAbility[] _abilities;

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
        _abilities = this.GetComponents<EnemyAbility>();
        _health.OnValueChange.AddListener(CheckStageUnlocks);
        this.CheckStageUnlocks(_health.PercentFull);

        // For prototype purposes, just assume all player characters are our targets
        // TODO: actually implement a targeting system...
        this.Targets = GameObject.FindObjectsOfType<CombatCharacter>().Cast<ICombattant>().ToList();
    }

    private void Update()
    {
        this.SelectAbilitiesToUse().ForEach(a => a.Execute());  // Not great for efficiency, but hey, it's a prototype
    }
    // ============================================================================================

    // Methods ====================================================================================
    /// Lock/unlock abilities based on the current behavior stage, which is represented by the index
    /// of the lowest threshold percentage under which the health value has reached.
    public virtual void CheckStageUnlocks(float percentFull)
    {
        bool lowestThreshHit = false;
        List<float> thresholds = _stageHealthThresholds.OrderByDescending(f => f).ToList();
        for (int i = thresholds.Count - 1; i > -1; i--)
        {
            if (percentFull < thresholds[i])
            {
                _abilities
                    .Where(a => a.Stage == i + 2)   // i+2 b/c stage 1 is considered unlocked at full health
                    .ToList()
                    .ForEach(a => a.IsUnlocked = !lowestThreshHit || !a.LockOnNextStage);
                
                lowestThreshHit = true;
            }
        }
    }

    /// Choose which abilities to use at this time. Currently just using all available ones at once
    /// as each becomes available.
    /// 
    /// Override this function to change selection behavior, e.g. if you were only allowed to
    /// choose one ability every N seconds regardless of how many are available, etc.
    public virtual List<EnemyAbility> SelectAbilitiesToUse()
    {
        return _abilities.Where(a => a.IsUnlocked && a.IsAvailable).ToList();
    }
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
