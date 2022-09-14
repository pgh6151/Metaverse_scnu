using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class miniGameCotroller : MonoBehaviour
{
    
    public void SwapScene()
    {
        SceneManager.LoadScene("Minigame1");
    }
}
