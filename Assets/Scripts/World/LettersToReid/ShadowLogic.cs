using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShadowLogic : Monobehavior
{
    public RigidBody2D playerBody;

    private float shadowSpeed;
    private float maxSpeed;
    private int memoryCount;

    void Awake()
    {
        //Some arbitrary max speed idk how fast a fast max speed is
        this.maxSpeed = 10.0f;
    }

    void Update()
    {
        //Changes direction towards player
        //Unsure if LookAt is appropriate for this
        GameObject player = GameObject.Find("Player");
        transform.LookAt(player.transform.position, transform.up);
        if (memoryCount < 9)
        {
            transform.Translate(Vector2.forward * shadowSpeed);
        }
        else if (memoryCount >= 9 && memoryCount <= 17)
        {
            //Unfinialized distance
            if (this.getDist(this, player) <= 10)
            {
                transform.Translate(Vector2.forward * shadowSpeed);
            }
        }
        //TODO
        //Implement mechanic for traveling between two memories in maps 4 and 5
    }

    private void updateSpeed()
    {
        // TODO
        // Replace parameter of Find methods with actual names of those objects
        GameObject player = GameObject.Find("Player");
        GameObject memory = GameObject.Find("Memory");

        //If player closer to memory than figure
        if (this.getDist(player, memory) < this.getDist(this, memory))
        {
            if (this.dist(player, memory) >= 2 * this.maxSpeed)
            {
                //arbitrary low speed
                this.shadowSpeed = 1.0f;
            }
            else
            {
                this.shadowSpeed = (this.maxSpeed - (1 / 2) * (this.getDist(player, memory)));
            }
        }
        //If player closer to figure than memory
        else if (this.getDist(player, figure) < this.getDist(this, memory))
        {
            this.shadowSpeed = (1 / 2) * this.getDist(player, figure);
        }

    }

    private float getDist(GameObject first, GameObject second)
    {
        return Vector2.Distance(first.transform.position, other.transform.position);
    }
}