using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMove : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * 0.05f);

    }

    private void OnTriggerEnter(Collider other) {


        if(other.gameObject.tag == "End")
        {
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
        }
    }

}
