using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagementV1.Models
{
    public class sinhvien
    {
        public long MSSV { get; set; }

        public String hoten { get; set; }

        public DateTime ngaysinh { get; set; }

        public String lop { get; set; }

        public Double diemtongket { get; set; }
    }
}