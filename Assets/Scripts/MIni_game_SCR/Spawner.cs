using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject Rock;

    Vector3 RanSpawner;
    int i;
    // Start is called before the first frame update
    void Start()
    {   
        StartCoroutine("InstRock");
    }

    // Update is called once per frame
    void Update()
    {
        RanSpawner = new Vector3(Random.Range(-7, 4), 1.18f, 44.93f);
        
        
    }

    IEnumerator InstRock()
    {
        
        while (i < 30)
        {
            yield return new WaitForSeconds(1f);
            Instantiate(Rock, RanSpawner, gameObject.transform.rotation);
            i++;
        }
        
    }
}
