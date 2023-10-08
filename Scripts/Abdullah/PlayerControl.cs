using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI xMousePosUI;
    [SerializeField] TextMeshProUGUI yMousePosUI;
    [SerializeField] TextMeshProUGUI zMousePosUI;

    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5;
        mousePos = cam.ScreenToWorldPoint(mousePos);





        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);


        //detect Objects 
        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            Debug.Log("I clicked on" + objectHit.name);

            Debug.DrawRay(transform.position, hit.transform.position - transform.position, Color.blue);

        }
        xMousePosUI.text = "mousePosX: " + mousePos.x.ToString();
        yMousePosUI.text = "mousePosY: " + mousePos.y.ToString();
        zMousePosUI.text = "mousePosZ: " + mousePos.z.ToString();

        Debug.DrawRay(transform.position, mousePos - transform.position, Color.red);



    }


}
