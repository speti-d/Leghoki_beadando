using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System;


public class PackController : Agent
{

    Rigidbody agent_rigidbody;
    public Transform TargetTransform;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0, 0.5f, -5);
        this.agent_rigidbody = GetComponent<Rigidbody>();
        this.RequestDecision();
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //Debug.Log(actions.ContinuousActions[0] * 90);
        //Debug.Log((Math.Abs(actions.ContinuousActions[1]) * 20 + 5));
        transform.Rotate(Vector3.up, actions.ContinuousActions[0] * 90);
        this.agent_rigidbody.AddForce(transform.forward * (Math.Abs(actions.ContinuousActions[1]) * 10 + 10));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Goal")
        {
            AddReward(10);
            EndEpisode();
        }
    }

    private void FixedUpdate()
    {
        float distance_scaled = Vector3.Distance(TargetTransform.localPosition, transform.localPosition) / 10;
        //Debug.Log(distance_scaled);
        AddReward(-distance_scaled);
    }
}

