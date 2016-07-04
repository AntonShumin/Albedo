using UnityEngine;
using System.Collections;

public class script_GameManager : script_Singleton<script_GameManager>
{

    void Start()
    {
        Application.targetFrameRate = 90;
    }
}
