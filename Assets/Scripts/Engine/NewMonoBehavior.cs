using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMonoBehavior : MonoBehaviour
{
    public GameObject gameObject;
    public Transform transform;

    private void CacheComponentGameObjectAndTransform()
    {
        this.gameObject = this.GetComponent<MonoBehaviour>().gameObject;
        this.transform = this.GetComponent<MonoBehaviour>().transform;
    }

    protected void Awake()
    {
        CacheComponentGameObjectAndTransform();
        NewAwake();
    }

    protected virtual void NewAwake() { }


    
}
