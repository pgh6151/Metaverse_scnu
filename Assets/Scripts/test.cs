using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] GameObject volleyball;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetReflected();
        //Spike();
    }

    private void GetReflected()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.black);
        Vector3 volleyballVector = transform.position - volleyball.transform.position;
        Debug.DrawRay(transform.position, volleyballVector, Color.red);
        /*Vector3 planeTangent = Vector3.Cross(volleyallVector, Camera.main.transform.forward);
        Debug.DrawRay(transform.position, planeTangent, Color.yellow);
        Vector3 planeNormal = Vector3.Cross(planeTangent, volleyallVector);
        Debug.DrawRay(transform.position, planeNormal, Color.green);*/
        Vector3 reflected = Vector3.Reflect(volleyballVector, Camera.main.transform.up);
        Debug.DrawRay(transform.position, reflected, Color.blue);
        //return reflected;
    }

    void Spike()
    {
        /*if (Physics.OverlapSphere(transform.position, 5f, LayerMask.NameToLayer("Ball")))
        {*/
        Collider[] go = Physics.OverlapSphere(transform.position, 5f);
        Debug.Log(go[1]);
        // go[1].GetComponent<Rigidbody>().AddForce(GetReflected() * Time.deltaTime);
        //}
        
        
    }
}
