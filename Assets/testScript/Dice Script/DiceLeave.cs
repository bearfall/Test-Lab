using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class DiceLeave : MonoBehaviour
{

    public VisualEffect VFXGraph;
    public MeshRenderer diceMesh;
    public float dissolveRate = 0.0125f;
    public float refreshRate = 0.025f;
    private Material[] diceMaterials;
    // Start is called before the first frame update
    private void Awake()
    {
        VFXGraph.Stop();
    }

    void Start()
    {



       
        
       

        if (diceMesh != null)
        {
            diceMaterials = diceMesh.materials;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DissolveCo());
        }
    }


    public void StartDissolve()
    {
        
        StartCoroutine(DissolveCo());
    }

    public void RefreshDiceMaterials()
    {

        diceMaterials[0].SetFloat("_DissolveAmount", 0);

    }


    public IEnumerator DissolveCo()
    {
        
        if (VFXGraph != null)
        {
            VFXGraph.Play();
        }
        
        if (diceMaterials.Length > 0)
        {
            
            float counter = 0;


            while(diceMaterials[0].GetFloat("_DissolveAmount") <  1)
            {
                counter += dissolveRate;
                for (int i = 0; i < diceMaterials.Length; i++)
                {
                    diceMaterials[i].SetFloat("_DissolveAmount", counter);


                }
                yield return new WaitForSeconds(refreshRate);

            }
        }



    }
}
