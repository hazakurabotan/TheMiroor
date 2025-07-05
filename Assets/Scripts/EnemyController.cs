using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;

    public float detectionRange = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
       agent = GetComponent<NavMeshAgent>();

        //開始0秒でWanderを発動、後は5秒おき
        InvokeRepeating("Wander",0,5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Wander()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        if (!agent.isStopped)
        {
            //insideUnitSphere は周辺 半径 1　の中からランダムで座標を選ぶ
            Vector3 randomPos = transform.position + Random.insideUnitSphere * detectionRange;
            //上下のランダム値は無視して、その時敵のいた高さに据置
            randomPos.y = transform.position.y;

            NavMeshHit hit;
            if(NavMesh.SamplePosition(randomPos,out hit,detectionRange,NavMesh.AllAreas))
            { 
                agent.SetDestination(hit.position);
            }
        }
    }
}
