using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sc_Ishihara_FinishPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject _showResultPanelObject;
    [SerializeField]
    private GameObject _showRetryPanelObject;
    public TextMeshProUGUI colorBlindType;
    private Rigidbody _rigidbody;
    
    void Start()
    {
        _rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    public void SetColorBlindType(string cbt) {
        colorBlindType.text = cbt;
        ToggleResultPanel(true);
    }

    /// <summary>
    /// This method is called by the Main Camera when it starts gazing at this GameObject.
    /// </summary>
    public void OnPointerEnter()
    {
        // UpdatePanels(true);
    }

    /// <summary>
    /// This method is called by the Main Camera when it stops gazing at this GameObject.
    /// </summary>
    public void OnPointerExit()
    {
        // UpdatePanels(false);
    }

    private void UpdatePanels(bool gazedAt) {
        if (gazedAt) {
            ToggleResultPanel(true);
        } else {
            ToggleResultPanel(false);
        }
    }

    private void ToggleResultPanel(bool state) {
        _showResultPanelObject.SetActive(state);
    }

    public void ToggleRetryPanel(bool state) {
        _showRetryPanelObject.SetActive(state);
    }
}
