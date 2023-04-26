using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using WebApp1.Data;
using WebApp1.Data.Model;
using WebApp1.Controllers;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WebApp1.Data.ViewModel;
using System.Security.Policy;

namespace WebAPPTest
{
    public class Tests
    {
        private static DbContextOptions<WebApp1Context> dbContextOptions = new DbContextOptionsBuilder<WebApp1Context>()
             .UseInMemoryDatabase(databaseName: "WebAppTest")
             .Options;

        WebApp1Context context;
        BatchService batchService;
        BatchesController batchController;
        [OneTimeSetUp]
        public void Setup()
        {
            context = new WebApp1Context(dbContextOptions);
            context.Database.EnsureCreated();

            SeedData();
            batchService = new BatchService(context);
            batchController = new BatchesController(batchService, new NullLogger<BatchesController>());
        }
        [Test]
        public void HTTPGET_GetBatchByGuid_ReturnsNotFound_Test()
        {
            var _GuidId = "7f768535-6e44-4d9b-a507-d400f6f7daef";

            IActionResult actionResult = batchController.GetBatch(_GuidId);

            Assert.That(actionResult, Is.TypeOf<NotFoundObjectResult>());

        }
        [Test]
        public void HTTPGET_GetBatchByGuid_ReturnsOk_Test()
        {
            var _GuidId = "7f768535-6e44-4d9b-a507-d400f6f7daed";

            IActionResult actionResult = batchController.GetBatch(_GuidId);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var BatchData = (actionResult as OkObjectResult).Value as BatchVM;

            Assert.That(BatchData.BusinessUnitName, Is.EqualTo("BusinessUnit-1").IgnoreCase);
        }

        [Test]
        public void HTTPPOST_AddBatch_ReturnsCreated_Test()
        {
            var batchAttributes = new List<AttributeVM>
            {
                 new AttributeVM(){
                  Key="attribute-Key-6",
                  Value="attribute-Value-6",
                 },
                 new AttributeVM(){
                  Key="attribute-Key-7",
                  Value="attribute-Value-7",
                 },
                 new AttributeVM(){
                  Key="attribute-Key-7",
                  Value="attribute-Value-7",
                 }
            };

            var users = new List<string>
            {
                "User7","User10","User8","User9"
            };

            var groups = new List<string>
            {
                "group7","group10","group8","group9"
            };
            var batchvm = new BatchVM()
            {
                BusinessUnitName = "Businessunit_add_test",
                AccessList = new AccessVM()
                {
                    Users = users,
                    Groups = groups
                },
                Attributes = batchAttributes,
                ExpiryDate = DateTime.Now,
            };

            IActionResult actionResult = batchController.PostBatch(batchvm);

            Assert.That(actionResult, Is.TypeOf<CreatedResult>());
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }
        private void SeedData()
        {
            var batchs = new List<Batch>
            {
                    new Batch() {
                        BatchGuid = "7f768535-6e44-4d9b-a507-d400f6f7daed",
                        BusinessUnitName="BusinessUnit-1",
                        ExpiryDate = DateTime.UtcNow
                    },
                    new Batch() {
                        BatchGuid = "7f768535-6e44-4d9b-a507-d400f6f7daec",
                        BusinessUnitName="BusinessUnit-2",
                        ExpiryDate = DateTime.UtcNow
                    },
                    new Batch() {
                        BatchGuid = "7f768535-6e44-4d9b-a507-d400f6f7daee",
                        BusinessUnitName="BusinessUnit-3",
                        ExpiryDate = DateTime.UtcNow
                    },
            };
            context.Batch.AddRange(batchs);

            var batchAttributes = new List<BatchAttribute>
            {
             new BatchAttribute(){
              Id=1,
              BatchGuid="7f768535-6e44-4d9b-a507-d400f6f7daed",
              Key="attribute-Key-1",
              Value="attribute-Value-1",
             },
             new BatchAttribute(){
              Id=2,
              BatchGuid="7f768535-6e44-4d9b-a507-d400f6f7daec",
              Key="attribute-Key-2",
              Value="attribute-Value-2",
             },
             new BatchAttribute(){
              Id=3,
              BatchGuid="7f768535-6e44-4d9b-a507-d400f6f7daed",
              Key="attribute-Key-3",
              Value="attribute-Value-3",
             },
             new BatchAttribute(){
              Id=4,
              BatchGuid="7f768535-6e44-4d9b-a507-d400f6f7daec",
              Key="attribute-Key-4",
              Value="attribute-Value-4",
             },
             new BatchAttribute(){
              Id=5,
              BatchGuid="7f768535-6e44-4d9b-a507-d400f6f7daec",
              Key="attribute-Key-5",
              Value="attribute-Value-5",
             },
            };
            context.BatchAttributes.AddRange(batchAttributes);

            var groups = new List<Groups>
            {
                new Groups(){
                 Id=1,
                 Name="Group1",
                 BatchGuid="7f768535-6e44-4d9b-a507-d400f6f7daed"
                },
                new Groups(){
                 Id=2,
                 Name="Group2",
                 BatchGuid="7f768535-6e44-4d9b-a507-d400f6f7daed"
                },
                new Groups(){
                 Id=3,
                 Name="Group3",
                 BatchGuid="7f768535-6e44-4d9b-a507-d400f6f7daec"
                },
                new Groups(){
                 Id=4,
                 Name="Group4",
                 BatchGuid="7f768535-6e44-4d9b-a507-d400f6f7daee"
                },
                new Groups(){
                 Id=5,
                 Name="Group5",
                 BatchGuid="7f768535-6e44-4d9b-a507-d400f6f7daec"
                }
            };
            context.Groups.AddRange(groups);

            var users = new List<Users>
            {
               new Users(){
                   Id=1,
                   Name="user1",
                   BatchGuid="7f768535-6e44-4d9b-a507-d400f6f7daed"
               },
               new Users(){
                   Id=2,
                   Name="user2",
                   BatchGuid="7f768535-6e44-4d9b-a507-d400f6f7daec"
               },
               new Users(){
                   Id=3,
                   Name="user3",
                   BatchGuid="7f768535-6e44-4d9b-a507-d400f6f7daec"
               },
               new Users(){
                   Id=4,
                   Name="user4",
                   BatchGuid="7f768535-6e44-4d9b-a507-d400f6f7daec"
               },
               new Users(){
                   Id=5,
                   Name="user5",
                   BatchGuid="7f768535-6e44-4d9b-a507-d400f6f7daee"
               },
               new Users(){
                   Id=6,
                   Name="user6",
                   BatchGuid="7f768535-6e44-4d9b-a507-d400f6f7daed"
               }
            };
            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}