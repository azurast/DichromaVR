using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Sends messages to gazed GameObject.
/// </summary>
public class Sc_Ishihara_CameraPointer : MonoBehaviour
{
     /* Input System */
    InputMaster controls;
    private GameObject _gazedAtObject = null;
    [SerializeField]
    private Sc_Ishihara_GameManager gameManager;
    private GameObject _playerParent;
    private const float _maxDistance = 15f;
    [SerializeField]
    private LayerMask layerMask;
    
    
    void Awake() {
        _playerParent = this.gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 15, Color.green);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance, layerMask)) {
            // Debug.Log("===hit.transform.gameObject.tag:"+hit.transform.gameObject.tag);
            if (_gazedAtObject != hit.transform.gameObject && hit.transform.gameObject.tag != "Player") // If the gazed at object is different from the one hit by raycast
            {
                // Let the previously gazed at cube know it is no longer being gazed at
                _gazedAtObject?.SendMessage("OnPointerExit", SendMessageOptions.DontRequireReceiver);
                // Change the game object being gazed at with the new cube
                _gazedAtObject = hit.transform.gameObject;
                // Debug.Log("gazedAt :"+_gazedAtObject);
                // Tells the new cube it is being gazed at
                _gazedAtObject.SendMessage("OnPointerEnter", SendMessageOptions.DontRequireReceiver);

                if (hit.collider) {
                    // Debug.Log("===")
                    Debug.Log("Looking to select");
                    // Can only do controls to gem object
                    // if current question unanswered, cant finish
                    if (gameManager.currentQuestion.hasBeenAnswered) {
                        if (_gazedAtObject.tag == "Finish_Point") {
                            controls.Player.FinishScene.performed += context => LoadScene();
                        }
                    } else {
                        controls.Player.Pickup.performed += context => SelectObject(_gazedAtObject);
                    }
                }
                
            }
        } else {
            // No GameObject detected in front of the camera.
            _gazedAtObject?.SendMessage("OnPointerExit", SendMessageOptions.DontRequireReceiver);
            _gazedAtObject = null;
        }
    }

    void LoadScene() {
        Debug.Log("=== lets go to farnsworth");
        SceneManager.LoadScene("Farnsworth Colours");
    }

    void SelectObject(GameObject selectedObject) {
        // Debug.Log("===Select Object");
        if (gameManager.currentQuestion.hasBeenAnswered == false) {
            // Debug.Log("===QUESTION NOT YET ANSWERED");
            gameManager.SendMessage("OnQuestionAnswered", selectedObject.tag);
            // if(selectedObject.tag == "Answer_Object") {
            //     gameManager.SendMessage("OnQuestionAnswered", "Answer_Object");
            // } else if (selectedObject.tag == "Neutral_Object") {
            //     gameManager.SendMessage("OnQuestionAnswered", "Neutral_Object");
            // } else {
            //     gameManager.SendMessage("OnQuestionAnswered", selectedObject.tag);
            // }
            _gazedAtObject?.SendMessage("Selected", gameManager.selectedGemWaypoint);
        } else {
            // Debug.Log("===QUESTION ALREADY ANSWERED");
        }
    }

    void OnEnable() {
        controls = new InputMaster();
        controls.Player.Enable();
    }

    void OnDisable() {
        controls.Player.Disable();
    }
}
