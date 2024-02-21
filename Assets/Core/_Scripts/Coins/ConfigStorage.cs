using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

public class ConfigStorage
{
    public ConfigStorage()
    {
        LoadResources();
    }
    protected void LoadResources()
    {
        Type objectType = this.GetType();

        foreach (FieldInfo field in objectType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        {
            var loadAttribute = field.GetCustomAttribute<LoadFromResources>();
            if (loadAttribute != null)
            {
                var values = Resources.LoadAll(loadAttribute.Patch, field.FieldType);
                field.SetValue(this, values[0]);
            }
        }
    }
}

[System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = true)]
sealed class LoadFromResources : Attribute
{
    public string Patch { get; private set; }
    public LoadFromResources(string patch = "")
    {
        Patch = patch;
    }
}
