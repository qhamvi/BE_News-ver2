using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PositionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string queri = @"
                            select PositionId, PositionName from
                            dbo.Position
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("webapiCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(queri, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Position pos)
        {

            string queri = @"insert into dbo.Position
                            values (@PositionName)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("webapiCon");
            SqlDataReader myReader;
            using(SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(queri,myCon))
                {
                    myCommand.Parameters.AddWithValue("@PositionName", pos.PositionName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Position pos)
        {
            string queri = @"update dbo.Position 
                           set PositionName = @PositionName 
                           where PositionId = @PositionId";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("webapiCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(queri,myCon))
                {
                    myCommand.Parameters.AddWithValue("@PositionId", pos.PositionId);
                    myCommand.Parameters.AddWithValue("@PositionName", pos.PositionName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.Position
                            where PositionId=@PositionId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("webapiCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@PositionId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
