using Microsoft.AspNetCore.Http;
using RestWithASPNETUdemy.Data.VO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Business
{
    public interface IFileBusiness
    {
        byte[] GetFile(string filename);
        Task<FileDetailVO> SaveFileToDisk(IFormFile file);
        Task<IList<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> files);
    }
}
