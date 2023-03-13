using UnityEngine;

namespace SimplePool
{
    public class PoolObject : MonoBehaviour
    {
        // �ش� ������Ʈ�� ���� ������Ʈ Ǯ���� ������ �����ϴ� ����
        private ObjectPool pool;

        // ������Ʈ Ǯ���� ������ �����ϴ� �Լ�
        public void SetPool(ObjectPool pool)
        {
            this.pool = pool;
        }

        // ������Ʈ�� �ٽ� ������Ʈ Ǯ���� ��ȯ�ϴ� �Լ�
        public void ReturnToPool()
        {
            if (pool != null)
            {
                pool.ReturnObject(gameObject);
            }
            else
            {
                Debug.LogError("Object doesn't belong to a pool.");
                Destroy(gameObject);
            }
        }
    }
}
