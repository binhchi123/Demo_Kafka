global using System.ComponentModel.DataAnnotations.Schema;
global using System.ComponentModel.DataAnnotations;
global using System.Text.Json.Serialization;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Confluent.Kafka;

global using StudentAPI.Infrastructure.Models;
global using StudentAPI.Infrastructure.Database;
global using StudentAPI.Infrastructure.InMemorys;
global using StudentAPI.Infrastructure.IService;
global using StudentAPI.Infrastructure.Seedings;
global using StudentAPI.Producer;
global using StudentAPI.Requests;
global using StudentAPI.Extensions;
global using StudentAPI.Service;