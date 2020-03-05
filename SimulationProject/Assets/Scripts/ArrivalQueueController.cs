using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivalQueueController : MonoBehaviour
{
    public GameObject customerPrefab;
    public GameObject[] customerSpawnPlace;
    public GameObject[] receptionistCounter;
    public bool generatingArrivals = false;
    StudentController studentController;
    Transform waitingArea;
    Transform ticketLine;
    Transform lastPlaceInQueue;

    Queue<GameObject> arrivalQueue = new Queue<GameObject>();
    //public Queue_Utilities queue_Utilities; //=new Queue_Utilities();

    //public float arrival_rate = 60; //arrival / min
    public float arrival_rate = 1f; //arrivals / sec

    // Start is called before the first frame update
    void Start()
    {
        //queue_Utilities = GetComponent<Queue_Utilities>();
        Queue_Utilities.lambda = arrival_rate;
        waitingArea = GameObject.FindGameObjectWithTag("WaitingArea").transform;
        lastPlaceInQueue = waitingArea;
        //StartCoroutine(GenerateArrivals());
    }

    IEnumerator GenerateArrivals()
    {

        while (generatingArrivals == true)
        {
            int rand = GenerateRandom();
            print(rand);
            
            //float inter_arrival_time_in_seconds = Queue_Utilities.ExpDist(1f / arrival_rate); //this is in min
            float inter_arrival_time_in_seconds = Queue_Utilities.ObservedDist(); //this is in min

            //inter_arrival_time_in_seconds *= 60;
            print("inter_arrival_time_in_seconds:" + inter_arrival_time_in_seconds);
            //StartCoroutine(GenerateArrivals());
            yield return new WaitForSeconds(inter_arrival_time_in_seconds);

            GameObject go = Instantiate(customerPrefab, customerSpawnPlace[rand].transform.position, Quaternion.identity);
            go.GetComponent<StudentController>().SetDestination(lastPlaceInQueue);
            lastPlaceInQueue = go.transform;
            
            print("Arrival Queue Length=" + arrivalQueue.Count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (!generatingArrivals)
            {
                StopCoroutine(GenerateArrivals());
                generatingArrivals = true;
                StartCoroutine(GenerateArrivals());
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (generatingArrivals)
            {
                StopCoroutine(GenerateArrivals());
                generatingArrivals = false;
            }
        }

    }


    public int ArrivalQueueCount()
    {
        return arrivalQueue.Count;
    }

    public int GenerateRandom(){
        return Random.Range(0, 2);
    }

    public GameObject GetFirstCustomer()
    {
        if (arrivalQueue.Count == 0)
        {
            return null;
        }
        else
        {

            GameObject go=arrivalQueue.Dequeue();
            GameObject goFirst = arrivalQueue.Peek();
            if(goFirst!= null)
            {
                goFirst.GetComponent<StudentController>().SetDestination(waitingArea);
            }

            return go;

        }
    }


}
