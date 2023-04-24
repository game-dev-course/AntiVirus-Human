using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickGloves : MonoBehaviour
{
    [Tooltip("The tag of the gloves")]
    [SerializeField] string glovesTag;

    [SerializeField] InputAction pickupKey = new InputAction(type: InputActionType.Button);

    private bool hasReachedGloves = false;
    private bool isHoldingGloves = false;
    private Collider2D _collider2D;

    private void OnEnable()
    {
        pickupKey.Enable();
    }

    private void OnDisable()
    {
        pickupKey.Disable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Checking if the player entered the plates pile and pressed the key
        if (other.CompareTag(glovesTag))
        {
            hasReachedGloves = true;
            _collider2D = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(glovesTag))
        {
            hasReachedGloves = false;
            _collider2D = null;
        }
    }

    private void Update()
    {
        if (hasReachedGloves && pickupKey.WasPressedThisFrame() && !isHoldingGloves)
        {
            isHoldingGloves = true;
            Destroy(_collider2D.GameObject());
        }
    }
    
    public bool IsHoldingGloves()
    {
        return isHoldingGloves;
    }

    public void SetHoldGloves(bool isHolding)
    {
        isHoldingGloves = isHolding;
    }
}
