using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    GameObject Char;
    GameObject QF;

    // Start is called before the first frame update
    void Start()
    {
        Char = GameObject.Find("Player(Clone)");
        QF = GameObject.Find("QuadF");
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        //QF.transform.rotation = new Quaternion(0, 0, 0, 0);

        //        cube.transform.position = new Vector3(h, 0, v));
        //(float)transform.position = (float)Char.transform.position + new Vector3(0, 0.743, 0.555);
        //transform.Translate(Char.transform.position.x, Char.transform.position.y+0.743, Char.transform.position.z+0.555);
        transform.position = new Vector3(Char.transform.position.x + 0.0f,
                                        Char.transform.position.y + 0.743f,
                                        Char.transform.position.z + 0.5f);
        //QF.transform.position = new Vector3(Char.transform.position.x + 0.0f,
                                        //Char.transform.position.y + 0.743f,
                                        //Char.transform.position.z + 1.2f);
    }
}