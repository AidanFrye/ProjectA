using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Tilemaps;
using Mono.Data.Sqlite;
using System.Xml.Serialization;
using Unity.VisualScripting;

public class tilemapLoader : MonoBehaviour
{
    private string dbPath;
    public Tilemap tilemap;
    public List<Tile> tiles = new List<Tile>();
    public TileBase[] tileSprites = new TileBase[16]; 

    void Start()
    {
        dbPath = "URI=file:levelDatabase.db";
        CreateDB();
        WriteMapToDatabase();
        GetNewTilemap();
        PlaceTilemap(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S)) 
        {
            ClearTiles();
            WriteMapToDatabase();
            Debug.Log("Map saved");
        }
    }

    public void CreateDB() 
    {
        using (var connection = new SqliteConnection(dbPath)) 
        {
            connection.Open();

            using (var command = connection.CreateCommand()) 
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS level1 (PositionX INT, PositionY INT, PositionZ INT, tileType INT);";
                command.ExecuteNonQuery();
            }

            connection.Close();
            Debug.Log("Database connected/created");
        }
    }

    public void PlaceTilemap(int yOffset)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].getType() < 16)
            {
                tilemap.SetTile(new Vector3Int(tiles[i].getPosX(), tiles[i].getPosY() + yOffset, tiles[i].getPosZ()), tileSprites[tiles[i].getType()]);
            }
        }
    }

    public void ClearTiles() 
    {
        using (var connection = new SqliteConnection(dbPath))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM level1";
                command.ExecuteNonQuery();
            }

            connection.Close();
            Debug.Log("Cleared tiles from database");
        }
    }

    public void AddTile(int posX, int posY, int posZ, int tileType) 
    {
        using (var connection = new SqliteConnection(dbPath)) 
        {
            connection.Open();

            using (var command = connection.CreateCommand()) 
            {
                command.CommandText = "INSERT INTO level1 (PositionX, PositionY, PositionZ, tileType) VALUES ('" + posX + "', '" + posY + "', '" + posZ + "', '" + tileType + "');";
                command.ExecuteNonQuery();
            }

            connection.Close();
            Debug.Log("Added tile to database");
        }
    }

    void WriteMapToDatabase()
    {
        List<Tile> tilesToWrite = new List<Tile>();
        for (int x = tilemap.cellBounds.min.x; x < tilemap.cellBounds.max.x; x++)
        {
            for (int y = tilemap.cellBounds.min.y; y < tilemap.cellBounds.max.y; y++)
            {
                for (int z = tilemap.cellBounds.min.z; z < tilemap.cellBounds.max.z; z++)
                {
                    if (tilemap.GetTile(new Vector3Int(x, y, z)) != null)
                    {
                        var tileToAdd = new Tile();
                        tileToAdd.setPosX(x);
                        tileToAdd.setPosY(y);
                        tileToAdd.setPosZ(z);
                        for (int i = 0; i < 18; i++)
                        {
                            if (tilemap.GetTile(new Vector3Int(x, y, z)).ToString() == "tileset v2_" + i + " (UnityEngine.Tilemaps.Tile)")
                            {
                                tileToAdd.setType(i);
                            }
                        }
                        tilesToWrite.Add(tileToAdd);
                    }
                }
            }

        }

        using (IDbConnection dbConnection = new SqliteConnection(dbPath))
        {
            dbConnection.Open();
            IDbCommand command = dbConnection.CreateCommand();

            command.CommandText = "INSERT INTO level1 (PositionX, PositionY, PositionZ, TileType) VALUES (@PositionX, @PositionY, @PositionZ, @TileType)";


            for (int i = 0; i < tilesToWrite.Count; i++)
            {
                IDbDataParameter posXParameter = command.CreateParameter();
                posXParameter.ParameterName = "@PositionX";
                posXParameter.Value = tilesToWrite[i].getPosX();
                command.Parameters.Add(posXParameter);

                IDbDataParameter posYParameter = command.CreateParameter();
                posYParameter.ParameterName = "@PositionY";
                posYParameter.Value = tilesToWrite[i].getPosY();
                command.Parameters.Add(posYParameter);

                IDbDataParameter posZParameter = command.CreateParameter();
                posZParameter.ParameterName = "@PositionZ";
                posZParameter.Value = tilesToWrite[i].getPosZ();
                command.Parameters.Add(posZParameter);

                IDbDataParameter typeParameter = command.CreateParameter();
                typeParameter.ParameterName = "@TileType";
                typeParameter.Value = tilesToWrite[i].getType();
                command.Parameters.Add(typeParameter);

                command.ExecuteNonQuery();
            }
            Debug.Log("SQLite: Data inserted successfully.");
        }
    }

    public void GetNewTilemap()
    {
        tiles.Clear();
        using (IDbConnection dbConnection = new SqliteConnection(dbPath))
        {
            dbConnection.Open();
            IDbCommand command = dbConnection.CreateCommand();
            command.CommandText = "SELECT * FROM level1";
            IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int posX = reader.GetInt32(0);
                int posY = reader.GetInt32(1);
                int posZ = reader.GetInt32(2);
                int tileType = reader.GetInt32(3);

                var tileObj = new Tile();
                tileObj.setPosX(posX);
                tileObj.setPosY(posY);
                tileObj.setPosZ(posZ);
                tileObj.setType(tileType);
                tiles.Add(tileObj);
            }
            reader.Close();
            Debug.Log("Tiles retrieved successfully");
            Debug.Log(tiles);
        }
    }
}



public class Tile
{
    private int posX;
    private int posY;
    private int posZ;
    private int type;

    public int getPosX()
    {
        return posX;
    }
    public int getPosY()
    {
        return posY;
    }
    public int getPosZ()
    {
        return posZ;
    }

    public int getType()
    {
        return type;
    }

    public void setPosX(int posX)
    {
        this.posX = posX;
    }
    public void setPosY(int posY)
    {
        this.posY = posY;
    }
    public void setPosZ(int posZ)
    {
        this.posZ = posZ;
    }
    public void setType(int tileType)
    {
        this.type = tileType;
    }
}