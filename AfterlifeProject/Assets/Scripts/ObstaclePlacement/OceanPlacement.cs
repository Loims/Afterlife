using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanPlacement : MonoBehaviour
{
    [Header("Plane Transform")]
    [SerializeField] private Transform planeTransform;

    [Space]
    [Header("Object Pooler")]
    [SerializeField] private ObjectPooler pooler;

    private List<GameObject> objectList;

    [SerializeField] private GameObject oceanFloor;


    [SerializeField] private int floorIncrement = 0;

    private void OnEnable()
    {
        InstantiatePrefabs();

        planeTransform = transform.parent;

        objectList = new List<GameObject>();
    }

    private void Start()
    {
        pooler = ObjectPooler.instance;
        if (oceanFloor != null)
        {
            for (int i = 0; i < 3; i++)
            {
                objectList.Add(pooler.NewObject(oceanFloor, new Vector3(planeTransform.position.x, planeTransform.position.y - 8f, (i * oceanFloor.transform.localScale.z) * 10), Quaternion.identity));
                floorIncrement++;
            }
        }
    }

    private void Update()
    {
        foreach(GameObject obj in objectList)
        {
            MoveFloorTile(obj);
        }
    }

    private void InstantiatePrefabs()
    {
        oceanFloor = Resources.Load<GameObject>("OceanFloor");
    }

    private void MoveFloorTile(GameObject obj)
    {
        if (obj.transform.position.z <= planeTransform.position.z - (oceanFloor.transform.localScale.z * 5))
        {
            obj.transform.position = new Vector3(planeTransform.position.x, planeTransform.position.y - 8f, (floorIncrement * oceanFloor.transform.localScale.z) * 10);
            floorIncrement++;
        }
    }

}
