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
        
    }

    public void GoCrazy(int damage)
    {
        currentSanity -= damage;
    }
}
