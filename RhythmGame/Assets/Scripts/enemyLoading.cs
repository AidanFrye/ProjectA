using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.Tilemaps;

public class enemyLoading : MonoBehaviour
{
    private string dbPath;
    public List<Enemy> enemies = new List<Enemy>();
    public GameObject GameObject;

    // Start is called before the first frame update
    void Start()
    {
        dbPath = "URI=file:levelDatabase.db";
        CreateDB();
        GetNewEnemies();
        PlaceEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateDB()
    {
        using (var connection = new SqliteConnection(dbPath))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS level1enemies (PositionX INT, PositionY INT, PositionZ INT, enemyType INT);";
                command.ExecuteNonQuery();
            }

            connection.Close();
            Debug.Log("Database connected/created");
        }
    }

    /*public void ClearTable()
    {
        using (var connection = new SqliteConnection(dbPath))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM level1enemies";
                command.ExecuteNonQuery();
            }

            connection.Close();
            Debug.Log("Cleared enemies from database");
        }
    }*/

    public void AddEnemy(int posX, int posY, int posZ, int enemyType)
    {
        using (var connection = new SqliteConnection(dbPath))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO level1enemies (PositionX, PositionY, PositionZ, enemyType) VALUES ('" + posX + "', '" + posY + "', '" + posZ + "', '" + enemyType + "');";
                command.ExecuteNonQuery();
            }

            connection.Close();
            Debug.Log("Added tile to database");
        }
    }

    public void GetNewEnemies()
    {
        enemies.Clear();
        using (IDbConnection dbConnection = new SqliteConnection(dbPath))
        {
            dbConnection.Open();
            IDbCommand command = dbConnection.CreateCommand();
            command.CommandText = "SELECT * FROM level1enemies";
            IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int posX = reader.GetInt32(0);
                int posY = reader.GetInt32(1);
                int posZ = reader.GetInt32(2);
                int enemyType = reader.GetInt32(3);

                var enemyObj = new Enemy();
                enemyObj.setPosX(posX);
                enemyObj.setPosY(posY);
                enemyObj.setPosZ(posZ);
                enemyObj.setType(enemyType);
                enemies.Add(enemyObj);
            }
            reader.Close();
            Debug.Log("Tiles retrieved successfully");
        }
    }

    public void PlaceEnemies() 
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            Instantiate(GameObject, new Vector3(enemies[i].getPosX(), enemies[i].getPosY(), enemies[i].getPosZ()), Quaternion.identity);
        }
    }
}

public class Enemy
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