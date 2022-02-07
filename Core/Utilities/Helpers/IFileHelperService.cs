using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Helpers
{
    public interface IFileHelperService
    {
        string Upload(IFormFile file, string root);
        string Update(IFormFile file, string filePath, string root);
        void Delete(string filePath);


    }
}
