using System.Collections;
using System.Text;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _redScoreText;
    [SerializeField] TextMeshProUGUI _blueScoreText;
    [SerializeField] TextMeshProUGUI _waitTimeText;
    public void SetScoreText(int redPoints, int bluePoints)
    {
        _redScoreText.text = "RedScore : " + redPoints;
        _blueScoreText.text = "BlueScore : " + bluePoints;
    }

    public void WaitTimeText(int time)
    {
        if (time == 0)
            _waitTimeText.gameObject.SetActive(false);
        else
            _waitTimeText.gameObject.SetActive(true);
        _waitTimeText.text = string.Format("{0}", time);
    }

}
