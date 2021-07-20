using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sc_Farnsworth_GameManager : MonoBehaviour
{
    public GameObject PilotCube;
    public GameObject Cube;
    private GameObject CubeType;
    public FarnsworthColour[] RGB_DATA;
    public FarnsworthColour temp;
    public GameObject Tile;
    float xPosCube = -10f;
    float xPosTile = -10f;
    int lastIndex;
    int randIndex;

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
        
        for (int i = 0; i < RGB_DATA.Length; i++) {
            Color32 clr = new Color32(RGB_DATA[i].R, RGB_DATA[i].G, RGB_DATA[i].B, 255);
            // Debug.Log("RGB Color : " + clr);
            if (i == 0) {
                CubeType = PilotCube;
            } else {
                CubeType = Cube;
            }
            GameObject Prefab = Instantiate(CubeType, new Vector3(xPosCube, 0.75f, 0), Quaternion.identity);
            GameObject Canvas = Prefab.transform.GetChild(0).gameObject;
            Prefab.name = "Cap "+RGB_DATA[i].CapNumber.ToString();
            Prefab.GetComponent<Renderer>().material.color = clr;
            if (Prefab.gameObject.tag == "Cube_Object") {
                Prefab.GetComponent<Sc_Farnsworth_ObjectController>().SetIndex(i);
                Canvas.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = RGB_DATA[i].CapNumber.ToString();
            }
            xPosCube += 1.75f;
        }

        for (int i = 0; i < 16; i++) {
            GameObject TilePrefab = Instantiate(Tile, new Vector3(xPosTile, 0.5f, 0), Quaternion.identity);
            GameObject Canvas = TilePrefab.transform.GetChild(0).gameObject;
            if (i == 0) {
                Canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Pilot";
                TilePrefab.name = "Pilot Tile";
            } else {
                Canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.ToString();
                TilePrefab.name = "Tile "+i.ToString();
            }
            xPosTile += 1.75f;
        }
    }

    public void UpdateArray(int prevIndex, int newIndex) {
        temp = RGB_DATA[prevIndex];
        RGB_DATA[prevIndex] = RGB_DATA[newIndex];
        RGB_DATA[newIndex] = temp;
    }
}
