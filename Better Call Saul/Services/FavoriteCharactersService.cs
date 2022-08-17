using Better_Call_Saul.Models;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Better_Call_Saul.Services;

public static class FavoriteCharactersService
{
    static readonly string constr = @"Data Source=localhost;Initial Catalog=BCS;User ID=seno;Password=123";
    static FavoriteCharactersService()
    {
    }

    public static List<FavoriteCharacter> GetAll()
    {
        SqlConnection conn = new(constr);
        conn.Open();
        string sql = "Select * from FavoriteCharacters";
        SqlCommand cmd = new(sql, conn);
        SqlDataReader dataReader = cmd.ExecuteReader();

        List<FavoriteCharacter> characters = new();

        while (dataReader.Read())
        {
            int id = (int)dataReader.GetValue(0);
            string fname = (string)dataReader.GetValue(1);
            string sname = (string)dataReader.GetValue(2);
            FavoriteCharacter c = new()
            {
                Id = id,
                FirstName = fname,
                LastName = sname
            };

            characters.Add(c);
        }

        conn.Close();
        return characters;
    }

    public static FavoriteCharacter? Get(int id)
    {
        SqlConnection conn = new(constr);
        conn.Open();
        string sql = $"Select * from FavoriteCharacters where ID = {id}";
        Console.WriteLine(sql);
        SqlCommand cmd = new(sql, conn);
        SqlDataReader dataReader = cmd.ExecuteReader();

        if (dataReader.Read())
        {
            string fname = (string)dataReader.GetValue(1);
            string sname = (string)dataReader.GetValue(2);
            FavoriteCharacter c = new FavoriteCharacter
            {
                Id = id,
                FirstName = fname,
                LastName = sname
            };

            conn.Close();
            return c;
        }
        else
            return null;
    }

    public static void Add(FavoriteCharacter character)
    {
        SqlConnection conn = new(constr);
        conn.Open();
        string sql = $"INSERT INTO FavoriteCharacters VALUES ({character.FirstName},{character.LastName})";
        SqlCommand cmd = new(sql, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    public static void Delete(int id)
    {
        var character = Get(id);
        if (character is null)
            return;

        SqlConnection conn = new(constr);
        conn.Open();
        string sql = $"delete from FavoriteCharacters where id = {id}";
        SqlCommand cmd = new(sql, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    public static void Update(FavoriteCharacter character)
    {
        var c = Get(character.Id);
        if (c is null)
            return;

        SqlConnection conn = new(constr);
        conn.Open();
        string sql = $"update FavoriteCharacters set FirstName = {character.FirstName}, LastName = {character.LastName} where id = {character.Id}";
        SqlCommand cmd = new(sql, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }
}