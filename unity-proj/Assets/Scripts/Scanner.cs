using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetMask;
    public RaycastHit2D[] targets;
    public Transform nearestTarget;

    private void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetMask);
        nearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        
        float minDistance = 100f;
        foreach (var hit in targets)
        {
            float distance = Vector3.Distance(transform.position, hit.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                result = hit.transform;
            }
        }
        return result;
    }
}
