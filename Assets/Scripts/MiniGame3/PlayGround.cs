using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;

namespace MiniGame3
{
    public class PlayGround : MonoBehaviour
    {
        [SerializeField] Ground ground;
        int _maxPoint = 15;
        bool _cross;

    

        void Update()
        {
        
        }

        public Ground GetSide()
        {
            return ground;
        }

    

    }

    public enum Ground
    {
        Red,
        Blue,
        OutSide,
    }

    public enum Team
    {
        Red,
        Blue
    }
}