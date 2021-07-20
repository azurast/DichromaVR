 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CameraRotation : MonoBehaviour
{
    public float speed = 10f;
    public TextMeshProUGUI text;
    InputMaster controls;
    UnityEngine.Vector2 turn;
    UnityEngine.InputSystem.Gyroscope gyroscope;

    void Awake(){
        Debug.Log("Awake");
        gyroscope = UnityEngine.InputSystem.Gyroscope.current;
        InputSystem.EnableDevice(gyroscope);
        if (UnityEngine.InputSystem.Gyroscope.current.enabled) {
            Debug.Log("Gyroscope is enabled");
            text.text = "gyroscope enabled";
        }

        controls = new InputMaster();

        controls.Player.Turn.performed += ctx => turn = gyroscope.angularVelocity.ReadValue();
  
    }

    void Update() {
        Debug.Log("turn : "+turn);

        UnityEngine.Vector2 newRot = new UnityEngine.Vector2(turn.x, turn.y) * 100f * Time.deltaTime;
        transform.Rotate(newRot, Space.World);
    }

    void OnEnable() {
        Debug.Log("Enabled");
        controls.Player.Enable();
    }

    void OnDisable() {
        Debug.Log("Disabled");
        controls.Player.Disable();
    }
}
