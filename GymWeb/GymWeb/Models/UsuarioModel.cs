using GymWeb.Entities;


namespace GymWeb.Models
{
   
        public class UsuarioModel : IUsuarioModel
        {

            private readonly HttpClient _httpClient;
            private readonly IConfiguration _configuration;
            private string _urlApi;



            public UsuarioModel(HttpClient httpClient, IConfiguration configuration)
            {

                _httpClient = httpClient;
                _configuration = configuration;
                _urlApi = _configuration.GetSection("Llaves:urlApi").Value;
            }

        public UsuarioEnt? IniciarSesion(UsuarioEnt entidad)
        {
            throw new NotImplementedException();
        }

        public int RecuperarCuenta(UsuarioEnt entidad)
        {
            throw new NotImplementedException();
        }

        public int RegistrarUsuario(UsuarioEnt entidad)
            {
                string url = _urlApi + "api/Usuario/RegistrarUsuario";
                JsonContent obj = JsonContent.Create(entidad);
                var resp = _httpClient.PostAsync(url, obj).Result;

                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<int>().Result;
                else
                    return 0;

            }





        }

    }

