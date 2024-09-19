using System.Collections.Generic;
using UnityEngine;

public class TriggerVerification : MonoBehaviour{
    [SerializeField] private List<CubeTrigger> cubeTriggers;
    [SerializeField] private GameObject codeImage;
    private bool canShowCode;
    
    public delegate void codeHandler();

    public static event codeHandler notifyCodeOn;

    private void Start(){
        if(codeImage != null){ codeImage.SetActive(false);}
    }

    void Update(){
        int countCubes = 0;
        foreach (CubeTrigger cubeTrigger in cubeTriggers) {
            if (cubeTrigger.getIsCorrect()) {
                countCubes++;
            }

            if (countCubes == 4 && !canShowCode) {
                canShowCode = true;
                codeImage?.SetActive(true);
               // Debug.Log("SHOW PASSCODE");
                notifyCodeOn?.Invoke();
                
            }
        }
    }
}
