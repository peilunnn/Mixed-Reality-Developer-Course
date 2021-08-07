using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // Like what we learn in primary school, speed multiplied by time to get the distance
            // so we use speed * Time.deltaTime to have a smooth movement
            transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            // For moving backward, you have to add a - to speed so that you inverse the direction
            transform.Translate(0.0f, 0.0f, -speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed * Time.deltaTime, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f);
        }
    }
}
