﻿using Microsoft.AspNetCore.Http;
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
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public UserController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }


        [HttpGet]
        public JsonResult Get()
        {
            // string query = @"
            //                 select UserId,UserName,Account,Password,Phone,Email,PhotoFileName,Position
            //                 from dbo.User
            //                 ";
            string queri = @"
                            select [UserId],[UserName],[Account],[Password],[Phone],[Email],[PhotoFileName],[Position]
                            from [dbo].[User]
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

        [HttpGet("login/{account}:{password}")]
        public JsonResult Get(String account, String password)
        {
            string queri = @"
                            select *from [dbo].[User]
                            where [Account] = @Account and [Password] = @Password
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("webapiCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(queri, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Account", account);
                    myCommand.Parameters.AddWithValue("@Password", password);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        // [HttpPost("login/{account}:{password}")]
        // public JsonResult Post(String account, String password)
        // {
        //     string queri = @"
        //                     select *from [dbo].[User]
        //                     where [Account] = @Account and [Password] = @Password
        //                     ";

        //     DataTable table = new DataTable();
        //     string sqlDataSource = _configuration.GetConnectionString("webapiCon");
        //     SqlDataReader myReader;
        //     using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        //     {
        //         myCon.Open();
        //         using (SqlCommand myCommand = new SqlCommand(queri, myCon))
        //         {
        //             myCommand.Parameters.AddWithValue("@Account", account);
        //             myCommand.Parameters.AddWithValue("@Password", password);
        //             myReader = myCommand.ExecuteReader();
        //             table.Load(myReader);
        //             myReader.Close();
        //             myCon.Close();
        //         }
        //     }

        //     return new JsonResult(table);
        // }
        
        [HttpPost]
        public JsonResult Post(User user)
        {
            // string query = @"
            //                insert into dbo.User
            //                (UserName,Account,Password,Phone,Email,PhotoFileName,Position)
            //         values (@UserName,@Account,@Password,@Phone,@Email,@PhotoFileName,@Position)";
            string queri = @"
                           insert into [dbo].[User]
                           ([UserName],[Account],[Password],[Phone],[Email],[PhotoFileName],[Position])
                    values (@UserName,@Account,@Password,@Phone,@Email,@PhotoFileName,@Position)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("webapiCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(queri, myCon))
                {
                    myCommand.Parameters.AddWithValue("@UserName", user.UserName);
                    myCommand.Parameters.AddWithValue("@Account", user.Account);
                    myCommand.Parameters.AddWithValue("@Password", user.Password);
                    myCommand.Parameters.AddWithValue("@Phone", user.Phone);
                    myCommand.Parameters.AddWithValue("@Email", user.Email);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", user.PhotoFileName);
                    myCommand.Parameters.AddWithValue("@Position", user.Position);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(User user)
        {
            string query = @"
                           update [dbo].[User]
                           set [UserName] = @UserName,
                            [Account] = @Account,
                            [Password] = @Password,
                            [Phone] = @Phone,
                            [Email]= @Email,
                            [PhotoFileName]= @PhotoFileName,
                            [Position]= @Position
                            where [UserId] = @UserId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("webapiCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@UserId", user.UserId);
                    myCommand.Parameters.AddWithValue("@UserName", user.UserName);
                    myCommand.Parameters.AddWithValue("@Account", user.Account);
                    myCommand.Parameters.AddWithValue("@Password", user.Password);
                    myCommand.Parameters.AddWithValue("@Phone", user.Phone);
                    myCommand.Parameters.AddWithValue("@Email", user.Email);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", user.PhotoFileName);
                    myCommand.Parameters.AddWithValue("@Position", user.Position);
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
                           delete from [dbo].[User]
                            where [UserId]=@UserId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("webapiCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@UserId", id);

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
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

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