using GymWeb.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Net.Http.Headers;
using System.Text;

namespace GymWeb.Models
{

    public class UsuarioModel : IUsuarioModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string _urlApi;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly IUsuarioModel _usuarioModel;






        public UsuarioModel(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {

            _httpClient = httpClient;
            _configuration = configuration;
            _urlApi = _configuration.GetSection("Llaves:urlApi").Value;
            _HttpContextAccessor = httpContextAccessor;



        }

        public UsuarioEntRespuesta? InicioSesion(UsuarioEnt entidad)
        {
            try
            {
                var model = new UsuarioEnt();
                string url = "api/Usuario/InicioSesion";
                JsonContent jsonObject = JsonContent.Create(entidad);
                var response = _httpClient.PostAsync(_urlApi + url, jsonObject).Result;
                return response.Content.ReadFromJsonAsync<UsuarioEntRespuesta>().Result;
            }
            catch (Exception)
            {
                //Registrar error generado, siempre usar try catch para todo
                return new UsuarioEntRespuesta();
            }
        }

        public int RecuperarCuenta(UsuarioEnt entidad)
        {
            throw new NotImplementedException();
        }

        public int RegistrarUsuario(UsuarioEnt entidad)
        {
            try
            {
                string url = _urlApi + "api/Usuario/RegistrarUsuario";
                JsonContent obj = JsonContent.Create(entidad);
                var resp = _httpClient.PostAsync(url, obj).Result;

                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                return 0;
            }
            catch (Exception)
            {
                //Registrar error generado, siempre usar try catch para todo
                return 0;
            }
        }

        public int AgregarFotoPerfil(byte[] img, int id)
        {
            try
            {

                var collection = new List<KeyValuePair<string, string>>();
                collection.Add(new("foto", Convert.ToBase64String(img)));

                string url = _urlApi + "api/Usuario/AgregarFotoPerfil?id=" + id;
                var content = new FormUrlEncodedContent(collection);
                var resp = _httpClient.PatchAsync(url, content).Result;

                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                return 0;
            }
            catch (Exception)
            {
                //Registrar error generado, siempre usar try catch para todo
                return 0;
            }
        }

        public UsuarioEnt ModificarInfoPerfil(UsuarioEnt entidad)
        {
            try
            {
                string url = _urlApi + "api/Usuario/ModificarPerfil";
                JsonContent obj = JsonContent.Create(entidad);
                var resp = _httpClient.PutAsync(url, obj).Result;

                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<UsuarioEnt>().Result;
                return new UsuarioEnt();
            }
            catch (Exception)
            {
                return new UsuarioEnt();
            }
        }

        public UsuarioEntRespuesta? ConsultarUsuarios()
        {


            string url = "api/Usuario/ConsultarUsuarios";

            string ApiUrl = (_urlApi + url);

            var response = _httpClient.GetAsync(ApiUrl).Result;
            return response.Content.ReadFromJsonAsync<UsuarioEntRespuesta>().Result;



        }

        public async Task<int> ActualizarEstadoUsuario(int IdUsuario, bool Estado)
        {
            string url = _urlApi + "api/Usuario/ActualizarEstadoUsuario";


            var parametros = new { IdUsuario, Estado };

            JsonContent obj = JsonContent.Create(parametros);

            var resp = await _httpClient.PutAsync(url, obj);

            if (resp.IsSuccessStatusCode)
            {

                return await resp.Content.ReadFromJsonAsync<int>();
            }
            else
            {

                return 0;
            }
        }

        public UsuarioEntRespuesta? ConsultarUsuario(long q)
        {
            string url = "api/Usuario/ConsultarUsuario?q=" + q;
            var response = _httpClient.GetAsync(_urlApi + url).Result;
            return response.Content.ReadFromJsonAsync<UsuarioEntRespuesta>().Result;
        }


        public UsuarioEntRespuesta? ModificarUsuario(UsuarioEnt entidad)
        {

            string url = "api/Usuario/ModificarUsuario";
            JsonContent jsonObject = JsonContent.Create(entidad);
            var response = _httpClient.PutAsync(_urlApi + url, jsonObject).Result;
            return response.Content.ReadFromJsonAsync<UsuarioEntRespuesta>().Result;

        }






    }
}



