using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public InputField input;
    public static string ip;
  
    //to load difft scenes:
    public void toscene0()
    {
        SceneManager.LoadScene("scene0");
        ip = input.text.ToString();
    }

    public void toscene1()
    {
        SceneManager.LoadScene("SAVe");
        ip = input.text.ToString();
    }
    /* public void toscene2()
     {
         SceneManager.LoadScene("scene2");
          ip = input.text.ToString();
     }*/

    public void OnLocalHost()
    {
        input.text = "127.0.0.1";
        
    }

   


}
