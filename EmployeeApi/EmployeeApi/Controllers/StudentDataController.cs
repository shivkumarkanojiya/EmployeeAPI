using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using EmployeeApi.Models;

namespace EmployeeApi.Controllers
{
    public class StudentDataController : ApiController
    {
        string strcon = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        [HttpGet]
        public IHttpActionResult GetStudentData()
        {

            

            SqlConnection con = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand("SpselStudentData",con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            List<Student_Data> stdlist = new List<Student_Data>();
            if (dt.Rows.Count > 0)
            
            
            {            
                for (int i = 0; i <dt.Rows.Count; i++)
                
                {

                    Student_Data std = new Student_Data();
                    std.rollno = Convert.ToInt32(dt.Rows[i]["rollno"]);
                    std.sname = dt.Rows[i]["sname"].ToString();
                    std.fathername = dt.Rows[i]["fathername"].ToString();
                    std.mothername = dt.Rows[i]["mothername"].ToString();
                    stdlist.Add(std);
                }
                
            }
            return Ok(stdlist);
        }
        //
        [HttpGet]
        public IHttpActionResult GetStudentData(int id)
        {
            Student_Data obj = new Student_Data();
            SqlConnection con = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand("SpSelstudent_data", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@rollno", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
          
            while(dr.Read())
            {
               
                obj.rollno = Convert.ToInt32(dr["rollno"]);
                obj.sname = dr["sname"].ToString();
                obj.fathername = dr["fathername"].ToString();
                obj.mothername = dr["mothername"].ToString();
            }
            con.Close();
            return Ok(obj);
        }
    }
}
