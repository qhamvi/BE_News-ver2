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
    public class ReasonController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ReasonController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string queri = @"
                            select ReasonId,Title, ReasonName from
                            dbo.Reason
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
        public JsonResult Post(Reason rea)
        {
            string queri = @"insert into dbo.Reason
                            (Title,ReasonName)
                            values (@Title,@ReasonName)"; 
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("webapiCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(queri, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Title", rea.Title);
                    myCommand.Parameters.AddWithValue("@ReasonName", rea.ReasonName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Reason rea)
        {
            string queri = @"update dbo.Reason set Title = @Title,ReasonName = @ReasonName where ReasonId = @ReasonId";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("webapiCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(queri, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ReasonId", rea.ReasonId);
                    myCommand.Parameters.AddWithValue("@Title", rea.Title);
                    myCommand.Parameters.AddWithValue("@ReasonName", rea.ReasonName);
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
                           delete from dbo.Reason
                            where ReasonId=@ReasonId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("webapiCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ReasonId", id);

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
