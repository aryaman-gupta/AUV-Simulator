using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArenaMenu : MonoBehaviour {

    public Rigidbody rb;
    public Vector3 v;

    //stores AUV's initial position 
    public void Start()
    {
        v = rb.transform.position;
    }

    //method to launch the main menu
    public void main_menu()
    {
        SceneManager.LoadScene("menu");
    }

    //method to reset the transforms of the AUV
    public void reset_button()
    {

        rb.transform.position = v;
        rb.transform.rotation = Quaternion.Euler(0, 0, 0);
        rb.velocity = new Vector3(0, 0, 0);
    }
}
