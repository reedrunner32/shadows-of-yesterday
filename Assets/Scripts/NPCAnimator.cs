using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().speed = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
