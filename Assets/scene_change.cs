using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scene_change : MonoBehaviour
{
    public InputField input;
    public static string ip;
  
    //to load difft scenes:
    public void toscene0()
    {
        SceneManager.LoadScene("scene0");
    }

    public void toscene1()
    {
        SceneManager.LoadScene("SAVe");
    }
   /* public void toscene2()
    {
        SceneManager.LoadScene("scene2");
    }*/

    public void OnLocalHost()
    {
        input.text = "127.0.0.1";
        
    }

    public void OnApply()
    {
        ip = input.text.ToString();
    }


}
