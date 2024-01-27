using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityDamage : MonoBehaviour
{
    private float timer = 0;
    private int initialSanity;
    PlayerSanity playerSanity;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           initialSanity = playerSanity.currentSanity;

        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timer--;
            playerSanity.GoCrazy(1);
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int predictedSanity = initialSanity - (int)timer;
            if (playerSanity.currentSanity != predictedSanity)
            {
                playerSanity.GoCrazy(playerSanity.currentSanity - predictedSanity);
            }
            timer = 0;
        }
    }

}
