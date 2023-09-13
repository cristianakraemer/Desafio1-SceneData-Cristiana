using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableObject
{
    public string objectName; // Nome do objeto
    public string objectType; // GameObject, Primitive, Prefab
    public SerializableTransform transform;
    public SerializableCollider colliderType; // None, Collider, Trigger
    public SerializableColor renderColor;
    public List<SerializableComponent> components = new List<SerializableComponent>(); // Lista de componentes adicionais
}

[System.Serializable]
public class SerializableTransform
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
}

[System.Serializable]
public class SerializableCollider
{
    public string colliderType;
}

[System.Serializable]
public class SerializableColor
{
    public float r, g, b, a;
}

[System.Serializable]
public class SerializableComponent
{
    public string componentName; // Nome do componente
    public bool isEnabled; // Indica se o componente está habilitado ou desabilitado
}
