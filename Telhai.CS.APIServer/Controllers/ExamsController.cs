using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Telhai.CS.APIServer.Models;
using Telhai.CS.FinalProject;


namespace Telhai.CS.APIServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        public static readonly string serverConnectionString = "mongodb://localhost:27017";
        public static readonly string dbName = "server-client";
        public static readonly string eCollName = "exams";
        public static readonly string tCollName = "answers";

        //private ExamDbContext _context;

        //  private IExamRepository repo;
        public ExamsController()
        {
            //_context = context;
            //var count = (from a in _context.Exams.AsEnumerable() select a).Count();
            /*
            if (count == 0)
            {
                Exam ex1 = new Exam("DolandDuck");
                Exam ex2 = new Exam("MickyMouse");

                _context.Exams.Add(ex1);
                _context.Exams.Add(ex2);
                _context.SaveChanges();
            }
            count = (from a in _context.Exams.AsEnumerable() select a).Count();
            */
        }

        // GET: api/Exams1
        [HttpGet]
        public List<Exam> GetExams()
        {
            var client = new MongoClient(serverConnectionString);
            var database = client.GetDatabase(dbName);
            var collection = database.GetCollection<Exam>(eCollName);
            return collection.Find(x => true).ToList();
            //return await _context.Exams.Include("questions").ToListAsync();

        }

        // GET: api/Exams1/5
        [HttpGet("{id}")]
        public Exam GetExam(string id)
        {

            var client = new MongoClient(serverConnectionString);
            var database = client.GetDatabase(dbName);
            var collection = database.GetCollection<Exam>(eCollName);
            return collection.Find(x => x.examId == id).SingleOrDefault();

        }

        // PUT: api/Exams1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        public  void PutExam(string id, Exam exam)
        {

            var client = new MongoClient(serverConnectionString);
            var database = client.GetDatabase(dbName);
            var collection = database.GetCollection<Exam>(eCollName);

            Exam ex = collection.FindOneAndDelete(x => x.examId == id); // finding and deleteing the requested exam
            if (ex == null)
                return;
            

            ex.date =exam.date;
            ex.TeacherName = exam.TeacherName;
            ex.examName = exam.examName;
            ex.BeginTime = exam.BeginTime;
            ex.duration = exam.duration;
            ex.isRandom = exam.isRandom;
            ex.examId = exam.examId;
            ex.questions = exam.questions;

            collection.InsertOne(exam);
            /* var client = new MongoClient(serverConnectionString);
             var database = client.GetDatabase(dbName);
             var collection = database.GetCollection<Exam>(eCollName);
             var update = Builders<Exam>.Update.Set("examName", exam);*/

            // return collection.UpdateOne(x => x.examId == id, update);

        }

        // POST: api/Exams1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Exam>> PostExam(Exam exam)
        {
            var client = new MongoClient(serverConnectionString);
            var database = client.GetDatabase(dbName);
            var collection = database.GetCollection<Exam>(eCollName);
            
            var filter = Builders<Exam>.Filter.Eq("examId", exam.examId);
            Exam? search = collection.Find(filter).FirstOrDefault();
            if (search == null)
            {
                collection.InsertOne(exam);
                return Ok(collection);
            }
               
            else
            {
                return BadRequest();
            }
            
        }

        [HttpPost("answers/{id}")]
        public async Task<ActionResult<Exam>> PostExam(int studentID, Exam exam)
        {
            var client = new MongoClient(serverConnectionString);
            var database = client.GetDatabase(dbName);
            var collection = database.GetCollection<Exam>(tCollName);

            var filter = Builders<Exam>.Filter.Eq("examId", exam.examId);
            Exam? search = collection.Find(filter).FirstOrDefault();
            if (search == null)
            {
                collection.InsertOne(exam);
                return Ok(collection);
            }

            else
            {
                return BadRequest();
            }

        }
        // DELETE: api/Exams1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(string id)
        {
            var client = new MongoClient(serverConnectionString);
            var database = client.GetDatabase(dbName);
            var collection = database.GetCollection<Exam>(eCollName);

            collection.FindOneAndDelete(x => x.examId == id);

            return NoContent();
        }

        
    }
}
