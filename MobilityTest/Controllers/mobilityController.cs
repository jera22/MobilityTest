using Microsoft.AspNetCore.Mvc;
using System;
using MobilityTest.Services;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Newtonsoft.Json;
using MobilityTest.Models;

namespace MobilityTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class mobilityController : ControllerBase
    {

        private readonly IDataService _dataService;
        private readonly ILogger<mobilityController> _logger;


        public mobilityController(IDataService dataService, ILogger<mobilityController> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }



        [HttpGet]
        public IActionResult GetStructure()
        {
            try
            {
                return Ok(_dataService.GetStructure());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string errorMessage = "Failed to get folder structure";
                return BadRequest(errorMessage);
            }
        }


        [HttpGet]
        public IActionResult DeleteFile(int fileId)
        {
            try
            {
                return Ok(_dataService.DeleteFile(fileId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string errorMessage = "Failed to delete file with id or file doesnt even exist";
                return BadRequest(errorMessage);
            }
        }


        [HttpGet]
        public IActionResult DelteFolder(int folderId)
        {
            try
            {
                return Ok(_dataService.DeleteFolder(folderId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string errorMessage = "Failed to delete folder with id or folder doesnt even exist";
                return BadRequest(errorMessage);
            }
        }


        [HttpGet]
        public IActionResult SearchFile(string name)
        {
            try
            {
                return Ok(_dataService.SearchFile(name));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string errorMessage = "Failed to search for files with name:" + name;
                return BadRequest(errorMessage);
            }
        }

        [HttpGet]
        public IActionResult SearchFileInParent(int folderId, string name)
        {
            try
            {
                return Ok(_dataService.SearchFileInParent(folderId, name));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string errorMessage = "Failed to search for files with name:" + name;
                return BadRequest(errorMessage);
            }
        }

        [HttpPost]
        public IActionResult CreateFolder(string Folder)
        {
            Folder _folder = JsonConvert.DeserializeObject<Folder>(Folder);

            try
            {
                if (_dataService.CreateFolder(_folder))
                {
                    return Ok();
                }
                return BadRequest("Failed to create a folder");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string errorMessage = "Failed to create a folder";
                return BadRequest(errorMessage);
            }
        }



        [HttpPost]
        public IActionResult CreateFile(string file)
        {
            File _file = JsonConvert.DeserializeObject<File>(file);

            try
            {
                if (_dataService.CreateFile(_file))
                {
                    return Ok();
                }
                return BadRequest("Failed to create a file");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string errorMessage = "Failed to create a file";
                return BadRequest(errorMessage);
            }
        }

    }
}