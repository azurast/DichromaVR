using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Sends messages to gazed GameObject.
/// </summary>
public class Sc_Farnsworth_CameraPointer : MonoBehaviour
{
    /* Input System */
    InputMaster controls;

    /* Pickup & Drop Object */ 
    private GameObject _playerParent;
    public Transform holdPoint;
    public Transform tempPoint;
    [SerializeField]
    private GameObject _heldObject;
    [SerializeField]
    private GameObject _temp;
    public float moveForce = 250f;
    protected bool isHolding = false;

    /* Object being gazed at */
    // [SerializeField]
    private GameObject _gazedAtObject = null;
    private const float _maxDistance = 15f;

    /* Empty game object for empty tile */
    public GameObject emptyObjectPrefab;

    /* The Array that holds the objects*/
    public Sc_Farnsworth_GameManager FarnsworthArray; 
    public Sc_Farnsworth_Checker FarnsworthChecker;

    /// <summary>
    /// The panel shown when player wants to end the game,
    /// </summary>
    [SerializeField]
    private GameObject _finishPanelObject;
    private bool _finishPanelState;
    private bool confirmCalculateResult = false;
    private string currentAction = null;

    void Awake() {
        // _finishPanelState = _finishPanelObject.activeSelf;
        _playerParent = this.gameObject.transform.parent.gameObject;
    }

    void Start() {
        // controls.Player.FinishTest.performed += _ => {
        //     _finishPanelState = !_finishPanelState;
        //     _finishPanelObject.SetActive(_finishPanelState);
        // };
    }
   
