using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGround : MonoBehaviour
{
    [SerializeField] Side _side;
    int _point;
    int _maxPoint = 15;
    int _touchCount = 3;
    bool _cross;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public Side GetSide()
    {
        return _side;
    }

}

public enum Side
{
    Red,
    Blue
}
