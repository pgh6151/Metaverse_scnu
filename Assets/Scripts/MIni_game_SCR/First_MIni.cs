using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class First_MIni : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Player.transform.position  = new Vector3(-1.2f,0,0);
        }
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Player.transform.position  = new Vector3(1.2f,0,0);
        }

    }
}
