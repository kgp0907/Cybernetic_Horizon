using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawn : MonoBehaviour
{
       
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Renderer _renderer1;
    [SerializeField] private Renderer _renderer2;
    [SerializeField] private Renderer _renderer3;
    [SerializeField] private Material mtrlOrg;
    [SerializeField] private Material mtrlDissolve;
    [SerializeField] private float fadeTime = 2f;
    
    
    
    //void Start()
    //{
    //    if (Random.Range(0, 100) < 50)
    //    {
    //        _renderer.material = mtrlDissolve;
    //        Dofade(0,1,fadeTime);
    //    }
    //    else
    //    {
    //        _renderer.material = mtrlPhase;
    //        Dofade( 0 , 2, fadeTime);
    //    }
    //}

    void Dofade(float start, float dest, float time)
    {
        iTween.ValueTo( gameObject, iTween.Hash(
            "from", start, "to", dest, "time", time, "onupdatetarget", gameObject,
            "onupdate", "TweenOnUpdate", "oncomplte", "TweenOnComplte",
            "easetype", iTween.EaseType.easeInOutCubic ));
        
    }

    private void Update()
    {
        //if (Input.GetKey(KeyCode.Y))
        //    Dofase2();
        //if (Input.GetKey(KeyCode.A))
        //    Dofade(0,1.2f,fadeTime);
    }

    void Dofase2()
    {
        _renderer.material = mtrlDissolve;
        _renderer1.material = mtrlDissolve;
        _renderer2.material = mtrlDissolve;
        _renderer3.material = mtrlDissolve;
        Dofade(1.2f, 0, fadeTime);
    }

    void TweenOnUpdate(float value)
    {
        _renderer.material.SetFloat( "_SpiltValue", value);
        _renderer1.material.SetFloat("_SpiltValue", value);
        _renderer2.material.SetFloat("_SpiltValue", value);
        _renderer3.material.SetFloat("_SpiltValue", value);
    }

    void TweenOnComplte()
    {
        _renderer.material = mtrlOrg;
        _renderer1.material = mtrlOrg;
        _renderer2.material = mtrlOrg;
        _renderer3.material = mtrlOrg;
    }
    
}
