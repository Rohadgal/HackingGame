using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVerification : MonoBehaviour{
    [SerializeField] private List<CubeTrigger> cubeTriggers;
    private bool canShowCode;
    void Update(){
        int countCubes = 0;
        foreach (CubeTrigger cubeTrigger in cubeTriggers) {
            if (cubeTrigger.getIsCorrect()) {
                countCubes++;
            }

            if (countCubes == 4 && !canShowCode) {
                canShowCode = true;
                Debug.Log("SHOW PASSCODE");
            }
        }
    }
}
