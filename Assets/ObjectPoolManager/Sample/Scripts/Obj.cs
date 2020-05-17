using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mugitea.ObjectPool;

public class Obj : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(enumerator());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0f, 0.1f, 0f);
    }

    private IEnumerator enumerator()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
