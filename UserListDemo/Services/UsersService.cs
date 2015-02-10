using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UserListDemo.Models;

namespace UserListDemo.Services
{
    public class UsersService
    {
        private const string InsertQuery =
            @"
                insert into Users (USR_LName, USR_FName, USR_Email, USR_Phone, USR_Age)
                values (@LastName, @FirstName, @Email, @Phone, @Age)
            ";

        private const string UpdateQuery =
            @"
                update Users
                    set USR_LName = @LastName,
                        USR_FName = @FirstName,
                        USR_Phone = @Phone,
                        USR_Age = @Age,
                        USR_Email = @Email
                where USR_ID = @ID
            ";

        private const string DeleteQuery =
            @"
                delete from Users
                where USR_ID = @ID
            ";

        public UserModel FillList(SqlDataReader rdr)
        {
            UserModel newUsr = new UserModel
            {
                ID = Convert.ToInt32(rdr[0]),
                LName = rdr[1].ToString(),
                FName = rdr[2].ToString(),
                Email = rdr[3].ToString(),
                Phone = rdr[4].ToString(),
                Age = Convert.ToInt32(rdr[5])
            };

            return newUsr;
        }

        public List<UserModel> GetUsers()
        {
            var list = new List<UserModel>();
            using (var conn = GetConn())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from Users", conn);

                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(FillList(rdr));
                    }
                }
            }
            return list;
        }

        private void SingleDbData(UserModel usr, string updateString)
        {
            using (var conn = GetConn())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(updateString, conn);
                cmd.Parameters.Add(new SqlParameter("ID", usr.ID));

                cmd.ExecuteNonQuery();
            }
        }

        private void MultiDbData(UserModel usr, string updateString)
        {
            using (var conn = GetConn())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(updateString, conn);
                cmd.Parameters.Add(new SqlParameter("ID", usr.ID));
                cmd.Parameters.Add(new SqlParameter("LastName", usr.LName));
                cmd.Parameters.Add(new SqlParameter("FirstName", usr.FName));
                cmd.Parameters.Add(new SqlParameter("Email", usr.Email));
                cmd.Parameters.Add(new SqlParameter("Phone", usr.Phone));
                cmd.Parameters.Add(new SqlParameter("Age", usr.Age));

                cmd.ExecuteNonQuery();
            }
        }

        public UserModel GetUserById(int usrId)
        {
            UserModel newUsr = new UserModel();

            using (var conn = GetConn())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from Users where usr_id = @usrID", conn);
                cmd.Parameters.Add(new SqlParameter("usrID", usrId));

                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        newUsr = FillList(rdr);
                    }
                }
            }
            return newUsr;
        }

        private SqlConnection GetConn()
        {
            SqlConnection conn = new SqlConnection(
        "Data Source=SKN12010;Initial Catalog=Web_test;Integrated Security=SSPI");

            return conn;
        }

        public void UpdateUser(UserModel usr)
        {
            MultiDbData(usr, UpdateQuery);
        }

        public void DeleteUser(UserModel usr)
        {
            SingleDbData(usr, DeleteQuery);
        }

        public void CreateUser(UserModel usr)
        {
            MultiDbData(usr, InsertQuery);
        }

    }
}