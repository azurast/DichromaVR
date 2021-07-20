using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_Lobby_Laptop_CameraPointer : MonoBehaviour
{
    public void GotoIshihara() {
        SceneManager.LoadScene("Ishihara Colours");
    }
    public void GotoFarnsworth() {
        SceneManager.LoadScene("Farnsworth Colours");
    }
}
