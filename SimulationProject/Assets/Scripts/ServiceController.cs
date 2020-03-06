using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceController : MonoBehaviour
{
    public GameObject arrivalQueueControllerGO;
    ArrivalQueueController arrivalQueueController;
    public bool isServicing=false;
    public float waitForCustomer=.5f; // sec

    // public float service_rate = 0.8f;// one service every .8 sec in avarage
    public float service_time_in_seconds = 0.5f;// one service every .8 sec in avarage

    Queue_Utilities queue_Utilities; // = new Queue_Utilities();

    public Transform desk;
    public Transform checkout;
    public Transform exitDoor;

    private GameObject CurrentOrderCustomer;
    // private GameObject CurrentCheckoutCustomer;

    // private float service_time_in_seconds;

    // Start is called before the first frame update
    void Start()
    {
        //queue_Utilities = new Queue_Utilities();
        arrivalQueueController = arrivalQueueControllerGO.GetComponent<ArrivalQueueController>();
        desk = GameObject.FindGameObjectWithTag("Desk").transform;
        checkout = GameObject.FindGameObjectWithTag("Checkout").transform;
        exitDoor = GameObject.FindGameObjectWithTag("Exit").transform;
        
                // service_time_in_seconds = Queue_Utilities.ExpDist(1f / service_rate); //this is in sec
                print("service_time_in_seconds:" + service_time_in_seconds);

                CurrentOrderCustomer = null;
    }

    void Update()
    {
        if (!isServicing)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                // StopCoroutine(DoService());
                isServicing = true;
                // StartCoroutine(DoService());
            }
        }
        else{
            if( CurrentOrderCustomer == null ){
                NextInLine();
            }
        }
    }

    void NextInLine()
    {
            GameObject go = arrivalQueueController.GetFirstCustomer();
            
                go.GetComponent<StudentController>().SetDestination(desk);

                CurrentOrderCustomer = go;
    }

    public void WaitForService()
    {
        // Debug.Log("Before WaitForService: "+service_time_in_seconds);
                StartCoroutine(DoService());
        // Debug.Log("After WaitForService: "+service_time_in_seconds);
    }

    IEnumerator DoService(){
                yield return new WaitForSeconds(service_time_in_seconds);
                CurrentOrderCustomer.GetComponent<StudentController>().SetDestination(checkout);

                CurrentOrderCustomer = null;
    }

    public void Checkout(GameObject customer)
    {
        // Debug.Log("Before WaitForService: "+service_time_in_seconds);
                StartCoroutine(DoCheckout(customer));
        // Debug.Log("After WaitForService: "+service_time_in_seconds);
    }

    IEnumerator DoCheckout(GameObject customer){
                yield return new WaitForSeconds(service_time_in_seconds);
                customer.GetComponent<StudentController>().SetDestination(exitDoor);

                customer = null;
    }
    

    // IEnumerator DoService()
    // {
    //     while (isServicing)
    //     {
    //         GameObject go = arrivalQueueController.GetFirstCustomer();
    //         if(go == null)
    //         {
    //             yield return new WaitForSeconds(waitForCustomer); // wait one frame
    //         }
    //         else
    //         {
    //             // float service_time_in_seconds = Queue_Utilities.ExpDist(1f / service_rate); //this is in sec
    //             print("service_time_in_seconds:" + service_time_in_seconds);
    //             go.GetComponent<StudentController>().SetDestination(desk);


    //             yield return new WaitForSeconds(service_time_in_seconds);
    //             go.GetComponent<StudentController>().SetDestination(exitDoor);

    //         }
    //     }

    // }

}
