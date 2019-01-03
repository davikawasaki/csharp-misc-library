using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using CSharpMiscLibrary.Services;
using CSharpMiscLibrary.Classes.APIs;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CSharpMiscLibrary.APIs
{
    /// <summary>
    /// Kapow REST API HTTP model.
    /// </summary>
    public class KapowRESTAPI : BaseHTTPAPI
    {
        /// <summary>
        /// Empty constructor.
        /// </summary>
        public KapowRESTAPI() { }

        /// <summary>
        /// Start async request to start Kapow Robot. Checks for 200 request code, as well as eventual errors from the robot-error JSON attribute.
        /// </summary>
        /// <param name="robotName">Kapow Robot process name.</param>
        /// <param name="uri">Kapow robot public URI to be called.</param>
        /// <param name="stw">(Optional) Stream writer to store buffer message from execution.</param>
        /// <param name="content">(Optional) HTTP content to be sent.</param>
        /// /// <param name="method">(Optional) HTTP method to be used (check ConstantsService for options).</param>
        /// <todo>Allow XML as deserializing method option</todo>
        /// <todo>Refactor method to return response</todo>
        /// <returns>Async task with HTTP response message.</returns>
        public async Task<HttpResponseMessage> RunAsyncKapowRobot(string robotName, string uri, StreamWriter stw = null, HttpContent content = null, string method = ConstantsService.HTTP_CLIENT_REQUEST_METHOD_GET_NAME)
        {
            string message = DateTime.Now + " [INFO] Starting " + robotName + " robot at Management Console...";
            if (stw != null) stw.WriteLine(message + "\n\r");
            Console.WriteLine(message);

            HttpResponseMessage response;
            switch (method)
            {
                case ConstantsService.HTTP_CLIENT_REQUEST_METHOD_POST_NAME:
                    response = await PostMethod(uri, content, false);
                    break;
                case ConstantsService.HTTP_CLIENT_REQUEST_METHOD_PUT_NAME:
                    response = await PutMethod(uri, content, false);
                    break;
                case ConstantsService.HTTP_CLIENT_REQUEST_METHOD_DELETE_NAME:
                    response = await DeleteMethod(uri, false);
                    break;
                default:
                    response = await GetMethod(uri, false);
                    break;
            }

            if (response.IsSuccessStatusCode)
            {

                string _robotResponse = await response.Content.ReadAsStringAsync();
                var _robotResponseJSON = JsonConvert.DeserializeObject<KapowResponse>(_robotResponse);
                if (_robotResponseJSON.robotError != null)
                {
                    throw new ApplicationException(robotName + " robot finished batch at Management Console with errors: " + _robotResponseJSON.robotError.errorMessage);
                }
                else
                {
                    message = DateTime.Now + " [SUCCESS] " + robotName + " robot finished batch at Management Console without errors! Check the result in the Console to check if the robot was fully executed or was stopped.";
                    stw.WriteLine(message + "\n\r");
                    Console.WriteLine(message);
                }
            }
            else
            {
                message = DateTime.Now + " [ERROR] A connection error with Management Console happened: " + response.StatusCode + ": " + response.ReasonPhrase + ".";
                stw.WriteLine(message + "\n\r");
                Console.WriteLine(message);
            }

            return response;
        }
    }
}
