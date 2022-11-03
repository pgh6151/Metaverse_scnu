using System;
using ExitGames.Client.Photon.StructWrapping;
using MiniGame3;
using Photon.Pun;
using UnityEngine;


public class VolleyBall : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private float power = 5f;
    [SerializeField] float verticalPower = 3f;
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
        if (_volleyBallManager.isCollided)
            return;
        switch (collision.collider.tag)
        {
            case "Player":
                photonView.RPC(nameof(CollisionProcess), RpcTarget.All, collision);
                break;
            // 바깥 부분 땅
            case "Ground":
                _volleyBallManager.OutScore(_team);
                _volleyBallManager.isCollided = true;
                break;
        }
    }

    [PunRPC]
    void CollisionProcess(Collision collision)
    {
        if (photonView.IsMine)
        {
            Transform camTrans = collision.collider.GetComponentInChildren<Camera>().transform;
            Vector3 direction = camTrans.forward + Vector3.up * verticalPower;
            _rigidbody.AddForce(direction * power, ForceMode.Impulse);

            _team = collision.collider.GetComponent<VolleyBallPlayer>().GetTeam();
            if (_previousTeam.Equals(_team))
                _volleyBallManager.IncreaseTouchCount(_team);
            _previousTeam = _team;
            _isPassedSky = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (_volleyBallManager.isCollided)
            return;
        switch (other.tag)
        {
            // 빨간 / 파란 부분 땅
            case "Ground":
                var ground = other.GetComponent<PlayGround>().GetSide();
                _volleyBallManager.IncreaseScore(ground, _team, _isPassedSky);
                _volleyBallManager.isCollided = true;
                break;
            case "Sky":
                _isPassedSky = true;
                break;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_rigidbody.position);
            stream.SendNext(_rigidbody.rotation);
            stream.SendNext(_rigidbody.velocity);
        }
        else
        {
            _rigidbody.position = (Vector3)stream.ReceiveNext();
            _rigidbody.rotation = (Quaternion)stream.ReceiveNext();
            _rigidbody.velocity = (Vector3)stream.ReceiveNext();
        }
    }
}
