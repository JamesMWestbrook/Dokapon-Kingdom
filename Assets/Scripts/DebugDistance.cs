using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDistance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Vector2.Distance(this.transform.position, GameObject.Find("GameManager").transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
