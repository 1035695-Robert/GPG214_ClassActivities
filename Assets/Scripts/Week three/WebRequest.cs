using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class WebRequest : MonoBehaviour
{
    [SerializeField] private string webAddress;

    public Image myWebTexture;
    private Sprite mySpriteFromWeb;


    private IEnumerator Start()
    {
        //show a loading screen
        yield return StartCoroutine(LoadTextureFromWeb());

        myWebTexture.sprite = mySpriteFromWeb;

        //hide the loading screen and show the image 
        // wait unitl the above is done. // then do more
        yield return null;
    }

    IEnumerator LoadTextureFromWeb()
    {

        UnityWebRequest imageRequest = UnityWebRequest.Get(webAddress);
        
        AsyncOperation downloadOperation = imageRequest.SendWebRequest();

        while(!downloadOperation.isDone)
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
        Texture2D myTexture = new Texture2D(2,2);

        myTexture.LoadImage(allDataDownloaded);

        mySpriteFromWeb = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), Vector2.zero);

        yield return null;

    }

}

