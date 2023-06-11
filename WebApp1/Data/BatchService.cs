using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using WebApp1.Data.Model;
using WebApp1.Data.ViewModel;

namespace WebApp1.Data
{
    public class BatchService : IBatchService
    {
        private WebApp1Context _context;
        public BatchService(WebApp1Context Context)
        {
            _context = Context;
        }

        public string AddBatch(BatchVM batch)
        {
            if (batch.BusinessUnitName == null)
            {
                throw new ApplicationException("BusinessUnitName required");
            }
            else if (batch.Attributes.Any(u=>u.Key==null || u.Value==null)) {
                throw new ApplicationException("Attribute must contain key and value");
            }

            var _batch = new Batch()
            {
                BusinessUnitName = batch.BusinessUnitName,
                ExpiryDate = batch.ExpiryDate,
                BatchGuid = Guid.NewGuid().ToString()
            };

            _context.Batch.Add(_batch);

            var _accessVM = batch.AccessList;
            foreach (var userName in _accessVM.Users)
            {
                var _user = new Users()
                {
                    BatchGuid = _batch.BatchGuid,
                    Name = userName
                };
                _context.Users.Add(_user);
            }

            foreach (var groupName in _accessVM.Groups)
            {
                var _groups = new Groups()
                {
                    BatchGuid = _batch.BatchGuid,
                    Name = groupName
                };
                _context.Groups.Add(_groups);
            }
            foreach (var atr in batch.Attributes)
            {
                var _batchAttribute = new BatchAttribute()
                {
                    BatchGuid = _batch.BatchGuid,
                    Key = atr.Key,
                    Value = atr.Value
                };
                _context.BatchAttributes.Add(_batchAttribute);
            }

            _context.SaveChanges();

            return _batch.BatchGuid;
        }

        public BatchVM GetBatchByGuid(string guid)
        {
            var _batch = _context.Batch
                    .Where(b => b.BatchGuid == guid)
                    .FirstOrDefault();

            if (_batch != null)
            {
                //var _batchAttribute1 = (from b in _context._batchAttribute
                //                       where (b.BatchGuid == guid)
                //                       select new AttributeVM() {
                //                           key = b.key,
                //                           value = b.value
                //                       }).ToList();

                var _batchAttributes = _context.BatchAttributes
                                      .Where(b => b.BatchGuid == guid)
                                      .Select(u => new AttributeVM() { Key = u.Key, Value = u.Value })
                                      .ToList();

                var _user = _context.Users.Where(b => b.BatchGuid == guid).Select(u => u.Name).ToList();
                var _group = _context.Groups.Where(b => b.BatchGuid == guid).Select(u => u.Name).ToList();

                var batchvm = new BatchVM()
                {
                    BusinessUnitName = _batch.BusinessUnitName,
                    AccessList = new AccessVM()
                    {
                        Users = _user,
                        Groups = _group
                    },
                    Attributes = _batchAttributes,
                    ExpiryDate = _batch.ExpiryDate
                };

                return batchvm;

            }
            else
            {
                return null;
            }
        }

        [ExcludeFromCodeCoverage]
        public void AddFileBatch(string path,string _fileName,string _batchId) {

            var _file = new BatchFiles()
            {
                filepath = path,
                FileName= _fileName,
                BatchGuid = _batchId
            };

            _context.BatchFiles.Add(_file);

            _context.SaveChanges();
            
        }
    }
}

//this is test changes 
