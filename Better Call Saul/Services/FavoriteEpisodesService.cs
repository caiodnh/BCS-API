using Better_Call_Saul.Models;
using System.Data.SqlClient;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Better_Call_Saul.Services;

public static class FavoriteEpisodesService
{
    static readonly string constr = @"Data Source=localhost;Initial Catalog=BCS;User ID=seno;Password=123";
    static FavoriteEpisodesService()
    {
    }

    public static List<FavoriteEpisode> GetAll()
    {
        SqlConnection conn = new(constr);
        conn.Open();
        string sql = "Select * from FavoriteEpisodes";
        SqlCommand cmd = new(sql, conn);
        SqlDataReader dataReader = cmd.ExecuteReader();

        List<FavoriteEpisode> Episodes = new();

        while (dataReader.Read())
        {
            int id = (int)dataReader.GetValue(0);
            int season = (int)dataReader.GetValue(1);
            int ep = (int)dataReader.GetValue(2);
            string name = (string)dataReader.GetValue(3);
            FavoriteEpisode c = new()
            {
                Id = id,
                Season = season,
                NumberWithInSeason = ep,
                Title = name
            };

            Episodes.Add(c);
        }

        conn.Close();
        return Episodes;
    }

    public static FavoriteEpisode? Get(int id)
    {
        SqlConnection conn = new(constr);
        conn.Open();
        string sql = $"Select * from FavoriteEpisodes where ID = {id}";
        Console.WriteLine(sql);
        SqlCommand cmd = new(sql, conn);
        SqlDataReader dataReader = cmd.ExecuteReader();

        if (dataReader.Read())
        {
            int season = (int)dataReader.GetValue(1);
            int ep = (int)dataReader.GetValue(2);
            string name = (string)dataReader.GetValue(3);
            FavoriteEpisode c = new()
            {
                Id = id,
                Season = season,
                NumberWithInSeason = ep,
                Title = name
            };

            conn.Close();
            return c;
        }
        else
            return null;
    }

    public static void Add(FavoriteEpisode episode)
    {
        SqlConnection conn = new(constr);
        conn.Open();
        string sql = $"INSERT INTO FavoriteEpisodes VALUES ({episode.Season},{episode.NumberWithInSeason},{episode.Title})";
        SqlCommand cmd = new(sql, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    public static void Delete(int id)
    {
        var Episode = Get(id);
        if (Episode is null)
            return;

        SqlConnection conn = new(constr);
        conn.Open();
        string sql = $"delete from FavoriteEpisodes where id = {id}";
        SqlCommand cmd = new(sql, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    public static void Update(FavoriteEpisode episode)
    {
        var c = Get(episode.Id);
        if (c is null)
            return;

        SqlConnection conn = new(constr);
        conn.Open();
        string sql = $"update FavoriteEpisodes set Season = {episode.Season}, NumberWithInSeason = {episode.NumberWithInSeason}, Title = {episode.Title} where id = {episode.Id}";
        SqlCommand cmd = new(sql, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }
}