using System;
using UnityEngine;
using Zenject;

public class Test : IInitializable
{
    public void Initialize()
    {
        TestEnum();
    }

    private void TestEnum()
    {
        PrintEnum<Enum1>();
        var def1 = default(Enum1);
        Debug.Log(def1);
        PrintEnum<Enum2>();
        Debug.Log((int) Enum2.B);
        Debug.Log((int) Enum2.C);
        PrintEnum<Enum3>();
        var def3 = default(Enum3);
        Debug.Log(def3);
        Debug.Log((Enum2) 1);
    }

    private void PrintEnum<T>()
    {
        var type = typeof(T);
        if (!type.IsEnum)
        {
            Debug.LogError($"Тип {type.Name} не является enum");
            return;
        }
        var names = Enum.GetNames(type);
        var values = Enum.GetValues(type) as int[];
        Debug.Log($"Печатаются значения и номера типа {type.Name}:");
        for (var i = 0; i < names.Length; i++)
        {
            Debug.Log($"name = {names[i]}, value = {values[i]}");
        }
    }

    private enum Enum1
    {
        A,
        B,
        C,
        D,
        E
    }
    private enum Enum2
    {
        A,
        B,
        C = 1,
        D,
        E
    }
    private enum Enum3
    {
        A = 1,
        B,
        C,
        D,
        E
    }
}