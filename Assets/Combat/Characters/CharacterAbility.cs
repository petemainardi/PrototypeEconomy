using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
/**
 * Allow the character to do something in combat, based on the availability of the associated
 * resource pool, charges, and cooldown time.
 * 
 * Call Execute() from a click event to link this to the UI.
 */
// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
public class CharacterAbility : MonoBehaviour
{
    // Fields & Properties ========================================================================
    [SerializeField] private string _name;
    public string AbilityName => _name;

    [SerializeField] private bool _unlocked = true;
    public bool IsUnlocked
    {
        get => _unlocked;
        set
        {
            _unlocked = value;
            _onAvailable.Invoke(this.IsAvailable);
        }
    }

    [Header("Resource Usage")]
    [SerializeField] private ResourcePool _source;
    [SerializeField, Range(0, 100)] private int _cost;
    public int Cost => _cost;

    [Space, Space]
    [SerializeField, Range(1, 100)] private int _maxCharges = 1;
    [SerializeField] private int _currentCharges = 1;
    public int MaxCharges => _maxCharges;
    public int ChargesLeft => _currentCharges;

    [Space, Space]
    [SerializeField] private UnityEvent _onExecute;

    [Header("Animation")]
    [SerializeField] private Animator _animator;
    [SerializeField] private string _animState;

    [Header("Cooldown")]
    [SerializeField] private float _cooldown;
    private float _cooldownTimer;
    public float Cooldown => _cooldown;
    public float TimeRemaining => Mathf.Max(0, _cooldownTimer);

    [Space]
    [SerializeField] private UnityEvent _onCooldownReset;
    [SerializeField] private BoolEvent _onAvailable;
    private bool _isOnCooldown = false;
    public bool IsAvailable => this.IsUnlocked && !_isOnCooldown && (_source == null || _source.Value >= _cost);
    // ============================================================================================

    // Monobehavior ===============================================================================
    private void Update()
    {
        _cooldownTimer -= Time.deltaTime;
        if (_cooldownTimer <= 0 && _isOnCooldown)
        {
            _isOnCooldown = false;
            if (_currentCharges <= 0)
                _currentCharges = _maxCharges;

            _onCooldownReset.Invoke();
        }
    }
    // ============================================================================================

    // Methods ====================================================================================
    // Trigger the ability and associated events ----------------------------------------
    public void Execute()
    {
        if (this.IsAvailable)
        {
            if (_source != null)
                _source.Expend(_cost);

            _currentCharges--;
            _cooldownTimer = _cooldown  // Only use cooldownTime if no charges remaining
                * Mathf.Clamp((_maxCharges - _currentCharges) / _maxCharges, 0.001f, 1);
            _isOnCooldown = true;

            if (_animator != null)
            {
                if (!string.IsNullOrEmpty(_animState))
                    _animator.Play(_animState);
            }

            _onExecute.Invoke();
        }
    }
    // ----------------------------------------------------------------------------------
    // ============================================================================================
}
// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
