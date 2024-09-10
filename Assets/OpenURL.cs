using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenURL : MonoBehaviour{
   private string _url = "";
   public void OpenWebsite(){
      if (_url == "") {
         Debug.Log("No valid url");
         return;
      }
      Application.OpenURL(_url);
   }

   public void GetUrl(string t_url){
      _url = t_url;
      Debug.Log(_url);
   }
}
