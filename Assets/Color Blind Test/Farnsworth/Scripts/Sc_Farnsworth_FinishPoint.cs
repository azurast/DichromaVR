using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sc_Farnsworth_FinishPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject _showResultPanelObject;
    [SerializeField]
    private GameObject _showPromptPanelObject;
    public TextMeshProUGUI colorBlindType; 
    private bool _showResultPanelObjectState = false;
    private bool _showPromptPanelObjectState = false;
    private Rigidbody _rigidbody;
    private bool confirmShowResult;
    
    void Start()
    {
        _rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    public void SetColorBlindType(string cbt) {
        Debug.Log("===cbt; "+cbt);
        colorBlindType.text = cbt;
    }

    /// <summary>
    /// This method is called by the Main Camera when it starts gazing at this GameObject.
    /// </summary>
    public void OnPointerEnter(bool isConfirmed)
    {
        confirmShowResult = isConfirmed;
        // Debug.Log("===THIS OBJECT IS BEING GAZED AT");
        UpdatePanels(true, confirmShowResult);
    }

    /// <summary>
    /// This method is called by the Main Camera when it stops gazing at this GameObject.
    /// </summary>
    public void OnPointerExit()
    {
        // Debug.Log("===THIS OBJECT IS NO LONGER BEING GAZED AT");
        UpdatePanels(false, confirmShowResult);
    }

    private void UpdatePanels(bool gazedAt, bool isConfirmed) {
        if (gazedAt) {
            if (!isConfirmed) {
                TogglePromptPanel(true);
            } else {
                ToggleResultPanel(true);
            }
        } else {
            ToggleResultPanel(false);
            TogglePromptPanel(false);
        }
    }

    public void ToggleResultPanel(bool state) {
        _showResultPanelObject.SetActive(state);
    }

     private void TogglePromptPanel(bool state) {
        _showPromptPanelObject.SetActive(state);
    }
}
