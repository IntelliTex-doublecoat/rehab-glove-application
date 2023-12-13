using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotMove : MonoBehaviour
{
    public float amplitude = 1.0f;  // amplitude
    public float speed = 1.0f;      // speed

    private Vector3 startPos;       // original pos

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;  // record original pos
    }

    // Update is called once per frame
    void Update()
    {
        // sin wave, positive range
        float newY = startPos.y + Mathf.Abs(Mathf.Sin(Time.time * speed)) * amplitude;

        // refresh position
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    
    }
}
