using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LookRotate : MonoBehaviour
{
    float rotate_x;
    float rotate_y;
    float clamp_x;
    float clamp_y;
    public Transform Player;
    private CamRot rot;
    Vector3 currentAngles;

    [SerializeField]
    float distance = 5f;

    [SerializeField]
    float sensitivity = 50f;

    void Start() {
        Player = GameObject.Find("Dummy/Look").transform;
        transform.position = Player.position - transform.forward * distance;
    }

    public void RotateCam(Vector3 NowPos)
        {
            transform.position = Player.position - transform.forward * distance;
            Vector2 movedPos = NowPos - transform.position;
            rotate_x = movedPos.y / Screen.width * sensitivity;
            rotate_y = -movedPos.x / Screen.width * sensitivity;
            clamp_x = Mathf.Clamp(rotate_x, -30, 30);
            clamp_y = Mathf.Clamp(rotate_y, -50, 50);
        }

    public void Current(bool isInput){
        Debug.Log("ff");
        // transform.Rotate(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
    }

    public void RotateEuler()
    {
        currentAngles = new Vector3(clamp_x, clamp_y, 0);
        transform.localEulerAngles = currentAngles;
        transform.rotation = Quaternion.Euler(transform.eulerAngles);
    }

        void Update()
        {
            RotateEuler();
            // transform.Rotate(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);



            // Quaternion.Euler(transform.localEulerAngles);
            // transform.rotation = new Quaternion(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
            // Quaterniontransform.localEulerAngles.x;
            // transform.rotation.y = transform.localEulerAngles.y;
            // transform.rotation.z = 0;
            
            // Debug.Log("a" + transform.localEulerAngles);
            // Debug.Log("b" + transform.rotation);

        }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.EventSystems;

// public class LookRotate : MonoBehaviour
// {
//     float rotate_x;
//     float rotate_y;
//     float clamp_x;
//     float clamp_y;
//     public Transform Player;
//     Quaternion Rotate;
//     bool isInput;

//     [SerializeField]
//     float distance = 5f;

//     [SerializeField]
//     float sensitivity = 0.1f;

//     void Start() {
//         Player = GameObject.Find("Dummy/Look").transform;
//         transform.position = Player.position - transform.forward * distance;
//     }

//     public void RotateCam(Vector3 NowPos, bool isInput)
//         {
//             transform.position = Player.position - transform.forward * distance;
//             Vector2 movedPos = NowPos - transform.position;
//             rotate_x = movedPos.y;
//             rotate_y = -movedPos.x;                   
//             clamp_x = Mathf.Clamp(rotate_x, -30, 30);
//             clamp_y = Mathf.Clamp(rotate_y, -50, 50);
//             Rotate = Quaternion.Euler(new Vector3(clamp_x, clamp_y, 0));
//         }

//     public void RotateEuler()
//     {

//         // transform.localEulerAngles = Vector3.Slerp(Quaternion.Euler(transform.rotation), new Vector3(clamp_x, clamp_y, 0), Time.deltaTime * sensitivity);
//         // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(clamp_x, clamp_y, 0), Time.deltaTime);
//         transform.rotation = Quaternion.FromToRotation(transform.rotation, Rotate, Time.deltaTime * sensitivity);
//     }

//         void Update()
//         {
//             RotateEuler();

//             // if(isInput == false)
//             // {
//             //     Debug.Log("false");
//             // }
//         }
// }

