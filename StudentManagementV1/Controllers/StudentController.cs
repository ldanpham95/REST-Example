using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StudentManagementV1.Models;
using System.Collections;

namespace StudentManagementV1.Controllers
{
    public class StudentController : ApiController
    {
        // GET: api/Student
        public ArrayList Get()
        {
            StudentPersistance sp = new StudentPersistance();
            return sp.getStudent();
        }

        // GET: api/Student/5
        public sinhvien Get(long id)
        {
            StudentPersistance sp = new StudentPersistance();
            sinhvien sv = sp.getStudent(id);
            return sv;
        }

        // POST: api/Student
        public HttpResponseMessage Post([FromBody]sinhvien value)
        {
            StudentPersistance sp = new StudentPersistance();
            long id;
            id = sp.saveStudent(value);
            value.MSSV = id;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, String.Format("student/{0}", id));
            return response;
        }

        // PUT: api/Student/5
        public HttpResponseMessage Put(long id, [FromBody]sinhvien value)
        {
            StudentPersistance sp = new StudentPersistance();
            bool recordExisted = false;

            recordExisted = sp.updateStudent(id, value);

            HttpResponseMessage response;

            if (recordExisted)
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }

        // DELETE: api/Student/5
        public HttpResponseMessage Delete(long id)
        {
            StudentPersistance sp = new StudentPersistance();
            bool recordExisted = false;

            recordExisted = sp.deleteStudent(id);

            HttpResponseMessage response;

            if (recordExisted)
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }
    }
}
