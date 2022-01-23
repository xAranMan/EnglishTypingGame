using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainScroll : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float endPos;
    [SerializeField] float movePos;
    
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(speed, 0, 0);
        if(transform.position.x > endPos){
            transform.position = new Vector3(movePos, transform.position.y, 0);
        }
    }
}
