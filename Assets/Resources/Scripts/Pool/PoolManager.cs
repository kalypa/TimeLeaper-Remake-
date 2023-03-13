using System.Collections.Generic;
using UnityEngine;

namespace SimplePool
{
    public class PoolManager : SingleMonobehaviour<PoolManager>
    {
        // ObjectPool�� Dictionary�� �����ϴ� ����
        private Dictionary<string, ObjectPool> poolDictionary = new Dictionary<string, ObjectPool>();

        // �ش� Ÿ���� ������Ʈ�� Ǯ���ϴ� �Լ�
        public void CreatePool<T>(T prefab, int count) where T : Component
        {
            // ������Ʈ Ǯ���� �̸��� ����
            string poolName = prefab.name + " Pool";

            // ������Ʈ Ǯ���� �������� ���� �� ���� ����
            if (!poolDictionary.ContainsKey(poolName))
            {
                // ������Ʈ Ǯ���� �����Ͽ� Dictionary�� �߰�
                ObjectPool newPool = new ObjectPool();
                newPool.Init(prefab.gameObject, count, transform);
                poolDictionary.Add(poolName, newPool);
            }
        }

        // �ش� Ÿ���� ������Ʈ�� ��ȯ�ϴ� �Լ�
        public T GetPoolObject<T>(T prefab, Vector3 position, Quaternion rotation) where T : Component
        {
            // ������Ʈ Ǯ���� �̸��� ����
            string poolName = prefab.name + " Pool";

            // �ش� �̸��� ������Ʈ Ǯ���� �������� ������ ���� �޽��� ��� �� �Լ� ����
            if (!poolDictionary.ContainsKey(poolName))
            {
                Debug.LogError($"Pool with name {poolName} doesn't exist.");
                return null;
            }

            // �ش� ������Ʈ Ǯ������ ������Ʈ�� ������
            GameObject poolObject = poolDictionary[poolName].GetObject(position, rotation);

            // ������Ʈ Ǯ������ ������ ������Ʈ�� PoolObject ������Ʈ�� �߰���
            T component = poolObject.GetComponent<T>();
            PoolObject poolObjectComponent = poolObject.GetComponent<PoolObject>();

            if (component != null && poolObjectComponent == null)
            {
                poolObjectComponent = poolObject.AddComponent<PoolObject>();
            }

            // ������Ʈ Ǯ������ ������ ������Ʈ�� PoolObject ������Ʈ�� ObjectPool�� ������ ������
            if (poolObjectComponent != null)
            {
                poolObjectComponent.SetPool(poolDictionary[poolName]);
            }

            return component;
        }
    }
}
