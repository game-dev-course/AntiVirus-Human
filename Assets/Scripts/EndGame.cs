using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [Tooltip("The counter text of the player's viruses")]
    [SerializeField] private NumberField virusCounterField;

    [Tooltip("Player's maximum viruses amount")] [SerializeField]
    private int maxVirusesAmount;

    // Update is called once per frame
    void Update()
    {
        // Player lost because has more than maxVirusesAmount viruses
        if (virusCounterField.GetNumber() >= maxVirusesAmount)
        {
            SceneManager.LoadScene("LoseGameOver");
        }
    }
}
