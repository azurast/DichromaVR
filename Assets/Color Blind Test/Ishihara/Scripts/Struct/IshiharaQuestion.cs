using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

[System.Serializable]
public class IshiharaQuestion {
    public IshiharaColour referenceObject;
    public IshiharaColour answerObject;
    public Color neutralObject;
    public IshiharaColour firstChoiceObject;
    public IshiharaColour secondChoiceObject;
    public bool answer;
    public bool hasBeenAnswered;
}
