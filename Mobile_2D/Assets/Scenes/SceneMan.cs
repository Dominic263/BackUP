
/*
* This module handles scene management for the Home, About Us and Statistics
* pages. The Unity.SceneManagement namespace allows for this functionality
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan : MonoBehaviour
{

    
    
    /* Function to invoke the About us page */
    public void AboutUs()
    {
        SceneManager.LoadScene(1);
    }

    /* Function to invoke the Statistics/Monitoring Page */
    public void Statistics()
    { 
        SceneManager.LoadScene(2);
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }
}
