using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SceneLoader : MonoBehaviour
{
    public string jsonFileName = "sceneData.json"; // Nome do arquivo JSON

    private SceneBuilder sceneBuilder;

    // Dicionário que serve para mapear os tipos de objetos Primitive.
    private Dictionary<string, PrimitiveType> objectTypeToPrimitiveType = new Dictionary<string, PrimitiveType>
{
    { "Cube", PrimitiveType.Cube },
    { "Capsule", PrimitiveType.Capsule },
    { "Sphere", PrimitiveType.Sphere },
    { "Cylinder", PrimitiveType.Cylinder },
    { "Plane", PrimitiveType.Plane },
    { "Quad", PrimitiveType.Quad }
};

    private void Start()
    {
        sceneBuilder = GetComponent<SceneBuilder>(); // Obtêm a referência para o SceneBuilder
        BuildAndSaveScene(); // Salva a cena em JSON
        LoadSceneFromJSON(); // Carrega a cena a partir do JSON
    }

    private void BuildAndSaveScene() // Método que serve para serializar
    {
        sceneBuilder.BuildScene();
        SerializableSceneData sceneData = sceneBuilder.GetSceneData();
        string json = JsonUtility.ToJson(sceneData, true);

        // Salva o JSON na pasta "Assets"
        string saveFilePath = Path.Combine(Application.dataPath, jsonFileName);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Scene data saved to: " + saveFilePath);
    }

    private void LoadSceneFromJSON() // Método que serve para desserializar
    {
        // Lê o JSON da pasta "Assets"
        string jsonFilePath = Path.Combine(Application.dataPath, jsonFileName);
        string json = File.ReadAllText(jsonFilePath);

        // Serve para converter o JSON em objetos
        SerializableSceneData sceneData = JsonUtility.FromJson<SerializableSceneData>(json);

        // Responsável por recriar os objetos na cena com base nos dados serializados armazenados na sceneData
        foreach (SerializableObject objectData in sceneData.sceneObjects)
        {
            if (objectData.objectType == "GameObject")
            {
                GameObject newObj = new GameObject(objectData.objectName);

                newObj.transform.position = objectData.transform.position;
                newObj.transform.rotation = objectData.transform.rotation;
                newObj.transform.localScale = objectData.transform.scale;

                if (objectData.colliderType.colliderType == "Collider")
                {
                    newObj.AddComponent<BoxCollider>();
                }
                else if (objectData.colliderType.colliderType == "Trigger")
                {
                    newObj.AddComponent<BoxCollider>().isTrigger = true;
                }

                Color objectColor = new Color(objectData.renderColor.r, objectData.renderColor.g, objectData.renderColor.b, objectData.renderColor.a);
                newObj.AddComponent<MeshRenderer>().material.color = objectColor;
            }
            else if (objectData.objectType == "Prefab")
            {
                GameObject prefab = Resources.Load<GameObject>(objectData.objectName);
                if (prefab != null)
                {
                    GameObject newObj = Instantiate(prefab);
                    newObj.name = objectData.objectName;

                    newObj.transform.position = objectData.transform.position;
                    newObj.transform.rotation = objectData.transform.rotation;
                    newObj.transform.localScale = objectData.transform.scale;

                    if (objectData.colliderType.colliderType == "Collider")
                    {
                        newObj.AddComponent<BoxCollider>();
                    }
                    else if (objectData.colliderType.colliderType == "Trigger")
                    {
                        newObj.AddComponent<BoxCollider>().isTrigger = true;
                    }

                    Color objectColor = new Color(objectData.renderColor.r, objectData.renderColor.g, objectData.renderColor.b, objectData.renderColor.a);
                    newObj.GetComponent<Renderer>().material.color = objectColor;
                }
                else
                {
                    Debug.LogError("Prefab não encontrado: " + objectData.objectName);
                }
            }
            else if (objectTypeToPrimitiveType.TryGetValue(objectData.objectType, out PrimitiveType primitiveType))
            {
                GameObject newObj = GameObject.CreatePrimitive(primitiveType);
                newObj.name = objectData.objectName;

                newObj.transform.position = objectData.transform.position;
                newObj.transform.rotation = objectData.transform.rotation;
                newObj.transform.localScale = objectData.transform.scale;

                if (objectData.colliderType.colliderType == "Collider")
                {
                    newObj.AddComponent<BoxCollider>();
                }
                else if (objectData.colliderType.colliderType == "Trigger")
                {
                    newObj.AddComponent<BoxCollider>().isTrigger = true;
                }

                Color objectColor = new Color(objectData.renderColor.r, objectData.renderColor.g, objectData.renderColor.b, objectData.renderColor.a);
                newObj.GetComponent<Renderer>().material.color = objectColor;
            }
            else
            {
                Debug.LogError("Tipo de objeto desconhecido: " + objectData.objectType);
            } 
        }

        // Armazena os dados dos componentes adicionais
        foreach (SerializableObject objectData in sceneData.sceneObjects)
        {
            foreach (SerializableComponent component in objectData.components)
            {
                // Lógica para aplicar o componente ao objeto representado por objectData
            }
        }
    }
}
