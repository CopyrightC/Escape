using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaclemovement : MonoBehaviour
{
    float period = 2f;
    // Start is called before the first frame update
    [SerializeField] Vector3 movementvector;
    Vector3 startingpos;
    [Range(0,1)][SerializeField] float movementslide;
    void Start()
    {
        startingpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2f;
        float sinwave = Mathf.Sin(cycles * tau);
        movementslide = (sinwave /2f) + 0.5f;
        Vector3 changepos = movementslide*movementvector;
        transform.position = startingpos + changepos;
    }
}
