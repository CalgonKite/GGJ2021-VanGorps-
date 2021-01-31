using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    [Header("Vitals")]
    public HP Ui;
    public GameObject LastMarker;

    public Vector3 lastMarkerPos = new Vector3();
    public int currentScreen;

    public GameObject[] screenMarkers;
    public GameObject[] screenCameras;

    [Header("Fragments")]
    public int starFragments;

    private void Awake()
    {
        screenMarkers = GameObject.FindGameObjectsWithTag("Marker"); /// Iterative through tag & add to array ///
        screenCameras = GameObject.FindGameObjectsWithTag("Camera");
        advanceScreen(0); /// Then set the current screen to 0 ///
    }

    private void OnTriggerEnter2D(Collider2D collision) /// Special trigger checks ///
    {
        if(collision.tag == "Fragment")
        {
            collision.gameObject.SetActive(false);
            starFragments += 1;
        }

        if(collision.tag == "ScreenEnd") /// If colliding with an endmarker ///
        {
            screenCameras[currentScreen].SetActive(false);
            advanceScreen(currentScreen += 1); /// Pass through current screen int + 1 ///   
        }
    }

    public void advanceScreen(int screenNum) /// Function to advance the screen and reassign the reset point ///
    {
        currentScreen = screenNum; /// Change the array index to current screen ///
        screenCameras[screenNum].SetActive(true);
        lastMarkerPos = screenMarkers[screenNum].transform.position; /// Assign vector through array index of reset point ///


        resetPlayer(); /// Reset player to new point ///
    }

    public void resetPlayer() /// Function to reset the player within the screen ///
    {
        transform.position = lastMarkerPos; /// Set the players location to the rest point ///
    }
}