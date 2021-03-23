using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
/**
 * Functionality to react to when the specified ResourcePool's Value reaches its Capacity.
 */
// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
public class DetectResourceOnFull : MonoBehaviour
{
    [SerializeField] private ResourcePool _pool;
    [SerializeField] private UnityEvent _onFull;

    private void Start()
    {
        _pool.OnValueChange.AddListener(CheckForFull);
        this.CheckForFull(_pool.PercentFull);
    }

    private void CheckForFull(float percentage)
    {
        if (percentage == 1)
            _onFull.Invoke();
    }
}
// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
