global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using System.Text.Json.Serialization;
global using System.Text;
global using System.Text.Json;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Confluent.Kafka;

global using CourseAPI.Infrastructure.Models;
global using CourseAPI.Infrastructure.Database;
global using CourseAPI.Infrastructure.InMemorys;
global using CourseAPI.Infrastructure.IService;
global using CourseAPI.Producer;
global using CourseAPI.Requests;
global using CourseAPI.Service;
