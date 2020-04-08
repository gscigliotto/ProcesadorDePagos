using ProcesarPDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

using Google.Apis.Discovery.v1;
using Google.Apis.Discovery.v1.Data;
using Google.Apis.Services;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using System.IO;
using System.Threading;
using Google.Apis.Util.Store;
using System.Reflection;

namespace Repositorio
{
    public class RepoStore : IExportDataHandler
    {

        static string[] Scopes = { SheetsService.Scope.Spreadsheets};
        static string ApplicationName = "AgenteInicial";

        public void Prueba() {

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"conf\credentials.json");

            UserCredential credential;

            using (var stream =
                new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "guillermo@gmail.com",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            String spreadsheetId = "1hjvWNsJHItwErDxukH2ffhNJ4hVuIES5_5hXWjK157Q";
            String range = "Comprobantes!A:A";
            //List<List<String>> data = new List<List<string>>();
            //List<string> datos = new List<string>();
            //datos.Add("Agregado");
            //data.Add(datos);
            //ValueRange rango = new ValueRange() {  Range="A:A", Values= new };
            //service.Spreadsheets.Values.
            IList<object> headerList = new List<object>() {
            "ID","DTStamp","DTShiftStart","ModelNbr","SerialNbr","PassFail","LineNbr","ShiftNbr","Computer","Word40","Word41","Word42"
            ,"Word43","Word44","Word45","Word46","Word47","Word48","Word49","Word50","Word51","Word52","Word53","Word54","Word55","Word56"
            ,"Word57","Word58","Word59","Word60","Word61","Word62","Word63","Word64","Word65","Word66","Word67","Word68","Word69","Word70"
            ,"Word71","Word72","Word73","Word74","Word75","Word76","Word77","Word78","Word79","Word80"};
            IList<IList<object>> elementos = new List<IList<object>>();
            elementos.Add(headerList);
            elementos.Add(headerList);
            ValueRange vr = new ValueRange() { Values = elementos, Range = range };
            SpreadsheetsResource.ValuesResource.AppendRequest requestdos= service.Spreadsheets.Values.Append(vr, spreadsheetId, "Comprobantes!A:A");
            requestdos.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
                AppendValuesResponse responsedos = requestdos.Execute();




            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Prints the names and majors of students in a sample spreadsheet:
            // https://docs.google.com/spreadsheets/d/1hjvWNsJHItwErDxukH2ffhNJ4hVuIES5_5hXWjK157Q/edit
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                Console.WriteLine("Name, Major");
                foreach (var row in values)
                {
                    // Print columns A and E, which correspond to indices 0 and 4.
                    Console.WriteLine("{0}, {1}", row[0], row[4]);
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }
            Console.Read();
        }

        public void SendData(List<Comprobante> comprobantes)
        {
            UserCredential credential;
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"conf\credentials.json");

            using (var stream =
                new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "guillermo@gmail.com",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }



            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            String spreadsheetId = "1hjvWNsJHItwErDxukH2ffhNJ4hVuIES5_5hXWjK157Q";
            String range = "Comprobantes!A:A";
            IList<IList<object>> elementos = new List<IList<object>>();
            
            foreach (Comprobante comp in comprobantes)
            {
                IList<object> headerList = new List<object>() { comp.Movimiento, comp.Numero, comp.Beneficiario, comp.Concepto, comp.Referencia, comp.Fecha, comp.Importe };
                //headerList.Add(comp.Movimiento);
                //headerList.Add(comp.Numero);
                //headerList.Add(comp.Beneficiario);
                //headerList.Add(comp.Concepto);
                //headerList.Add(comp.Referencia);
                //headerList.Add(comp.Fecha);
                //headerList.Add(comp.Importe);


                elementos.Add(headerList);
            }
            
            ValueRange vr = new ValueRange() { Values = elementos, Range = range };
            SpreadsheetsResource.ValuesResource.AppendRequest requestdos = service.Spreadsheets.Values.Append(vr, spreadsheetId, "Comprobantes!A:A");
            requestdos.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            AppendValuesResponse responsedos = requestdos.Execute();

        }
    }
}
