using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;

public static class SaveSystem
{
    public static string npcSavePath = Application.persistentDataPath + "/character.data";
    public static string playerSavePath = Application.persistentDataPath + "/npc.data";
    public static string itemsSavePath = Application.persistentDataPath + "/items.data";
    public static void SavePlayer(Character character)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(playerSavePath, FileMode.Create);

        PlayerData data = new PlayerData(character);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        if (File.Exists(playerSavePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(playerSavePath, FileMode.Open);

            // TODO: Error Handling
            PlayerData data = (PlayerData) formatter.Deserialize(stream);

            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError($"Save file not found in {playerSavePath}");
            return null;
        }
    }

    public static void SaveNPC(NPC npc)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(npcSavePath, FileMode.Create);
        NPCData data = new NPCData(npc);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static NPCData LoadNPC()
    {
        if (File.Exists(npcSavePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(npcSavePath, FileMode.Open);

            // TODO: Error Handling
            NPCData data = (NPCData) formatter.Deserialize(stream);

            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError($"Save file not found in {npcSavePath}");
            return null;
        }
    }

    public static void SaveWorldItems(Item[] items)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(itemsSavePath, FileMode.Create);


        ItemsData data = new ItemsData(items);
        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static ItemsData LoadWorldItems()
    {
        if (File.Exists(itemsSavePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(itemsSavePath, FileMode.Open);

            // TODO: Error Handling
            ItemsData data = (ItemsData)formatter.Deserialize(stream);

            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError($"Save file not found in {itemsSavePath}");
            return null;
        }
    }



    // If Data class is modified this need to be called or Deserialize will throw error
    public static void DeleteProgress()
    {
        File.Delete(playerSavePath);
        File.Delete(npcSavePath);
        File.Delete(itemsSavePath);
        Debug.Log("Progress Deleted!!!");
    }
}