    public void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 15, Color.green);

        if (isHolding) {
            MoveObject();
        }

        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed at.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            // GameObject detected in front of the camera.
            if (_gazedAtObject != hit.transform.gameObject) // If the gazed at object is different from the one hit by raycast
            {
                // Let the previously gazed at cube know it is no longer being gazed at
                _gazedAtObject?.SendMessage("OnPointerExit", isHolding, SendMessageOptions.DontRequireReceiver);
                if(_gazedAtObject != null && _gazedAtObject.tag == "Hidden_Cube") {
                    if (tempPoint.transform.childCount > 0 && tempPoint.transform.GetChild(0).gameObject != null) {
                        Destroy(tempPoint.transform.GetChild(0).gameObject);
                    }
                }
                // Change the game object being gazed at with the new cube
                _gazedAtObject = hit.transform.gameObject;
                // Tells the new cube it is being gazed at
                _gazedAtObject.SendMessage("OnPointerEnter", isHolding, SendMessageOptions.DontRequireReceiver);

                if (hit.collider) {
                    if (!isHolding) {
                        if (_gazedAtObject.tag == "Finish_Point") {
                            _gazedAtObject.SendMessage("OnPointerEnter", confirmCalculateResult);
                            Debug.Log("===confirmCalculateResult:"+confirmCalculateResult);
                            if (!confirmCalculateResult) {
                                Debug.Log("TESSSSSSTT1");
                                controls.Player.FinishScene.performed += context => CalculateResult();
                                controls.Player.ResumeScene.performed += context => ResumeScene();
                            } 
                            else {
                                Debug.Log("TESSSSSSTT");
                                controls.Player.FinishScene.performed += context => BackToHome();
                            }
                        } else {
                            Debug.Log("===is NOT Holding & is looking to PICKUP "+ _gazedAtObject.name);
                            currentAction = "pickup";
                            controls.Player.Pickup.performed += context => PickupObject(_gazedAtObject);
                            // controls.Player.Pickup.performed += context => {
                            //     Debug.Log("Pickup context.action is "+context.action);
                            //     PickupObject(_gazedAtObject);
                            // };
                        }
                    } else {
                        if (_gazedAtObject.tag == "Cube_Object") {
                            Debug.Log("===is Holding & is looking to SWAP with " + _gazedAtObject.name);
                            currentAction = "swap";
                            controls.Player.Swap.performed += context => SwapObject(_gazedAtObject);
                            // controls.Player.Swap.canceled += context => _gazedAtObject = null;
                            // controls.Player.Pickup.Disable() += context => _gazedAtObject = null;
                            // controls.Player.Drop..Disable() += context => _gazedAtObject = null;
                        } else if (_gazedAtObject.tag == "Hidden_Cube") {
                            Debug.Log("===is Holding & is looking to DROP at" + _gazedAtObject.name);
                            currentAction = "drop";
                            controls.Player.Drop.performed += context => DropObject(_gazedAtObject);
                            // controls.Player.Drop.canceled += context => _gazedAtObject = null;
                            // controls.Player.Pickup..Disable() += context => _gazedAtObject = null;
                            // controls.Player.Swap..Disable() += context => _gazedAtObject = null;
                        }    
                    }
                }
                
            }
        }
        else
        {
            // No GameObject detected in front of the camera.
            _gazedAtObject?.SendMessage("OnPointerExit", isHolding, SendMessageOptions.DontRequireReceiver);
            _gazedAtObject = null;
        }

        // Checks for screen touches.
        if (Google.XR.Cardboard.Api.IsTriggerPressed)
        {
            _gazedAtObject?.SendMessage("OnPointerClick");
        }
    }

    void CalculateResult() {
        Debug.Log("calculate result");
        FarnsworthChecker.Run_Checker();
        confirmCalculateResult = true;
        _gazedAtObject.SendMessage("OnPointerEnter", confirmCalculateResult);
    }

    void ResumeScene() {
        Debug.Log("Resume");
        _gazedAtObject?.SendMessage("OnPointerExit");
    }

    void BackToHome() {
        SceneManager.LoadScene("Main Lobby");
    }

    void MoveObject () {
        if (Vector3.Distance(_heldObject.transform.position, holdPoint.position) > 0.1f) {
            Vector3 moveDirection = (holdPoint.position - _heldObject.transform.position);
            _heldObject.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
        }
    }

    void PickupObject(GameObject pickupObject) { 
        Debug.Log("===pickup gazed at:"+_gazedAtObject);
        Debug.Log("===PickupObject() isHolding:"+isHolding);
        if (currentAction == "pickup" && !isHolding && _gazedAtObject != null) {
            // Store the initial position and rotation of the pickupObject
            Vector3 initialPos = pickupObject.transform.position;
            Quaternion initialRotation = pickupObject.transform.rotation;
            // Get the rigidbody of the object that wants to be pickedup
            Rigidbody pickupObjectRb = pickupObject.GetComponent<Rigidbody>();
            // Set it's gravity to false so that it can float
            pickupObjectRb.useGravity = false;
            // Set drag so that it moves
            pickupObjectRb.drag = 10;
            // Fixed the distance of the 'hold' position in front of the player
            // Attaches the picked up object with the holdPoint
            pickupObjectRb.transform.parent = holdPoint;
            // Make the pickedup object as the object being held
            _heldObject = pickupObject;
            // Instantiate empty object so that can detect the position for dropping object in empty tile
            Instantiate(emptyObjectPrefab, initialPos, initialRotation);
            // Set status to let the game know player is holding something
            isHolding = true;
            // Let Sc_Farnsworth_ObjectController know that the object is picked up so the corresponding UI must change, asking to swap with other game object
            _gazedAtObject.SendMessage("OnPointerEnter", isHolding);
        }
    }

    void SwapObject(GameObject swapObject) {
        Debug.Log("===swap gazed at:"+_gazedAtObject);
        Debug.Log("===SwapObject() isHolding:"+isHolding);
        if (currentAction == "swap" && isHolding && _gazedAtObject != null) {
            // Store their indexes
            int heldObjectIndex = _heldObject.GetComponent<Sc_Farnsworth_ObjectController>().GetIndex();
            int swapObjectIndex = swapObject.GetComponent<Sc_Farnsworth_ObjectController>().GetIndex();
            FarnsworthArray.UpdateArray(swapObjectIndex, heldObjectIndex);
            // Get position of the object that wants to be swapped with, the object being held will be placed here
            Vector3 initialPosition = swapObject.transform.position;
            Vector3 initialDirection = swapObject.transform.forward;

            // Get the rigidbody of the object that wants to be pickedup
            Rigidbody swapObjectRb = swapObject.GetComponent<Rigidbody>();
            // Get the rigidbody of the object currently at hand
            Rigidbody heldObjectRb = _heldObject.GetComponent<Rigidbody>();
            // Make room for in the held point by moving the _heldObject as _temp in tempPoint
            _temp = _heldObject;
            _temp.transform.position = tempPoint.transform.position;
            _temp.transform.parent = tempPoint;

            // Update rigidbody properties of the object we want to hold
            swapObjectRb.useGravity = false;
            swapObjectRb.drag = 10;
            
            // Move the position to the holdPoint
            swapObject.transform.position = holdPoint.transform.position;

            // Now this object is our _heldObject & attach it to the holdPoint
            _heldObject = swapObject;
            _heldObject.GetComponent<Sc_Farnsworth_ObjectController>().SetIndex(heldObjectIndex);
            _heldObject.GetComponent<Rigidbody>().transform.parent = holdPoint;

            // Move the previous item (temp) to the initial position
            _temp.transform.position = initialPosition;
            _temp.transform.forward = initialDirection;
            _temp.GetComponent<Sc_Farnsworth_ObjectController>().SetIndex(swapObjectIndex);

            // detach it from tempPoint so that it stays
            _temp.GetComponent<Rigidbody>().transform.parent = null;
            _temp.GetComponent<Rigidbody>().useGravity = true;
            _temp.GetComponent<Rigidbody>().drag = 0;            
            _temp = null;

            isHolding = true;
            // Let Sc_Farnsworth_ObjectController know that the object is picked up so the corresponding UI must change, asking to swap with other game object
            _gazedAtObject.SendMessage("OnPointerEnter", isHolding);
            _heldObject.GetComponent<Rigidbody>().transform.parent = holdPoint;
            _heldObject.GetComponent<Rigidbody>().drag = 10;
            _heldObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    void DropObject(GameObject dropPoint) {
        Debug.Log("===drop gazed at:"+_gazedAtObject);
        Debug.Log("===DropObject() isHolding:"+isHolding);
        if (currentAction == "drop" && isHolding && _gazedAtObject != null) {
            // Get drop point's location
            Vector3 initialPosition = dropPoint.transform.position;
            Vector3 initialDirection = dropPoint.transform.forward;
            // Move the hidden cube to temp & destroy it as we no longer need it
            _temp = dropPoint;
            _temp.transform.position = tempPoint.transform.position;
            _temp.transform.parent = tempPoint.transform;
            // Place the held game object to the drop point
            _heldObject.transform.position = initialPosition;
            _heldObject.transform.forward = initialDirection;
            Rigidbody heldObjectRb = _heldObject.GetComponent<Rigidbody>();
            heldObjectRb.useGravity = true;
            heldObjectRb.drag = 0;
            _heldObject.transform.parent = null;
            isHolding = false;
            _heldObject = null;
            Destroy(tempPoint.transform.GetChild(0).gameObject);
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

