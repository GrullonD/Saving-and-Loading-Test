using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// The idea of this game is to be able to instantiate objects into the scene, move them around in the editor, 
/// save the scene state, and then load it using JSON. As we agreed Friday that is most likely the route we will
/// go.
/// 
/// The theory is that since everything loaded here (and our magazine) is a prefab, we should theoretically just
/// need to remember which prefab it is and it's transforms. All of the GameObjects internal scripts (if they 
/// have any) will take care of the rest.
/// </summary>
public class GameController : MonoBehaviour
{
    private string FILE_PATH;

    public GameObject AnimalsParent;
    public GameObject InstantiationLocation;

    public GameObject m_Chicken;
    public GameObject m_Cow;
    public GameObject m_Duck;
    public GameObject m_Pig;
    public GameObject m_Sheep;
    public GameObject m_Background;

    private void Start()
    {
        FILE_PATH = Application.dataPath + "/Resources/sceneInformation.json";
    }

    /// <summary>
    /// Called when [save click].
    /// </summary>
    public void OnSaveClick()
    {
        // Get scene information
        ListContainer sceneInformation = GetScenesObjectInformation();

        // Get json string
        string jsonData = JsonUtility.ToJson(sceneInformation, true);

        // Save json string to file
        File.WriteAllText(FILE_PATH, jsonData);

    }

    /// <summary>
    /// Called when [load click].
    /// </summary>
    public void OnLoadClick()
    {
        if (System.IO.File.Exists(FILE_PATH))
        {        
            // Get JSON file
            string jsonData = File.ReadAllText(FILE_PATH);
            Debug.Log("Loaded JSON Data: " + jsonData);

            // Turn JSON file into class
            ListContainer sceneInformation = JsonUtility.FromJson<ListContainer>(jsonData);

            LoadScene(sceneInformation.ListOfOI);
        }
        else Debug.LogWarning("No saved scene located");
    }

    /// <summary>
    /// Loads the scene. This is something super fast for a proof of concept and demo so we
    /// could talk about it in person. 
    /// 
    /// Instead of hashing it all out and dealing with duplicates. I'm just going to delete the
    /// scene and load it as the JSON file says to
    /// 
    /// Instead of creating some function that finds the prefab that matches the ObjectInformation's
    /// objectsPrefab variable, I'm making a quick switch statement that knows where each is.
    /// 
    /// Once again, just a quick set up to test this out
    /// </summary>
    private void LoadScene(List<ObjectInformation> oiList)
    {
        // Delete all the children
        foreach (Transform child in AnimalsParent.transform)
        {
            Destroy(child.gameObject);
        }

        // Spawn each child
        GameObject instantiatedObject;
        foreach (ObjectInformation oi in oiList)
        {
            switch (oi.objectsPrefab)
            {
                case ObjectPrefab.Chicken:
                    instantiatedObject = InstantiateAnimal(m_Chicken);
                    break;
                case ObjectPrefab.Cow:
                    instantiatedObject = InstantiateAnimal(m_Cow);
                    break;
                case ObjectPrefab.Duck:
                    instantiatedObject = InstantiateAnimal(m_Duck);
                    break;
                case ObjectPrefab.Pig:
                    instantiatedObject = InstantiateAnimal(m_Pig);
                    break;
                case ObjectPrefab.Sheep:
                    instantiatedObject = InstantiateAnimal(m_Sheep);
                    break;
                default:
                    instantiatedObject = null;
                    break;
            }

            if (instantiatedObject != null)
            {
                Vector3 position = new Vector3(oi.Position[0], oi.Position[1], oi.Position[2]);
                Quaternion rotation = new Quaternion(oi.Quaternion[1], oi.Quaternion[2], oi.Quaternion[3], oi.Quaternion[0]);

                instantiatedObject.transform.position = position;
                instantiatedObject.transform.rotation = rotation;
            }
        }
    }


    /// <summary>
    /// Gets the scenes object information.
    /// </summary>
    /// <returns>A container containing a list of ObjectInformation</returns>
    private ListContainer GetScenesObjectInformation()
    {
        List<ObjectInformation> oiList = new List<ObjectInformation>();

        int numberOfAnimals = AnimalsParent.transform.childCount;

        ObjectInformation oi;
        Transform animal;
        for(int i = 0; i < numberOfAnimals; i++)
        {
            animal = AnimalsParent.transform.GetChild(i);
            oi = new ObjectInformation();

            // Add id
            oi.ID = i;

            // Add position
            oi.Position[0] = animal.position.x;
            oi.Position[1] = animal.position.y;
            oi.Position[2] = animal.position.z;

            // Add rotation
            oi.Quaternion[0] = animal.rotation.w;
            oi.Quaternion[1] = animal.rotation.x;
            oi.Quaternion[2] = animal.rotation.y;
            oi.Quaternion[3] = animal.rotation.z;

            // Add scale
            oi.Scale[0] = animal.localScale.x;
            oi.Scale[1] = animal.localScale.y;
            oi.Scale[2] = animal.localScale.z;

            // Add prefab type
            oi.objectsPrefab = animal.GetComponent<ObjectController>().objectPrefab;

            // Add to list
            oiList.Add(oi);
        }

        return new ListContainer(oiList);
    }

    /*
     * 
     * Instantiate Animals 
     * 
     */
    private GameObject InstantiateAnimal(GameObject go)
    {
        return Instantiate(go, InstantiationLocation.transform.position, go.transform.rotation, AnimalsParent.transform);
    }
    /// <summary>
    /// Called when [chicken click].
    /// </summary>
    public void OnChickenClick()
    {
        InstantiateAnimal(m_Chicken);
    }
    /// <summary>
    /// Called when [cow click].
    /// </summary>
    public void OnCowClick()
    {
        InstantiateAnimal(m_Cow);
    }
    /// <summary>
    /// Called when [duck click].
    /// </summary>
    public void OnDuckClick()
    {
        InstantiateAnimal(m_Duck);
    }
    /// <summary>
    /// Called when [pig click].
    /// </summary>
    public void OnPigClick()
    {
        InstantiateAnimal(m_Pig);
    }
    /// <summary>
    /// Called when [sheep click].
    /// </summary>
    public void OnSheepClick()
    {
        InstantiateAnimal(m_Sheep);
    }

}
