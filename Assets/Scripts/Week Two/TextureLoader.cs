using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TextureLoader : MonoBehaviour
{

    public string fileName = "sandshrew.jpg";
    public string folderPath = Application.streamingAssetsPath;
    private string combinedFilePathLocation;

    public string spriteName = "Textures/heart.jpg";
    private string combinedHeartsFilePath;

    public Image playerHealthSprite;
    void Start()
    {
        //combine the path.
        combinedFilePathLocation = Path.Combine(folderPath, fileName);

        combinedHeartsFilePath = Path.Combine(folderPath, spriteName);

        LoadTexture();

        LoadSprite();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LoadTexture()
    {
        if (File.Exists(combinedFilePathLocation))
        {
            // read in all the bytes of data i.e 1001010
            byte[] imageBytes = File.ReadAllBytes(combinedFilePathLocation);

            //create a temporary texture to hold our texture in 
            Texture2D texture = new Texture2D(2, 2);
            //takes the byte in and convert it into an image 
            texture.LoadImage(imageBytes);

            GetComponent<Renderer>().material.mainTexture = texture;
        }
        else
        {
            Debug.Log("texture file mot found at path" + combinedFilePathLocation);
        }
    }
    private void LoadSprite()
    {
        if (File.Exists(combinedFilePathLocation))
        {
            // read in all the bytes of data i.e 1001010
            byte[] spriteBytes = File.ReadAllBytes(combinedHeartsFilePath);

            //create a temporary texture to hold our texture in 
            Texture2D texture = new Texture2D(2, 2);
            //takes the byte in and convert it into an image 
            texture.LoadImage(spriteBytes);

            playerHealthSprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

        }
        else
        {
            Debug.Log("texture file mot found at path" + combinedHeartsFilePath);
        }
    }
}
