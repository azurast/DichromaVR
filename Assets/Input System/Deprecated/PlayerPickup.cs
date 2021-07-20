using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerPickup : MonoBehaviour
{
    public float pickupRange = 5f;
    // private GameObject objectToHold;
    InputMaster controls;
    private bool _Pressed;
    private bool _Released;
    
    void IsPickup(bool state) {
        if (state) {
            Debug.Log("===A is pressed, picking up");
        } else {
            Debug.Log("===B is pressed, releasing");
        }
    }

    void OnEnable() {
        Debug.Log("Enabled Pickup");
        controls = new InputMaster();
        controls.Player.Enable();
        controls.Player.Pickup.performed += ctx => IsPickup(true);
        controls.Player.Swap.performed += ctx => IsPickup(false);
        // controls.Player.Pickup.performed += ctx => _Pressed = true;
        // controls.Player.Pickup.canceled += ctx => _Released = false;
    }

    void OnDisable() {
        Debug.Log("Disabled Pickup");
        controls.Player.Disable();
    }

}