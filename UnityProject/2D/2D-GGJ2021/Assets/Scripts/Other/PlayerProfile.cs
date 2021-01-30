using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    [Header("Vitals")]
    public int Health = 3;
    public HP Ui;

    [Header("Fragments")]
    public int starFragments;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Fragment")
        {
            collision.gameObject.SetActive(false);
            starFragments += 1;
        }
    }

    public void changeHP(int damage) /// Damage function, passed damage amount ///
    {
        if (Health - damage == 0) /// IF the current health - the damage is equal to 0 ///
        {
            Debug.Log("Game Over");
            Ui.changeDisplay(0); /// Change display to game over state ///
        }
        else /// Otherwise ///
        {
            Ui.changeDisplay(Health - damage); /// Apply the damage to the display ///
            Health -= damage; /// Apply the damage ///
        }
    }
}