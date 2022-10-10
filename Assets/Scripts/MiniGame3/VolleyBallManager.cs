using System.Collections;
using Mapbox.Unity.Utilities;
using MiniGame3;
using Unity.VisualScripting;
using UnityEngine;

public class VolleyBallManager : MonoBehaviour
{
    public Team team = Team.Red;
    [SerializeField] int redPoints;
    [SerializeField] int bluePoints;
    [SerializeField] Vector3 redBallPos = new Vector3(0, 7f, -13.9f);
    [SerializeField] Vector3 blueBallPos = new Vector3(0, 7f, 8.1f);
    [SerializeField] float _time = 3f;
    [SerializeField] bool _isBlueGetPoint = false;
    [SerializeField] int _touchCount = 3;

    VolleyBall _volleyBall;
    Score _score;

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
        var objs = FindObjectsOfType<VolleyBallManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        
        _volleyBall = FindObjectOfType<VolleyBall>();
        _score = FindObjectOfType<Score>();
        ResetRound(_isBlueGetPoint);
    }

    public void OutScore(Team team)
    {
        if (team == Team.Red)
        {
            bluePoints++;
            _isBlueGetPoint = true;
            if (bluePoints == 15)
            {
                // 블루 승리
                
            }
        }
        else
        {
            redPoints++;
            _isBlueGetPoint = false;
            if (redPoints == 15)
            {
                // 레드 승리
            
            }
        }

        ResetRound(_isBlueGetPoint);
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
                    }
                    else
                    {
                        bluePoints++;
                        _isBlueGetPoint = true;
                    }
                    break;
                case Team.Blue:
                    if (ground == Ground.Blue)
                    {
                        redPoints++;
                        _isBlueGetPoint = false;
                    }
                    else
                    {
                        bluePoints++;
                        _isBlueGetPoint = true;
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
            }
            else
            {
                redPoints++;
                _isBlueGetPoint = false;
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
        yield return new WaitForSeconds(_time);
        ballRigid.useGravity = true;
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
            }
            else
            {
                bluePoints++;
                _isBlueGetPoint = true;
            }
            ResetRound(_isBlueGetPoint);
        }
    }
}
