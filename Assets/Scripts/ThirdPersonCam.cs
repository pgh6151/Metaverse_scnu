using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    #region Private Fields
    Transform offsetTrans;
    #endregion

    #region Public Fields
    [SerializeField]
    float mouseXSensivity = 100f;
    [SerializeField]
    float mouseYSensivity = 300f;
    [SerializeField]
    GameObject topViewCam;
    #endregion

    #region MonoBehaviour Methods
    private void Awake() 
    {
        offsetTrans = transform;
    }
    void Start()
    {
    }

    void Update()
    {
        
    }
    #endregion

    #region Public Methods
    public void BackToOffset()
    {
        if (topViewCam.activeSelf)
        {
            topViewCam.SetActive(false);
            transform.position = offsetTrans.position;
            transform.rotation = offsetTrans.rotation;
        }
        else
        {
            topViewCam.SetActive(true);

        }
        
    }
    #endregion
}
