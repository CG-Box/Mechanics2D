using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType
{
    Walk, // 1,4 - 1,6
    Run, // 2,22 - 3,33
    Bicycle, // 4,17 - 6,9
    Car // 5,5 до 11,1 
}

public class Speeder
{
    MoveType moveType;
    public float Speed { get { return speedDict[moveType]; } }

    Dictionary<MoveType, float> speedDict;

    public Speeder()
    {
        speedDict = new Dictionary<MoveType, float>();
        speedDict[MoveType.Walk] = 1.5f;
        speedDict[MoveType.Run] = 2.5f;
        speedDict[MoveType.Bicycle] = 5f;
        speedDict[MoveType.Car] = 8f;
    }
    public void SetType(MoveType type)
    {
        moveType = type;
    }
}

public class MapMover : MonoBehaviour
{
    [SerializeField] ZoneID currentZoneID;
    [SerializeField] MoveType moveType;

    [SerializeField] private Zone[] zones;
    [SerializeField] private ZoneEntry[] entries;
    [SerializeField] private Road[] roads;

    List<Road> lastRoads = new List<Road>();

    Speeder speeder;

    void OnEnable()
    {
        BindListeners();
    }
    void OnDisable() 
    {
        UnbindListeners();
    }

    void Start()
    {
        Init();
    }

    void ActivateZone(ZoneID zoneID)
    {
        foreach(var zone in zones)
        {
            if(zone.ZoneID == zoneID)
            {
                zone.AddActive();
            }
            else
            {
                zone.RemoveActive();
            }
        }
    }

    void DisableZoneEntries(ZoneID zoneID)
    {
        foreach(var entry in entries)
        {
            if(entry.ConnectedZone.ZoneID == zoneID)
            {
                entry.Disable();
            }
            else
            {
                entry.Enable();
            }
        }
    }

    Road GetRoad(RoadID roadID)
    {
        foreach(var road in roads)
        {
            if(road.RoadID == roadID)
            {
                return road;
            }
        }
        return null;
    }

    void AddRoadListeners(Road road)
    {
        road.OnSelectRoad += MapMover_OnSelectRoad;
    }
    void RemoveRoadListeners(Road road)
    {
        road.OnSelectRoad -= MapMover_OnSelectRoad;
    }


    void AddEntryListeners(ZoneEntry entry)
    {
        entry.OnSelectEntry += MapMover_OnSelectEntry;
    }
    void RemoveEntryListeners(ZoneEntry entry)
    {
        entry.OnSelectEntry -= MapMover_OnSelectEntry;
    }
    void BindListeners()
    {
        foreach(var road in roads)
        {
            AddRoadListeners(road);
        }

        foreach(var entry in entries)
        {
            AddEntryListeners(entry);
        }
    }
    void UnbindListeners()
    {
        foreach(var road in roads)
        {
            RemoveRoadListeners(road);
        }

        foreach(var entry in entries)
        {
            RemoveEntryListeners(entry);
        }
    }


    void Init()
    {
        speeder = new Speeder();
        speeder.SetType(moveType);

        ActivateZone(currentZoneID);
        DisableZoneEntries(currentZoneID);
    }

    [ContextMenu("ChangeType")]
    public void ChangeType()
    {
        speeder.SetType(moveType);
    }

    void CalcWayTime(int wayLength)
    {
        float time = 0;
        time += wayLength / speeder.Speed;

        Debug.Log($"length {wayLength}, time : {time}");
    }

    void MapMover_OnSelectRoad(object sender, int length)
    {
        CalcWayTime(length);
    }
    void MapMover_OnSelectEntry(object sender, ZoneEntry entry)
    {
        ActivateZone(entry.ConnectedZone.ZoneID);
        DisableZoneEntries(entry.ConnectedZone.ZoneID);
        Navigate(entry);
    }

    void Navigate(ZoneEntry entry)
    {
        int wayLength = 0;
        List<Road> wayRoads = new List<Road>();

        switch (entry.EntryID)
        {
            case EntryID.A1:
            case EntryID.A2:
                if(currentZoneID == ZoneID.B)
                {
                    wayRoads.Add(GetRoad(RoadID.AB));
                }
                else if(currentZoneID == ZoneID.C)
                {
                    wayRoads.Add(GetRoad(RoadID.AC));
                }
                else if(currentZoneID == ZoneID.D)
                {
                    wayRoads.Add(GetRoad(RoadID.BD));
                    wayRoads.Add(GetRoad(RoadID.AB));
                }
                break;
            case EntryID.B1:
            case EntryID.B2:
            case EntryID.B3:
                if(currentZoneID == ZoneID.A)
                {
                    wayRoads.Add(GetRoad(RoadID.AB));
                }
                else if(currentZoneID == ZoneID.C)
                {
                    wayRoads.Add(GetRoad(RoadID.CB));
                }
                else if(currentZoneID == ZoneID.D)
                {
                    wayRoads.Add(GetRoad(RoadID.BD));
                }
                break;
            case EntryID.C1:
            case EntryID.C2:
                if(currentZoneID == ZoneID.A)
                {
                    wayRoads.Add(GetRoad(RoadID.AC));
                }
                else if(currentZoneID == ZoneID.B)
                {
                    wayRoads.Add(GetRoad(RoadID.CB));
                }
                else if(currentZoneID == ZoneID.D)
                {
                    wayRoads.Add(GetRoad(RoadID.BD));
                    wayRoads.Add(GetRoad(RoadID.CB));
                }
                break;
            case EntryID.D1:
                if(currentZoneID == ZoneID.A)
                {
                    wayRoads.Add(GetRoad(RoadID.AB));
                    wayRoads.Add(GetRoad(RoadID.BD));
                }
                else if(currentZoneID == ZoneID.B)
                {
                    wayRoads.Add(GetRoad(RoadID.BD));
                }
                else if(currentZoneID == ZoneID.C)
                {
                    wayRoads.Add(GetRoad(RoadID.CB));
                    wayRoads.Add(GetRoad(RoadID.BD));
                }
                break;
            default:
                Debug.Log($"Add new entry to switch for {entry.EntryID}");
                break;
        }

        float delay = 0.2f;
        foreach(var wayRoad in wayRoads)
        {
            wayLength += wayRoad.Length;
            wayRoad.Bounce(delay);
            delay *=2;
        }

        lastRoads = wayRoads;

        CalcWayTime(wayLength);
        Debug.Log($"EntryID {entry.EntryID}, wayLength {wayLength}");

        currentZoneID = entry.ConnectedZone.ZoneID;
    }
}
