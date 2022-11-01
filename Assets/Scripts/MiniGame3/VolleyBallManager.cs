using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Utilities;
using MiniGame3;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class VolleyBallManager : MonoBehaviour
{
    public Team team = Team.Red;
    public bool isCollided;
    [SerializeField] int redPoints;
    [SerializeField] int bluePoints;
    [SerializeField] Vector3 redBallPos = new Vector3(0, 7f, -13.9f);
    [SerializeField] Vector3 blueBallPos = new Vector3(0, 7f, 8.1f);
    [SerializeField] int _time = 0;
    [SerializeField] int _timer = 3;
    [SerializeField] bool _isBlueGetPoint = false;
    [SerializeField] int _touchCount = 3;
    [SerializeField] float _winTime = 5f;
    [SerializeField] Transform blueWinTrans;
    [SerializeField] Transform redWinTrans;

    VolleyBall _volleyBall;
    Score _score;
    List<ParticleSystem> blueParticles = new List<ParticleSystem>();
    List<ParticleSystem> redParticles = new List<ParticleSystem>();

    private static VolleyBallManager _instance;
    public static VolleyBallManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("@VolleyBallManager");
                _instance = go.AddComponent<VolleyBallManager>();
            }
            return _instance;
        }
    }
    void Awake()
    {
        Init();
    }

    void Init()
    {
        _volleyBall = PhotonNetwork.Instantiate("Volleyball", Vector3.zero, Quaternion.identity).GetComponent<VolleyBall>();
        _score = FindObjectOfType<Score>();
        _time = _timer;
        blueWinTrans = GameObject.Find("BlueWinParticles").transform;
        foreach (Transform go in blueWinTrans)
        {
            blueParticles.Add(go.GetComponent<ParticleSystem>());
        }
        redWinTrans = GameObject.Find("RedWinParticles").transform;
        foreach (Transform go in redWinTrans)
        {
            redParticles.Add(go.GetComponent<ParticleSystem>());
        }
        ResetRound(_isBlueGetPoint);
    }

    public void OutScore(Team team)
    {
        if (team == Team.Red)
        {
            bluePoints++;
            _isBlueGetPoint = true;
            _score.SetScoreText(redPoints, bluePoints);

            if (bluePoints == 15)
            {
                // 블루 승리
                StartCoroutine(BlueWIn());
                return;
            }
        }
        else
        {
            redPoints++;
            _isBlueGetPoint = false;
            _score.SetScoreText(redPoints, bluePoints);

            if (redPoints == 15)
            {
                // 레드 승리
                StartCoroutine(RedWIn());
                return;
            }
        }

        ResetRound(_isBlueGetPoint);
    }

    IEnumerator BlueWIn()
    {
        foreach (var particleSystem in blueParticles)
        {
            particleSystem.Play();
        }
        yield return new WaitForSeconds(_winTime);
        foreach (var particleSystem in blueParticles)
        {
            particleSystem.Stop();
        }
    }
    
    IEnumerator RedWIn()
    {
        foreach (var particleSystem in redParticles)
        {
            particleSystem.Play();
        }
        yield return new WaitForSeconds(_winTime);
        foreach (var particleSystem in redParticles)
        {
            particleSystem.Stop();
        }
        
    }

    public void IncreaseScore(Ground ground, Team team, bool isPassedSky)
    {
        // 네트 위를 넘었을 때
        if (isPassedSky)
        {
            switch (team)
            {
                case Team.Red:
                    if (ground == Ground.Blue)
                    {
                        redPoints++;
                        _isBlueGetPoint = false;
                        _score.SetScoreText(redPoints, bluePoints);

                        if (redPoints == 15)
                        {
                            // 레드 승리
                            Debug.Log("Why");
                            StartCoroutine(RedWIn());
                            return;
                        }
                    }
                    else
                    {
                        bluePoints++;
                        _isBlueGetPoint = true;
                        _score.SetScoreText(redPoints, bluePoints);

                        if (bluePoints == 15)
                        {
                            // 블루 승리
                            StartCoroutine(BlueWIn());
                            return;
                        }
                    }
                    break;
                case Team.Blue:
                    if (ground == Ground.Blue)
                    {
                        redPoints++;
                        _isBlueGetPoint = false;
                        _score.SetScoreText(redPoints, bluePoints);

                        if (redPoints == 15)
                        {
                            // 레드 승리
                            StartCoroutine(RedWIn());
                            return;
                        }
                    }
                    else
                    {
                        bluePoints++;
                        _isBlueGetPoint = true;
                        _score.SetScoreText(redPoints, bluePoints);

                        if (bluePoints == 15)
                        {
                            // 블루 승리
                            StartCoroutine(BlueWIn());
                            return;
                        }
                    }
                    break;
            }
        }
        // 네트 위를 안 넘었을 때
        else
        {
            if (ground == Ground.Red)
            {
                bluePoints++;
                _isBlueGetPoint = true;
                _score.SetScoreText(redPoints, bluePoints);

                if (bluePoints == 15)
                {
                    // 블루 승리
                    StartCoroutine(BlueWIn());
                    return;
                }
            }
            else
            {
                redPoints++;
                _isBlueGetPoint = false;
                _score.SetScoreText(redPoints, bluePoints);

                if (redPoints == 15)
                {
                    // 레드 승리
                    StartCoroutine(RedWIn());
                    return;
                }
            }
        }

        ResetRound(_isBlueGetPoint);
    }

    void ResetRound(bool isBlueGetPoint)
    {
        _score.SetScoreText(redPoints, bluePoints);
        if (isBlueGetPoint)
            _volleyBall.transform.position = blueBallPos;
        else
            _volleyBall.transform.position = redBallPos;
        
        var ballRigid = _volleyBall.GetComponent<Rigidbody>();
        ballRigid.velocity = Vector3.zero;
        ballRigid.angularVelocity = Vector3.zero;
        _touchCount = 0;
        StartCoroutine(WaitForRound(ballRigid));
    }

    IEnumerator WaitForRound(Rigidbody ballRigid)
    {
        ballRigid.useGravity = false;
        while (_time >= 0)
        {
            _score.WaitTimeText(_time);
            yield return new WaitForSeconds(1f);
            _time--;
        }
        ballRigid.useGravity = true;
        isCollided = false;
        _time = _timer;
    }

    public void IncreaseTouchCount(Team team)
    {
        _touchCount++;
        if (_touchCount > 3)
        {
            if (team == Team.Blue)
            {
                redPoints++;
                _isBlueGetPoint = false;
                isCollided = true;
            }
            else
            {
                bluePoints++;
                _isBlueGetPoint = true;
                isCollided = true;
            }
            ResetRound(_isBlueGetPoint);
        }
    }

    
}
