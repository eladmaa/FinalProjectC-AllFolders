using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Telhai.CS.APIServer.Models;
using Telhai.CS.FinalProject;

namespace Telhai.CS.APIServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private  ExamDbContext _context;

      //  private IExamRepository repo;
        public ExamsController(ExamDbContext context)
        {
            _context = context;
            var count = (from a in _context.Exams.AsEnumerable() select a).Count();
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
        public async Task<ActionResult<IEnumerable<Exam>>> GetExams()
        {
            return await _context.Exams.ToListAsync();
        }

        // GET: api/Exams1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Exam>> GetExam(string id)
        {
            var exam = await _context.Exams.FindAsync(id);

            if (exam == null)
            {
                return NotFound();
            }

            return exam;
        }

        // PUT: api/Exams1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExam(string id, Exam exam)
        {
            if (id != exam.id)
            {
                return BadRequest();
            }

            _context.Entry(exam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Exams1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Exam>> PostExam(Exam exam)
        {
            string id = exam.id;
            if (exam.id == "")
            {
                exam.id = Guid.NewGuid().ToString();
                _context.Exams.Add(exam);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ExamExists(exam.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetExam", new { id = exam.id }, exam);
        }

        // DELETE: api/Exams1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(string id)
        {
            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExamExists(string id)
        {
            return _context.Exams.Any(e => e.id == id);
        }
    }
}
