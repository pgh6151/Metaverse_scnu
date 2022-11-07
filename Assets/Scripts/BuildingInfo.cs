using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TextInfo
{
    public string building;
    public string name;
    public string info;
}

[Serializable]
public class TextInfos
{
    public List<TextInfo> infos = new List<TextInfo>();

    public void MakeDict(Dictionary<string, TextInfo> dict)
    {
        foreach (var info in infos)
            dict.Add(info.building, info);
        // return dict;
    }
}

public class BuildingInfo : MonoBehaviour
{
    [SerializeField] GameObject infoPanel;
    [SerializeField] Text infoText;
    [SerializeField] Text nameText;
    [SerializeField] Image infoImage;
    Dictionary<string, TextInfo> _dict = new Dictionary<string, TextInfo>();

    void Start()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Data/Data");
        Debug.Log(textAsset);
        TextInfos textInfos = JsonUtility.FromJson<TextInfos>(textAsset.text);
        textInfos.MakeDict(_dict);
        Debug.Log(_dict.Count);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            infoPanel.SetActive(true);
            infoText.text = _dict[name].info;
            nameText.text = $"{name} : {_dict[name].name}";
            infoImage.sprite = Resources.Load<Sprite>($"Texture/Buildings/{name}");
        }
    }
    
}
