using Microsoft.Extensions.Logging;
using MobilityTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MobilityTest.Services
{
    public class DataService : IDataService
    {

        private readonly ILogger<DataService> _logger;
        private readonly mobilityTestContext _dbContext;
        public DataService(ILogger<DataService> logger, mobilityTestContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }


        public bool CreateFile(File file)
        {
            try
            {
                if (_dbContext.Folder.Any(x => x.Id == file.ParentId))
                {
                    _dbContext.File.Add(file);
                    _dbContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                _logger.LogError("Failed creating a file with id:" + file.Id + " Exception: " + e);

                throw;
            }


        }

        public bool CreateFolder(Folder folder)
        {
            try
            {
                if (_dbContext.Folder.Any(x => x.Id == folder.ParentId))
                {
                    _dbContext.Folder.Add(folder);
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError("Failed creating a folder with id:" + folder.Id + " Exception: " + e);
                throw;
            }

        }

        public bool DeleteFile(int fileId)
        {
            try
            {
                var fileToRemove = _dbContext.File.FirstOrDefault(x => x.Id == fileId);
                if (fileToRemove != null)
                {
                    _dbContext.File.Remove(fileToRemove);
                    return true;
                }
                return false;

            }
            catch (Exception e)
            {
                _logger.LogError("Failed removing a file with id:" + fileId + " Exception: " + e);
                throw;
            }
        }


        public bool DeleteFolder(int folderId)
        {
            try
            {
                var folderToRemove = _dbContext.Folder.FirstOrDefault(x => x.Id == folderId);
                if (folderToRemove != null)
                {
                    _dbContext.Folder.Remove(folderToRemove);
                    return true;
                }
                return false;

            }
            catch (Exception e)
            {
                _logger.LogError("Failed removing a folder with id:" + folderId + " Exception: " + e);
                throw;
            }
        }

        public Node GetStructure()
        {
            Folder rootFolder = _dbContext.Folder.FirstOrDefault(x => x.Name.Equals("root"));

            GetNodeChildren(rootFolder);


            return rootFolder;
        }

        private Node GetNodeRecursive(Node rootFolder)
        {
            var folders = _dbContext.Folder.Where(x => x.ParentId == rootFolder.Id);
            var files = _dbContext.File.Where(x => x.ParentId == rootFolder.Id);
            if (folders != null && files != null)
            {

                rootFolder.listOfNodes.AddRange(folders);
                rootFolder.listOfNodes.AddRange(files);
            }
            else if (folders != null && files == null)
            {
                rootFolder.listOfNodes.AddRange(folders);
            }
            else if (folders == null && files != null)
            {
                rootFolder.listOfNodes.AddRange(files);
            }
            else
            {
                return null;
            }
            return rootFolder;

        }



        IEnumerable<Node> GetNodeChildren(Node node)
        {
            var children = _dbContext.Folder.Where(x => x.ParentId == node.Id);
            var files = _dbContext.File.Where(x => x.ParentId == node.Id);
            if (files.Any())
            {
                node.listOfNodes.AddRange(files);
            }


            if (children.Any())
            {
                foreach (Node child in children)
                {
                    yield return child;

                    var grandchildren = GetNodeChildren(child);
                    foreach (Node grandchild in grandchildren)
                    {
                        yield return grandchild;
                    }
                }
            }
        }

        public List<File> SearchFile(string name)
        {
            var listOfFiles = _dbContext.File.Where(x => x.Name.StartsWith(name)).ToList();
            if (listOfFiles != null)
            {
                return listOfFiles;
            }
            return null;
        }

        public List<File> SearchFileInParent(int folderId, string fileName)
        {
            var listOfFiles = _dbContext.File.Where(x => x.Name.StartsWith(fileName) && x.ParentId==folderId).ToList();
            if (listOfFiles != null)
            {
                return listOfFiles;
            }
            return null;
        }
    }
}
