using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadAssetBundlesFromServer : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DownloadAssetBundleFromServer());

    }
    private IEnumerator DownloadAssetBundleFromServer()
    {
        GameObject obj = null;

        string url = "https://drive.google.com/u/0/uc?id=1TuxYZRi3Yti8zuL4jCRRUOGHpM8tbbiE&export=download";

        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {

            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning("Error" + url + "" + www.error);
            }
            else
            {
                Debug.Log(www);
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
                obj = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as GameObject;
                bundle.Unload(false);
                yield return new WaitForEndOfFrame();
            }
            www.Dispose();
        }
        InstantiateGameobjectFromAssetBundle(obj);

    }
    private void InstantiateGameobjectFromAssetBundle(GameObject obj)
    {
        if (obj != null)
        {
            GameObject instanceObj = Instantiate(obj);
            // instanceObj.transform.position = Vector3.zero;
        }
        else
        {
            Debug.LogWarning("Asset Bundle is null");
        }
    }
}