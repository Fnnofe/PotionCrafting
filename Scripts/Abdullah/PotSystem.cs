using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PotSystem : MonoBehaviour
{
    int newSubColor= -1;
    int currentSubColor;

    [SerializeField] int potCapacity;
    [SerializeField] Material material;
    [SerializeField] ParticleSystem smoke;
    GameObject potion;
    int isBuff, isDebuff, isHeal, isDamage, isStatesUp, isStatesDown;
    int[] materialColor= new int[6];
    Color colorLerp= new Color(1f,1f,1f,0.25f);
    int resetPot;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        material.color = colorLerp;
        resetPot = potCapacity;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        material.color = colorLerp;

    }

    public void OnTriggerStay(Collider trigger)
    {
        
        if (Input.GetKeyUp(KeyCode.Mouse0) && trigger.gameObject.tag=="Player")
        {
            Debug.Log(trigger.gameObject.name);
            trigger.gameObject.SetActive(false);

            ///Take the ingrident stats
            ///add it to the pot cacluation PotReact()
            ///increase the limit on the pot capacity.
            ///exceed limit PotExplode()
            ///ResetPot()

            ///At anytime CraftPotion()
            PotReact(trigger.gameObject);
        }

    }


    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.tag == "Potion" && trigger.GetComponent<PotionSystem>().isFull==false)
        {
            potion = trigger.gameObject;
            CraftPotion(potion);
        }



    }

    void PotReact(GameObject trigger)
    {
        int currentColor = 0;

        IngredientTemplet ingredientTemplet = trigger.GetComponent<IngredientTemplet>();
        isBuff += ingredientTemplet.buff;
        isDebuff += ingredientTemplet.debuff;
        isHeal += ingredientTemplet.heal;
        isDamage += ingredientTemplet.damage;
        isStatesUp += ingredientTemplet.statesUp;
        isStatesDown += ingredientTemplet.stateDown;

        materialColor[0] = isBuff;
        materialColor[1] = isDebuff;
        materialColor[2] = isHeal;
        materialColor[3] = isDamage;
        materialColor[4] = isStatesUp;
        materialColor[5] = isStatesDown;

        //
        for (int i=0; materialColor.Length>i; i++)
        {
            if (currentColor < materialColor[i])
            {
                currentSubColor = newSubColor;
                newSubColor = i;
             
              
                
                currentColor = materialColor[i];

                PotColor(i);
                SmokeColor(currentSubColor);
            }

        }

        potCapacity--;
        Debug.Log("potCapacity" + potCapacity);
        if (potCapacity < 0) PotExplode();

    }

    void PotColor(int seletcolor)
    {


        //buff
        if (seletcolor == 0)
        {
            colorLerp = Color.Lerp(material.color, Color.yellow, 1);
            Debug.Log("BUFF");

        }
        //debuff
        else if (seletcolor == 1)
        {
            colorLerp = Color.Lerp(material.color, Color.blue, 1);
            Debug.Log("DEBUFF");

        }
        //heal
        else if (seletcolor == 2)
        {
            colorLerp = Color.Lerp(material.color, Color.red, 1);
            Debug.Log("HEALING");

        }
        //damage
        else if (seletcolor == 3)
        {
            colorLerp = Color.Lerp(material.color, Color.black, 1);
            Debug.Log("POISION");

        }
        //state up
        else if (seletcolor == 4)
        {
            colorLerp = Color.Lerp(material.color, Color.white, 1);
            Debug.Log("STATES UP");

        }
        //state down
        else if (seletcolor == 5)
        {
            colorLerp = Color.Lerp(material.color, Color.gray, 1);
            Debug.Log("STATES DOWN");

        }





    }
    void SmokeColor(int seletcolor)
    {

        //    lerpedColor = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 1));
        //    renderer.material.color = lerpedColor;

        //buff
        if (seletcolor == 0)
        {
            smoke.startColor = Color.yellow;
            Debug.Log("Smoke is BUFF");
        }
        //debuff
        else if (seletcolor == 1)
        {
            smoke.startColor = Color.blue;
            Debug.Log("Smoke is DEBUFF");

        }
        //heal
        else if (seletcolor == 2)
        {
            smoke.startColor = Color.red;
            Debug.Log("Smoke is HEALING");

        }
        //damage
        else if (seletcolor == 3)
        {
            smoke.startColor = Color.black;
            Debug.Log("Smoke is POISION");

        }
        //state up
        else if (seletcolor == 4)
        {
            smoke.startColor = Color.white;
            Debug.Log("Smoke is STATES UP");

        }
        //state down
        else if (seletcolor == 5)
        {
            smoke.startColor = Color.gray;
            Debug.Log("Smoke is STATES DOWN");

        }


    }

    void PotExplode()
    {

    }
    void ResetPot()
    {
        //load scene
        potCapacity = resetPot;


    }
    void CraftPotion(GameObject potion)
    {
        PotionSystem potionSystem = potion.GetComponent<PotionSystem>();
        potionSystem.meshRenderer.material.color = colorLerp;
        potionSystem.potionSmoke.startColor = smoke.startColor;
        potionSystem.isFull = true;

        Debug.Log(potionSystem.meshRenderer.material.color = colorLerp);


        potionSystem.potionSmoke.Play();



      colorLerp = new Color(1f, 1f, 1f, 0.25f);
        smoke.Stop();



    }






    // Update is called once per frame
}
