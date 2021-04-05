using ContactInfoHandler.Application.Dto.Base;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ContactInfoHandler.Application.Core.JsonConverter.Base.Exceptions;

namespace ContactInfoHandler.Application.Core.JsonConverter.Service
{
    internal class ConverterService : IConverterService
    {

        public void ValidateNoNullPath(string path) 
        {
            if (string.IsNullOrEmpty(path)) 
            {
                throw new NullPathToConvertException("Por favor rectifique la ruta");
            }
        }

        public async Task<string> ExportJson<T>(string path, T DtoToConvert) where T : IEnumerable<DtoBase>
        {
            ValidateNoNullPath(path);

            var SerializedDto = JsonConvert.SerializeObject(DtoToConvert);
            var PathDefinied =  path + ".json";

            using (FileStream _fileStream = File.Create(PathDefinied)) 
            {
                byte[] info = new UTF8Encoding(true).GetBytes(SerializedDto);
                _fileStream.Write(info, 0, info.Length);
            }

            return await Task.FromResult(SerializedDto).ConfigureAwait(false);
        }


        public async Task<T> ImportJson<T>(string path) where T : IEnumerable<DtoBase>
        {
            ValidateNoNullPath(path);

            var DeserializedObject = "";
            var JsonDoc = path + ".json";
            using (StreamReader _streamReader = File.OpenText(JsonDoc)) 
            {
                var StringToOverwrite ="";
                while ((StringToOverwrite = _streamReader.ReadLine()) != null) 
                {
                    DeserializedObject = StringToOverwrite;
                }
            }
            return await Task.FromResult(JsonConvert.DeserializeObject<T>(DeserializedObject)).ConfigureAwait(false);
        }
    }
}
