using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject Rock;
    Vector3 RanSpawner;
    int routineControll =0;
    
    void Start()
    {   
        
    }

    // Update is called once per frame
    void Update()
    {   
        RanSpawner = new Vector3(Random.Range(-7, 4), 1.18f, 44.93f);

        if(MIniGamemanager.Instance.ST == true)
        {
            routineControll = 1;
        }

        if(routineControll == 1)
        {
            StartCoroutine("InstRock");
            routineControll--;
        }
    }

    IEnumerator InstRock()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.5f);
            Instantiate(Rock, RanSpawner, gameObject.transform.rotation);
            
        }
        

    }
        
        
}
