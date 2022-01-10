using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;
using UnityEngine.UI;

// RollerAgent
public class Amove : Agent
{
    Rigidbody rBody;

    // 初期化時に呼ばれる
    public override void Initialize()
    {
        this.rBody = GetComponent<Rigidbody>();
    }

    // エピソード開始時に呼ばれる
    public override void OnEpisodeBegin()
    {
    }

    // 観察取得時に呼ばれる
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(this.transform.localRotation);
    }

    // 行動実行時に呼ばれる
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // RollerAgentに力を加える
        Vector3 controlSignal = Vector3.zero;
        Vector3 roteSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.y = actionBuffers.ContinuousActions[1];
        rBody.AddForce(controlSignal * 200f);
        rBody.velocity = Vector3.zero; 

        roteSignal.z = actionBuffers.ContinuousActions[2];
        transform.Rotate(roteSignal * 4f);
        
    }
    //OnTriggerEnter関数
    //接触したオブジェクトが引数otherとして渡される
    void OnTriggerEnter(Collider other)
    {
            if (other.CompareTag("red_attack"))
            {
                this.transform.localPosition = new Vector3(
                    -27f, 0.2f, 0.0f);
                this.AddReward(-1f);
                EndEpisode();
            }

            if (other.CompareTag("R_target"))
            {
                this.AddReward(1f);

        }

            if (other.CompareTag("B_target"))
            {
                this.AddReward(0.1f);


        }

            if (other.CompareTag("map_end"))
            {
                this.transform.localPosition = new Vector3(
                    -27f, 0.2f, 0.0f);
            this.AddReward(-1f);
                EndEpisode();
            }
    }

    // ヒューリスティックモードの行動決定時に呼ばれる
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
}