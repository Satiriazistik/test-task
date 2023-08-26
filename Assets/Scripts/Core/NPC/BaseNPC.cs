using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseNPC : MonoBehaviour
{

    public NavMeshAgent NavAgent;

    public Transform CurrentTarget { get; set; }

    protected virtual void Update()
    {
        if (CurrentTarget != null)
            NavAgent.SetDestination(CurrentTarget.position);
    }

    public virtual void SetTarget(Transform target) => CurrentTarget = target;
}
