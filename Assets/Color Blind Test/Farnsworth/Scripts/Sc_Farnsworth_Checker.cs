using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Sc_Farnsworth_Checker : MonoBehaviour
{
    InputMaster controls;
    public Sc_Farnsworth_GameManager Farnsworth_Conversion;
    private FarnsworthColour[] Caps;
    private float U2 = 0;
    private float V2 = 0;
    private float UV = 0;
    private float DU = 0;
    private float DV = 0;
    private float D = 0;
    ///<summary>
    /// Confusion Angle
    ///</summary>
    private double ConfusionAngle;
    /// <summary>
    /// Severity
    /// </summary>
    private string Severity;
    /// <summary>
    /// Color Blind Type
    /// </summary>
    private string ColorBlindType;
    ///<summary>
    /// Perpendicular Angle
    ///</summary>
    private float MajorAngle;
    ///<summary>
    /// Confusion Angle
    ///</summary>
    private float MinorAngle;
    ///<summary>
    /// Major Moment
    ///</summary>
    private float MajorMoment;
    ///<summary>
    /// Minor Moment
    ///</summary>
    private float MinorMoment;
    ///<summary>
    /// Major Radius
    ///</summary>
    private float MajorRadius;
    ///<summary>
    /// Minor Radius
    ///</summary>
    private float MinorRadius;
    ///<summary>
    /// Total Error Score
    ///</summary>
    private float TotalErrorScore;
    ///<summary>
    /// S-Index
    ///</summary>
    private float SIndex;
    ///<summary>
    /// C-Index
    ///</summary>
    private float CIndex;
    ///<summary>
    /// Major Radius Of Perfect Arrangment (Normal Vision)
    ///</summary>
    private float MajorRadiusOfPerfectArrangment = 9.234669f;
    /// <summary>
    /// The result panel.
    /// </summary>
    public TextMeshProUGUI ResultText;
    public TextMeshProUGUI ConfusionAngleText;
    public TextMeshProUGUI MinorRadiusText;
    public TextMeshProUGUI MajorRadiusText;
    public TextMeshProUGUI TotalErrorScoreText;
    public TextMeshProUGUI SIndexText;
    public TextMeshProUGUI CIndexText;
    public TextMeshProUGUI ColorBlindText;
    [SerializeField]
    private GameObject FinishPoint;

    void Awake() {
        // controls = new InputMaster();
        // _resultPanelState = _resultPanelObject.activeSelf;
        // controls.Player.Calculate.performed += ctx => Run_Checker();
    }

    // Start is called before the first frame update
    public void Run_Checker()
    {
        Debug.Log("===Run Checker");
        Caps = Farnsworth_Conversion.RGB_DATA;
        Calculate_SumsOfSquares_and_CrossProducts();
        D = U2 - V2;
        Calculate_Angle_and_MomentsOfInertia();
        Calculate_Results();
        Print_Results();
    }

    ///<summary>
    /// Calculate the sums of squares and cross products
    ///</summary>

    private void Calculate_SumsOfSquares_and_CrossProducts() {
        for (int i = 1; i < Caps.Length; i++) {
            DU = Caps[i].U - Caps[i-1].U;
            DV = Caps[i].V - Caps[i-1].V;

            U2 += Mathf.Pow(DU, 2);
            V2 += Mathf.Pow(DV, 2);
            UV += (DU * DV);
        }
    }

    ///<summary>
    /// Calculate Major Radius, Minor Radius, and Confusion Angle
    ///</summary>

    private void Calculate_Angle_and_MomentsOfInertia() {
        // Angle
        if (D == 0) {
            MajorAngle = 0.7854f;
        } else {
            MajorAngle = Mathf.Atan(2 * (UV/D))/2;
        }

        // Major Moment
        MajorMoment = (U2 * Mathf.Pow(Mathf.Sin(MajorAngle), 2)) + (V2 * Mathf.Pow(Mathf.Cos(MajorAngle), 2)) 
        - (2 * UV * Mathf.Sin(MajorAngle) * Mathf.Cos(MajorAngle));

        // Check the perpendicular angle
        if (MajorAngle < 0) {
            MinorAngle = MajorAngle + 1.5708f;
        } else {
            MinorAngle = MajorAngle - 1.5708f;
        }

        // Minor Moment
        MinorMoment = (U2 * Mathf.Pow(Mathf.Sin(MinorAngle), 2)) + (V2 * Mathf.Pow(Mathf.Cos(MinorAngle), 2)) 
        - (2 * UV * Mathf.Sin(MinorAngle) * Mathf.Cos(MinorAngle));

        // Check if the major moment is smaller than the minor moment
        if (MajorMoment < MinorMoment) {
            float temp = MajorAngle;
            MajorAngle = MinorAngle;
            MinorAngle = temp;
            temp = MajorMoment;
            MajorMoment = MinorMoment;
            MinorMoment = temp;
        }    
    }

    ///<summary>
    /// Calculate the final results
    ///</summary>

    
    private void Calculate_Results() {
        MajorRadius = Mathf.Sqrt(MajorMoment/Caps.Length);
        MinorRadius = Mathf.Sqrt(MinorMoment/Caps.Length);
        TotalErrorScore = Mathf.Sqrt(Mathf.Pow(MajorRadius, 2) + Mathf.Pow(MinorRadius, 2));
        SIndex = MajorRadius/MinorRadius;
        CIndex = MajorRadius/MajorRadiusOfPerfectArrangment;
        ConfusionAngle = 57.3 * MinorAngle;

        if (ConfusionAngle > 17) {
            // Normal
            ColorBlindType = "Normal Vision";
        } else if (ConfusionAngle > 0.7 && ConfusionAngle <= 17) {
            // Protanopia
            ColorBlindType = "Protanopia";
        } else if (ConfusionAngle >= -65 && ConfusionAngle <= 0.7) {
            // Deuteranopia
            ColorBlindType = "Deuteranopia";
        } else if (ConfusionAngle < -65) {
            // Tritanopia
            ColorBlindType = "Tritanopia";
        }

        if (ColorBlindType != "Normal Vision") {
            if (CIndex < 1.6) {
                // Not Severe
                Severity = "Not Severe";
            } else {
                // Severe
                Severity = "Severe";
            }
        } else {
            Severity = "";
        }
    }

    private void Print_Results() {
        ConfusionAngleText.text = "CA\n"+ConfusionAngle.ToString("F1");
        MinorRadiusText.text = "Min.R\n"+MajorRadius.ToString("F1");
        MajorRadiusText.text = "Maj.R\n"+MinorRadius.ToString("F1");
        TotalErrorScoreText.text = "TES\n"+TotalErrorScore.ToString("F1");
        SIndexText.text = "S.Indx\n"+SIndex.ToString("F1");
        CIndexText.text = "C.Indx\n"+CIndex.ToString("F1");

        // ResultText.text = "Standard D15 Test Result\nConfusion Angle : "+ConfusionAngle.ToString("F1")+"\nMajor Radius : "+MajorRadius.ToString("F1")+"\nMinor Radius : "+MinorRadius.ToString("F1")+"\nTotal Error Score : "+TotalErrorScore.ToString("F1")+"\nC-Index : "+CIndex.ToString("F1")+"\nS-Index : "+SIndex.ToString("F1"); 
        ColorBlindText.text = "Yor result shows you might have :\n"+Severity+" "+ColorBlindType;
        Debug.Log("Standard D15 Test Result");
        Debug.Log("Confusion Angle : "+ConfusionAngle);
        Debug.Log("Major Radius : "+MajorRadius);
        Debug.Log("Minor Radius : "+MinorRadius);
        Debug.Log("Total Error Score : "+TotalErrorScore);
        Debug.Log("C-Index : "+CIndex);
        Debug.Log("S-Index : "+SIndex);
        FinishPoint.GetComponent<Sc_Farnsworth_FinishPoint>().ToggleResultPanel(true);
    }

    // void OnEnable() {
    //     Debug.Log("Enabled Movement");
    //     controls.Player.Enable();
    // }

    // void OnDisable() {
    //     Debug.Log("Disabled Movement");
    //     controls.Player.Disable();
    // }

}
