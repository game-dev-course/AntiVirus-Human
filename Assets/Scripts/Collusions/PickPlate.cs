using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickPlate : MonoBehaviour
{
    [Tooltip("The tag of the plates pile")]
    [SerializeField] string pileTag;

    [Tooltip("Number of plates in the current pile")] [SerializeField]
    private int platesNum;
    
    [SerializeField] InputAction pickupKey = new InputAction(type: InputActionType.Button);

    private bool hasReachedPile = false;
    private bool isHoldingPlate = false;


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
        if (other.CompareTag(pileTag))
        {
            hasReachedPile = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(pileTag))
        {
            hasReachedPile = false;
        }
    }

    private void Update()
    {
        if (hasReachedPile && pickupKey.WasPressedThisFrame() && !isHoldingPlate)
        {
            isHoldingPlate = true;
            Destroy(GameObject.Find("Plate" + platesNum));
            platesNum--;
        }
    }
    
    public bool IsHoldingPlate()
    {
        return isHoldingPlate;
    }

    public void SetHoldPlate(bool isHolding)
    {
        isHoldingPlate = isHolding;
    }

    public int GetPlatesAmount()
    {
        return platesNum;
    }
}
