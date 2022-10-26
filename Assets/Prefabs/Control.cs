using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    private float h = 0.0f;
    private float v = 0.0f;
    private float r = 0.0f;
    private float moveSpeed = 5.0f;
    private float rotationSpeed = 50.0f;
    private Transform playerTr;

    // Start is called before the first frame update
    void Start()
    {
        playerTr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxis("Mouse X");
        Debug.Log("H:" + h.ToString() + "V:" + v.ToString());
        playerTr.Translate(new Vector3(h, 0, v) * moveSpeed * Time.deltaTime);
        playerTr.Rotate(new Vector3(0, r, 0) * rotationSpeed * Time.deltaTime);
    }
}