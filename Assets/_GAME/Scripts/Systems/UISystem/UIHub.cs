using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHub : PersistentSingleton<UIHub>
{
    [SerializeField] private List<BaseUI> uisByPriority;

    private readonly Dictionary<Type, BaseUI> uis = new Dictionary<Type, BaseUI>();

    protected override void Awake()
    {
        base.Awake();
        PopulateDictionary();
    }

    private void PopulateDictionary()
    {
        foreach (BaseUI ui in uisByPriority)
        {
            uis.Add(ui.GetType(), ui);
        }
    }

    public static T Get<T>() where T : BaseUI
    {
        return (T)Instance.uis[typeof(T)];
    }
}
