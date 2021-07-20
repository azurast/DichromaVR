using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_Ishihara_GameManager : MonoBehaviour
{
    public IshiharaQuestion[] ishiharaQuestions;
    private static List<IshiharaQuestion> unansweredQuestions;
    public IshiharaQuestion currentQuestion;
    public GameObject[] gemsPrefab;
    public GameObject containerPrefab;
    private List<Color> currentQuestionOptions;
    // [SerializeField]
    public GameObject selectedGemWaypoint;
    private GameObject[] currentQuestionObjects; 
    int referenceXPos = 0;
    float optionsXPos = -2.25f;

    // For spawning the question in front of the player
    private Vector3 spawnPointPosition;
    private Vector3 spawnPointDirection;
    private Quaternion spawnPointRotation;
    private float spawnDistance = 1f;
    private Vector3 referenceSpawnPosition;
    private Vector3 optionsSpawnPosition;
    private Vector3 middlePos;
    public int randomIndex;
    private int correctScore = 0;
    private int neutralScore = 0;
    private int wrongScore = 9;
    private string colorBlindType = "";
    private GameObject currentQuestionFenceReference;
    public GameObject finishPoint;

    void Start()
    {
        currentQuestionObjects = new GameObject[4];
        if (unansweredQuestions == null || unansweredQuestions.Count == 0) {
            unansweredQuestions = ishiharaQuestions.ToList<IshiharaQuestion>();
        }

    }

    // Get a random question from the array
    public void GetRandomQuestion() {
        randomIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[0];
    }

    public void SpawnQuestion(Transform spawnPointTransform, GameObject fence) {
        
        // Conver each data to Color 
        Color referenceColor = LUV_to_Yxy(currentQuestion.referenceObject.U, currentQuestion.referenceObject.V);
        Color answerColor = LUV_to_Yxy(currentQuestion.answerObject.U, currentQuestion.answerObject.V); 
        Color firstChoiceColor = LUV_to_Yxy(currentQuestion.firstChoiceObject.U, currentQuestion.firstChoiceObject.V);;
        Color secondChoiceColor = LUV_to_Yxy(currentQuestion.secondChoiceObject.U, currentQuestion.secondChoiceObject.V);;
        Color neutralColor = currentQuestion.neutralObject;
 
        // Store reference to the spawn point's position, direction and rotation
        spawnPointPosition = spawnPointTransform.position;
        spawnPointDirection = spawnPointTransform.forward;
        spawnPointRotation = spawnPointTransform.rotation;

        // Reference Object Spawn Point
        referenceSpawnPosition = spawnPointPosition;

        // Object Directions
        if (spawnPointTransform.forward.x > 0) {
            middlePos = new Vector3(0, 0, -2.25f);
        } else if (spawnPointTransform.forward.x < 0) {
            middlePos = new Vector3(0, 0, 2.25f);
        } else if (spawnPointTransform.forward.z > 0) {
            middlePos = new Vector3(-2.25f, 0, 0);
        } else if (spawnPointTransform.forward.z < 0) {
            middlePos = new Vector3(2.25f, 0, 0);
        }
        optionsSpawnPosition = (spawnPointPosition - middlePos) + (spawnPointDirection * spawnDistance);

        // Initialize Options
        currentQuestionOptions = new List<Color>() ;

        // Set the options to get randomized order
        currentQuestionOptions.Add(answerColor);
        currentQuestionOptions.Add(neutralColor);
        currentQuestionOptions.Add(firstChoiceColor);
        currentQuestionOptions.Add(secondChoiceColor);

        // Randomize the order of the options
        currentQuestionOptions = Shuffle(currentQuestionOptions);

        // Instantiate the reference object
        GameObject referenceObject = Instantiate(containerPrefab, new Vector3(referenceSpawnPosition.x, 2f, referenceSpawnPosition.z), spawnPointRotation);
        referenceObject.GetComponent<Renderer>().material.color = referenceObject.GetComponent<Renderer>().material.color = referenceColor;
        selectedGemWaypoint = referenceObject.transform.GetChild(0).gameObject;
        
        // Instantiate the options
        for (int i = 0; i < 4; i++) {
            GameObject option = Instantiate(gemsPrefab[0], new Vector3(optionsSpawnPosition.x, 2.4f, optionsSpawnPosition.z), spawnPointRotation);
            // For checking later if the player has picked the correct object color
            if(currentQuestionOptions[i] == answerColor) {
                option.gameObject.tag = "Answer_Object";
            } else if(currentQuestionOptions[i] == neutralColor) {
                option.gameObject.tag = "Neutral_Object";
            } else if(currentQuestionOptions[i] == firstChoiceColor){
                option.gameObject.tag = "Choice_Object1";
            } else {
                 option.gameObject.tag = "Choice_Object2";
            }
            option.GetComponent<Renderer>().material.color = currentQuestionOptions[i];
            currentQuestionObjects[i] = option;
            /*
            * if player is facing x axis then decrement z position
            *  else if player is facing z axis then decrement x position
            */
            if (spawnPointTransform.forward.x > 0) {
                optionsSpawnPosition.z -= 1.5f;
            } else if (spawnPointTransform.forward.x < 0) {
                optionsSpawnPosition.x += 1.5f;
            } else if (spawnPointTransform.forward.z > 0) {
                optionsSpawnPosition.x -= 1.5f;
            } else if (spawnPointTransform.forward.z < 0) {
                optionsSpawnPosition.x += 1.5f;
            }
        }

        currentQuestionFenceReference = fence;
    }

    public void OnQuestionAnswered(string objectType) {
        /*  Answer is always false unless the object is answer object
            Keeps track of the correct & neutral answers for result purposes
        */
        bool answer = false;
        if (objectType == "Answer_Object") {
            answer = true;
            correctScore += 1;
        } else if (objectType == "Neutral_Object") {
            neutralScore += 1;
            // wrongScore += 1;
        } 
        // else {
        //     wrongScore += 1;
        // }

        // Destory the unanswered questions
        for(int i = 0; i < 4; i++) {
            if (currentQuestionObjects[i].tag != objectType) {
                Destroy(currentQuestionObjects[i]);
            }
        }

        // Store data
        unansweredQuestions[randomIndex].answer = answer;
        unansweredQuestions[randomIndex].hasBeenAnswered = true;

        // Remove question from list so that it doesn't get respawned
        unansweredQuestions.RemoveAt(randomIndex);

        // Mark level as finished
        currentQuestionFenceReference.gameObject.GetComponent<Animator>().SetBool("isLevelFinished", true);
  
        if (unansweredQuestions.Count <= 0) {
            CalculateResult();
        }
    }

    public void CalculateResult() {
        wrongScore -=  correctScore;
        if (neutralScore >= 5) {
            // Play again
            Debug.Log("Play Again");
            finishPoint.GetComponent<Sc_Ishihara_FinishPoint>().ToggleRetryPanel(true);
            StartCoroutine(RestartCoroutine());
            SceneManager.LoadScene("Ishihara Colours");
        } else {
           if (wrongScore <= 1) {
               // NormalZ
               colorBlindType = "Normal Color Vision";
           } else if (wrongScore >= 2 && wrongScore < 5) {
               // Slight
               colorBlindType = "Slight Red-Greed Color Deficiency";
           } else if (wrongScore >= 5) {
               // Strong
               colorBlindType = "Strong Red-Greed Color Deficiency";
           }
           Debug.Log("===Color Blind Type :"+colorBlindType);
           finishPoint.SendMessage("SetColorBlindType", colorBlindType);
        }
    }
    // Randomize the order or choices
    List<Color> Shuffle(List<Color> options) {
        for (int i = 0; i < 3; i++) {
            int rnd = Random.Range(0, 3);
            Color tempGO = options[rnd];
            options[rnd] = options[i];
            options[i] = tempGO;
        }
        // Debug.Log("===options[0] :"+ options[0]);
        return options;
    }

    /* COLOR SPACE CONVERSIONS */
    Color LUV_to_Yxy(double u, double v) {
        double x = ((9 * u) / (6 * u - 16 * v + 12));
        double y = ((4 * v) / (6 * u - 16 * v + 12));

        Color res = Yxy_to_XYZ(x, y);
        return res;
    }
    Color Yxy_to_XYZ(double x, double y) {
        // Constant Luminance
        double Y = 100;
        double X = (x / y) * Y;
        double Z = ((1 - x - y) / y) * Y;

        Color res = XYZ_to_RGB(X, Y, Z);
        return res;
    }
    Color XYZ_to_RGB(double X, double Y, double Z) {
        double R = ((3.2404542 * X) + (-1.5371385 * Y) + (-0.4985314 * Z))/255;
        double G = ((-0.9692660 * X) + (1.8760108 * Y) + (0.0415560 * Z))/255;
        double B = ((0.0556434 * X) + (-0.2040259 * Y) + (1.0572252 * Z))/255;

        Color res = new Color((float)R, (float)G, (float)B, 1f);
        return res;
    } 

    IEnumerator RestartCoroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);
    }
}
