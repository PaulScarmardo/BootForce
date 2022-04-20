using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    int counter = 0; // minimum
    private bool canJump = false;
    private int fps = 60;

    void Start()
    { // set the framerate to 60 fps
        Application.targetFrameRate = fps;
    }
    void Update()
    {   // a simple log
        Debug.Log("Force : " + counter + " Degree : " + Mathf.FloorToInt(counter / fps));
        // an event on mouse
        if (Input.GetKey("space") && canJump==true)
        { // keep the left mouse button pressed so as to increase foreAmount value
            counter++;
            // give it a max value
            if (counter > 180)
                counter = 180; // because 3*60 = 180
        }
        // release left mouse button
        if (Input.GetKeyUp("space")&&canJump ==true)
        {   // reinitialize forceAmount value at start
            int forceAmount;
            // compute forceAmount according counter value
            if (counter > 120)
                forceAmount = 3;
            else if (counter > 60)
                forceAmount = 2;
            else
                forceAmount = 1;
            // mouse position
            Vector3 posInScreen = Camera.main.WorldToScreenPoint(transform.position);
            // so compute direction and normalize it
            Vector3 dirToMouse = Input.mousePosition - posInScreen;
            dirToMouse.Normalize();
            // adding the force to the 2D Rigidbody according forceAmount value
            GetComponent<Rigidbody2D>().AddForce(dirToMouse * 100 * forceAmount);
            // reinitialize counter value
            counter = 0;
        }
    }

    void OnCollisionStay2D(Collision2D Object1)
    {
        canJump = true;
    }

    void OnCollisionExit2D(Collision2D Object1)
    {
        canJump = false;
    }
}