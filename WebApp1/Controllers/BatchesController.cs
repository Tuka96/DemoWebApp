using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using WebApp1.Data;
using WebApp1.Data.Model;
using WebApp1.Data.ViewModel;
using static System.Net.WebRequestMethods;

namespace WebApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchesController : ControllerBase
    {
        private readonly IBatchService _batchService;
        private readonly ILogger<BatchesController> _logger;
        public BatchesController(IBatchService batchService, ILogger<BatchesController> logger)
        {
            _batchService = batchService;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="batch"></param>
        /// <returns>created BatchResponse json object</returns>
        /// <exception cref="ApplicationException"></exception>
        [HttpPost("add-batch")]
        public IActionResult PostBatch(BatchVM batch)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return BadRequest(ModelState);
                }
                var guid = _batchService.AddBatch(batch);

                var _batch = new BatchResponse()
                {
                    BatchGuid = guid
                };

                _logger.LogInformation("User added", _batch);

                return Created("~/api/Batches/get-batch-by-batchguid/{guid}", _batch);
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>BatchVM objects</returns>
        /// <exception cref="ApplicationException"></exception>
        [HttpGet("get-batch-by-batchguid/{guid}")]
        public IActionResult GetBatch(string guid)
        {

            if (guid == null)
            {
                _logger.LogInformation("Required parameter Batch Guid");
                throw new ApplicationException("Required parameter Batch Guid");
            }
            else if (!Guid.TryParse(guid, out var _guid)) {
                throw new ApplicationException("Guid is not valid, Please check the given guid");
            }
            else {
                var _batch = _batchService.GetBatchByGuid(guid);
                if (_batch != null)
                {
                    return Ok(_batch);
                }
                else
                {
                    _logger.LogInformation("Batch for given guid is not found");
                    return NotFound("Batch for given guid is not found");
                }
            }

        }

        [ExcludeFromCodeCoverage]
        [HttpPost]
        public IActionResult UploadFile(IFormFile files, string _batchGuid)
        {
            string FileName = files.FileName;

            var fileurl = string.Empty;
            BlobContainerClient container = new BlobContainerClient("DefaultEndpointsProtocol=https;AccountName=demostoragewebapp1;AccountKey=vgwzYJe6Dy6iGzju8qXfWMVY2lL5elLzf7hbQxjtr3nlahuS8OaF//OIb25KKfQkS4B7ZelDScVs+ASthq/ixw==;EndpointSuffix=core.windows.net", "democontainer");
            try
            {
                BlobClient blob = container.GetBlobClient(FileName);
                using (Stream stream = files.OpenReadStream())
                {
                    blob.Upload(stream);
                }
                fileurl = blob.Uri.AbsoluteUri;

                _batchService.AddFileBatch(fileurl,FileName,_batchGuid);

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [ExcludeFromCodeCoverage]
        [HttpPost("AddFile/{batchId}/{localFilePath}")]
        public IActionResult UploadStream(string batchId, string localFilePath)
        {
            string fileName = Path.GetFileName(localFilePath);
            fileName = fileName + batchId;
            BlobContainerClient containerClient= new BlobContainerClient("DefaultEndpointsProtocol=https;AccountName=demostoragewebapp1;AccountKey=vgwzYJe6Dy6iGzju8qXfWMVY2lL5elLzf7hbQxjtr3nlahuS8OaF//OIb25KKfQkS4B7ZelDScVs+ASthq/ixw==;EndpointSuffix=core.windows.net", "democontainer");
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            FileStream fileStream = new FileStream(localFilePath, FileMode.Open, FileAccess.Read); ;

            var a= blobClient.Upload(fileStream, true);
            fileStream.Close();
            return Ok();
        }
    }
}
