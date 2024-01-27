using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSanity : MonoBehaviour
{
    public int maxSanity = 100;
    public int currentSanity;

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
           // Destroy(gameObject);
        }
    }

    public void GoCrazy(int damage)
    {
        currentSanity -= damage;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Insanity Area"))
        {
            GoCrazy(1);

        }
    }
}
