using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Farnsworth_Conversion: MonoBehaviour
{
    public GameObject PilotCube;
    public GameObject Cube;
    private GameObject CubeType;
    public FarnsworthColour[] RGB_DATA;
    public FarnsworthColour temp;
    float xPos = -10f;
    int lastIndex;
    int randIndex;
    // Start is called before the first frame update
    void Start()
    {

        // Randomize the order
        lastIndex = RGB_DATA.Length -1;
        
        while(lastIndex > 1) {
            randIndex = Random.Range(1, lastIndex);
            temp = RGB_DATA[lastIndex];
            RGB_DATA[lastIndex] = RGB_DATA[randIndex];
            RGB_DATA[randIndex] = temp;
            lastIndex -= 1;
        }
        
        // Colors adapted from https://rdrr.io/cran/CVD/man/FarnsworthD15.html
        for (int i = 0; i < RGB_DATA.Length; i++) {
            Color32 clr = new Color32(RGB_DATA[i].R, RGB_DATA[i].G, RGB_DATA[i].B, 255);
            // Debug.Log("RGB Color : " + clr);
            if (i == 0) {
                CubeType = PilotCube;
            } else {
                CubeType = Cube;
            }
            GameObject Prefab = Instantiate(CubeType, new Vector3(xPos, 0.75f, 0), Quaternion.identity);
            Prefab.name = "Cap "+RGB_DATA[i].CapNumber.ToString();
            Prefab.GetComponent<Renderer>().material.color = clr;
            if (Prefab.gameObject.tag == "Cube_Object") {
                Prefab.GetComponent<Sc_Farnsworth_ObjectController>().SetIndex(i);
            }
            xPos += 1.75f;
        }
    }

    public void UpdateArray(int prevIndex, int newIndex) {
        temp = RGB_DATA[prevIndex];
        RGB_DATA[prevIndex] = RGB_DATA[newIndex];
        RGB_DATA[newIndex] = temp;
    }
}
