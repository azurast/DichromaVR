using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public TextMeshProUGUI text;
    InputMaster controls;
    UnityEngine.Vector2 walk;
    UnityEngine.Vector2 turn;
    public Transform cameraTransform;
    UnityEngine.Vector3 cameraForward;

    void Awake(){
        Debug.Log("Awake");

        cameraTransform = this.gameObject.transform.GetChild(0).GetComponent<Transform>();
        cameraForward = cameraTransform.forward;

        controls = new InputMaster();

        controls.Player.Walk.performed += ctx => walk = ctx.ReadValue<UnityEngine.Vector2>();
        controls.Player.Walk.canceled += ctx => walk = Vector2.zero;
        
        /* Turning only intended for debugging inside unity, when building, please comment these lines since turning will be done through the gyroscope on the phone*/
        // controls.Player.Turn.performed += ctx => turn = ctx.ReadValue<UnityEngine.Vector2>();
        // controls.Player.Turn.canceled += ctx => turn = Vector2.zero;

        controls.Player.Restart.performed += ctx => RestartScene();

    }


    void RestartScene() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name.ToString());
    }

    void FixedUpdate() {
        if (walk.x >= 0.8) {
            walk.x = 0.8f;
        }
        if (walk.y >= 0.8) {
            walk.y = 0.8f;
        }

        UnityEngine.Vector3 newDir = new UnityEngine.Vector3(walk.x, 0, walk.y);
        transform.Translate(newDir * speed * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.up * turn.x);
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
