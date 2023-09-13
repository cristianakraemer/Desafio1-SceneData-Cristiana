using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe que serve para criar os objetos na cena ao dar play, e guarda os dados.
 public class SceneBuilder : MonoBehaviour
 {
     private SerializableSceneData sceneData = new SerializableSceneData();

    public void BuildScene()
     {
         // Criação do chão, paredes, jogador e inimigo
         CreateGround();
         CreateWalls();
         CreatePlayer();
         CreateEnemy();
    }

    private void CreateGround()
    {
         // Criação do chão (Primitive Cube) - Serve para criar o chão na cena através do código
         GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
         ground.transform.position = new Vector3(0f, 0f, 0f); // Posição
         ground.transform.localScale = new Vector3(20f, 1f, 20f); // Tamanho
         ground.transform.rotation = Quaternion.identity; // Rotação
         ground.GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 0.5f, 1f); // Cor
         ground.GetComponent<BoxCollider>(); // Colisor

        // Serve para adicionar os dados do objeto 'chão' à lista. Parte serializável.
        SerializableObject objectData = new SerializableObject
         {
             objectName = "Ground", // Nome do objeto
             objectType = "Cube", // Tipo do objeto
             transform = new SerializableTransform // Pega a transform do objeto
             {
                 position = ground.transform.position,
                 rotation = ground.transform.rotation,
                 scale = ground.transform.localScale
             },
             colliderType = new SerializableCollider { colliderType = "Collider" }, // Tipo de colisor, se é None, Collider ou Trigger
             renderColor = new SerializableColor
             {
                 r = 0.5f,
                 g = 0.5f,
                 b = 0.5f,
                 a = 1 // Cor do chão (cinza)
             },
        };

        SerializableComponent exampleComponent = new SerializableComponent
        {
            componentName = "ExampleComponentGround", // Nome do componente adicional
            isEnabled = true // Indica se o componente está habilitado ou desabilitado
        };
        objectData.components.Add(exampleComponent);

        sceneData.sceneObjects.Add(objectData);
    }

    private void CreateWalls() // Criação de duas paredes (Primitive Cube)
    {
         GameObject wall1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
         wall1.transform.position = new Vector3(0f, 2f, 10f);
         wall1.transform.localScale = new Vector3(20f, 4f, 0.1f);
         wall1.transform.rotation = Quaternion.identity;
         wall1.GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f, 1f);
         wall1.GetComponent<BoxCollider>();

         SerializableObject objectData1 = new SerializableObject
         {
             objectName = "Wall1",
             objectType = "Cube",
             transform = new SerializableTransform
             {
                 position = wall1.transform.position,
                 rotation = wall1.transform.rotation,
                 scale = wall1.transform.localScale
             },
             colliderType = new SerializableCollider { colliderType = "Collider" },
             renderColor = new SerializableColor
             {
                 r = 0f,
                 g = 1f,
                 b = 0f,
                 a = 1f // Cor da parede 1 (verde)
             }
         };

         SerializableComponent exampleComponent1 = new SerializableComponent
         {
             componentName = "ExampleComponentWall1",
             isEnabled = false
         };
         objectData1.components.Add(exampleComponent1);
        
         sceneData.sceneObjects.Add(objectData1);


         GameObject wall2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
         wall2.transform.position = new Vector3(-10f, 2f, 0f);
         wall2.transform.localScale = new Vector3(0.1f, 4f, 20f);
         wall2.transform.rotation = Quaternion.identity;
         wall2.GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f, 1f);
         wall2.GetComponent<BoxCollider>();

         SerializableObject objectData2 = new SerializableObject
         {
             objectName = "Wall2",
             objectType = "Cube",
             transform = new SerializableTransform
             {
                 position = wall2.transform.position,
                 rotation = wall2.transform.rotation,
                 scale = wall2.transform.localScale
             },
             colliderType = new SerializableCollider { colliderType = "Trigger" },
             renderColor = new SerializableColor
             {
                 r = 0f,
                 g = 1f,
                 b = 0f,
                 a = 1f // Cor da parede 2 (verde)
             }
         };

         SerializableComponent exampleComponent2 = new SerializableComponent
         {
             componentName = "ExampleComponentWall2",
             isEnabled = true
         };
         objectData1.components.Add(exampleComponent2);
        
         sceneData.sceneObjects.Add(objectData2);
    }

    private void CreatePlayer()
    {
         // Criação do jogador (Primitive - Capsule)
         GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
         player.transform.position = new Vector3(0f, 1.45f, 0f);
         player.transform.localScale = new Vector3(1f, 1f, 1f);
         player.transform.rotation = Quaternion.identity;
         player.GetComponent<Renderer>().material.color = new Color(0f, 0f, 1f, 1f);
         player.GetComponent<CapsuleCollider>();

         SerializableObject objectData = new SerializableObject
         {
             objectName = "Player",
             objectType = "Capsule",
             transform = new SerializableTransform
             {
                 position = player.transform.position,
                 rotation = player.transform.rotation,
                 scale = player.transform.localScale
             },
             colliderType = new SerializableCollider { colliderType = "Collider" },
             renderColor = new SerializableColor
             {
                 r = 0f,
                 g = 0f,
                 b = 1f,
                 a = 1 // Cor do player (azul)
             }
         };

         SerializableComponent exampleComponent = new SerializableComponent
         {
             componentName = "ExampleComponentPlayer",
             isEnabled = false
         };
         objectData.components.Add(exampleComponent);
        
         sceneData.sceneObjects.Add(objectData);
    }

    private void CreateEnemy()
    {
        // Criação do inimigo (Prefab - Sphere)
        GameObject enemyPrefab = Resources.Load<GameObject>("Enemy");

        if (enemyPrefab != null)
        {
            // Instancie o prefab na cena
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.position = new Vector3(-5f, 1.45f, -2f);
            enemy.transform.localScale = new Vector3(2f, 2f, 2f);
            enemy.transform.rotation = Quaternion.identity;
            enemy.GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f, 1f);
            enemy.GetComponent<SphereCollider>();

            SerializableObject objectData = new SerializableObject
            {
                objectName = "Enemy",
                objectType = "Prefab",
                transform = new SerializableTransform
                {
                    position = enemy.transform.position,
                    rotation = enemy.transform.rotation,
                    scale = enemy.transform.localScale
                },
                colliderType = new SerializableCollider { colliderType = "Collider" },
                renderColor = new SerializableColor
                {
                    r = 1f,
                    g = 0f,
                    b = 0f,
                    a = 1 // Cor do inimigo (vermelho)
                }
            };

            SerializableComponent exampleComponent = new SerializableComponent
            {
                componentName = "ExampleComponentEnemy",
                isEnabled = false
            };
            objectData.components.Add(exampleComponent);

            sceneData.sceneObjects.Add(objectData);
        }
        else
        {
            Debug.LogError("Prefab 'EnemyPrefab' não encontrado na pasta Resources.");
        }
    }

    public SerializableSceneData GetSceneData()
    {
        return sceneData;
    }
 }
