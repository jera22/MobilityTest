using MobilityTest.Models;
using System.Collections.Generic;

namespace MobilityTest.Services
{
    public interface IDataService
    {
        bool CreateFolder(Folder folder);
        bool CreateFile(File file);
        Node GetStructure();
        bool DeleteFile(int fileId);
        bool DeleteFolder(int fodlerId);
        List<File> SearchFile(string name);
        List<File> SearchFileInParent(int folderId, string fileName);
    }
}
