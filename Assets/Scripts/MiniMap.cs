using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    void Awake()
    {
        StaticInfo.miniMap = gameObject;
        gameObject.SetActive(false);
    }
}
