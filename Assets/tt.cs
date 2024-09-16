using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tt : MonoBehaviour
{
    // Start is called before the first frame update
    private cokeName _coke = new coke();

    void Start()
    {
        Debug.Log(_coke.GetName());
        _coke = new iceCoke(_coke as coke);
        Debug.Log(_coke.GetName());

    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class coke : cokeName
{
    public string name = "콜라";
    public string GetName()
    {
        return name;
    }
}

public class iceCoke : cokeName
{
    public coke _coke;

    public string GetName()
    {
        return "얼음" + _coke.GetName();
    }

    public iceCoke(coke _value)
    {
        _coke = _value;
    }
}

public interface cokeName
{
    public string GetName();



}



