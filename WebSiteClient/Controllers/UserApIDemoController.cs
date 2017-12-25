using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Domain.Models;
using Newtonsoft.Json;

namespace WebSiteClient.Controllers
{
    public class UserApIDemoController : Controller
    {
        private readonly Uri _uri = new Uri(string.Format(ConstantFields.ApiBaseUrl, string.Empty));
        private readonly HttpClient _httpClient;

        public UserApIDemoController()
        {
            _httpClient = new HttpClient { MaxResponseContentBufferSize = 256000, BaseAddress = _uri };
        }
        // GET: UserApIDemo
        public async Task<ActionResult> Index()
        {
            var allUsers=new List<User>();
            try
            {
                var response = await _httpClient.GetAsync("user/getalluser");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    allUsers = JsonConvert.DeserializeObject<List<User>>(content);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
                throw;
            }
            return View(allUsers);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            try
            {
                var userData = JsonConvert.SerializeObject(user);
                var content = new StringContent(userData, Encoding.UTF8, "application/json");
                if (userData != null)
                {
                    var response = await _httpClient.PostAsync("user/adduser", content);
                    if (response.IsSuccessStatusCode)
                    {
                        //allUsers = JsonConvert.DeserializeObject<List<User>>(content);
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
                throw;
            }
            return View(user);
        }
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var user=new User();
            try
            {
               
                var response = await _httpClient.GetAsync($"user/getuserdetails?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(content);
                    return View(user);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
                throw;
            }
            return View(user);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var user=new User();
            try
            {

                var response = await _httpClient.GetAsync($"user/getuserdetails?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(content);
                    return View(user);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
                throw;
            }
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(User user)
        {
            try
            {
                var userData = JsonConvert.SerializeObject(user);
                var content = new StringContent(userData, Encoding.UTF8, "application/json");
                if (userData != null)
                {
                    var response = await _httpClient.PutAsync("user/adduser", content);
                    if (response.IsSuccessStatusCode)
                    {
                        //allUsers = JsonConvert.DeserializeObject<List<User>>(content);
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
                throw;
            }
            return View(user);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"user/deleteuser?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    //user = JsonConvert.DeserializeObject<User>(content);
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
                throw;
            }
            return RedirectToAction("Index");
        }
    }
}