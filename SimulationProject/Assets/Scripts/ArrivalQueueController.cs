using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrivalQueueController : MonoBehaviour
{
    public Text unitOfTime;
    public Text arrival;
    public Text service;

    public GameObject customerPrefab;
    public Transform customerSpawnPlace;
    public bool generatingArrivals = false;
    // StudentController studentController;
    public Transform waitingRoom;
    // public Transform orderPoint;
    // public Transform checkoutPoint;
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
        Queue_Utilities.Init();

        if(waitingRoom == null){
            waitingRoom = GameObject.FindGameObjectWithTag("WaitingArea").transform;
        }
        // if(orderPoint == null){
        //     orderPoint = GameObject.FindGameObjectWithTag("Order").transform;
        // }
        // if(checkoutPoint == null){
        //     checkoutPoint = GameObject.FindGameObjectWithTag("Checkout").transform;
        // }
        lastPlaceInQueue = waitingRoom;
        //StartCoroutine(GenerateArrivals());
    }

    public void ApplyInputData()
    {
        Queue_Utilities.setData(float.Parse(unitOfTime.text), float.Parse(arrival.text), float.Parse(service.text));
    }

    IEnumerator GenerateArrivals()
    {

        while (generatingArrivals == true)
        {

            //float inter_arrival_time_in_seconds = Queue_Utilities.ExpDist(1f / arrival_rate); //this is in min
            float inter_arrival_time_in_seconds = Queue_Utilities.ObservedDist(); //this is in min

            //inter_arrival_time_in_seconds *= 60;
            print("inter_arrival_time_in_seconds:" + inter_arrival_time_in_seconds);
            //StartCoroutine(GenerateArrivals());
            yield return new WaitForSeconds(inter_arrival_time_in_seconds);

// print("Last Place In Queue: "+lastPlaceInQueue);
            if(lastPlaceInQueue == null){
                lastPlaceInQueue = waitingRoom;
            }
            
            GameObject go = Instantiate(customerPrefab, customerSpawnPlace.position, Quaternion.identity);
            go.GetComponent<StudentController>().SetDestination(lastPlaceInQueue);
            lastPlaceInQueue = go.transform;
            
            
            arrivalQueue.Enqueue(go);
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

    public GameObject GetFirstCustomer()
    {
        if (arrivalQueue.Count == 0)
        {
            return null;
        }
        else
        {
            // GameObject go=arrivalQueue.Dequeue();
            // GameObject goFirst = arrivalQueue.Peek();
            // if(goFirst!= null)
            // {
            //     goFirst.GetComponent<StudentController>().SetDestination(waitingRoom);
            // }

            GameObject go=arrivalQueue.Dequeue();
            if(go!= null)
            {
                go.GetComponent<StudentController>().SetDestination(waitingRoom);
            }

            return go;

        }
    }


}
