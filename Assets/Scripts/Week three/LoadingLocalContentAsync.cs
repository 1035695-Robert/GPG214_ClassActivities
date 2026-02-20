using System.Collections;
using System.Diagnostics.Contracts;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class LoadingLocalContentAsync : MonoBehaviour
{
    public string jsonFileName;
    public string jsonData;

    public Texture texture;
    public Sprite spriteImage;
    public string imageFileName;


    public AudioClip clip;
    public string audioFileName;

    
    public AssetBundle bundle;
    public string assetBundleName;

    public string streamingAssetsFolderPath = Application.streamingAssetsPath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        yield return StartCoroutine(LoadLocalJsonAsync());
        //waits and do the next one.

        yield return StartCoroutine(LoadTextureFromFile());
    
        yield return StartCoroutine(LoadAudioClipFromFile());

        yield return StartCoroutine(LoadAssetBundleFromFile());
    }

    // Update is called once per frame


    IEnumerator LoadLocalJsonAsync()
    {
        UnityWebRequest jsonLoadingRequest = UnityWebRequest.Get(Path.Combine(streamingAssetsFolderPath, jsonFileName));

        AsyncOperation downloadOperation = jsonLoadingRequest.SendWebRequest();

        while (!downloadOperation.isDone)
        {
            Debug.Log("download progress: " + ((downloadOperation.progress / 1f) * 100) + "%");
            yield return null;
        }
        if (jsonLoadingRequest.result == UnityWebRequest.Result.ConnectionError || jsonLoadingRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("error with downloading file");
            yield break;
        }

        Debug.Log("download complete");

        //access the web request acess the download handler and just grab the text;
        jsonData = jsonLoadingRequest.downloadHandler.text;

        jsonLoadingRequest.Dispose();
        //best practice: frees up memory

        yield return null;
    }

    IEnumerator LoadTextureFromFile()
    {

        UnityWebRequest imageRequest = UnityWebRequest.Get(Path.Combine(streamingAssetsFolderPath, imageFileName));

        AsyncOperation downloadOperation = imageRequest.SendWebRequest();

        while (!downloadOperation.isDone)
        {
            Debug.Log("download progress: " + ((downloadOperation.progress / 1f) * 100) + "%");
            yield return null;
        }
        if (imageRequest.result == UnityWebRequest.Result.ConnectionError || imageRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("error with downloading file");
            yield break;
        }

        Debug.Log("download complete");

        byte[] allDataDownloaded = imageRequest.downloadHandler.data;
        Texture2D myTexture = new Texture2D(2, 2);

        myTexture.LoadImage(allDataDownloaded);

        texture = myTexture;

        spriteImage = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), Vector2.zero);

        imageRequest.Dispose();

        yield return null;
    }
    IEnumerator LoadAudioClipFromFile()
    {

        UnityWebRequest audioClipRequest = UnityWebRequestMultimedia.GetAudioClip(Path.Combine(streamingAssetsFolderPath, audioFileName), AudioType.WAV);

        AsyncOperation downloadOperation = audioClipRequest.SendWebRequest();

        while (!downloadOperation.isDone)
        {
            Debug.Log("download progress: " + ((downloadOperation.progress / 1f) * 100) + "%");
            yield return null;
        }
        if (audioClipRequest.result == UnityWebRequest.Result.ConnectionError || audioClipRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("error with downloading file");
            yield break;
        }

    Debug.Log("download complete");
        clip = DownloadHandlerAudioClip.GetContent(audioClipRequest);
        //byte[] allDataDownloaded = audioClipRequest.downloadHandler.data;

        //float[] floatArray = new float[allDataDownloaded.Length / 2];

        //for(int i = 0; i < floatArray.Length; i++)
        //{
        //    short bitValue = System.BitConverter.ToInt16(allDataDownloaded, i * 2);

        //    floatArray[i] = bitValue / 32768.0f;
        //}

        //clip = AudioClip.Create("coin", floatArray.Length, 1, 44100, false);
        //clip.SetData(floatArray, 0);

        AudioSource.PlayClipAtPoint(clip, Vector3.zero);

        audioClipRequest.Dispose();
        yield return null;
    }

    IEnumerator LoadAssetBundleFromFile()
    {
        UnityWebRequest assetBundleRequest = UnityWebRequest.Get(Path.Combine(streamingAssetsFolderPath, assetBundleName));

        AsyncOperation downloadOperation = assetBundleRequest.SendWebRequest();

        while (!downloadOperation.isDone)
        {
            Debug.Log("download progress: " + ((downloadOperation.progress / 1f) * 100) + "%");
            yield return null;
        }
        if (assetBundleRequest.result == UnityWebRequest.Result.ConnectionError || assetBundleRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("error with downloading file");
            yield break;
        }

        Debug.Log("download complete");

        bundle = DownloadHandlerAssetBundle.GetContent(assetBundleRequest);

        //byte[] allDataDownloaded = assetBundleRequest.downloadHandler.data;
        
        //AssetBundleCreateRequest assetBundleCreateRequest = AssetBundle.LoadFromMemoryAsync(allDataDownloaded);

        //yield return assetBundleCreateRequest;

        //bundle = assetBundleCreateRequest.assetBundle;

        assetBundleRequest.Dispose();
        yield return null;
    }
}