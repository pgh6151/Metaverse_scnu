using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_first_player : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    public Animator animator;
    float hAxis;

    Vector3 moveVec;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if(MIniGamemanager.Instance.ST == true)
        {
            animator.enabled = true;
        }

        hAxis = Input.GetAxisRaw("Horizontal");

        moveVec  = new Vector3(hAxis,0,0).normalized;

        transform.position += moveVec * 3f * Time.deltaTime;
    }
    

}