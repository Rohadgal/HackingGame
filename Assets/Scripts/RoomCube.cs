using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCube : MonoBehaviour{
  [SerializeField]
  private int cubeNumber;

  public int getCubeNumber(){
    return cubeNumber;
  }
}
