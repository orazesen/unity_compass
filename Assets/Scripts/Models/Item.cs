using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;
    public string name;
    public string fullName;
    public string description;
    public List<string> image;

    public Item(int _id,string _name, string _fullName, string _description, List<string> _image) {
        id = _id;
        name = _name;
        fullName = _fullName;
        description = _description;
        image = _image;
    }
}
