using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar_Script : MonoBehaviour
{
    public float floatSpeed = 0.1f;
    public float customFloatHeight = -1f;

    // Update is called once per frame
    void Update()
    {
        if (customFloatHeight == -1) {
            transform.position = transform.position + transform.up * (Mathf.Sin (Time.time * floatSpeed * (1 / transform.localScale.x)) * 0.01f * (1 / transform.localScale.x));
        }
        else {
            transform.position = transform.position + transform.up * (Mathf.Sin (Time.time * floatSpeed) * 0.01f * customFloatHeight);
        }
    }
}
