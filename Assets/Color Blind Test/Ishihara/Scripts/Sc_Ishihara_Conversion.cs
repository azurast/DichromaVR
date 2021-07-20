using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Sc_Ishihara_Conversion : MonoBehaviour
{
    public GameObject Sphere;
    [SerializeField]
    public Sc_Ishihara_GameManager gameManager;
    public IshiharaQuestion[] ishiharaQuestions;
    private IshiharaColour[] LUV_DATA;
    public List<Color> RGB_DATA = new List<Color>();
    int xPos = 0;
    int zPos = 0;
    int count = 0;
    
    // void Awake() {}
    void Start()
    {
        // testQuestions = gameManager.testQuestions;
        /* POPULATE DATA */
        LUV_DATA = new IshiharaColour[26];
        LUV_DATA[0] = new IshiharaColour(0.243f, 0.516f);
        LUV_DATA[1] = new IshiharaColour(0.243f, 0.506f);
        LUV_DATA[2] = new IshiharaColour(0.211f, 0.512f);
        LUV_DATA[3] = new IshiharaColour(0.208f, 0.516f);
        LUV_DATA[4] = new IshiharaColour(0.211f, 0.506f);
        LUV_DATA[5] = new IshiharaColour(0.241f, 0.502f);
        LUV_DATA[6] = new IshiharaColour(0.249f, 0.512f);
        LUV_DATA[7] = new IshiharaColour(0.198f, 0.502f);
        LUV_DATA[8] = new IshiharaColour(0.202f, 0.509f);
        LUV_DATA[9] = new IshiharaColour(0.259f, 0.514f);
        LUV_DATA[10] = new IshiharaColour(0.257f, 0.515f);
        LUV_DATA[11] = new IshiharaColour(0.197f, 0.502f);
        LUV_DATA[12] = new IshiharaColour(0.203f, 0.503f);
        LUV_DATA[13] = new IshiharaColour(0.196f, 0.497f);
        LUV_DATA[14] = new IshiharaColour(0.203f, 0.505f);
        LUV_DATA[15] = new IshiharaColour(0.256f, 0.523f);
        LUV_DATA[16] = new IshiharaColour(0.229f, 0.534f);
        LUV_DATA[17] = new IshiharaColour(0.268f, 0.512f);
        LUV_DATA[18] = new IshiharaColour(0.208f, 0.496f);
        LUV_DATA[19] = new IshiharaColour(0.268f, 0.520f);
        LUV_DATA[20] = new IshiharaColour(0.236f, 0.509f);
        LUV_DATA[21] = new IshiharaColour(0.228f, 0.502f);
        LUV_DATA[22] = new IshiharaColour(0.183f, 0.507f);
        LUV_DATA[23] = new IshiharaColour(0.242f, 0.505f);
        LUV_DATA[24] = new IshiharaColour(0.222f, 0.514f);
        LUV_DATA[25] = new IshiharaColour(0.199f, 0.510f);

        for (int i = 0; i < LUV_DATA.Length; i++) {
            LUV_to_Yxy(LUV_DATA[i].U, LUV_DATA[i].V);
        }

        for (int i = 0,  j = 0; i < RGB_DATA.Count; i++) {
            GameObject Prefab = Instantiate(Sphere, new Vector3(xPos, 3f, zPos), Quaternion.identity);
            GameObject Canvas = Prefab.transform.GetChild(0).gameObject;
            Canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i+1).ToString();
            Prefab.GetComponent<Renderer>().material.color = RGB_DATA[i];
            zPos -= 2;
        }
        
    }

    void LUV_to_Yxy(double u, double v) {
        // Debug.Log("Initial LUV (u, v) : ("+u+", "+v+")");

        double x = ((9 * u) / (6 * u - 16 * v + 12));
        double y = ((4 * v) / (6 * u - 16 * v + 12));

        // Debug.Log("LUV TO Yxy (x, y) : ("+x+", "+y+")");
        Yxy_to_XYZ(x, y);
    }

    void Yxy_to_XYZ(double x, double y) {
        // Constant Luminance
        double Y = 100;
        double X = (x / y) * Y;
        double Z = ((1 - x - y) / y) * Y;

        // Debug.Log("Yxy to XYZ (X, Y, Z) : ("+X+", "+Y+""+", "+Z+")");
        XYZ_to_RGB(X, Y, Z);
    }

    void XYZ_to_RGB(double X, double Y, double Z) {
        double R = ((3.2404542 * X) + (-1.5371385 * Y) + (-0.4985314 * Z))/255;
        double G = ((-0.9692660 * X) + (1.8760108 * Y) + (0.0415560 * Z))/255;
        double B = ((0.0556434 * X) + (-0.2040259 * Y) + (1.0572252 * Z))/255;

        // Debug.Log("XYZ to RGB (R, G, B) : ("+R+", "+G+", "+B+")");
        // GammaCorrection(R, G, B);

        Color clr = new Color((float)R, (float)G, (float)B, 1f);
        RGB_DATA.Add(clr);
        Debug.Log("RGB : " + clr);

    } 

    void GammaCorrection(double R, double G, double B) {
        if (R <= 0.0031308) {
            R *= 12.92;
        } else {
            R = 1.055 * Mathf.Pow((float)R, 0.41666667f) - 0.055;
        }
        Color clr = new Color((float)R, (float)G, (float)B, 1f);
        RGB_DATA.Add(clr);
        Debug.Log("Color: " + clr);
        
    }

    private int SortColors(Color a, Color b)
    {
        if (a.r < b.r)
            return 1;
        else if (a.r > b.r)
            return -1;
        else 
        {
            if (a.g < b.g)
                return 1;
            else if (a.g > b.g)
                return -1;
            else 
            {
                if (a.b < b.b)
                    return 1;
                else if (a.b > b.b)
                    return -1;
            }
        }

        return 0;
    }
    // List<Color> GetColors() {
    //     return RGB_DATA;
    // }
}
