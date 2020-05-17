using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mugitea.ObjectPool
{

    public class ObjectPoolManager : SingletonMonoBehaviour<ObjectPoolManager>
    {
        private List<GameObject> mObjectPool = new List<GameObject>();

        [System.Serializable]
        private struct InstantiateOnEnable
        {
            public GameObject gameObject;
            public int instantiateNum;
        }

        [SerializeField] private InstantiateOnEnable[] mInstantiateOnEnable;

        private void OnEnable()
        {
            foreach (var instantiateOnEnable in mInstantiateOnEnable)
            {
                for (int count = 0; count < instantiateOnEnable.instantiateNum; count++)
                {
                    AddObjectPoolAndInstantiate(instantiateOnEnable.gameObject).SetActive(false);
                }
            }
        }

        /// <summary>
        /// オブジェクトプールに追加・生成.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="transform"></param>
        /// <param name="isWorldPos"></param>
        /// <returns></returns>
        public GameObject InstantiateWithObjectPool(GameObject gameObject, Transform parent = null, bool isWorldPosisitonStays = true)
        {
            //オブジェクトプールにある場合
            foreach (var obj in mObjectPool)
            {
                if (obj.name != gameObject.name) continue;
                if (obj.activeSelf) continue;

                //オブジェクトプールにあり、非オブジェクトなオブジェクトの場合
                obj.SetActive(true);
                obj.transform.parent = parent;
                return obj;
            }

            //生成しないといけない場合
            return AddObjectPoolAndInstantiate(gameObject, parent, isWorldPosisitonStays);
        }

        /// <summary>
        /// オブジェクトプールに追加・生成.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="transform"></param>
        /// <param name="isWorldPos"></param>
        /// <returns></returns>
        public GameObject InstantiateWithObjectPool(GameObject gameObject, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), Transform parent = null)
        {
            //オブジェクトプールにある場合
            foreach (var obj in mObjectPool)
            {
                if (obj.name != gameObject.name) continue;
                if (obj.activeSelf) continue;

                //オブジェクトプールにあり、非オブジェクトなオブジェクトの場合
                obj.SetActive(true);
                obj.transform.parent = parent;
                return obj;
            }

            //生成しないといけない場合
            return AddObjectPoolAndInstantiate(gameObject, position, rotation, parent);
        }

        /// <summary>
        /// オブジェクトプールに追加・生成.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="transform"></param>
        /// <param name="isWorldPos"></param>
        /// <returns></returns>
        public GameObject InstantiateWithObjectPool(GameObject gameObject)
        {
            //オブジェクトプールにある場合
            foreach (var obj in mObjectPool)
            {
                if (obj.name != gameObject.name) continue;
                if (obj.activeSelf) continue;

                //オブジェクトプールにあり、非オブジェクトなオブジェクトの場合
                obj.SetActive(true);
                return obj;
            }

            //生成しないといけない場合
            return AddObjectPoolAndInstantiate(gameObject);
        }

        /// <summary>
        /// オブジェクトプールから削除し、Destroy.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="isForce"></param>
        /// <returns></returns>
        public GameObject RemoveAtObjectPool(GameObject gameObject)
        {

            foreach (var obj in mObjectPool)
            {
                if (obj.name != gameObject.name) continue;

                //オブジェクトプールにあり、非オブジェクトなオブジェクトの場合
                //(削除できるのは非アクティブの場合のみ)
                if (!obj.activeSelf)
                {
                    Destroy(obj);
                    mObjectPool.Remove(obj);
                    return obj;
                }

            }

            return null;
        }

        private GameObject AddObjectPoolAndInstantiate(GameObject gameObject, Transform parent = null, bool isWorldPosisitonStays = true)
        {
            var obj = Instantiate(gameObject, parent, isWorldPosisitonStays);
            obj.name = gameObject.name;
            mObjectPool.Add(obj);
            return obj;
        }

        private GameObject AddObjectPoolAndInstantiate(GameObject gameObject, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), Transform parent = null)
        {
            var obj = Instantiate(gameObject, position, rotation, parent);
            obj.name = gameObject.name;
            mObjectPool.Add(obj);
            return obj;
        }

        private GameObject AddObjectPoolAndInstantiate(GameObject gameObject)
        {
            var obj = Instantiate(gameObject);
            obj.name = gameObject.name;
            mObjectPool.Add(obj);
            return obj;
        }
    }
}