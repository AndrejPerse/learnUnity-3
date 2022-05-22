using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startLocation;
    private float repeatWidth;

    // Start is called before the first frame update
    void Start()
    {
        startLocation = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < (startLocation.x - repeatWidth))
        {
            transform.position = startLocation;
        }
    }
}
