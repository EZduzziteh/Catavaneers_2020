using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTrap : MonoBehaviour
{

    [SerializeField] bool IsTriggered;
    [SerializeField] bool IsOverTime;
    [SerializeField] bool IsCatNip;
    [SerializeField] float trap_duration;
    [SerializeField] float trap_impact;

    float timer;
    CharacterStats playerreference;

    


    // Update is called once per frame
    void Update()
    {
        if (IsOverTime) {
            //check trap is triggered
            if (IsTriggered)
            {
                //checks if the timer has elapsed
                if (Time.time > timer)
                {
                    //Reset the trap


                    if (IsCatNip)
                    {
                        //reset the player speed
                        playerreference.ModifySpeed(trap_impact);
                    }
                    else
                    {
                        //reset the player speed
                        playerreference.ModifySpeed(-trap_impact);
                    }
                  

                    IsTriggered = false;

                    //#TODO Add destroy functionality
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsOverTime)
        {
           
            //reset player speed
            if (IsCatNip)
            {
                playerreference.ModifySpeed(trap_impact);
            }
            else
            {
                playerreference.ModifySpeed(-trap_impact);
            }
            //reset trap
            IsTriggered = false;
        }

    }

    private void OnTriggerEnter(Collider collision)
    {

        
        //Only handle collision if trap is not already triggered.
        if (!IsTriggered)
        {
            //check if collided object was a player
            if (collision.gameObject.tag == "Player")
             
            {
                //get a reference to the character controller on collided player
                playerreference = collision.gameObject.GetComponent<CharacterStats>();

                if (IsCatNip)
                {
                    playerreference.ModifySpeed(-trap_impact);
                }
                else
                {
                    playerreference.ModifySpeed(trap_impact);
                }
                
                //set trap as triggered so it cant be triggered mutliple times at once.
                IsTriggered = true;
                
                //Set timer to equal current time our trap duration
                timer = Time.time + trap_duration;
              
               

            }
        }
        

        //else if...tag==enemy
        //..modify enemy speed value
    }
}
