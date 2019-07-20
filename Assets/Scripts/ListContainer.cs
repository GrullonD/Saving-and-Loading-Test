using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ListContainer
{
    public List<ObjectInformation> ListOfOI;

	public ListContainer(List<ObjectInformation> _listOfOI)
    {
        ListOfOI = _listOfOI;
    }
}
