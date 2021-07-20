using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_IshiharaPlayer : MonoBehaviour
{
    [SerializeField]
    private Sc_Ishihara_GameManager gameManager;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Entry_Point") {
            bool hasPlayerEntered = other.gameObject.GetComponent<Sc_Ishihara_SpawnPoint>().hasPlayerEntered;
            GameObject fence = other.gameObject.transform.parent.transform.GetChild(1).gameObject;
            if (!hasPlayerEntered) {
                Transform spawnPointTransform = other.gameObject.transform.GetChild(0).transform;
                Debug.Log("spawnPoint.forward :"+spawnPointTransform.forward);
                gameManager.GetRandomQuestion();
                gameManager.SpawnQuestion(spawnPointTransform, fence);
            }
        }
    }
}
