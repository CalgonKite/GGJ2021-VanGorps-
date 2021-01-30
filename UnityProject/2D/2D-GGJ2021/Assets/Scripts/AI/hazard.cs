using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hazard : MonoBehaviour
{
    PlayerProfile pStats; /// Ease of access for script ///

    private void Start()
    {
        pStats = GameObject.FindWithTag("Player").GetComponent<PlayerProfile>(); /// Find script via tags and assign it ///
    }

    private void OnCollisionEnter(Collision collision) /// Collision detection ///
    {
        if (collision.gameObject.tag == "Player") /// IF the player is hit //
        {
            pStats.changeHP(1); /// Call damage function ///
        }
    }
}