using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    // Setting variables to public alows us to acces them within the Unity editor.
    // This allows us to change variable or point them to game objects.
    public float displayTime = 4.0f;
    public GameObject dialogBox;

    // Variables default to private, so the private keyword is unnecessary.
    float timerDisplay;

    // Start is called before the first frame update (Auto Generated).
    void Start()
    {
        //This makes sure the dialog box is disabled
        dialogBox.SetActive(false); 

        // This makes sure the dialog box doesn't display when it is not supposed to.
        timerDisplay = -1.0f;
    }

    // Update is called once per frame (Auto Generated).
    void Update()
    {
        if(timerDisplay >= 0) // This checks if the dialog box is being displayed.
        {
            // This decrements the timer.
            timerDisplay -= Time.deltaTime;

            // This hides the dialog box when the timer reaches 0.
            if (timerDisplay < 0) 
            {
                dialogBox.SetActive(false);
            }
        }
    }

    // This function displays the dialog box for the amount of time specified.
    public void DisplayDialog()
    {
        timerDisplay = displayTime;

        // Makes the dialog active, displaying it in the game.
        dialogBox.SetActive(true);
    }
}
