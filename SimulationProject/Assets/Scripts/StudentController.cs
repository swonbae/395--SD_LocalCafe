using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentController : MonoBehaviour
{
    public Transform standByArea;
    // public Transform getTicketCounter;
    // public Transform enrollmentCounter;

    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    
    public ServiceController serviceController;
    
    // Start is called before the first frame update
    void Start()
    {
        standByArea = GameObject.FindGameObjectWithTag("WaitingArea").transform;
        
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.SetDestination(standByArea.position);

        serviceController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ServiceController>();
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


    void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.gameObject.tag);

        if (other.gameObject.tag == "Desk")
        {
            serviceController.WaitForService();
        }
        else if (other.gameObject.tag == "Checkout")
        {
            serviceController.Checkout(this.gameObject);
        }
        else if (other.gameObject.tag == "Exit")
        {
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
               
    }
}
