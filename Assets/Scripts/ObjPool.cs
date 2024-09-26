using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjPool<T>
{
    private Func<T> _metodoFactory = default;
    private Action<T, bool> _switchOnOffCallBack = default;
    private bool _dinamico = true;

    private List<T> _currentStock = new List<T>();

    public ObjPool(Func<T> metodoFactory, Action<T, bool> callBack, int stockInicial = 1, bool dinamico = true)
    {
        _metodoFactory = metodoFactory;
        _switchOnOffCallBack = callBack;
        _dinamico = dinamico;

        for (int i = 0; i < stockInicial; i++)
        {
            T obj = _metodoFactory();
            _switchOnOffCallBack(obj, false);
            _currentStock.Add(obj);
        }
    }

    public T GetObject()
    {
        var resultado = default(T);

        if (_currentStock.Count > 0)
        {
            resultado = _currentStock[0];
            _currentStock.RemoveAt(0);
        }
        else if (_dinamico == true)
        {
            resultado = _metodoFactory();
        }

        if (resultado != null)
        {
            _switchOnOffCallBack(resultado, true);  
        }

        return resultado;
    }
    public void ReturnObj(T obj)
    {
        _switchOnOffCallBack(obj, false);
        _currentStock.Add(obj);
    }
}