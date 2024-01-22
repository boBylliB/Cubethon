using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    void Update()
    {
        // Deliberately putting this in the Update and not the FixedUpdate so that it still runs when timescale is 0
        if (Input.GetKeyDown("escape"))
        {
            togglePause();
        }
    }

    public void togglePause()
    {
        FindObjectOfType<GameManager>().togglePause();
    }
}
