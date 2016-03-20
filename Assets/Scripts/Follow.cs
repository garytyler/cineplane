using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

    public GameObject follow;

    void Update()
    {
        transform.position = follow.transform.position;
        transform.rotation = follow.transform.rotation;
    }
}
