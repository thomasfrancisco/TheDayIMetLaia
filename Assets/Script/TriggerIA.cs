using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerIA : MonoBehaviour {

    public float distanceArea;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanceArea);
    }
}
