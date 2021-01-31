using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hazard : MonoBehaviour
{
    public PlayerProfile pStats; /// Ease of access for script ///

    private void Start()
    {
        pStats = GameObject.FindWithTag("Player").GetComponent<PlayerProfile>(); /// Find script via tags and assign it ///
    }

    private void OnTriggerEnter2D(Collider2D collision) /// Collision detection ///
    {
        if (collision.gameObject.tag == "Player") /// IF the player is hit //
        {
            pStats.resetPlayer(); /// Call damage function ///
        }
    }
}