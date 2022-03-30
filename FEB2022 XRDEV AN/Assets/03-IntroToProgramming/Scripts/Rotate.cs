using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Rotate : MonoBehaviour
{
    public Transform transformToRotate;
    public float spinSpeed;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transformToRotate.Rotate(0, 1 * Time.deltaTime * spinSpeed, 0);
    }
}
