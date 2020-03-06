using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentController : MonoBehaviour
{
    public GameObject standByArea;
    public GameObject getTicketCounter;

    public bool hasTicket = false;
    public bool isServiced = false;

    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    
    // Start is called before the first frame update
    void Start()
    {
        standByArea = GameObject.FindGameObjectWithTag("WaitingArea");
        getTicketCounter = GameObject.FindGameObjectWithTag("TicketDesk");
        
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.SetDestination(standByArea.transform.position);
    }

    public void SetDestination(Transform transform)
    {
        standByArea.transform.position = transform.position;
        navMeshAgent.SetDestination(standByArea.transform.position);
    }
    public Transform GetDestination()
    {
        return standByArea.transform ;
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
