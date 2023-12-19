using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{

    public void MySwitchScenes ()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
        
}
