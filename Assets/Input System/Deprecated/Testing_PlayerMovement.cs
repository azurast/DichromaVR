using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Testing_PlayerMovement : MonoBehaviour
{
    public float speed;
    public TextMeshProUGUI text;
    InputMaster controls;
    UnityEngine.Vector2 walk;
    UnityEngine.Vector2 turn;
    // UnityEngine.InputSystem.Gyroscope gyroscope;
    Transform cameraTransform;
    UnityEngine.Vector3 cameraForward;

    void Awake(){
        Debug.Log("Awake");
        // gyroscope = UnityEngine.InputSystem.Gyroscope.current;
        // InputSystem.EnableDevice(gyroscope);
        // if (UnityEngine.InputSystem.Gyroscope.current.enabled) {
        //     Debug.Log("Gyroscope is enabled");
        //     text.text = "gyroscope enabled";
        // }

        cameraTransform = this.gameObject.transform.GetChild(0).GetComponent<Transform>();
        cameraForward = cameraTransform.forward;

        controls = new InputMaster();

        controls.Player.Walk.performed += ctx => walk = ctx.ReadValue<UnityEngine.Vector2>();
        controls.Player.Walk.canceled += ctx => walk = Vector2.zero;
        
        controls.Player.Turn.performed += ctx => turn = ctx.ReadValue<UnityEngine.Vector2>();
        controls.Player.Turn.canceled += ctx => turn = Vector2.zero;
        // controls.Player.Turn.performed += ctx => turn = gyroscope.angularVelocity.ReadValue();
        // controls.Player.Turn.canceled += ctx => turn = Vector2.zero;
    }

    void Update() {

        UnityEngine.Vector3 newDir = new UnityEngine.Vector3(walk.x, 0, walk.y);
        transform.Translate(newDir * speed * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.up * turn.x);

        if (transform.rotation != cameraTransform.rotation) {
            Debug.Log("Parent's rotation is not the same with camera");
            transform.rotation = cameraTransform.rotation;
            transform.rotation.Normalize();
        }
    }

    void OnEnable() {
        Debug.Log("Enabled Movement");
        controls.Player.Enable();
    }

    void OnDisable() {
        Debug.Log("Disabled Movement");
        controls.Player.Disable();
    }
}
