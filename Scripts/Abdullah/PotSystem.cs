using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotSystem : MonoBehaviour
{
    int newSubColor= -1;
    int currentSubColor;

    [SerializeField] int potCapacity;
    [SerializeField] Material material;
    [SerializeField] ParticleSystem smoke;
    [SerializeField] GameObject potion;
    int isBuff, isDebuff, isHeal, isDamage, isStatesUp, isStatesDown;
    int[] materialColor= new int[6];
    Color colorLerp= new Color(1f,1f,1f,0.25f);
    int resetPot;
    [SerializeField] ParticleSystem explodeParticle;
    public Animator testerControler;
    PotionSystem potionSystem;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        material.color = colorLerp;
        resetPot = potCapacity;
        smoke.Stop();
        potionSystem = potion.GetComponent<PotionSystem>();

    }

    void Update()
    {
        if (potCapacity < 0)
        {
            ResetPot();


        }


    }

    private void FixedUpdate()
    {
        material.color = colorLerp;

    }

    public void OnTriggerStay(Collider trigger)
    {
        smoke.Play();
        if ( trigger.gameObject.tag== "Ingredient")
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
            testerControler.SetInteger("PotionState", 0);

        }
        //debuff
        else if (seletcolor == 1)
        {
            colorLerp = Color.Lerp(material.color, Color.blue, 1);
            Debug.Log("DEBUFF");
            testerControler.SetInteger("PotionState", 1);

        }
        //heal
        else if (seletcolor == 2)
        {
            colorLerp = Color.Lerp(material.color, Color.red, 1);
            Debug.Log("HEALING");
            testerControler.SetInteger("PotionState", 2);

        }
        //damage
        else if (seletcolor == 3)
        {
            colorLerp = Color.Lerp(material.color, Color.black, 1);
            Debug.Log("POISION");
            testerControler.SetInteger("PotionState", 3);

        }
        //state up
        else if (seletcolor == 4)
        {
            colorLerp = Color.Lerp(material.color, Color.white, 1);
            Debug.Log("STATES UP");
            testerControler.SetInteger("PotionState", 4);

        }
        //state down
        else if (seletcolor == 5)
        {
            colorLerp = Color.Lerp(material.color, Color.gray, 1);
            Debug.Log("STATES DOWN");
            testerControler.SetInteger("PotionState", 5);

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

    public void ResetPotion()
    {
        potionSystem.meshRenderer.material.color = colorLerp;
        potionSystem.potionSmoke.startColor = smoke.startColor;
        potionSystem.potionSmoke.Stop();
        potionSystem.isFull = false;



    }
    void PotExplode()
    {
        smoke.Stop();
        explodeParticle.Play();

        colorLerp = new Color(1f, 1f, 1f, 0);

        isBuff = 0;
        isDebuff = 0;
        isHeal = 0;
        isDamage = 0;
        isStatesUp =0;
        isStatesDown =0;

        testerControler.SetInteger("PotionState",-1);
        testerControler.SetTrigger("isExploded");
        testerControler.gameObject.GetComponent<Collider>().enabled = false;
        ResetPot();

    }
    public void ResetPot()
    {
        //load scene
        potCapacity = resetPot;
        colorLerp = new Color(1f, 1f, 1f, 0.25f);
        isBuff = 0;
        isDebuff = 0;
        isHeal = 0;
        isDamage = 0;
        isStatesUp = 0;
        isStatesDown = 0;
        gameObject.GetComponent<Collider>().enabled = true;


    }

    public void CraftPotion(GameObject potion)
    {
        testerControler.gameObject.GetComponent<Collider>().enabled = true;

        potionSystem.meshRenderer.material.color = colorLerp;
        potionSystem.potionSmoke.startColor = smoke.startColor;
        potionSystem.isFull = true;



        Debug.Log(potionSystem.meshRenderer.material.color = colorLerp);
        potionSystem.potionSmoke.Play();




        gameObject.GetComponent<Collider>().enabled = false;

      colorLerp = new Color(1f, 1f, 1f, 0.25f);

        smoke.Stop();

    

    }






    // Update is called once per frame
}
