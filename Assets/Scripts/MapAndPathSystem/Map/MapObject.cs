using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static MapGenerator;

[System.Serializable]
public class MapObject
{
    [Header("Nodes")]
    [SerializeField]
    private MapNode[,] mapNodes;

    [Header("Map Information")]
    [SerializeField]
    private GridType tileGridType;
    [SerializeField]
    private GridType mapGridType;
    [SerializeField]
    private int mapWidth;
    [SerializeField]
    private int mapHeight;
    [SerializeField]
    private float XTileOffset;
    [SerializeField]
    private float YTileOffset;

    #region Map LifeCycle
    public MapObject(GridType tileGridType = GridType.HEX, GridType mapGridType = GridType.SQUARE, int mapWidth = 10, int mapHeight = 10, float XTileOffset = 1.0f, float YTileOffset = 1.0f)
    {
        mapNodes = null;

        this.tileGridType = tileGridType;
        this.mapGridType = mapGridType;

        this.mapWidth = mapWidth;
        this.mapHeight = mapHeight;

        this.XTileOffset = XTileOffset;
        this.YTileOffset = YTileOffset;
    }

    #region Map Node Generation Functions
    public void GenerateMapNodes()
    {
        if(mapNodes != null)
        {
            DestroyMap();
        }

        switch(mapGridType)
        {
            case GridType.SQUARE:
                GenerateSquareMap();
                break;
            case GridType.HEX:
                GenerateHexMap();
                break;
        }

        OnMapGenerationCompleteEvent?.Invoke();

        OnMapCompletedEvent?.Invoke();
        return;
    }
    private void GenerateSquareMap()
    {
        mapNodes = new MapNode[mapWidth, mapHeight];

        Vector3 OffSetStart = new Vector3(XTileOffset * mapWidth / 2.0f, 0.0f, YTileOffset * mapHeight / 2.0f);

        float xPos = 0.0f;
        float yPos = 0.0f;

        int itt = 0;

        for(int x = 0; x < mapWidth; x++)
        {
            for(int y = 0; y < mapHeight; y++)
            {
                Vector3 nodeLocation = new Vector3(yPos, 0.0f, xPos) - OffSetStart;

                MapNode node = new MapNode(tileGridType, nodeLocation, new Vector2(x, y), itt);

                mapNodes[x, y] = node;

                switch (tileGridType)
                {
                    case GridType.SQUARE:
                        break;
                    case GridType.HEX:
                        ConnectNodeInSequenceSquareMapHexGrid(node, x, y);
                        break;
                }

                yPos += YTileOffset;

                itt++;
            }

            yPos = 0.0f;
            if(x % 2 == 0)
            {
                yPos += YTileOffset / 2.0f;
            }
            xPos += XTileOffset;
        }
    }
    private void ConnectNodeInSequenceSquareMapHexGrid(MapNode node, int x, int y)
    {
        PathFindingNode currentNode = mapNodes[x, y].GetPathNode();

        if (y > 0)
        {
            PathFindingNode leftNode = mapNodes[x, y - 1].GetPathNode();
            currentNode.ConnectNodeOnSide(NodeConnectionDirections.LEFT, leftNode);
            leftNode.ConnectNodeOnSide(NodeConnectionDirections.RIGHT, currentNode);
        }

        PathFindingNode downLeftNode = null;
        PathFindingNode downRightNode = null;

        if (x % 2 == 1)
        {
            //Debug.Log(x);
            if (x > 0)
            {
                downLeftNode = mapNodes[x - 1, y].GetPathNode();
                currentNode.ConnectNodeOnSide(NodeConnectionDirections.DOWN_LEFT, downLeftNode);
                downLeftNode.ConnectNodeOnSide(NodeConnectionDirections.UP_RIGHT, currentNode);
            }

            if (x > 0 && y < mapHeight - 1)
            {
                downRightNode = mapNodes[x - 1, y + 1].GetPathNode();
                currentNode.ConnectNodeOnSide(NodeConnectionDirections.DOWN_RIGHT, downRightNode);
                downRightNode.ConnectNodeOnSide(NodeConnectionDirections.UP_LEFT, currentNode);
            }

            return;
        }

        //Debug.Log(x);
        if (x > 0 && y > 0)
        {
            downLeftNode = mapNodes[x - 1, y - 1].GetPathNode();
            currentNode.ConnectNodeOnSide(NodeConnectionDirections.DOWN_LEFT, downLeftNode);
            downLeftNode.ConnectNodeOnSide(NodeConnectionDirections.UP_RIGHT, currentNode);
        }

        if (x > 0)
        {
            downRightNode = mapNodes[x - 1, y].GetPathNode();
            currentNode.ConnectNodeOnSide(NodeConnectionDirections.DOWN_RIGHT, downRightNode);
            downRightNode.ConnectNodeOnSide(NodeConnectionDirections.UP_LEFT, currentNode);
        }

    }
    private void GenerateHexMap()
    {
        mapNodes = new MapNode[0, 0];
    }

    #endregion

    #region Map Node Destruction Functions
    public void DestroyMapNodes()
    {

        for(int x = 0; x < mapWidth; x++)
        {
            for(int y = 0; y < mapHeight; y++)
            {
                mapNodes[x, y].DestroyNode();
                mapNodes[x, y] = null;
            }
        }

        mapNodes = null;

        OnMapDestructionEvent?.Invoke();
    }

    #endregion

    #region Map Generation Functions
    public void RandomizeMap()
    {
        foreach(MapNode node in mapNodes)
        {
            int random = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TileType)).Length);

            int tileWeight = TileAssets.GetInstance().GetTileWeight((TileType)random);

            node.SetTileType((TileType)random, tileWeight);
        }
    }

    #endregion

    #endregion

    #region Map Data Functions
    public GridType GetMapTileGridType()
    {
        return tileGridType;
    }
    public GridType GetMapGridType()
    {
        return mapGridType;
    }
    public int GetMapWidth()
    {
        return mapWidth;
    }
    public int GetMapHeight()
    {
        return mapHeight;
    }
    public int GetNodeCount()
    {
        int count = 0;
        
        foreach(MapNode node in mapNodes)
        {
            if (node == null)
                continue;

            count++;
        }

        return count;
    }
    public MapNode GetMapNode(int x, int y)
    {
        return mapNodes[x, y];
    }
    public MapNode GetNodeByPos(Vector3 pos) //might need to change this to be a bit more optimized. Maybe use chunk based sorting to reduce number to check
    {
        foreach (MapNode node in mapNodes)
        {
            if(node.GetNodePosition() == pos)
            {
                return node;
            }
        }

        return null;
    }
    public int GetNumberOfNodes()
    {
        return mapNodes.Length;
    }
    #endregion
}
