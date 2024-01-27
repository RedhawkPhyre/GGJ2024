using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSanity : MonoBehaviour
{
    public int maxSanity = 100;
    public int currentSanity;
    private int timer;
    private int rate = 10;

    // Start is called before the first frame update
    void Start()
    {
        currentSanity = maxSanity;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSanity <= 0)
        {
           Destroy(gameObject);
        }
    }

    public void GoCrazy(int damage)
    {
        currentSanity -= damage;
    }

    void OnTriggerStay(Collider other)
    {
        timer++;
        if (other.gameObject.CompareTag("Insanity Area") && timer % rate == 0)
        {
            GoCrazy(1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        timer = 0;
    }
}
