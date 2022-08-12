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
    Vector3 currentAngles;
    Vector3 SavePos;
    Vector3 SPos;

    [SerializeField]
    float distance = 5f;

    [SerializeField]
    private CamRot rot;

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
            currentAngles = new Vector3(clamp_x, clamp_y, 0);
        }

    public void Current(bool isInput){
        Debug.Log("ff");
        // transform.position= new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
        // transform.Rotate(new Vector3(clamp_x, clamp_y, 0));
        // transform.Rotate(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
    }

    public void RotateEuler()
    {
        
        // transform.rotation = (Quaternion.Euler(currentAngles), Space.World);
        // transform.Rotate(new Vector3(clamp_x, clamp_y, 0));

        // transform.rotation = Quaternion.Euler(currentAngles.x, currentAngles.y, currentAngles.z);

        // SPos = new Vector3(currentAngles.x, currentAngles.y, currentAngles.z);
        // SavePos = SPos;

        // transform.position = new Vector3(SavePos.x, SavePos.y, SavePos.z);
        // Debug.Log("b" + transform.rotation);
        // Debug.Log("b" + transform.position);
        
        // rot.Save(currentAngles);
    }

        void Update()
        {
            //RotateEuler();
            Debug.Log("b" + transform.rotation);
        transform.localEulerAngles = currentAngles;
            // transform.rotate(transform.eulerAngles);
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

