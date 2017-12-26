using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using StudentManagementV1.Models;
using System.Collections;

namespace StudentManagementV1
{
    public class StudentPersistance
    {
        private MySql.Data.MySqlClient.MySqlConnection conn;

        public StudentPersistance()
        {
            string myConnectionString;
            myConnectionString = "server=localhost;port=3306;user=root;password=123456;database=sinhviendb";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

            }
        }

        public ArrayList getStudent()
        {
            ArrayList studentArray = new ArrayList();
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

            String sqlString = "SELECT * FROM sinhvien INNER JOIN lop ON lop.idlop=sinhvien.idlop;";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

            mySQLReader = cmd.ExecuteReader();
            while (mySQLReader.Read())
            {
                sinhvien sv = new sinhvien();
                sv.MSSV = mySQLReader.GetInt32(0);
                sv.hoten = mySQLReader.GetString(1);
                sv.ngaysinh = mySQLReader.GetDateTime(2);
                sv.diemtongket = mySQLReader.GetFloat(4);
                sv.lop = mySQLReader.GetString(6);
                studentArray.Add(sv);
            }

            return studentArray;
        }

        public sinhvien getStudent (long ID)
        {
            sinhvien sv = new sinhvien();
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

            String sqlString = "SELECT * FROM sinhvien INNER JOIN lop ON lop.idlop=sinhvien.idlop WHERE MSSV=" + ID.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

            mySQLReader = cmd.ExecuteReader();
            if (mySQLReader.Read())
            {
                sv.MSSV = mySQLReader.GetInt32(0);
                sv.hoten = mySQLReader.GetString(1);
                sv.ngaysinh = mySQLReader.GetDateTime(2);
                sv.diemtongket = mySQLReader.GetFloat(4);
                sv.lop = mySQLReader.GetString(6);
                System.Diagnostics.Debug.WriteLine("lop = " + sv.lop);
                return sv;
            }
            else
            {
                return null;
            }
        }

        public long saveStudent(sinhvien studentToSave)
        {
            //System.Diagnostics.Debug.WriteLine("payrate = " + studentToSave.PayRate.ToString());

            String sqlString = "INSERT INTO sinhvien (hoten, ngaysinh, idlop, diemtongket) VALUES ('"
                                + studentToSave.hoten + "','" + studentToSave.ngaysinh.ToString("yyyy-MM-dd HH:mm:ss")
                                + "',(SELECT idlop FROM lop WHERE lop.tenlop='" + studentToSave.lop + "'),'"
                                + studentToSave.diemtongket.ToString().Replace(",", ".") + "')";
            System.Diagnostics.Debug.WriteLine(sqlString);
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            cmd.ExecuteNonQuery();
            long id = cmd.LastInsertedId;
            return id;
        }

        public bool updateStudent(long ID, sinhvien studentToSave)
        {
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

            String sqlString = "SELECT * FROM sinhvien WHERE MSSV=" + ID.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

            mySQLReader = cmd.ExecuteReader();
            if (mySQLReader.Read())
            {
                mySQLReader.Close();

                sqlString = "UPDATE sinhvien SET hoten='" + studentToSave.hoten + "', ngaysinh='"
                                + studentToSave.ngaysinh.ToString("yyyy-MM-dd HH:mm:ss")
                                + "', idlop=(SELECT idlop FROM lop WHERE lop.tenlop='" + studentToSave.lop
                                + "'), diemtongket='" + studentToSave.diemtongket.ToString().Replace(",", ".")
                                + "' WHERE MSSV=" + ID.ToString();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                cmd.ExecuteNonQuery();

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool deleteStudent(long ID)
        {
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

            String sqlString = "SELECT * FROM sinhvien WHERE MSSV=" + ID.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

            mySQLReader = cmd.ExecuteReader();
            if (mySQLReader.Read())
            {
                mySQLReader.Close();

                sqlString = "DELETE FROM sinhvien WHERE MSSV=" + ID.ToString();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                cmd.ExecuteNonQuery();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}