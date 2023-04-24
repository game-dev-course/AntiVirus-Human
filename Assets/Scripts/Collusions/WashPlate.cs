using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WashPlate : MonoBehaviour
{
    [Tooltip("Sink washer tag")]
    [SerializeField] private string SinkWasherTag;
    
    [Tooltip("Amount of virus to increase when player washed without gloves")]
    [SerializeField] private int virusUnhygienicWashAmount;
    
    [Tooltip("Amount of virus to increase when player washed with gloves")]
    [SerializeField] private int virusHygienicWashAmount;
    
    [Tooltip("The counter text of the player's viruses")]
    [SerializeField] private NumberField virusCounterField;

    [SerializeField] private SpriteRenderer emptySink;
    [SerializeField] private SpriteRenderer fullSink;
    
    [Tooltip("Player's maximum viruses amount")] [SerializeField]
    private int maxVirusesAmount;

    private PickPlate _platePickup;
    private PickGloves _glovesPickup;
    
    private void Start()
    {
        fullSink.enabled = false;
        _platePickup = FindObjectOfType<PickPlate>();
        _glovesPickup = FindObjectOfType<PickGloves>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(SinkWasherTag))
        {
            Debug.Log("Washing.....");

            if (_platePickup != null && _platePickup.IsHoldingPlate())
            {
                // Destroy(platePickup.gameObject);
                _platePickup.SetHoldPlate(false);
                Debug.Log("Washing...");

                // Player wears hygienic wash gloves
                if (_glovesPickup.IsHoldingGloves())
                {
                    virusCounterField.AddNumber(virusHygienicWashAmount);
                    _glovesPickup.SetHoldGloves(false);
                }
                else
                {
                    virusCounterField.AddNumber(virusUnhygienicWashAmount);
                }
                emptySink.enabled = false;
                fullSink.enabled = true;

                // Player won the game (washed all dishes without reaching to max amount of viruses).
                if (_platePickup.GetPlatesAmount() == 0 && virusCounterField.GetNumber() < maxVirusesAmount)
                {
                    SceneManager.LoadScene("WinGameOver");

                }
            }
        }
    }
}
