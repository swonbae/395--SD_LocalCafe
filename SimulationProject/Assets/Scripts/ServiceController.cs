using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceController : MonoBehaviour
{
    public GameObject arrivalQueueControllerGO;
    ArrivalQueueController arrivalQueueController;
    public bool isServicing=false;
    public float waitForCustomer=.5f; // sec

    public float service_rate = 0.8f;// one service every .8 sec in avarage

    public float getTicket_rate = 1.0f;

    Queue_Utilities queue_Utilities; // = new Queue_Utilities();

    public GameObject[] exits;
    public GameObject[] counters;


    // Start is called before the first frame update
    void Start()
    {
        //queue_Utilities = new Queue_Utilities();
        arrivalQueueController = arrivalQueueControllerGO.GetComponent<ArrivalQueueController>();
        exits = GameObject.FindGameObjectsWithTag("Exits");
        counters = GameObject.FindGameObjectsWithTag("Counters");
    }

    void Update()
    {
        if (!isServicing)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                StopCoroutine(DoService());
                isServicing = true;
                StartCoroutine(DoService());
            }
        }
    }


    IEnumerator DoService()
    {
        while (isServicing)
        {
            int randExit = GenerateRandomExit();
            int randCounter = GenerateRandomCounter();

            GameObject go = arrivalQueueController.GetFirstCustomer();
            print(go);
            if(go == null)
            {
                yield return new WaitForSeconds(waitForCustomer); // wait one frame

            }
            else
            {
                bool ticket = go.GetComponent<StudentController>().hasTicket;
                bool serviced = go.GetComponent<StudentController>().isServiced;

                float get_ticket_time_in_seconds = Queue_Utilities.ExpDist(1f / getTicket_rate);
                go.GetComponent<StudentController>().SetDestination(go.GetComponent<StudentController>().getTicketCounter.transform);
                yield return new WaitForSeconds(get_ticket_time_in_seconds);
                ticket = true;

                float service_time_in_seconds = Queue_Utilities.ExpDist(1f / service_rate); //this is in sec
                print("service_time_in_seconds:" + service_time_in_seconds);
                
                if(ticket && !serviced){
                    go.GetComponent<StudentController>().SetDestination(counters[randCounter].transform);
                }


                yield return new WaitForSeconds(service_time_in_seconds);
                go.GetComponent<StudentController>().isServiced = true;
                go.GetComponent<StudentController>().SetDestination(exits[randExit].transform);

            }
        }

    }

    public int GenerateRandomExit(){
        return Random.Range(0, 2);
    }

    public int GenerateRandomCounter(){
        return Random.Range(0, 7);
    }

}
