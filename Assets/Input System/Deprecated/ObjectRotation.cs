using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectRotation : MonoBehaviour
{
    public TextMeshProUGUI text;
    Transform objectTransform;
    Quaternion rotation;
    Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        objectTransform = this.gameObject.GetComponent<Transform>();
        // rotation = this.gameObject.transform.rotation;
        // position = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Rot x : "+ objectTransform.rotation.x.ToString("F2") + "\nRot y : "+ objectTransform.rotation.y.ToString("F2") + "\nRot z : "+ objectTransform.rotation.z.ToString("F2") ;
        // text.text = "EA x : "+ objectTransform.eulerAngles.x.ToString("F2") + "\nEA y : "+ objectTransform.eulerAngles.y.ToString("F2") + "\nEA z : "+ objectTransform.eulerAngles.z.ToString("F2") ;
        // text.text = "X : " + rotation.x.ToString() + "\nY : " + rotation.y.ToString() + "\nZ : " +rotation.z.ToString();
        // text.text = "X : " + position.x.ToString() + "\nY : " + position.y.ToString() + "\nZ : " +position.z.ToString();
    }
}
