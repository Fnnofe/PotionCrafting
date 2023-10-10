using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI xMousePosUI;
    [SerializeField] TextMeshProUGUI yMousePosUI;
    [SerializeField] TextMeshProUGUI zMousePosUI;
    [SerializeField] Transform spawnPosition;
    Camera cam;
    PotSystem pot;
    [SerializeField]PotionSystem potion;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        pot = FindAnyObjectByType<PotSystem>();

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


            //spawn object
            if (objectHit.tag == "Ingredient" && Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameObject spawned = Instantiate(objectHit.gameObject, spawnPosition.position, Quaternion.identity);

                spawned.GetComponent<Rigidbody>().isKinematic= false;

            }

            if (objectHit.tag =="Potion" && Input.GetKeyDown(KeyCode.Mouse0) && objectHit.GetComponent<PotionSystem>().isFull==false)
            {
                pot.CraftPotion(objectHit.gameObject);

            }
            if(objectHit.tag == "Player" && Input.GetKeyDown(KeyCode.Mouse0) && potion.isFull==true)
            {
                Debug.Log("GEEEEEEEEEEEEE");
                pot.GetComponent<Collider>().enabled = true;
                pot.ResetPot();
                pot.testerControler.SetTrigger("Act");
                potion.isFull = false;

                pot.ResetPotion();




            }

        }
        xMousePosUI.text = "mousePosX: " + mousePos.x.ToString();
        yMousePosUI.text = "mousePosY: " + mousePos.y.ToString();
        zMousePosUI.text = "mousePosZ: " + mousePos.z.ToString();

        Debug.DrawRay(transform.position, mousePos - transform.position, Color.red);



       

    }


}
