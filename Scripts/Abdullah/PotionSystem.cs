using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSystem : MonoBehaviour
{
    public bool divine,neutral,deadly=false;
    public bool isThrowable = false;
    public bool isFull = false;

    public MeshRenderer meshRenderer;
    public ParticleSystem potionSmoke;
    public Transform currentPostion;

    private void Start()
    {
         currentPostion = gameObject.transform;


        potionSmoke.Stop();


    }

    // Start is called before the first frame update
}
