using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

    public GameObject target;

    void Update()
    {
        transform.position = target.transform.position;
        transform.rotation = target.transform.rotation;
    }
}
