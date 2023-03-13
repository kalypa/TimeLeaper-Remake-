using System.Collections.Generic;
using UnityEngine;

namespace SimplePool
{
    public class ObjectPool : MonoBehaviour
    {
        // ������Ʈ Ǯ���� ���� ������
        public GameObject prefab;
        // ������Ʈ Ǯ���� �ʱ� ũ��
        public int poolSize = 10;

        // ������Ʈ Ǯ������ ��� ������ ������Ʈ�� �����ϴ� ����Ʈ
        private List<GameObject> pool = new List<GameObject>();

        // ������Ʈ Ǯ���� �ʱ�ȭ�ϴ� �Լ�
        public void Init(GameObject Obj, int cnt, Transform t)
        {
            // �ʱ� ũ�⸸ŭ ������Ʈ�� �����ϰ� Ǯ ����Ʈ�� �߰�
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                obj.transform.SetParent(transform);
                pool.Add(obj);
            }
        }

        // ������Ʈ Ǯ������ ��� ������ ������Ʈ�� ��ȯ�ϴ� �Լ�
        public GameObject GetObject(Vector3 position, Quaternion rotation)
        {
            // Ǯ ����Ʈ���� ��� ������ ������Ʈ�� ã�Ƽ� ��ȯ
            foreach (GameObject obj in pool)
            {
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }
            // ��� ������ ������Ʈ�� ������ ���ο� ������Ʈ�� �����ؼ� ��ȯ
            GameObject newObj = Instantiate(prefab);
            newObj.transform.SetParent(transform);
            pool.Add(newObj);
            return newObj;
        }

        // ������Ʈ�� �ٽ� ������Ʈ Ǯ���� ��ȯ�ϴ� �Լ�
        public void ReturnObject(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}
