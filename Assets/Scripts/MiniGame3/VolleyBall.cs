using System;
using ExitGames.Client.Photon.StructWrapping;
using MiniGame3;
using UnityEngine;


public class VolleyBall : MonoBehaviour
{
    [SerializeField] private float power = 5f;
    private Rigidbody _rigidbody;
    Team _team = Team.Red;
    Team _previousTeam;
    [SerializeField] bool _isPassedSky = false;
    // TODO : 처음 팀 세팅 해줘야함
    VolleyBallManager _volleyBallManager;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _volleyBallManager = VolleyBallManager.Instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.tag)
        {
            case "Player":
                Vector3 direction = Camera.main.transform.forward + Vector3.up * 3f;
                _rigidbody.AddForce(direction * power, ForceMode.Impulse);
                
                _team = collision.collider.GetComponent<VolleyBallPlayer>().GetTeam();
                if (_previousTeam.Equals(_team))
                    _volleyBallManager.IncreaseTouchCount(_team);
                _previousTeam = _team;
                
                _isPassedSky = false;
                break;
            // 바깥 부분 땅
            case "Ground":
                _volleyBallManager.OutScore(_team);
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            // 빨간 / 파란 부분 땅
            case "Ground":
                var ground = other.GetComponent<PlayGround>().GetSide();
                _volleyBallManager.IncreaseScore(ground, _team, _isPassedSky);
                break;
            case "Sky":
                _isPassedSky = true;
                break;
        }
    }
}
