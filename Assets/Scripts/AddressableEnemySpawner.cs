using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableEnemySpawner : MonoBehaviour
{
    [Header("Addressables")]
    [SerializeField] private string enemyAddress = "Enemy";

    [Header("Target")]
    [SerializeField] private Transform target;

    private GameObject spawnedEnemy;
    private AsyncOperationHandle<GameObject> spawnHandle;

    public void LoadAddressable()
    {
        if (spawnedEnemy != null)
        {
            Debug.Log("Enemy zaten yüklü.");
            return;
        }

        spawnHandle = Addressables.InstantiateAsync(enemyAddress, transform.position, transform.rotation);
        spawnHandle.Completed += OnEnemyLoaded;
    }

    private void OnEnemyLoaded(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            spawnedEnemy = handle.Result;

            EnemyMove enemyMove = spawnedEnemy.GetComponent<EnemyMove>();

            if (enemyMove != null)
            {
                enemyMove.SetTarget(target);
            }

            Debug.Log("Enemy Addressables ile yüklendi.");
        }
        else
        {
            Debug.LogError("Enemy yüklenemedi. Address adını kontrol et.");
        }
    }

    public void UnloadAddressable()
    {
        if (spawnedEnemy == null)
        {
            Debug.Log("Unload edilecek enemy yok.");
            return;
        }

        Addressables.ReleaseInstance(spawnedEnemy);
        spawnedEnemy = null;

        Debug.Log("Enemy Addressables ile unload edildi.");
    }
}