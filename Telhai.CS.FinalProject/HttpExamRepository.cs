using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telhai.CS.FinalProject;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Windows;

namespace Telhai.CS.APIServer.Models
{
    public class HttpExamRepository
    {
        public List<Exam> examList;
        HttpClient clientApi;

        static private HttpExamRepository _instance = null;

        private HttpExamRepository()
        {
            examList = new List<Exam>();
            clientApi = new HttpClient();
            clientApi.BaseAddress = new Uri("https://localhost:7109");
        }
        public static HttpExamRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HttpExamRepository();
                }
                return _instance;
            }
        }
        //****************************************************************************************************
        public async Task<List<Exam>> GetAllExamsAsync()
        {
            HttpResponseMessage response = await clientApi.GetAsync("api/Exams");
            if (response != null)
            {
                response.EnsureSuccessStatusCode();
                string? dataString = await response.Content.ReadAsStringAsync();
                var exams = JsonSerializer.Deserialize<List<Exam>>(dataString);
                if (exams != null)
                {
                    return exams;
                }
            }
            return new List<Exam>();
        }
        //****************************************************************************************************

        public async Task<HttpResponseMessage> AddExamAsync(Exam exam)
        {

            //var options = new JsonSerializerOptions { WriteIndented = true };
            if(examList.FindIndex(e => e.examId == exam.examId) < 0)
            {
               // MessageBox.Show("added to repo");
                 examList.Add(exam);
            }
            /*string jsonExamString = JsonSerializer.Serialize<Exam>(exam);

            var content = new StringContent(jsonExamString, Encoding.UTF8, "application/json");
            var response = await clientApi.PostAsync("api/exams", content);*/


            return null;
        }

        public async Task<HttpResponseMessage> UpdateExamAsync(string id, Exam exam)
        {
            string jsonExamString = JsonSerializer.Serialize<Exam>(exam);
            var content = new StringContent(jsonExamString, Encoding.UTF8, "application/json");
            var response = await clientApi.PutAsync("api/exams" + id, content);

            return response;
        }
        //****************************************************************************************************

        public async Task<HttpResponseMessage> RemoveExam(Exam exam)
        {
            string jsonExamString = JsonSerializer.Serialize<Exam>(exam);
            var content = new StringContent(jsonExamString, Encoding.UTF8, "application/json");

            var response = await clientApi.DeleteAsync("api/exams/" + exam.examId);
            if (response != null)
            {
                response.EnsureSuccessStatusCode();
                //..
            }
            return response;
        }

    }
}
