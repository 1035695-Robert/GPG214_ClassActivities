using JetBrains.Annotations;
using UnityEngine;



[System.Serializable]
public class PlayerStats
{
    public string playerName;
    public float playerHealth;
    public string playerWeapon;
    //public Vector3 playerPosition = new Vector3(0,0,0);
    public float[] playerPositionArray = new float[] {0, 0, 0 };
    public PlayerStats()
    { 

    }

    public PlayerStats(string PlayerName, string PlayerHealth, string PlayerWeapon, Vector3 PlayerPosition)
    {
        playerName = PlayerName;
        if (!float.TryParse(PlayerHealth, out playerHealth))
        {
            Debug.LogError("cant convert player Health into float");
        }
    }

    public void SetPlayerPosition(Vector3 PlayerPosition)
    {
        playerPositionArray[0] = PlayerPosition.x;
        playerPositionArray[1] = PlayerPosition.y;
        playerPositionArray[2] = PlayerPosition.z;
    }
    public Vector3 ReturnPlayerPosition()
    {
        if (playerPositionArray.Length < 3)
        {
            Debug.Log("not elements in the array");
            return Vector3.zero;
        }

        return new Vector3(playerPositionArray[0], playerPositionArray[1], playerPositionArray[2]);
    }

    public string ReturnPlayerSaveData()
    {
        string returnData = "Name: " + playerName + "\n, Health: " + playerHealth + "\n, Weapon: " + playerWeapon;
        return returnData;
    }
}
