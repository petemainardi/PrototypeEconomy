using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
/**
 * Functionality to update a fillable image.
 */
// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
[RequireComponent(typeof(Image))]
public class ResourceFillbar : MonoBehaviour
{
    private Image _image;

    [SerializeField] private ResourcePool _pool;

    private void Awake()
    {
        _image = this.GetComponent<Image>();
    }

    private void Start()
    {
        _pool.OnValueChange.AddListener(this.UpdateFill);
        this.UpdateFill(_pool.PercentFull);
    }

    public void UpdateFill(float percentage)
    {
        _image.fillAmount = Mathf.Clamp01(percentage);
    }
}
// ================================================================================================
// ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
// ================================================================================================
