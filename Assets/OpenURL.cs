using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
   public void OpenWebsite(string url = "https://unity3d.com/"){
      Application.OpenURL(url);
   }
}
