using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowModel : MonoBehaviour
{
    [SerializeField] private Transform model;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, model.position, 10);
    }
}
