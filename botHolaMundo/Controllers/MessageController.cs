using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

/***
 * PRIMER APLICACION CREADA CON BOT BUILDER CON MICROSOFT BOT FRAMEWORK
 * ESTA APLICACION ES UN CLASICO HOLA MUNDO.
 ***/

namespace botHolaMundo.Controllers
{
    // DIRECTIVA NECESARIA PARA COMENZAR CON LA PROGRAMACION DE NUESTRO BOT
    // CREAMOS UN CONTROLLER NUEVO
    [BotAuthentication]
    public class MessageController : ApiController
    {
        // CREAMOS UN ROUTE TIPO POST QUE RETORNA UN ASYNC Y RECIBE UNA ACTIVITY POR PARAMETRO
        public async Task<HttpResponseMessage> Post([FromBody] Activity act)
        {
            // AQUI EVALUAMOS QUE TIPO DE ACTIVITY NOS INGRESO
            if (act.Type == ActivityTypes.Message)
            {
                // CONSTRUIMOS UN REPLY CON EL MENSAJE CORRESPONDIENTE
                var reply = act.CreateReply($"Esto es lo que me pediste {act.Text}");
                // ESTABLECEMOS HACIENDO DONDE DEBEMOS REALIZAR LA RESPUESTA. EN ESTE CASO HACIA LA URL DEL 
                // PROPIO SERVICIO
                var connector = new ConnectorClient(new Uri(act.ServiceUrl));
                // Y FINALMENTE LANZAMOS UN AWAIT PARA HACER LA REPLICA DEL MENSAJE OSEA DEVOLVER UNA RESPUESTA
                // AL USUARIO.
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                // pequeña funcion que se encarga de manejar el resto de activities que podamos
                // pasarles
                handlerMessage(act);
            }
            // retornamos un http accepted
            return Request.CreateResponse(HttpStatusCode.Accepted);
        }

        private void handlerMessage(IActivity act)
        {
            if (act.Type == ActivityTypes.Ping)
            {
                // aqui realizacion de una accion
            }
        }
    }
}
