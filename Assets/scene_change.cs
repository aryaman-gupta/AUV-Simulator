using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_change : MonoBehaviour
{
   
  
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
}
