using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using RpgMvc.Models;
using RpgMvc.Models.Enuns; 
using RpgMvc.Controllers;

namespace RpgMvc.Controllers
{
    public class ArmasController : Controller
    {
        public string uriBase = "http://vitinho011.somee.com/RpgApi/Armas/";

        [HttpGet]
    public async Task<ActionResult> IndexAsync()
    {
        try
        {
            string uriComplementar = "GetAll";
            HttpClient httpClient = new HttpClient();
            string token = HttpContext.Session.GetString("SessionTokenUsuario");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await httpClient.GetAsync(uriBase + uriComplementar);
            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                List<ArmaViewModel> listaArmas = await Task.Run(() =>
                    JsonConvert.DeserializeObject<List<ArmaViewModel>>(serialized));

                return View(listaArmas);
            }
            else
            throw new System.Exception(serialized); 
        }
        catch (System.Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(ArmaViewModel a)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            string token = HttpContext.Session.GetString("SessionTokenUsuario");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonConvert.SerializeObject(a));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uriBase, content);
            string serialized = await response.Content.ReadAsStringAsync();

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                TempData["Mensagem"] = string.Format("Personagem {0}, Id {1} salvo com sucesso!", a.Nome, serialized);
                return RedirectToAction("Index");
            }
            else
                throw new System.Exception(serialized);
        }
        catch (System.Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
            return RedirectToAction("Create");
        }
    }
    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    [HttpGet]
    public async Task<ActionResult> DetailsAsync(int? id)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            string token = HttpContext.Session.GetString("SessionTokenUsuario");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString()); 
            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ArmaViewModel p = await Task.Run(() =>
                JsonConvert.DeserializeObject<ArmaViewModel>(serialized));
                return View(p);
            }
            else
                throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
    }
    [HttpGet]
    public async Task<ActionResult> EditAsync(int? id)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            string token = HttpContext.Session.GetString("SessionTokenUsuario");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString());

            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ArmaViewModel p = await Task.Run(() =>
                JsonConvert.DeserializeObject<ArmaViewModel>(serialized));
                return View(p);
            }
            else
                throw new System.Exception(serialized);
        }
        catch (System.Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
            return  RedirectToAction("Index");
        }
    }
    [HttpPost]
    public async Task<ActionResult> EditAsync(ArmaViewModel p)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            string token = HttpContext.Session.GetString("SessionTokenUsuario");

            httpClient.DefaultRequestHeaders.Authorization =  new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonConvert.SerializeObject(p));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await httpClient.PutAsync(uriBase, content);
            string serialized = await response.Content.ReadAsStringAsync();

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                TempData["Mensagem"] = 
                string.Format("Arma atualizada com sucesso!", p.Nome);

                return RedirectToAction("Index");
            }
            else
            throw new System.Exception(serialized);
        }
        catch (System.Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
            return RedirectToAction("Index");
        }
    }
    [HttpGet]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            string token = HttpContext.Session.GetString("SessionTokenUsuario");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await httpClient.DeleteAsync(uriBase + id.ToString());
            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                TempData["MensagemErro"]= string.Format("Arma Id {0} removido com sucesso!", id);
                return RedirectToAction("Index");
            }
            else
                throw new System.Exception(serialized);
        }
        catch (System.Exception ex)
        {
            TempData["MensagemErro"]= ex.Message;
                return RedirectToAction("Index");
        }
    }
    }
}