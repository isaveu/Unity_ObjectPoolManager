using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mugitea.ObjectPool;

public class InputMouseButtons : MonoBehaviour
{
    [SerializeField] private GameObject mObject1;
    [SerializeField] private GameObject mObject2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ObjectPoolManager.Instance.InstantiateWithObjectPool(mObject1).transform.position = new Vector3(0f, 0f, 0f);
        }
        if (Input.GetMouseButtonDown(1))
        {
            ObjectPoolManager.Instance.InstantiateWithObjectPool(mObject2).transform.position = new Vector3(0f, 0f, 0f);
        }
    }
}
