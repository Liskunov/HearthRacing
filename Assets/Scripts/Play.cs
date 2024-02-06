using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{

    public void Start ()
    {
        Object.DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);
    }
        
}
