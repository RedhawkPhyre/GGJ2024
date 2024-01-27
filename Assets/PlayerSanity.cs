using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSanity : MonoBehaviour
{
    public float maxSanity = 100;
    public float currentSanity;

    private int timer;
    private int rate = 10;

    public Image sanityBar;
    

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
           //Destroy(gameObject);
        }
        sanityBar.fillAmount = Mathf.Clamp(currentSanity/maxSanity,0,1);s
    }

    public void GoCrazy(float damage)
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
