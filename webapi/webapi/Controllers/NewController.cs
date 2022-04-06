using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using webapi.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public NewController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select * from [dbo].[New]
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("webapiCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader(); 
                    table.Load(myReader); 
                    myReader.Close(); 
                    myCon.Close(); 
                }
            }

            return new JsonResult(table);
        }
        // 1 tin 
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            string query = @"
                           select *from New where NewId = @NewId;
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("webapiCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@NewId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
        //
        [HttpGet("topic={top}")]
        public JsonResult Get(String top)
        {
            string query = @"
                           select *from New where Topic = @Topic;
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("webapiCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Topic", top);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpGet("false")]
        public JsonResult GetFalse()
        {
           
            string query = @"
                            select * from [dbo].[New]
                            where [NewStatus] = 'false'
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("webapiCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpGet("true")]
        public JsonResult GetTrue()
        {
            string query = @"
                            select *from [dbo].[New]
                            where [NewStatus] = 'true'
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("webapiCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
// Tao new cho Author -- Admin
        [HttpPost]
        public JsonResult Post(New neww)
        {
            string query = @"
                           insert into [dbo].[New]
                           ([NewTitle],[NewSummary],[NewContent],[NewStatus],[User],[Topic],[createDate],[publishDate],[ImageFileName],[Reason])
                    values (@NewTitle,@NewSummary,@NewContent,'false',@User,@Topic,getdate(),null,@ImageFileName,null)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("webapiCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@NewTitle", neww.NewTitle);
                    myCommand.Parameters.AddWithValue("@NewSummary", neww.NewSummary);
                    myCommand.Parameters.AddWithValue("@NewContent", neww.NewContent);
                    // myCommand.Parameters.AddWithValue("@NewStatus", "false");
                    myCommand.Parameters.AddWithValue("@User", neww.User);
                    myCommand.Parameters.AddWithValue("@Topic", neww.Topic);
                    // myCommand.Parameters.AddWithValue("@createDate", neww.createDate);
                    // myCommand.Parameters.AddWithValue("@publishDate", null);
                    myCommand.Parameters.AddWithValue("@ImageFileName", neww.ImageFileName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }
         [HttpPut("refuseNew")]
        public JsonResult PutRefuse(New neww)
        {
            string query = @"
                           update [dbo].[New]
                           set 
                            [Reason] = @Reason
                            where [NewId]= @NewId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("webapiCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@NewId", neww.NewId);
                    myCommand.Parameters.AddWithValue("@Reason", neww.Reason);
                    // myCommand.Parameters.AddWithValue("@NewTitle", neww.NewTitle);
                    // myCommand.Parameters.AddWithValue("@NewSummary", neww.NewSummary);
                    // myCommand.Parameters.AddWithValue("@NewContent", neww.NewContent);
                    // // myCommand.Parameters.AddWithValue("@NewStatus", neww.NewStatus);
                    // myCommand.Parameters.AddWithValue("@User", neww.User);
                    // myCommand.Parameters.AddWithValue("@Topic", neww.Topic);
                    // myCommand.Parameters.AddWithValue("@createDate", neww.createDate);
                    // // myCommand.Parameters.AddWithValue("@publishDate", neww.publishDate);
                    // myCommand.Parameters.AddWithValue("@ImageFileName", neww.ImageFileName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Refuse successfully");
        }

        [HttpPut("acceptNew")]
        public JsonResult PutTrue(New neww)
        {
            string query = @"
                           update [dbo].[New]
                           set 
                            [NewStatus] = 'true',
                            [publishDate]= getdate()
                            where [NewId]= @NewId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("webapiCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@NewId", neww.NewId);
                    // myCommand.Parameters.AddWithValue("@NewTitle", neww.NewTitle);
                    // myCommand.Parameters.AddWithValue("@NewSummary", neww.NewSummary);
                    // myCommand.Parameters.AddWithValue("@NewContent", neww.NewContent);
                    // // myCommand.Parameters.AddWithValue("@NewStatus", neww.NewStatus);
                    // myCommand.Parameters.AddWithValue("@User", neww.User);
                    // myCommand.Parameters.AddWithValue("@Topic", neww.Topic);
                    // myCommand.Parameters.AddWithValue("@createDate", neww.createDate);
                    // // myCommand.Parameters.AddWithValue("@publishDate", neww.publishDate);
                    // myCommand.Parameters.AddWithValue("@ImageFileName", neww.ImageFileName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Accept successfully");
        }

        [HttpPut]
        public JsonResult Put(New neww)
        {
            string query = @"
                           update [dbo].[New]
                           set [NewTitle] = @NewTitle,
                            [NewSummary] = @NewSummary,
                            [NewContent] = @NewContent,
                            [NewStatus] = 'false',
                            [User]= @User,
                            [Topic]= @Topic,
                            [createDate]= @createDate,
                            [publishDate]= null,
                            [ImageFileName]= @ImageFileName 
                            where [NewId]= @NewId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("webapiCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@NewId", neww.NewId);
                    myCommand.Parameters.AddWithValue("@NewTitle", neww.NewTitle);
                    myCommand.Parameters.AddWithValue("@NewSummary", neww.NewSummary);
                    myCommand.Parameters.AddWithValue("@NewContent", neww.NewContent);
                    // myCommand.Parameters.AddWithValue("@NewStatus", neww.NewStatus);
                    myCommand.Parameters.AddWithValue("@User", neww.User);
                    myCommand.Parameters.AddWithValue("@Topic", neww.Topic);
                    myCommand.Parameters.AddWithValue("@createDate", neww.createDate);
                    // myCommand.Parameters.AddWithValue("@publishDate", neww.publishDate);
                    myCommand.Parameters.AddWithValue("@ImageFileName", neww.ImageFileName);
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
                           delete from dbo.New
                            where NewId=@NewId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("webapiCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@NewId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }


        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Images/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("cp.png");
            }
        }

    }
}