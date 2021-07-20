//-----------------------------------------------------------------------
// <copyright file="CameraPointer.cs" company="Google LLC">
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

/// <summary>
/// Sends messages to gazed GameObject.
/// </summary>
public class CameraPointer : MonoBehaviour
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

    /// <summary>
    /// The panel shown when player wants to end the game,
    /// </summary>
    [SerializeField]
    private GameObject _finishPanelObject;
    private bool _finishPanelState;

    void Awake() {
        _finishPanelState = _finishPanelObject.activeSelf;
        _playerParent = this.gameObject.transform.parent.gameObject;
    }

    void Start() {
        controls.Player.FinishTest.performed += _ => {
            _finishPanelState = !_finishPanelState;
            _finishPanelObject.SetActive(_finishPanelState);
        };
    }
   
    public void Update()
    {
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
                _gazedAtObject?.SendMessage("OnPointerExit", isHolding);
                if(_gazedAtObject != null && _gazedAtObject.tag == "Hidden_Cube") {
                    if (tempPoint.transform.childCount > 0 && tempPoint.transform.GetChild(0).gameObject != null) {
                        Destroy(tempPoint.transform.GetChild(0).gameObject);
                    }
                }
                // Change the game object being gazed at with the new cube
                _gazedAtObject = hit.transform.gameObject;
                // Tells the new cube it is being gazed at
                _gazedAtObject.SendMessage("OnPointerEnter", isHolding);

                if (hit.collider) {
                    if (!isHolding) {
                        Debug.Log("===is NOT Holding & is looking to PICKUP "+ _gazedAtObject.name);
                        controls.Player.Pickup.performed += context => PickupObject(_gazedAtObject);
                        // controls.Player.Pickup.performed += context => {
                        //     Debug.Log("Pickup context.action is "+context.action);
                        //     PickupObject(_gazedAtObject);
                        // };
                    } else {
                        if (_gazedAtObject.tag == "Cube_Object") {
                            Debug.Log("===is Holding & is looking to SWAP with " + _gazedAtObject.name);
                            controls.Player.Swap.performed += context => SwapObject(_gazedAtObject);
                        } else if (_gazedAtObject.tag == "Hidden_Cube") {
                            Debug.Log("===is Holding & is looking to DROP at" + _gazedAtObject.name);
                            controls.Player.Drop.performed += context => DropObject(_gazedAtObject);
                        }    
                    }
                }
                
            }
        
        }
        else
        {
            // No GameObject detected in front of the camera.
            _gazedAtObject?.SendMessage("OnPointerExit", isHolding);
            _gazedAtObject = null;
        }

        

        // Checks for screen touches.
        // if (Google.XR.Cardboard.Api.IsTriggerPressed)
        // {
        //     _gazedAtObject?.SendMessage("OnPointerClick");
        // }
    }

    void MoveObject () {
        if (Vector3.Distance(_heldObject.transform.position, holdPoint.position) > 0.1f) {
            Vector3 moveDirection = (holdPoint.position - _heldObject.transform.position);
            _heldObject.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
        }
    }

    void PickupObject(GameObject pickupObject) { 
        Debug.Log("===PickupObject()");
        if (!isHolding) {
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
        Debug.Log("===SwapObject()");
        if (isHolding) {
            // Store their indexes
            int heldObjectIndex = _heldObject.GetComponent<Sc_Farnsworth_ObjectController>().GetIndex();
            int swapObjectIndex = swapObject.GetComponent<Sc_Farnsworth_ObjectController>().GetIndex();
            FarnsworthArray.UpdateArray(swapObjectIndex, heldObjectIndex);
            // Get position of the object that wants to be swapped with, the object being held will be placed here
            Vector3 initialPosition = swapObject.transform.position;
            // Get the rigidbody of the object that wants to be pickedup
            Rigidbody swapObjectRb = swapObject.GetComponent<Rigidbody>();
            // Get the rigidbody of the object currently at hand
            Rigidbody heldObjectRb = _heldObject.GetComponent<Rigidbody>();
            
            // Make room for in the held point by moving the _heldObject as _temp in tempPoint
            _temp = _heldObject;
            _temp.transform.position = tempPoint.transform.position;
            // Update rigidbody properties of the object we want to hold
            swapObjectRb.useGravity = false;
            swapObjectRb.drag = 10;
            // Move the position to the holdPoint
            swapObject.transform.position = holdPoint.transform.position;
            // Now this object is our _heldObject & attaches it to the holdPoint
            _heldObject = swapObject;
            _heldObject.GetComponent<Sc_Farnsworth_ObjectController>().SetIndex(heldObjectIndex);
            _heldObject.GetComponent<Rigidbody>().transform.parent = holdPoint;
            // Move the previous item (temp) to the initial position
            _temp.transform.position = initialPosition;
            _temp.GetComponent<Sc_Farnsworth_ObjectController>().SetIndex(swapObjectIndex);
            // detach it from tempPoint so that it stays
            _temp.GetComponent<Rigidbody>().transform.parent = null;
            // _temp.GetComponent<Rigidbody>().useGravity = true;
            // _temp.GetComponent<Rigidbody>().drag = 1;
            _temp = null;
            isHolding = true;
            // Let Sc_Farnsworth_ObjectController know that the object is picked up so the corresponding UI must change, asking to swap with other game object
            _gazedAtObject.SendMessage("OnPointerEnter", isHolding);
        }
    }

    void DropObject(GameObject dropPoint) {
        Debug.Log("===DropObject()");
        if (isHolding) {
            // Get drop point's location
            Vector3 initialPosition = dropPoint.transform.position;
            // Move the hidden cube to temp & destroy it as we no longer need it
            _temp = dropPoint;
            _temp.transform.position = tempPoint.transform.position;
            _temp.transform.parent = tempPoint.transform;
            // Place the held game object to the drop point
            _heldObject.transform.position = initialPosition;
            Rigidbody heldObjectRb = _heldObject.GetComponent<Rigidbody>();
            heldObjectRb.useGravity = true;
            heldObjectRb.drag = 1;
            _heldObject.transform.parent = null;
            isHolding = false;
            _heldObject = null;
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
