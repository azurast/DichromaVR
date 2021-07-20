//-----------------------------------------------------------------------
// <copyright file="ObjectController.cs" company="Google LLC">
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
using TMPro;

/// <summary>
/// Controls target objects behaviour.
/// </summary>
public class Sc_Farnsworth_ObjectController : MonoBehaviour
{
    /// <summary>
    /// The text that is shown to when an object is gazed at and the player is holding nothing.
    /// </summary>
    [SerializeField]
    private GameObject _pickupPanelObject;

    /// <summary>
    /// The text that is shown to when another object is gazed at and the player is holding something.
    /// </summary>
    [SerializeField]
    private GameObject _swapPanelObject;

    /// <summary>
    /// The text that is shown to when an empty tile/spot is gazed at and the player is holding something.
    /// </summary>
    [SerializeField]
    private GameObject _dropPanelObject;

    /// <summary>
    /// The index of the cube in the RGB_DATA array
    /// </sumary>
    public int currentIndex;
    private GameObject PromptCanvas;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    public void Start()
    {
        PromptCanvas = this.gameObject.transform.GetChild(0).gameObject;
    }

    /// <summary>
    /// This method is called by the Main Camera when it starts gazing at this GameObject.
    /// </summary>
    public void OnPointerEnter(bool isHolding)
    {
        UpdatePanels(isHolding, true);
    }

    /// <summary>
    /// This method is called by the Main Camera when it stops gazing at this GameObject.
    /// </summary>
    public void OnPointerExit(bool isHolding)
    {
        UpdatePanels(isHolding, false);
    }


    /// <summary>
    /// Update this instance's panels according to the status
    /// </summary>
    ///
    /// <param name="gazedAt">
    /// Value `true` if this object is being gazed at, `false` otherwise.
    /// </param>
    private void UpdatePanels(bool isHolding, bool gazedAt) {
        if (gazedAt) {
            if (isHolding) { // Looking at this game object while holding another game object
                if (this.gameObject.tag == "Cube_Object") { // If looking at another cube while holding a cube can only SWAP
                    TogglePickupPanel(false);
                    ToggleSwapPanel(true);
                    ToggleDropPanel(false);
                } else if (this.gameObject.tag == "Hidden_Cube") { // If looking at non cube game object while holding a cube can only DROP
                    TogglePickupPanel(false);
                    ToggleSwapPanel(false);
                    ToggleDropPanel(true);
                }
            } else { // Looking at this game object while holding nothing
                if (this.gameObject.tag == "Cube_Object") { // If looking at another cube while holding nothing can only PICKUP
                    TogglePickupPanel(true);
                    ToggleSwapPanel(false);
                    ToggleDropPanel(false);
                } else {  // If looking at non cube game object while holding nothing -> NOTHING
                    TogglePickupPanel(false);
                    ToggleSwapPanel(false);
                    ToggleDropPanel(false);
                }
            }
        } else { // If the game object is not being gazed at -> NOTHING
            TogglePickupPanel(false);
            ToggleSwapPanel(false);
            ToggleDropPanel(false);
        }
    }

    private void TogglePickupPanel(bool state) {
        _pickupPanelObject.SetActive(state);
    }

    private void ToggleSwapPanel(bool state) {
        _swapPanelObject.SetActive(state);
    }

    private void ToggleDropPanel(bool state) {
        _dropPanelObject.SetActive(state);
    }

    public void SetIndex(int index) {
        currentIndex = index;
    }

    public int GetIndex() {
        return currentIndex;
    }


}
