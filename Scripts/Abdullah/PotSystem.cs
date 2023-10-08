using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotSystem : MonoBehaviour
{

    [SerializeField] int potCapacity;
    Material material;
    [SerializeField] PotionSystem potionSystem;
    int isBuff, isDebuff, isHeal, isDamage, isStatesUp, isStatesDown;
    int[] materialColor= new int[6];
    Color colorLerp;


    // Start is called before the first frame update
    void Start()
    {
        colorLerp = material.color;

    }

    void Update()
    {
        material.color = colorLerp;

    }

     void OnTriggerStay(Collider trigger)
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
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
                currentColor = materialColor[i];

                PotColor(i);

            }

        }

        potCapacity--;
        if (potCapacity < 0) PotExplode();
    }

    void PotColor(int seletcolor)
    {

       //    lerpedColor = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 1));
       //    renderer.material.color = lerpedColor;

        //buff
        if (seletcolor == 0)
        {
            colorLerp = Color.Lerp(material.color, Color.yellow, 1);

        }
        //debuff
        else if (seletcolor == 1)
        {
            colorLerp = Color.Lerp(material.color, Color.yellow, 1);

            material.color = Color.blue;
        }
        //heal
        else if (seletcolor == 2)
        {
            colorLerp = Color.Lerp(material.color, Color.red, 1);

        }
        //damage
        else if (seletcolor == 3)
        {
            colorLerp = Color.Lerp(material.color, Color.black, 1);

        }
        //state up
        else if (seletcolor == 4)
        {
            colorLerp = Color.Lerp(material.color, Color.white, 1);

        }
        //state down
        else if (seletcolor == 5)
        {
            colorLerp = Color.Lerp(material.color, Color.gray, 1);

        }





    }

    void PotExplode()
    {

    }
    void ResetPot()
    {

    }
    void CraftPotion()
    {

    }






    // Update is called once per frame
}
