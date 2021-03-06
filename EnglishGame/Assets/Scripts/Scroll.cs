using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] float endPos;
    [SerializeField] float movePos;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(0, speed, 0);
        if(transform.position.y > endPos){
            transform.position = new Vector3(transform.position.x, movePos, 0);
        }
    }
}
