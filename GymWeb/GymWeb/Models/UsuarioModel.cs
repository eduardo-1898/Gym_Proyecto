using GymWeb.Entities;


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
    }
}

