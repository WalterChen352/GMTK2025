using UnityEngine;
using System.Collections.Generic;
using System;

public class TreeState : MonoBehaviour, IInteractable
{
    public float Startinghitpoints = 4;
    public float Hitpoints;
    public WoodCounter woodCounter;
    public GameObject beaver;
    [SerializeField] List<Sprite> Sprites = new List<Sprite>();
    public SpriteRenderer Sprite;
    public bool IsInteractable { get; set; }
    public Outline outline;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int Health;
    public Rigidbody TrunkRB;
    public float Push = 100f;
    public GameObject trunk;
    public float minDamage= 4/3 +(float)0.01; //range is 3-6 bites if dealing 4 damage over 3 hits 4/3
    public float maxDamage= 4/6+ (float)0.01;

    public float baseEnergyCost= 10;
    public int timesEaten = 0;
    public float costFactor=(float) 1.2;
    private float damage;
    

    void Start()
    {
        damage = UnityEngine.Random.Range(minDamage, maxDamage);
        Hitpoints = Startinghitpoints;
        beaver = GameObject.Find("Beaver");
        woodCounter = beaver.GetComponent<WoodCounter>();
        outline = GetComponent<Outline>();
        Hitpoints = 4;
        IsInteractable = true;
        outline.enabled = false;
        //
        Sprite = GetComponentInChildren<SpriteRenderer>();
        if(Sprite == null)
        {
            Debug.LogError("child sprite not found");
        }
        //if (trunk != null)
        //{
        //    TrunkRB = trunk.GetComponent<Rigidbody>();
        //    if (TrunkRB != null)
        //    {
        //        TrunkRB.isKinematic = true;
        //        TrunkRB.useGravity = false;
        //    }
        //}

        //// Ensure full tree is active and parts are disabled initially
        //if (fullTree != null) fullTree.SetActive(true);
        //if (trunk != null) trunk.SetActive(false);
        //if (stump != null) stump.SetActive(false);

    }

    public void Initialize(int health)
    {
        Health = health;
    }


  
    public void Interact()
    {
        if (IsInteractable == true)
        {
            float energyCost = (float)(baseEnergyCost*  Math.Pow(costFactor, timesEaten));
            timesEaten++;
            var EnergySystem = beaver.GetComponent<EnergySystem>();
            if(EnergySystem == null)
            {
                Debug.LogError("beaver's energy systme not found");
            }
            else
            {
                EnergySystem.AddEnergy(-energyCost);
            }
            ChopTree();
            Debug.Log("Tree interacted with");
        }
        
    }
    public void Highlight(bool on)
    {
        if (on  && IsInteractable)
        {
            outline.enabled = true;
        } else {
            outline.enabled = false;
        }
    }
    public void ChopTree()
    {
        if (Hitpoints > 0)
        {
            Debug.Log("Chopping...");
            Hitpoints -= 1;
        }

        if (Hitpoints <=2 && Hitpoints> 1)
        {
            Debug.Log("Partly Chopped");
            Sprite.sprite = Sprites[0];
        }

        if (Hitpoints <=1 && Hitpoints > 0)
        {
            Debug.Log("Mostly Chopped");
            Sprite.sprite = Sprites[1];
        }

        if (Hitpoints <= 0)
        {
            Debug.Log("Stumped");
            Sprite.sprite = Sprites[2];
            TreeFall();
            CollectWood();

        }

    }
    public void TreeFall()
    {
        bool fallRight = beaver.transform.position.x >= this.transform.position.x ;
        GameObject trunkCopy;
        Vector3 pos = transform.position;
        if (fallRight)
        {
            pos.z += 1;
            pos.x += (float).25;
            pos.y += (float)0.25;
            trunkCopy = Instantiate(trunk, pos, Quaternion.identity);
            
            Rigidbody rb = trunkCopy.GetComponent<Rigidbody>();
            rb.AddTorque(new Vector3(0, 0, 1000), ForceMode.Impulse);
        }
        else
        {
            pos.z += (float)0.75;
            pos.x += (float).25;
            //pos.y += (float)0.25;
            trunkCopy = Instantiate(trunk, pos, Quaternion.identity);
            Rigidbody rb = trunkCopy.GetComponent<Rigidbody>();
            rb.AddTorque(new Vector3(0, 0, -1000), ForceMode.Impulse);
        }

        if (trunk.GetComponentInChildren<SpriteRenderer>().flipX)
        {
            trunkCopy.GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        else
        {
            trunkCopy.GetComponentInChildren<SpriteRenderer>().flipX = true;
        }

        trunkCopy.transform.localScale=this.transform.localScale;

        IsInteractable = false;
        outline.enabled = false;
        Debug.Log("Tree Fallen Down!");
    }
    public void CollectWood()
    {
        woodCounter.AddWood(4);
    }
}
