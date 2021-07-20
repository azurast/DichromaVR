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

/// <summary>
/// Sends messages to gazed GameObject.
/// </summary>
public class Original_CameraPointer : MonoBehaviour
{
    InputMaster controls;
    private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;
    [SerializeField]
    public GameObject mainMenuPanel;
    public GameObject aboutPanel;
    public GameObject creditsPanel;
    public GameObject[] howToPlayPanels;
    [SerializeField]
    private LayerMask layerMask;

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(transform.position);
        if (Physics.Raycast(ray, out hit, _maxDistance, layerMask))
        {
            Debug.Log("masuk if raycast");
            // Debug.Log("hit.transform.gameObject :"+hit.transform.gameObject.tag);
            // GameObject detected in front of the camera.
            if (_gazedAtObject != hit.transform.gameObject)
            {
                Debug.Log("===fullfilled");
                // New GameObject.
                // _gazedAtObject?.SendMessage("OnPointerExit");
                _gazedAtObject = hit.transform.gameObject;
                Debug.Log("GazedAtObject tag :"+ _gazedAtObject.tag);
                // _gazedAtObject.SendMessage("OnPointerEnter");
            } else {
                Debug.Log("===else");
            }
        }
        else
        {
            Debug.Log("gak masuk if raycast");
            // No GameObject detected in front of the camera.
            // _gazedAtObject?.SendMessage("OnPointerExit");
            _gazedAtObject = null;
        }

        // Checks for screen touches.
        if (Google.XR.Cardboard.Api.IsTriggerPressed)
        {
            // _gazedAtObject?.SendMessage("OnPointerClick");
        }
    }

    public void onStartBtnClick() {
        Debug.Log("===start button is clicked");
    }

    public void onHowToPlayBtnClick() {
        Debug.Log("===how to play button is clicked");
    }

    public void onAboutBtnClick(bool state) {
        Debug.Log("===about button is clicked");
        aboutPanel.SetActive(!state);
    }

    public void onCreditsBtnClick() {
        Debug.Log("===credits button is clicked");
    }
    
}
