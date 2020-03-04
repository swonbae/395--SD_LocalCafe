using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentController : MonoBehaviour
{
    public Transform standByArea;
    public Transform getTicketCounter;
    public Transform enrollmentCounter;

    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    
    // Start is called before the first frame update
    void Start()
    {
        standByArea = GameObject.FindGameObjectWithTag("WaitingArea").transform;
        
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.SetDestination(standByArea.position);

    }

    public void SetDestination(Transform transform)
    {
        standByArea = transform;
        navMeshAgent.SetDestination(standByArea.position);
    }
    public Transform GetDestination()
    {
        return standByArea ;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Exits")
        {
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
               
    }
}
