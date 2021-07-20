using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sc_Ishihara_ObjectController : MonoBehaviour
{
    private Renderer _myRenderer;
    private Vector3 _startingPosition;
    [SerializeField]
    private GameObject _selectPanelObject;
    private bool _selectPanelState = false;
    private Rigidbody _rigidbody;
    
    void Start()
    {
        _rigidbody = this.gameObject.GetComponent<Rigidbody>();
        _startingPosition = transform.localPosition;
        _myRenderer = GetComponent<Renderer>();
    }

    /// <summary>
    /// This method is called by the Main Camera when it starts gazing at this GameObject.
    /// </summary>
    public void OnPointerEnter()
    {
        Debug.Log("===THIS OBJECT IS BEING GAZED AT");
        UpdatePanels(true);
    }

    /// <summary>
    /// This method is called by the Main Camera when it stops gazing at this GameObject.
    /// </summary>
    public void OnPointerExit()
    {
        Debug.Log("===THIS OBJECT IS NO LONGER BEING GAZED AT");
        UpdatePanels(false);
    }

    public void Selected(GameObject waypoint) {
        this.gameObject.transform.parent = waypoint.transform;
        this.gameObject.transform.transform.position = waypoint.transform.position;
        ToggleSelectPanel(false);
    }

    private void UpdatePanels(bool gazedAt) {
        if (gazedAt) {
            ToggleSelectPanel(true);
        } else {
            ToggleSelectPanel(false);
        }
    }

    private void ToggleSelectPanel(bool state) {
        _selectPanelObject.SetActive(state);
    }
}
