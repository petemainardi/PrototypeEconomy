using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
/**
 * Model the count of something that may be accumulated up to some limit or spent down to zero.
 */
// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
[CreateAssetMenu(fileName = "New ResourcePool", menuName = "ClickerGame/ResourcePool")]
public class ResourcePool : ScriptableObject
{
    // State ----------------------------------------------------------------------------
    [SerializeField] private int _max;
    public int Capacity
    {
        get => _max;
        set
        {
            _max = Mathf.Max(0, value);
            _val = Mathf.Min(_max, _val);
        }
    }

    [SerializeField] private int _val;
    public int Value => _val;

    public float PercentFull => this.Value / (float)this.Capacity;

    [Space, Space]
    public FloatEvent OnValueChange;
    // ----------------------------------------------------------------------------------

    // Adjust the current value, return the remainder, ----------------------------------
    // where remainder is positive if exceeding Capacity, negative if under minimum -----
    public int Expend(int n) => this.SetValue(_val - n);
    public void Expend_Event(int n) => this.Expend(n);
    public int Receive(int n) => this.SetValue(_val + n);
    public void Receive_Event(int n) => this.Receive(n);

    private int SetValue(int n)
    {
        _val = Mathf.Max(0, Mathf.Min(_max, n));
        this.OnValueChange.Invoke(this.PercentFull);
        return n - _val;
    }
    // ----------------------------------------------------------------------------------
}
// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
