using GymWeb.Entities;
using System.Collections;
using System.Text;

namespace GymWeb.Models
{

    public class UsuarioModel : IUsuarioModel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string _urlApi;
        private readonly IHttpContextAccessor _HttpContextAccessor;

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


        //Inicio metodo de recuperacion

        public int RecuperarContrasenna(UsuarioEnt entidad)
        {
            string url = _urlApi + "api/Usuario/RecuperarCuenta";
            JsonContent obj = JsonContent.Create(entidad);
            var resp = _httpClient.PostAsync(url, obj).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;
            else
                return 0;
        }

        //Fin metodo de recuperacion

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

        public int CambiarClaveCuenta(UsuarioEnt entidad)
        {
            string url = _urlApi + "api/Usuario/CambiarClaveCuenta";
            JsonContent obj = JsonContent.Create(entidad);
            var resp = _httpClient.PutAsync(url, obj).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<int>().Result;
            else
                return 0;

        }
        
        public int AgregarFotoPerfil(byte[] img, int id)
        {
            try
            {

                var collection = new List<KeyValuePair<string, string>>();
                collection.Add(new("foto", Convert.ToBase64String(img)));


                string url = _urlApi + "api/Usuario/AgregarFotoPerfil?id="+id;
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

        public UsuarioEnt ModificarInfoPerfil(UsuarioEnt entidad) {
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
    }
}

