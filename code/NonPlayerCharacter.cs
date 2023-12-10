using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    // lengd sýnitíma textans
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    float timerDisplay;

    void Start()
    {
        // byrjar á að fela diologBox
        dialogBox.SetActive(false);
        // -1 í tímanum
        timerDisplay = -1.0f;
    }

    void Update()
    {
        // ef tíminn er meira eða = 0
        if (timerDisplay >= 0)
        {
            // tímin lækkar
            timerDisplay -= Time.deltaTime;
            // ef timerinn er minna en 0 þá hverfur tíminn aftur
            if (timerDisplay < 0)
            {
                dialogBox.SetActive(false);
            }
        }
    }

    public void DisplayDialog()
    {
        // timerinn endursettur
        timerDisplay = displayTime;
        // sýnt textan
        dialogBox.SetActive(true);
    }
}
