using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _redScoreText;
    [SerializeField] TextMeshProUGUI _blueScoreText;
    public void SetScoreText(int redPoints, int bluePoints)
    {
        _redScoreText.text = "RedScore : " + redPoints;
        _blueScoreText.text = "BlueScore : " + bluePoints;
    }
}
