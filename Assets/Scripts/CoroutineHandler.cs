using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHandler : MonoBehaviour
{
    private static CoroutineHandler instance;
    public static CoroutineHandler Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject inst = new GameObject("CoroutineHandler");
                DontDestroyOnLoad(inst);
                instance = inst.AddComponent<CoroutineHandler>();
            }
            return instance;
        }
    }
}
