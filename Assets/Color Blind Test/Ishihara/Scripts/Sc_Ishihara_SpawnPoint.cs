using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Ishihara_SpawnPoint : MonoBehaviour
{
    public bool hasPlayerEntered = false;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" && hasPlayerEntered == false) {
            hasPlayerEntered = true;
        }
    }
}
