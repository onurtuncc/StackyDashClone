using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AdressableManager : MonoBehaviour
{
    
    [SerializeField] private AssetReference tpAssetReference;
    
    public Transform roadParent;
    private void Start()
    {
        
        Addressables.InitializeAsync().Completed += AdressableManager_Completed;
    }

    private void AdressableManager_Completed(AsyncOperationHandle<IResourceLocator> obj)
    {
        
        tpAssetReference.InstantiateAsync().Completed += (tpGo) =>
        {
            
            tpGo.Result.transform.SetParent(roadParent);
        };
        
        
             
        
    }
    //
}
