using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
/**
 * Oscillate the opacity of the color of a SpriteRenderer to create a blinking effect.
 */
// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
[RequireComponent(typeof(SpriteRenderer))]
public class BlinkSprite : MonoBehaviour
{
    private SpriteRenderer _renderer;
    [SerializeField] private float _minOpacity = 0.35f;
    [SerializeField] private float _cycleOffset = 8f;
    private Coroutine _blinkRoutine;

    private void Awake()
    {
        _renderer = this.GetComponent<SpriteRenderer>();
    }

    public void Blink(float duration)
    {
        if (_blinkRoutine != null)
            StopCoroutine(_blinkRoutine);

        _blinkRoutine = StartCoroutine(this.BlinkOpacity(duration));
    }

    private IEnumerator BlinkOpacity(float duration)
    {
        Color c = _renderer.color;

        while (duration > 0)
        {
            duration -= Time.deltaTime;

            c.a = Math.Max(
                this._minOpacity,
                0.5f + 0.5f * Mathf.Cos(2 * (float)Math.Pow(duration + this._cycleOffset, 2))
                );
            _renderer.color = c;

            yield return null;
        }

        c.a = 1;
        _renderer.color = c;
    }
}
// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
