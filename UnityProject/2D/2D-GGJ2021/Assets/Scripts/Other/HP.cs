using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    [Header("Icons")]
    public Image One;
    public Image Two;
    public Image Three;

    private void Start()
    {
        /// Enable all health ui on Start ///
        One.gameObject.SetActive(true);
        Two.gameObject.SetActive(true);
        Three.gameObject.SetActive(true);
    }

    public void changeDisplay(int newHP) /// Function used to update health ui, current health is passed through ///
    {
        switch (newHP) /// Switch determines which state is active ///
        {
            case 1: /// On 1 HP //
                One.gameObject.SetActive(true);
                Two.gameObject.SetActive(false);
                Three.gameObject.SetActive(false);
                break;
            case 2: /// On 2 HP //
                One.gameObject.SetActive(true);
                Two.gameObject.SetActive(true);
                Three.gameObject.SetActive(false);
                break;
            case 3: /// On 3 HP //
                One.gameObject.SetActive(true);
                Two.gameObject.SetActive(true);
                Three.gameObject.SetActive(true);
                break;
            case 0: /// Game over state ///
                One.gameObject.SetActive(false);
                Two.gameObject.SetActive(false);
                Three.gameObject.SetActive(false);
                break;
        }
    }
}
