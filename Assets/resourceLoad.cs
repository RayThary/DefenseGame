using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resourceLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject r = Resources.Load<GameObject>("s/Cube");
            Instantiate(r);
            r.transform.position= Vector3.zero;

        }
    }
}
